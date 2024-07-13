﻿using Inventorium.API.Models;

namespace Inventorium.API.Repositories.Contracts
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategoryModel>> GetProductCategories();
        Task<ProductCategoryModel> GetProductCategoryById(int id);
        ProductCategoryModel CreateProductCategory(ProductCategoryModel newProductCategory);
        void DeleteProductCategoryById(int categoryId);
        void UpdateProductCategoryById(int categoryId, ProductCategoryModel newProductCategory);
    }
}