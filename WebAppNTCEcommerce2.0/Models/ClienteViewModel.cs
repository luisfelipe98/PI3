using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppNTCEcommerce2._0.Models
{
    [NotMapped]
    public class ClienteViewModel {
        [Key]
        public virtual int idCliente { get; set; }
        [StringLength(70, MinimumLength = 5)]
        [DisplayName("Nome")]
        public virtual string nomeCompletoCliente { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [StringLength(100, MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [DisplayName("E-mail")]
        public virtual string emailCliente { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public virtual string senhaCliente { get; set; }
        [DisplayName("CPF")]
        [MaxLength(11, ErrorMessage = "Maximo é 11 dígitos")]
        [MinLength(11, ErrorMessage = "Minimo é 11 dígitos")]
        public string CPFCliente { get; set; }
        [DisplayName("Celular")]
        [MaxLength(11, ErrorMessage = "Máximo de 11 dígitos")]
        [MinLength(11, ErrorMessage = "Minimo é 11 dígitos")]
        public string celularCliente { get; set; }
        [DisplayName("Telefone Comercial")]
        [MaxLength(11, ErrorMessage = "Máximo de 11 dígitos")]
        [MinLength(11, ErrorMessage = "Minimo é 11 dígitos")]
        public string telComercialCliente { get; set; }
        [DisplayName("Telefone Residencial")]
        [MaxLength(11, ErrorMessage = "Máximo de 11 dígitos")]
        [MinLength(11, ErrorMessage = "Minimo é 11 dígitos")]
        public string telResidencialCliente { get; set; }
        [Required(ErrorMessage = "Preencha o campo Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayName("Data de Nascimento")]
        public DateTime? dtNascCliente { get; set; }
        [DisplayName("Deseja receber newsletter?")]
        public bool recebeNewsLetter { get; set; }
    }
}
