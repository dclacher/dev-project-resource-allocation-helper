using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Persistencia;
using Persistencia.DTO;


namespace SistemaApoioAlocacaoRH
{
    public partial class ConsultarProjetos : System.Web.UI.Page
    {
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCombos();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                LoadProjetos();
            }
        }

        protected void grvGridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.Equals("verRecursos"))
            {
                int idProjeto = Convert.ToInt32(e.CommandArgument.ToString());
                ConsultarRecursos(idProjeto);
            }
            else if(e.CommandName.Equals("alterarProjeto"))
            {
                int idProjeto = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("~/AlterarProjeto.aspx?IdProjeto=" + idProjeto);
            }
        }

        // evento que gerencia cada inserção de uma linha do gridView
        protected void grvGridProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // verfica se a row é do tipo de dados, pois ela pode ter outros valores
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Persistencia.Projeto dataItem = (Persistencia.Projeto)e.Row.DataItem;
                if(dataItem.proj_usu_id == Master.UsuarioAtual.UsuarioID)
                {
                    // se o gerente da row do grid for o mesmo gerente que fez a consulta, libera os links para alterar um projeto
                    LinkButton btnEditarProjeto = (LinkButton)e.Row.FindControl("btnEditarProjeto");
                    btnEditarProjeto.CssClass = "";
                    btnEditarProjeto.Enabled = true;
                }
            }
        }

        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaGerentes = fachada.LoadGerentes();
                listaGerentes.Insert(0, new DropDownItem(null, ""));
                ddlNomeGerente.DataSource = listaGerentes;
                ddlNomeGerente.DataBind();               

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LimparDados()
        {
            txtNomeProjeto.Text = String.Empty;
            ddlNomeGerente.Text = String.Empty;
            ddlStatusProjeto.Text = String.Empty;
            ddlPrioridadeProjeto.Text = String.Empty;
            txtDataInicio.Text = String.Empty;
            txtDataTermino.Text = String.Empty;

            grvGridProjetos.DataSource = new List<Persistencia.Projeto>();
            grvGridProjetos.DataBind();
        }

        private void LoadProjetos()
        {
            fachada = new Fachada();
            try
            {
                string nome = txtNomeProjeto.Text;
                int? idGerente = null;
                if(!String.IsNullOrEmpty(ddlNomeGerente.SelectedItem.Value))
                {
                    idGerente = Convert.ToInt32(ddlNomeGerente.SelectedItem.Value);
                }                
                string status = ddlStatusProjeto.Text;
                string prioridade = ddlPrioridadeProjeto.Text;
                DateTime? dataInicio = null; 
                if(!String.IsNullOrEmpty(txtDataInicio.Text))
                {
                    dataInicio = Convert.ToDateTime(txtDataInicio.Text, Master.Cultura);
                }                
                DateTime? dataTermino = null;
                if(!String.IsNullOrEmpty(txtDataTermino.Text))
                {
                    dataTermino = Convert.ToDateTime(txtDataTermino.Text, Master.Cultura);
                }
                
                List<Persistencia.Projeto> listaProjeto = fachada.LoadProjetos(nome, idGerente, status, prioridade, dataInicio, dataTermino);
                
                if(listaProjeto.Count > 0)
                {
                    grvGridProjetos.DataSource = listaProjeto;
                    grvGridProjetos.DataBind();
                }

            }
            catch (Exception ex)
            {                
                Master.OpenErrorModal(ex.Message);
            }
        } 
       
        private void ConsultarRecursos(int idProjeto)
        {
            fachada = new Fachada();
            try
            {
                List<ProjetoUsuarioDTO> listaRecursosDoProjeto = new List<ProjetoUsuarioDTO>();
                if(idProjeto != null && idProjeto > 0)
                {
                    listaRecursosDoProjeto = fachada.LoadRHProjeto(idProjeto);

                    if(listaRecursosDoProjeto.Count > 0)
                    {
                        grvRecursos.Visible = true;
                        lblPopupMsg.Visible = false;

                        grvRecursos.DataSource = listaRecursosDoProjeto;
                        grvRecursos.DataBind();
                    }
                    else
                    {
                        lblPopupMsg.Visible = true;
                        grvRecursos.Visible = false;
                    }
                    AbrirPopup();
                }
                
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);   
            }
        }

        private void AbrirPopup()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirPopup", "ShowModal();", true);
        }

       
    }
}