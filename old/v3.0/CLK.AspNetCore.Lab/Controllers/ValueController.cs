using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebAPI
{
    public class ValueController : Controller
    {
        // Methods
        public dynamic Get()
        {
            // Return
            return new { Id = 123, Name = "Clark" };
        }
    }
}