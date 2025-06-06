﻿using SmartStorage_API.DTO;
using SmartStorage_API.Model;
using SmartStorage_API.Model.Context;

namespace SmartStorage_API.Service.Implementations
{
    public class ShelfServiceImplementation : IShelfService
    {
        #region Propriedades

        private readonly SmartStorageContext _context;

        #endregion

        #region Construtores

        public ShelfServiceImplementation(SmartStorageContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos

        public List<ShelfDTO> FindAllProductsInShelves()
        {
            var queryEnter = from enter in _context.Enters
                             join product in _context.Products on enter.IdProduct equals product.Id
                             join shelf in _context.Shelves on enter.IdShelf equals shelf.Id
                             select new ShelfDTO
                             {
                                 productName = product.Name,
                                 productId = product.Id,
                                 shelfName = shelf.Name,
                                 qntd = enter.Qntd,
                                 allocateData = shelf.DataRegister,
                                 price = enter.Price,
                             };

            return queryEnter.OrderBy(q => q.productName).ToList();
        }

        public Enter AllocateProductToShelf(AllocateProductToShelfDTO newAllocation)
        {
            var product = _context.Products.Where(p => p.Id == newAllocation.ProductId).FirstOrDefault();

            try
            {
                if (product != null)
                {
                    if (product.Qntd >= newAllocation.ProductQuantity)
                    {
                        product.Qntd -= newAllocation.ProductQuantity;

                        var enter = _context.Enters.Where(e => e.IdProduct == newAllocation.ProductId && e.IdShelf == newAllocation.ShelfId).FirstOrDefault();

                        var shelf = _context.Shelves.Where(s => s.Id == newAllocation.ShelfId).FirstOrDefault();

                        if (shelf != null)
                        {
                            if (enter != null)
                            {
                                enter.Qntd += newAllocation.ProductQuantity;
                                enter.Price = newAllocation.ProductPrice;

                                _context.SaveChanges();

                                return enter;
                            }
                            else
                            {
                                var newEnterProduct = new Enter
                                {
                                    IdProduct = (int)product.Id,
                                    IdShelf = (int)shelf.Id,
                                    Qntd = newAllocation.ProductQuantity,
                                    DateEnter = DateTimeOffset.UtcNow.UtcDateTime,
                                    Price = newAllocation.ProductPrice
                                };

                                _context.Enters.Add(newEnterProduct);
                                _context.SaveChanges();

                                return newEnterProduct;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public List<Shelf> FindAllShelf()
        {
            return _context.Shelves.OrderBy(x => x.Name).ToList();
        }

        #endregion
    }
}
