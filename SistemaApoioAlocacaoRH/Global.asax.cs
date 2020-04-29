using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SistemaApoioAlocacaoRH
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = (id.Ticket);
                        if (!FormsAuthentication.CookiesSupported)
                        {
                            //If cookie is not supported for forms authentication, then the
                            //authentication ticket is stored in the URL, which is encrypted.
                            //So, decrypt it
                            ticket = FormsAuthentication.Decrypt(id.Ticket.Name);
                        }
                        // Get the stored user-data, in this case, user roles
                        if (!string.IsNullOrEmpty(ticket.UserData))
                        {
                            string tipoUsuario = ticket.UserData;
                            //string[] roles = userData.Split(',');
                            string[] roles = new string[] {tipoUsuario};
                            //Roles were put in the UserData property in the authentication ticket
                            //while creating it
                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                        }
                    }
                }
            }
        }       
    }
}