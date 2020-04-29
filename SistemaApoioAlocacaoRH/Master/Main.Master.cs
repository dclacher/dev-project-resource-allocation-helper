using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Persistencia.DTO;
using System.Web.Security;
using System.Globalization;

namespace SistemaApoioAlocacaoRH.Master
{
    public partial class Main : System.Web.UI.MasterPage
    {
        // propriedade que busca o usuario logado na session.
        public UsuarioDTO UsuarioAtual 
        {
            get { return (UsuarioDTO)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

        public CultureInfo Cultura
        {
            get 
            {
               return new CultureInfo("pt-BR"); 
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //lblNomeUsuarioMensagem.Text = UsuarioAtual.NomeUsuario + " " + UsuarioAtual.SobrenomeUsuario;
                lblUsuario.Text = UsuarioAtual.NomeUsuario + " " + UsuarioAtual.SobrenomeUsuario;
                AuthorizationLinks();
                LoadProjetosTerminados();
            }
        }

        protected void rptProjetosFinalizados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink lnkAvaliarProj = e.Item.FindControl("lnkAvaliarProj") as HyperLink;
            Label lblNomeUsuarioMensagem = e.Item.FindControl("lblNomeUsuarioMensagem") as Label;
            Label lblDataMensagem = e.Item.FindControl("lblDataMensagem") as Label;
            Label lblMensagem = e.Item.FindControl("lblMensagem") as Label;

            Persistencia.Projeto projeto = e.Item.DataItem as Persistencia.Projeto;

            if (projeto != null)
            {
                lnkAvaliarProj.NavigateUrl = "~/AvaliarEquipe.aspx?IdProjeto=" + projeto.proj_id;
                lblNomeUsuarioMensagem.Text = UsuarioAtual.NomeUsuario + " " + UsuarioAtual.SobrenomeUsuario;
                lblDataMensagem.Text = DateTime.Today.ToLongDateString();
                lblMensagem.Text = String.Format("O projeto {0} chegou ao fim! Por favor avalie sua equipe!", projeto.proj_nome);
            }
        }       

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        public void LoadProjetosTerminados()
        {
            int? idGerente = UsuarioAtual.UsuarioID;
            Fachada fachada = new Fachada();
            try
            {
                List<Persistencia.Projeto> listaProjetos = fachada.LoadProjetosTerminados(idGerente);
                if (listaProjetos.Count > 0)
                {
                    rptProjetosFinalizados.DataSource = listaProjetos;
                    rptProjetosFinalizados.DataBind();
                    lblquantidadeMensagens.Text = listaProjetos.Count.ToString();
                    liMensagem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                OpenErrorModal(ex.Message);
            }
        }

        // Define os links que usuário pode ver e acessar.
        private void AuthorizationLinks()
        {
            switch(UsuarioAtual.TipoUsuario)
            {
                case 1: // Gerente
                    lnkConsultaRecursos.Enabled = true;
                    lnkConsultaRecursos.Visible = true;

                    lnkCadastrarCompetencia.Enabled = true;
                    lnkCadastrarCompetencia.Visible = true;

                    lnkCadastrarUsuario.Enabled = false;
                    lnkCadastrarUsuario.Visible = false;

                    lnkCadastrarProjeto.Enabled = true;
                    lnkCadastrarProjeto.Visible = true;

                    lnkConsultarProjeto.Enabled = true;
                    lnkConsultarProjeto.Visible = true;

                    lnkSelecionarEquipe.Enabled = true;
                    lnkSelecionarEquipe.Visible = true;

                    lnkAtualizarCompetencias.Enabled = true;
                    lnkAtualizarCompetencias.Visible = true;

                    lnkAvaliarEquipe.Enabled = true;
                    lnkAvaliarEquipe.Visible = true;

                    lnkPerfil.Enabled = false;
                    lnkPerfil.Visible = false;

                    lnkAdicionarCompetencias.Enabled = true;
                    lnkAdicionarCompetencias.Visible = true;

                    lnkVisualizarHistorico.Enabled = true;
                    lnkVisualizarHistorico.Visible = true;

                    lnkConsultarCompetencias.Enabled = true;
                    lnkConsultarCompetencias.Visible = true;
                    break;
                case 2: // Funcionário
                    lnkConsultaRecursos.Enabled = false;
                    lnkConsultaRecursos.Visible = false;

                    lnkCadastrarCompetencia.Enabled = false;
                    lnkCadastrarCompetencia.Visible = false;

                    lnkCadastrarUsuario.Enabled = false;
                    lnkCadastrarUsuario.Visible = false;

                    lnkCadastrarProjeto.Enabled = false;
                    lnkCadastrarProjeto.Visible = false;

                    lnkConsultarProjeto.Enabled = false;
                    lnkConsultarProjeto.Visible = false;

                    lnkSelecionarEquipe.Enabled = false;
                    lnkSelecionarEquipe.Visible = false;

                    lnkAtualizarCompetencias.Enabled = false;
                    lnkAtualizarCompetencias.Visible = false;

                    lnkAvaliarEquipe.Enabled = false;
                    lnkAvaliarEquipe.Visible = false;

                    lnkPerfil.Enabled = true;
                    lnkPerfil.Visible = true;

                    lnkAdicionarCompetencias.Enabled = true;
                    lnkAdicionarCompetencias.Visible = true;

                    lnkVisualizarHistorico.Enabled = false;
                    lnkVisualizarHistorico.Visible = false;

                    lnkConsultarCompetencias.Enabled = true;
                    lnkConsultarCompetencias.Visible = true;
                    break;
                case 3: // Administrador
                    lnkConsultaRecursos.Enabled = true;
                    lnkConsultaRecursos.Visible = true;

                    lnkCadastrarCompetencia.Enabled = true;
                    lnkCadastrarCompetencia.Visible = true;

                    lnkCadastrarUsuario.Enabled = true;
                    lnkCadastrarUsuario.Visible = true;

                    lnkCadastrarProjeto.Enabled = false;
                    lnkCadastrarProjeto.Visible = false;

                    lnkConsultarProjeto.Enabled = false;
                    lnkConsultarProjeto.Visible = false;

                    lnkSelecionarEquipe.Enabled = false;
                    lnkSelecionarEquipe.Visible = false;

                    lnkAtualizarCompetencias.Enabled = true;
                    lnkAtualizarCompetencias.Visible = true;

                    lnkAvaliarEquipe.Enabled = false;
                    lnkAvaliarEquipe.Visible = false;

                    lnkPerfil.Enabled = false;
                    lnkPerfil.Visible = false;

                    lnkAdicionarCompetencias.Enabled = true;
                    lnkAdicionarCompetencias.Visible = true;

                    lnkVisualizarHistorico.Enabled = true;
                    lnkVisualizarHistorico.Visible = true;

                    lnkConsultarCompetencias.Enabled = true;
                    lnkConsultarCompetencias.Visible = true;
                    break;
            }
        }

        public void OpenErrorModal(string msg)
        {
            lblErroMsg.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarErro", "showError();", true);
        }

        public void OpenWarningModal(string msg)
        {
            lblAtencaoMsg.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarAtencao", "showWarning();", true);
        }

        public void OpenSuccesModal(string msg)
        {
            lblSucessoMsg.Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarSucesso", "showSuccess();", true);
        }
                       
    }
}