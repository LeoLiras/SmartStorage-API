﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartStorage_API.Data.VO;
using SmartStorage_API.DTO;
using SmartStorage_API.Service;

namespace SmartStorage_API.Controllers
{
    [ApiVersion($"{Utils.apiVersion}")]
    [Route("api/storage/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Propriedades

        private IProductService _productService;

        #endregion

        #region Construtores

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Métodos

        [HttpGet]
        public IActionResult FindAllProducts()
        {
            return Ok(_productService.FindAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult FindProductById(int id)
        {
            try
            {
                return Ok(_productService.FindProductById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateNewProduct([FromBody] ProductVO newProduct)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newProduct.Name))
                    throw new Exception("O Nome do Produto é obrigatório.");

                return Ok(_productService.CreateNewProduct(newProduct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductVO product)
        {
            try
            {
                if (productId.Equals(0))
                    throw new Exception("O campo ID do Produto é obrigatório.");

                return Ok(_productService.UpdateProduct(productId, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {
                if (productId.Equals(0))
                    throw new Exception("O campo ID do Produto é obrigatório.");

                return Ok(_productService.DeleteProduct(productId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
