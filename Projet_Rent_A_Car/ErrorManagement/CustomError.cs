﻿
using Models;
using System.Data.Entity.Infrastructure;

namespace ErrorManagement
{
    public class CustomError:ApplicationException
    {
        public CustomError(DbUpdateConcurrencyException o)
        {

              
        }


    }
}