﻿using NazirCevik.WebApiDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NazirCevik.WebApiDemo.DataAcces
{
    public class EfProductDal:EfEntityRepositoryBase<Product,NorthWindContext>,IProductDal
    {

    }
}
