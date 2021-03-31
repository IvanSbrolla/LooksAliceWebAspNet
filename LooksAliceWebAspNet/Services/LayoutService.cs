using LooksAliceWebAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LooksAliceWebAspNet.Services
{
    public class LayoutService
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public List<Categorias> GetCategorias()
        {
            var result = from obj in context.categorias select obj;
            return result
                .ToList();
        }
    }
}