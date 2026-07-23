using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFIleUploader _uploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductApplication(IProductRepository productRepository, IFIleUploader uploader, IProductCategoryRepository productCategoryRepository, IProductPictureRepository productPictureRepository)
        {
            _productRepository = productRepository;
            _uploader = uploader;
            _productCategoryRepository = productCategoryRepository;
            _productPictureRepository = productPictureRepository;
        }

        public async Task<OperationResult> CreateAsync(CreateProduct command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            if (await _productRepository.ExistAsync(x => x.Name == command.Name, cancellationToken))
               return  operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var slugBy = await _productCategoryRepository.GetSlugByAsync(command.CategoryId, cancellationToken);
            var picturePath = $"{slugBy}/{slugify}";
            var fileName = _uploader.Upload(command.PictureUrl, picturePath);

            var product = new Domain.ProductAgg.Product(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureTitle, command.PictureAlt, slugify,
                command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Add(product);
            await AddAdditionalPicturesAsync(product, command.AdditionalPictures, picturePath, cancellationToken);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> EditAsync(EditProduct command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();

            var product = await _productRepository.GetProductWithCategoriesAsync(command.Id, cancellationToken);
            if (product == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            if (await _productRepository.ExistAsync(x => x.Name == command.Name && x.Id != command.Id, cancellationToken))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var picturePath = $"{product.Category.Slug}/{slugify}";
            var fileName = _uploader.Upload(command.PictureUrl, picturePath);
            if (command.ClearMainPicture) product.ClearPicture();

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureTitle, command.PictureAlt,
                slugify, command.Keywords, command.MetaDescription, command.CategoryId);
            await AddAdditionalPicturesAsync(product, command.AdditionalPictures, picturePath, cancellationToken);
            return operationResult.Succeeded();

        }

        private async Task AddAdditionalPicturesAsync(Domain.ProductAgg.Product product, List<Microsoft.AspNetCore.Http.IFormFile> pictures, string path, CancellationToken cancellationToken)
        {
            if (pictures == null) return;

            foreach (var picture in pictures.Where(x => x is { Length: > 0 }))
            {
                var picturePath = _uploader.Upload(picture, path);
                var title = System.IO.Path.GetFileNameWithoutExtension(picture.FileName);
                _productPictureRepository.Add(new ShopManagement.Domain.ProductPictureAgg.ProductPicture(product.Id, picturePath, title, title));
            }
        }
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();    
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
    }
}
