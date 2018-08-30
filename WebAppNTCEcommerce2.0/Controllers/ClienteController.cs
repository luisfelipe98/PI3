using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebAppNTCEcommerce2._0.Models;

namespace WebAppNTCEcommerce2._0.Controllers
{
    public class ClienteController : Controller
    {
        private Entities1 db = new Entities1();

        public ActionResult Cadastrar()
        {
            if (Request.Cookies["IdCliente"] != null)
            {
                return RedirectToAction("Endereco");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                var idClienteCookie = new HttpCookie("IdCliente", cliente.idCliente.ToString());
                idClienteCookie.Expires = DateTime.Now.AddDays(365);
                Response.SetCookie(idClienteCookie);
                return RedirectToAction("Endereco");
            }
            return View(cliente);
        }

        public ActionResult Endereco()
        {
            if (Request.Cookies["IdCliente"] != null)
            {
                Endereco end = new Endereco();
                end.idCliente = Convert.ToInt32(Request.Cookies["IdCliente"].Value);
                return View(end);
            }
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public ActionResult Endereco(Endereco endereco)
        {
            if (Request.Cookies["IdCliente"] != null)
            {
                endereco.idCliente = Convert.ToInt32(Request.Cookies["IdCliente"].Value);
                if (ModelState.IsValid)
                {
                    db.Endereco.Add(endereco);
                    db.SaveChanges();
                    Response.Cookies["IdCliente"].Expires = DateTime.Now.AddDays(-1);
                    return RedirectToAction("CadastroSucesso");
                }
                return View(endereco);
            }
            return RedirectToAction("Cadastrar");
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Create() {

            return View();
        }

        public JsonResult VerificarCPF(string CPFCliente)
        {
            bool blnValida = Validacoes.ValidaCPF(CPFCliente);
            if (blnValida)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json("Digite um CPF válido", JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerificarEmail(string emailCliente)
        {
            Cliente cliente = db.Cliente.Where(s => s.emailCliente.Equals(emailCliente)).
                SingleOrDefault();
            if (cliente != null)
                return Json("E-mail já cadastrado", JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(ContaViewModel model, string returnUrl)
        {
           
            if (ModelState.IsValid)
            {

                Cliente cliente = ChecarAcesso(model);

                if (cliente != null)
                {       
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, cliente.nomeCompletoCliente));
                    claims.Add(new Claim(ClaimTypes.Email, cliente.emailCliente));
                    var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var ctx = Request.GetOwinContext();
                    var authenticationManager = ctx.Authentication;
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(id);
                    Session.Add("cliente", cliente);

                    HttpCookie cookie = new HttpCookie("cliente");
                    HttpContext.Response.Cookies.Remove("cliente");
                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value = cliente.idCliente.ToString();
                    HttpContext.Response.Cookies.Add(cookie);

                    if (returnUrl != null)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Mensagem = "Acesso negado, tente novamente...";
            return View(model);
        }

        private Cliente ChecarAcesso(ContaViewModel model)
        {
            try
            {
                Cliente cliente = db.Cliente.Where(m => m.emailCliente == model.Email && m.senhaCliente == model.Password).FirstOrDefault();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            HttpContext.Response.Cookies.Remove("cliente");
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult FinalizarCompra()
        {
            return View();
        }

        public ActionResult CadastroSucesso()
        {
            return View();
        }
    }
}