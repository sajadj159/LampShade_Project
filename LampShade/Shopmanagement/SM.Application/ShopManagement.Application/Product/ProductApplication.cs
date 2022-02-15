using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.VisualBasic;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application.Product
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationResult = new OperationResult();
            if (_productRepository.Exist(x => x.Name == command.Name))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            var product = new Domain.ProductAgg.Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.PictureUrl, command.PictureTitle, command.PictureAlt, slugify,
                command.Keywords, command.MetaDescription, command.CategoryId);
            _productRepository.Create(product);
            _productRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.Get(command.Id);
            if (product == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            if (_productRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slugify = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription,
                command.Description, command.PictureUrl, command.PictureTitle, command.PictureAlt,
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

        public OperationResult InStock(long id)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            product.InStock();
            _productRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult NotInStock(long id)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                operationResult.Failed(ApplicationMessages.RecordNotFound);
            product.NotInStock();
            _productRepository.Save();
          return  operationResult.Succeeded();
        }
    }
}