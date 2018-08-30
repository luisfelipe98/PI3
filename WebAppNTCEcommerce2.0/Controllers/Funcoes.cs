using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebAppNTCEcommerce2._0.Controllers
{
    public class Funcoes
    {
        public static string GetNome()
        {
            try
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claims = identity.Claims.ToList();
                return claims[0].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static string GetEmail()
        {
            try
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claims = identity.Claims.ToList();
                return claims[1].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static int GetId()
        {
            try
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claims = identity.Claims.ToList();
                return Convert.ToInt32(claims[2].Value.ToString());
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}