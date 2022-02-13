using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategory
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            var operationResult = new OperationResult();
            if (_productCategoryRepository.Exist(x => x.Name == command.Name))
                return operationResult.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفتا دوباره امتحان نمایید");

            var slug = command.Slug.Slugify();
            var productCategory = new Domain.ProductCategoryAgg.ProductCategory(command.Name, command.Description, command.PictureUrl,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operationResult = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operationResult.Failed("چنین اطلاعاتی موجود نمی باشد. لطفا مجدد تلاش فرمایید");

            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفتا دوباره امتحان نمایید");

            var slugify = command.Slug.Slugify();
            productCategory.Edit(command.Name, command.Description, command.PictureUrl, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slugify);
            _productCategoryRepository.Save();
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
    }
}