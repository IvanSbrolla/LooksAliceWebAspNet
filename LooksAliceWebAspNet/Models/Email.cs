using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LooksAliceWebAspNet.Models
{
    public class Email
    {
        [Key]
        public int NomeID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Número de caracteres excedido!")]
        [MinLength(3, ErrorMessage = "Número inválido de caracteres!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Insira seu e-mail.")]
        [EmailAddress(ErrorMessage = "Endereço de E-mail inválido!")]
        public string EnderecoEmail { get; set; }
        [Required(ErrorMessage = "Insira seu telefone.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Insira seu assunto.")]
        [MinLength(3, ErrorMessage = "Número inválido de caracteres!")]
        [MaxLength(60, ErrorMessage = "Número de caracteres excedido!")]
        public string Assunto { get; set; }
        [Required(ErrorMessage = "Insira a menssagem.")]
        [MinLength(10, ErrorMessage = "Número inválido de caracteres!")]
        public string Menssagem { get; set; }
    }
}