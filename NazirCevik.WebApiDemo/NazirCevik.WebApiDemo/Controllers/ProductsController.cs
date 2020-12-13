using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NazirCevik.WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController:Controller
    {
        [HttpGet("")]
        public string Get()
        {
            return "product1sadasdasdsa asdas d asd as das dasdasd";
        }
    }
}
