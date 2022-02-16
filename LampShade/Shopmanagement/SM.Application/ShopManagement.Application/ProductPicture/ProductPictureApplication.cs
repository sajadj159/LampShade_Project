using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPicture
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            if (_productPictureRepository.Exist(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.ProductId))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var productPicture = new Domain.ProductPictureAgg.ProductPicture(command.ProductId, command.PictureUrl, command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.Save();
            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operationResult = new OperationResult();

            var productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_productPictureRepository.Exist(x => x.PictureTitle == command.PictureTitle && x.ProductId == command.Id && x.Id != command.Id))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            productPicture.Edit(command.ProductId, command.PictureUrl, command.PictureTitle, command.PictureAlt);
            _productPictureRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.Save();
            return operationResult.Succeeded();

        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);

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