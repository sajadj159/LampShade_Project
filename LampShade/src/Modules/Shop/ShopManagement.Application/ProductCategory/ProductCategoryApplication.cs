using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IFIleUploader _uploader;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFIleUploader uploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _uploader = uploader;
        }

        public async Task<OperationResult> CreateAsync(CreateProductCategory command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            if (await _productCategoryRepository.ExistAsync(x => x.Name == command.Name, cancellationToken))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"{command.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl,path);

            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Name, command.Description, picturePath,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Add(productCategory);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> EditAsync(EditProductCategory command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var productCategory = await _productCategoryRepository.GetAsync(command.Id, cancellationToken);
            if (productCategory == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (await _productCategoryRepository.ExistAsync(x => x.Name == command.Name && x.Id != command.Id, cancellationToken))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var Path = $"{command.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl,Path);
            productCategory.Edit(command.Name, command.Description, picturePath, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slugify);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var productCategory = await _productCategoryRepository.GetAsync(id, cancellationToken);
            if (productCategory == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (await _productCategoryRepository.HasProductsAsync(id, cancellationToken))
                return operationResult.Failed("A category with products cannot be deleted.");

            await _productCategoryRepository.RemoveAsync(productCategory);
            return operationResult.Succeeded();
        }
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
    }
}
