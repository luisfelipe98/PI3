using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppNTCEcommerce2._0.Models;

namespace WebAppNTCEcommerce2._0.Controllers
{
    public class PedidoController : Controller
    {
        private Entities1 db = new Entities1();

        [Authorize]
        public ActionResult Consultar()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("cliente");
            Cliente cliente = new Cliente();
            cliente.idCliente = Convert.ToInt32(cookie.Value);
            if (cliente.idCliente > 0)
            {
                var resultado = (from grupo in
                                    (from ip in db.ItemPedido
                                     orderby ip.idPedido descending
                                     group ip by ip.idPedido into g
                                     select new { IdPedido = g.Key, IdCliente = cliente.idCliente, TotalPagamento = g.Sum(item => item.precoVendaItem * item.qtdProduto) })
                                 join pedidos in
                                     (from p in db.Pedido
                                      join c in db.Cliente
                                      on p.idCliente equals c.idCliente
                                      join sp in db.StatusPedido
                                      on p.idStatus equals sp.idStatus
                                      join tp in db.TipoPagamento
                                      on p.idTipoPagto equals tp.idTipoPagto
                                      where c.idCliente == cliente.idCliente
                                      orderby p.idPedido descending
                                      select new
                                      {
                                          IdPedido = p.idPedido,
                                          NomeCompletoCliente = c.nomeCompletoCliente,
                                          DescStatus = sp.descStatus,
                                          DataPedido = p.dataPedido,
                                          DescTipoPagto = tp.descTipoPagto
                                      })
                                      on grupo.IdPedido equals pedidos.IdPedido
                                 select new
                                 {
                                     IdPedido = pedidos.IdPedido,
                                     NomeCompletoCliente = pedidos.NomeCompletoCliente,
                                     DescStatus = pedidos.DescStatus,
                                     DataPedido = pedidos.DataPedido,
                                     DescTipoPagto = pedidos.DescTipoPagto,
                                     TotalPagto = grupo.TotalPagamento
                                 }).ToList();

                List<PedidoViewModel> lista = new List<PedidoViewModel>();

                foreach (var item in resultado)
                {
                    PedidoViewModel pvm = new PedidoViewModel();
                    pvm.idPedido = item.IdPedido;
                    pvm.nomeCompletoCliente = item.NomeCompletoCliente;
                    pvm.descStatus = item.DescStatus;
                    pvm.dataPedido = item.DataPedido;
                    pvm.descTipoPagto = item.DescTipoPagto;
                    pvm.totalPagto = item.TotalPagto;
                    lista.Add(pvm);
                }

                lista = lista.OrderByDescending(d => d.idPedido).ToList();

                return View(lista);
            }
            else
            {
                return View("SemPedido");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Detalhes(int pedido, decimal total)
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("cliente");
            Cliente cliente = new Cliente();
            cliente.idCliente = Convert.ToInt32(cookie.Value);
            if (cliente.idCliente > 0)
            {
                var resultado = (from p in db.Produto
                                 join ip in db.ItemPedido on p.idProduto equals ip.idProduto
                                 join ped in db.Pedido on ip.idPedido equals ped.idPedido
                                 where ped.idPedido == pedido
                                 where ped.idCliente == cliente.idCliente
                                 select new
                                 {
                                     idPedido = pedido,
                                     imagem = p.imagem,
                                     nomeProduto = p.nomeProduto,
                                     precoVendaItem = ip.precoVendaItem,
                                     qtdProduto = ip.qtdProduto
                                 }).ToList();

                List<PedidoDetalhesViewModel> lista =
                    new List<PedidoDetalhesViewModel>();
                foreach (var item in resultado)
                {
                    PedidoDetalhesViewModel pdvm = new PedidoDetalhesViewModel();
                    pdvm.idPedido = item.idPedido;
                    pdvm.imagem = item.imagem;
                    pdvm.nomeProduto = item.nomeProduto;
                    pdvm.precoVendaItem = item.precoVendaItem;
                    pdvm.qtdProduto = item.qtdProduto;
                    lista.Add(pdvm);
                }

                lista = lista.OrderBy(n => n.nomeProduto).ToList();

                ViewBag.Pedido = "Detalhes do Pedido: " + pedido.ToString() + " - Valor Total: R$" + total.ToString("#,##.00");

                return View(lista);
            }
            else
            {
                return View("SemPedido");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
