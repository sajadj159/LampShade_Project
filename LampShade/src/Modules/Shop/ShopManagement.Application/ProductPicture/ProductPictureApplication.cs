using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFIleUploader _uploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFIleUploader uploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _uploader = uploader;
        }

        public async Task<OperationResult> CreateAsync(CreateProductPicture command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();

            if (await _productPictureRepository.ExistAsync(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.ProductId, cancellationToken))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var product = await _productRepository.GetProductWithCategoriesAsync(command.ProductId, cancellationToken);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, picturePath, command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Add(productPicture);
            return operationResult.Succeeded();

        }

        public async Task<OperationResult> EditAsync(EditProductPicture command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();

            var productPicture = await _productPictureRepository.GetWithProductsAndCategoriesAsync(command.Id, cancellationToken);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (await _productPictureRepository.ExistAsync(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.ProductId && x.Id != command.Id, cancellationToken))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureTitle, command.PictureAlt);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var productPicture = await _productPictureRepository.GetAsync(id, cancellationToken);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            return operationResult.Succeeded();

        }

        public async Task<OperationResult> RestoreAsync(long id, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var productPicture = await _productPictureRepository.GetAsync(id, cancellationToken);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            return operationResult.Succeeded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }
    }
}
