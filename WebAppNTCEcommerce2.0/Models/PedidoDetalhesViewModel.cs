using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppNTCEcommerce2._0.Models
{
    [NotMapped]
    public class PedidoDetalhesViewModel
    {
        [DisplayName("Nº do Pedido")]
        public virtual int idPedido { get; set; }
        [DisplayName("Imagem")]
        public virtual byte[] imagem { get; set; }
        [DisplayName("Produto")]
        public virtual string nomeProduto { get; set; }
        [DisplayName("Preço Unitário (R$)")]
        public virtual decimal precoVendaItem { get; set; }
        [DisplayName("Quantidade")]
        public virtual short qtdProduto { get; set; }
    }
}