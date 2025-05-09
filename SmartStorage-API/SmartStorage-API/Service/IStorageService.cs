﻿using SmartStorage_API.Model;
using SmartStorage_API.DTO;

namespace SmartStorage_API.Service
{
    public interface IStorageService
    {
        Product CreateNewProduct(Product product);
        List<Product> FindAllProducts();
        Product FindProductById(int id);
        List<SaleDTO> FindAllSales();
        SaleDTO CreateNewSale(SaleDTO newSale);
        List<ShelfDTO> FindAllShelves();
        Enter AllocateProductToShelf(AllocateProductToShelfDTO newAllocation);
    }
}
