using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppNTCEcommerce2._0.Models;

namespace WebAppNTCEcommerce2._0.Controllers
{
    public class ProdutoController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Produto
        public ActionResult ListarProdutos(int id, string ordenar = "")
        {
            Entities1 db = new Entities1();
            var lista = db.Produto.Where(m => m.idCategoria == id).OrderBy(m => m.nomeProduto);
            var listaOrdenada = new List<Produto>();

            switch (ordenar)
            {
                case "1":
                    listaOrdenada = lista.OrderBy(x => x.precProduto).ToList();
                    //db.Produto.Where(p => p.idCategoria == idCategoria).OrderBy(o => o.precProduto).ToList();
                    break;
                case "2":
                    listaOrdenada = lista.OrderByDescending(x => x.precProduto).ToList();
                    //lista = db.Produto.Where(p => p.idCategoria == idCategoria).OrderByDescending(o => o.precProduto).ToList();
                    break;
                case "3":
                    listaOrdenada = lista.OrderBy(x => x.nomeProduto).ToList();
                    //lista = db.Produto.Where(p => p.idCategoria == idCategoria).OrderBy(o => o.nomeProduto).ToList();
                    break;

                default:
                    listaOrdenada = listaOrdenada = lista.OrderBy(x => x.nomeProduto).ToList();
                    break;


            }

            if (listaOrdenada.Count > 0)
                ViewBag.Resultado = listaOrdenada[0].Categoria.nomeCategoria;
            else
                ViewBag.Resultado = "Não produtos cadastrados nesta categoria";
            ViewBag.IdCategoria = id;
            return View(listaOrdenada);
        }

        public ActionResult Detalhes(int? id)
        {
            Entities1 db = new Entities1();
            var produto = db.Produto.Where(m => m.idProduto == id).FirstOrDefault();
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCarrinho(int id, int qtd)
        {
            using (var ctx = new Entities1())
            {

                var produto = ctx.Produto.FirstOrDefault(x => x.idProduto == id);
                if (produto == null)
                    return Detalhes(id);

                List<ItemCarrinho> itens = (List<ItemCarrinho>)Session["Carrinho"];
                var novoItem = new ItemCarrinho();
                if (itens == null)
                {
                    itens = new List<ItemCarrinho>();
                }

                ItemCarrinho item = itens.FirstOrDefault(x => x.productId == produto.idProduto);
                if (item != null) 
                {
                    itens.Remove(item);
                    item.productQtd += qtd; 
                    item.productTotal = item.productPrice * item.productQtd;
                    itens.Add(item); 
                }
                else
                {
                    novoItem.productId = produto.idProduto;
                    novoItem.productImage = Convert.ToBase64String(produto.imagem);
                    novoItem.productName = produto.nomeProduto;
                    novoItem.productPrice = produto.precProduto;
                    novoItem.productQtd = qtd;
                    novoItem.productTotal += Convert.ToDecimal(produto.precProduto - (produto.precProduto * (produto.descontoPromocao / 100)));
                    itens.Add(novoItem);
                }

                decimal total = 0;
                foreach (var prod in itens)
                {
                    total += (prod.productPrice * prod.productQtd);
                }

                Session["Carrinho"] = itens;
                Session["Total"] = total.ToString("#,#0.00");
            }

            return RedirectToAction("Carrinho");

        }

        public ActionResult Carrinho()
        {
            if(Session["Carrinho"] == null)
                Session["Carrinho"] = new List<ItemCarrinho>();

            if (Session["Total"] == null)
                Session["Total"] = 0;

            return View("_Carrinho");
        }

        public ActionResult PartialDestaques()
        {
            Entities1 db = new Entities1();
            var destaques = db.Produto.OrderBy(m => Guid.NewGuid()).Take(3);
            return PartialView(destaques);
        }

        public ActionResult PartialDestaquesBaratos()
        {
            try
            {

                Entities1 db = new Entities1();
                var destaquesBaratos = db.Produto.OrderBy(m => m.precProduto).Take(6);
                var finalDestaquesBaratos = destaquesBaratos.OrderBy(m => Guid.NewGuid()).Take(3);
                return PartialView(finalDestaquesBaratos);
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public ActionResult PartialDestaquesBaratos1()
        {
            Entities1 db = new Entities1();
            var destaquesBaratos = db.Produto.OrderBy(m => m.precProduto).Take(6);
            var finalDestaquesBaratos = destaquesBaratos.OrderBy(m => Guid.NewGuid()).Take(3);
            return PartialView(finalDestaquesBaratos);
        }

        public ActionResult PartialCompra()
        {
            Entities1 db = new Entities1();
            var preço = db.Produto.OrderBy(m => m.precProduto);
            return PartialView(preço);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Consultar(string pesquisar)
        {
            if (!string.IsNullOrEmpty(pesquisar))
                ViewBag.Mensagem = "Resultado da Busca por: '" + pesquisar + "'";
            var resultado = db.Produto.Where(s => s.nomeProduto.Contains(pesquisar) || s.descProduto.Contains(pesquisar));
            return View("Consultar", resultado);
        }
    }
}