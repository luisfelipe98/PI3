using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppNTCEcommerce2._0.Models
{
        [NotMapped]
        public class Carrinho
        {
            public virtual int idProduto { get; set; }
            public virtual string nomeProduto { get; set; }
            public virtual int qtdProduto { get; set; }
            public virtual decimal precoProduto { get; set; }
            public virtual decimal precoTotal { get; set; }
            public virtual string imageProduto { get; set; }
        }


    
}