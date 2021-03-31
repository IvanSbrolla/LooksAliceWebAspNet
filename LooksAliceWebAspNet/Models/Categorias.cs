using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LooksAliceWebAspNet.Models
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Categoria_Nome { get; set; }
    }
}