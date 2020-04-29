using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Persistencia;



namespace SistemaApoioAlocacaoRH
{
    public partial class CadastrarProjeto : System.Web.UI.Page
    {
        
        private List<DropDownItem> ListaCompetencias
        {
            get
            {
                return (List<DropDownItem>)ViewState["ListaCompetencias"];
            }
            set
            {
                ViewState["ListaCompetencias"] = value;
            }
        }
              

        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                txtNomeGerente.Text = Master.UsuarioAtual.NomeUsuario + " " + Master.UsuarioAtual.SobrenomeUsuario;
                ListaCompetencias = new List<DropDownItem>();
                grvSelecionados.DataSource = null;
                grvSelecionados.DataBind();
                LoadCombos();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                CadastrarNovoProjeto();
            }
            
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        protected void btnLimparSelecionados_Click(object sender, EventArgs e)
        {
            LimparCompetenciasSelecionadas();
        }

        protected void grvSelecionados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("RemoverComp"))
            {
                int idCompetencia = Convert.ToInt32(e.CommandArgument.ToString());
                foreach (DropDownItem item in ListaCompetencias)
                {
                    if (item.Code == idCompetencia)
                    {
                        ListaCompetencias.Remove(item);
                        grvSelecionados.DataSource = ListaCompetencias;
                        grvSelecionados.DataBind();
                        break;
                    }
                }

            }
        }

        protected void custValiSelecionados_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ListaCompetencias.Count > 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }

        }

        protected void ddlConhecimentos_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlConhecimentos.SelectedItem.Text))
            {
                string desc = ddlConhecimentos.SelectedItem.Text;
                int code = Convert.ToInt32(ddlConhecimentos.SelectedItem.Value);
                DropDownItem item = new DropDownItem(code, desc);

                if (!ListaCompetencias.Contains(item) && !String.IsNullOrEmpty(desc) && ListaCompetencias.Count < 10)
                {
                    ListaCompetencias.Add(item);
                    grvSelecionados.DataSource = ListaCompetencias;
                    grvSelecionados.DataBind();
                }
                else if (ListaCompetencias.Count == 10)
                {
                    Master.OpenWarningModal("A lista de competência pode ter no máximo 10 itens!");
                }
                else if (ListaCompetencias.Contains(item))
                {
                    Master.OpenWarningModal("A competência " + desc + " já está cadastrada!");
                }
            }
        }

        
        protected void ddlHabilidades_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlHabilidades.SelectedItem.Text))
            {
                string desc = ddlHabilidades.SelectedItem.Text;
                int code = Convert.ToInt32(ddlHabilidades.SelectedItem.Value);
                DropDownItem item = new DropDownItem(code, desc);

                if (!ListaCompetencias.Contains(item) && !String.IsNullOrEmpty(desc) && ListaCompetencias.Count < 10)
                {
                    ListaCompetencias.Add(item);
                    grvSelecionados.DataSource = ListaCompetencias;
                    grvSelecionados.DataBind();
                }
                else if (ListaCompetencias.Count == 10)
                {
                    Master.OpenWarningModal("A lista de competência pode ter no máximo 10 itens!");
                }
                else if (ListaCompetencias.Contains(item))
                {
                    Master.OpenWarningModal("A competência " + desc + " já está cadastrada!");
                }
            }
        }

        
        protected void ddlAtitudes_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlAtitudes.SelectedItem.Text))
            {
                string desc = ddlAtitudes.SelectedItem.Text;
                int code = Convert.ToInt32(ddlAtitudes.SelectedItem.Value);
                DropDownItem item = new DropDownItem(code, desc);

                if (!ListaCompetencias.Contains(item) && !String.IsNullOrEmpty(desc) && ListaCompetencias.Count < 10)
                {
                    ListaCompetencias.Add(item);
                    grvSelecionados.DataSource = ListaCompetencias;
                    grvSelecionados.DataBind();
                }
                else if (ListaCompetencias.Count == 10)
                {
                    Master.OpenWarningModal("A lista de competência pode ter no máximo 10 itens!");
                }
                else if (ListaCompetencias.Contains(item))
                {
                    Master.OpenWarningModal("A competência " + desc + " já está cadastrada!");
                }
            }
        }

        
        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaConhecimentos = fachada.LoadConhecimentos();
                listaConhecimentos.Insert(0, new DropDownItem(null, ""));
                ddlConhecimentos.DataSource = listaConhecimentos;
                ddlConhecimentos.DataBind();

                List<DropDownItem> listaHabilidades = fachada.LoadHabilidades();
                listaHabilidades.Insert(0, new DropDownItem(null, ""));
                ddlHabilidades.DataSource = listaHabilidades;
                ddlHabilidades.DataBind();

                List<DropDownItem> listaAtitudes = fachada.LoadAtitudes();
                listaAtitudes.Insert(0, new DropDownItem(null, ""));
                ddlAtitudes.DataSource = listaAtitudes;
                ddlAtitudes.DataBind();

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void CadastrarNovoProjeto()
        {
            fachada = new Fachada();
            try
            {
                int? idGerente = Master.UsuarioAtual.UsuarioID;
                string nomeProjeto = txtNomeProjeto.Text;
                string descProjeto = txtDescProjeto.Text;
                DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text, Master.Cultura);
                DateTime dataTermino = Convert.ToDateTime(txtDataTermino.Text, Master.Cultura);
                string prioridade = ddlPrioridadeProjeto.Text;
                string status = ddlStatusProjeto.Text;

                List<int?> listaIdComp = new List<int?>();
                foreach (DropDownItem item in ListaCompetencias)
                {
                    if (item.Code != null)
                    {
                        listaIdComp.Add(item.Code);
                    }
                }

                bool projetoCadastrado = fachada.CadastrarProjeto(idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdComp);

                if (projetoCadastrado)
                {
                    // abre a popup de sucesso.
                    Master.OpenSuccesModal("Projeto cadastrado com Sucesso!");
                    LimparDados();
                }
                else
                {
                    Master.OpenWarningModal("O usuário " + Master.UsuarioAtual.NomeUsuario + " " + Master.UsuarioAtual.SobrenomeUsuario + " já possuí um projeto cadastrado com o nome de " + nomeProjeto);
                }


            }
            catch(Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LimparCompetenciasSelecionadas()
        {
            ListaCompetencias.Clear();
            grvSelecionados.DataSource = null;
            grvSelecionados.DataBind();
        }

        private void LimparDados()
        {
            ListaCompetencias.Clear();
            grvSelecionados.DataSource = null;
            grvSelecionados.DataBind();

            txtNomeProjeto.Text = String.Empty;
            txtDescProjeto.Text = String.Empty;
            txtDataInicio.Text = String.Empty;
            txtDataTermino.Text = String.Empty;
            ddlAtitudes.Text = String.Empty;
            ddlHabilidades.Text = String.Empty;
            ddlConhecimentos.Text = String.Empty;
            ddlStatusProjeto.Text = String.Empty;
            ddlPrioridadeProjeto.Text = String.Empty;           
        }

       

      

       
    }
}