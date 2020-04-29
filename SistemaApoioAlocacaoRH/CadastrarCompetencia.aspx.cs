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
    public partial class CadastrarCompetencia : System.Web.UI.Page
    {
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LimparDados();
                LoadCombos();
            }
        }

        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaTipoCompetencia = fachada.LoadTipoCompetencia();
                listaTipoCompetencia.Insert(0, new DropDownItem(null, ""));
                ddlTipo.DataSource = listaTipoCompetencia;
                ddlTipo.DataBind();
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LimparDados()
        {
            txtNome.Text = String.Empty;
            txtDescricao.Text = String.Empty;
            ddlTipo.Text = String.Empty;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                CadastrarNovaCompetencia();
            }

        }

        private void CadastrarNovaCompetencia()
        {
            // se voltar false dispara a popup de warning avisando que ja existe essa competencia.
            fachada = new Fachada();
            try
            {
                string nome = txtNome.Text;
                string descricao = txtDescricao.Text;
                int tipo = Convert.ToInt32(ddlTipo.SelectedItem.Value);

                bool cadastrado = false;

                if (tipo == 1)
                {
                    cadastrado = fachada.CadastrarConhecimento(nome, descricao);
                }
                else if (tipo == 2)
                {
                    cadastrado = fachada.CadastrarHabilidade(nome, descricao);
                }
                else
                {
                    cadastrado = fachada.CadastrarAtitude(nome, descricao);
                }

                if (cadastrado)
                {
                    // abre a popup de sucesso. (verdinha)
                    Master.OpenSuccesModal(ddlTipo.SelectedItem.Text + " cadastrado(a) com Sucesso!");
                    LimparDados();
                }
                else
                {
                    Master.OpenWarningModal("O(A) " + ddlTipo.SelectedItem.Text + " já existe.");
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }
    }
}