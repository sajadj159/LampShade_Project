using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFIleUploader _uploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFIleUploader uploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _uploader = uploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationResult = new OperationResult();
            if (_productRepository.Exist(x => x.Name == command.Name))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var slugBy = _productCategoryRepository.GetSlugBy(command.CategoryId);
            var picturePath = $"{slugBy}/{slugify}";
            var fileName = _uploader.Upload(command.PictureUrl, picturePath);

            var product = new Domain.ProductAgg.Product(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureTitle, command.PictureAlt, slugify,
                command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.GetProductWithCategories(command.Id);
            if (product == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var picturePath = $"{product.Category.Slug}/{slugify}";
            var fileName = _uploader.Upload(command.PictureUrl, picturePath);

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, fileName, command.PictureTitle, command.PictureAlt,
                slugify, command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Save();
            return operationResult.Succeeded();

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