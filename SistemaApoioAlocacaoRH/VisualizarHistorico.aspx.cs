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
    public partial class VisualizarHistorico : System.Web.UI.Page
    {
        private int IDFuncionario
        {
            get
            {
                return (int)ViewState["IDFuncionario"];
            }
            set
            {
                ViewState["IDFuncionario"] = value;
            }
        }

        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
             if(!Page.IsPostBack)
             {
                 LoadCombos();
             }
        }

        protected void ddlNomeFuncionario_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlNomeFuncionario.SelectedItem.Text))
            {
                Page.Validate();
                if (Page.IsValid)
                {
                    LoadCompetencias();
                }
            }
        }

        protected void grvGridCompetencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("HistoricoComp"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int compID = Convert.ToInt32(args[0]);
                string compNome = args[1];
                Response.Redirect("~/Historico.aspx?IdCompetencia=" + compID + "&NomeCompetencia=" + compNome + "&IdFuncionario=" + IDFuncionario);
            }
        }
                
        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaFuncionarios = fachada.LoadFuncionarios();
                listaFuncionarios.Insert(0, new DropDownItem(null, ""));
                ddlNomeFuncionario.DataSource = listaFuncionarios;
                ddlNomeFuncionario.DataBind();
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LoadCompetencias()
        {
            fachada = new Fachada();
            try
            {
                if (!String.IsNullOrEmpty(ddlNomeFuncionario.SelectedItem.Value))
                {
                    fachada = new Fachada();
                    try
                    {
                        IDFuncionario = Convert.ToInt32(ddlNomeFuncionario.SelectedItem.Value);
                        List<CompetenciaNivelDTO> listaCompetenciasFuncionario = fachada.LoadCompetenciasFuncionario(IDFuncionario);

                        if (listaCompetenciasFuncionario.Count > 0)
                        {
                            grvGridCompetencias.DataSource = listaCompetenciasFuncionario;
                            grvGridCompetencias.DataBind();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        Master.OpenErrorModal(ex.Message);
                    }
                }                

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        

    }
}