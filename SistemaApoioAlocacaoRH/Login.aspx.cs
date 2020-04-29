using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Persistencia.DTO;
using Negocio;
using System.Web.Security;

namespace SistemaApoioAlocacaoRH
{
    public partial class Login : System.Web.UI.Page
    {
                
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    // Significa que o processo de Autorização foi negado e o usuário foi redirecionado 
                    //para a Login e agora será redirectionado para a página customizada de Acesso Negado.
                    Response.Redirect("~/Forms/AcessoNegado.aspx");
            }
        }
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginFail.Visible = false;
            ShowLoginPopup();
        }


        private void ShowLoginPopup()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowLoginPopup", "showLoginPopup();", true);
        }

        protected void btnPopupLogin_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                txtUserName.Text = string.Empty;
                txtPasword.Text = string.Empty;
            }
            else
            {
                Logar();
            }
        }

        private void Logar()
        {
            fachada = new Fachada();

            string nomeUsuario = txtUserName.Text;
            string senha = txtPasword.Text; 

            try
            {
                UsuarioDTO usuario = fachada.GetUsuario(nomeUsuario, senha);

                if(usuario.UsuarioID != null)
                {
                    if (chkRememberMe.Checked)
                    {
                        CriarCookieLembrar();
                    }
                    else
                    {
                        RemoverCookieLembrar();
                    }

                    // cria um usuário na session.
                    Session["Usuario"] = usuario;

                    // cria o cookie de autenticação do .Net.
                    if (!string.IsNullOrWhiteSpace(Request["RETURNURL"]))
                    {
                        FormsAuthentication.RedirectFromLoginPage(usuario.NomeUsuario, chkRememberMe.Checked);
                    }
                    else
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuario.NomeUsuario, DateTime.Now, DateTime.MaxValue, chkRememberMe.Checked, usuario.TipoUsuarioDesc);
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        if (!FormsAuthentication.CookiesSupported)
                        {                            
                            // Se o browser não suportar cookies então seta para a URL.
                            FormsAuthentication.SetAuthCookie(encryptedTicket, false);
                        }
                        else
                        {
                            // Se o uso de cookies estiver habilitado pno navegador, então cria um cookie
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                            encryptedTicket);
                            // Seta o tempo de expiração do cookie conforme o definido no Ticket de authenticação.
                            authCookie.Expires = ticket.Expiration;
                            // Adiciona o cookie.
                            HttpContext.Current.Response.Cookies.Add(authCookie);
                        }                        
                        Response.Redirect("Default.aspx", false); // o false aqui é pq a página atual ainda deve processar a informação do bloco "catch".
                    }
                    
                }
                else
                {
                    lblLoginFail.Visible = true;
                    ShowLoginPopup();
                }
            }
            catch (Exception ex)
            {
                OpenErrorModal(ex.Message);
            }
        }

        // cria um cookie para armazenar o nome de usuário no navegador para futuros acessos.
        private void CriarCookieLembrar()
        {
            HttpCookie lembrarCookie = new HttpCookie("UltimoUserLogado");
            lembrarCookie.Values.Remove("UltimoUserLogado");
            lembrarCookie.Values.Add("UltimoUserLogado", txtUserName.Text);
            lembrarCookie.Expires = DateTime.MaxValue;

            Response.Clear();
            Response.Cookies.Add(lembrarCookie);
        }

        private void RemoverCookieLembrar()
        {
            HttpCookie lembrarCookie = new HttpCookie("UltimoUserLogado");
            lembrarCookie.Values.Remove("UltimoUserLogado");

            Response.Clear();
            Response.Cookies.Add(lembrarCookie);
        }

        private void OpenErrorModal(string msg)
        {
            lblErroMsg.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarErro", "showError();", true);
        }
    }
}