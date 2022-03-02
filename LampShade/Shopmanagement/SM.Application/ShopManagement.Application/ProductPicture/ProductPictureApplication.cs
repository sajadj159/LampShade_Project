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

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            if (_productPictureRepository.Exist(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.ProductId))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var product = _productRepository.GetProductWithCategories(command.ProductId);
            var path = $"{product.Category.Slug}/{product.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, picturePath, command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.Save();
            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();

            var productPicture = _productPictureRepository.GetWithProductsAndCategories(command.Id);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_productPictureRepository.Exist(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.ProductId && x.Id != command.Id))
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.Save();
            return operationResult.Succeeded();

        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
               return operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.Save();
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