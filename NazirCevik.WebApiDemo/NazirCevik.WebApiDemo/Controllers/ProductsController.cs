using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NazirCevik.WebApiDemo.DataAcces;
using NazirCevik.WebApiDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NazirCevik.WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [HttpGet("")]
        [Authorize(Roles = "admin")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }
        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);

                if (product == null)
                {
                    return NotFound($"There is no product with id={productId} ");
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
        public IActionResult Post(Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch
            {

            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch
            {

            }
            return BadRequest();
        }
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product { ProductId=productId});
                return Ok();
            }
            catch
            {

            }
            return BadRequest();
        }
    }
}
