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
	public partial class AtualizarCompetencias : System.Web.UI.Page
	{
        private Fachada fachada;

        private int? IDFuncionario
        {
            get
            {
                return (int?)ViewState["IDFuncionario"];
            }
            set
            {
                ViewState["IDFuncionario"] = value;
            }
        }

        private List<CompetenciaNivelDTO> ListaCompetenciasComNivel
        {
            get
            {
                return (List<CompetenciaNivelDTO>)ViewState["ListaCompetencias"];
            }
            set
            {
                ViewState["ListaCompetencias"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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

            if (e.CommandName.Equals("AjustarNivel"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int compID = Convert.ToInt32(args[0]);
                string compNome = args[1];
                AbrirPopup(compID, compNome);
            }
        }

        private void AbrirPopup(int compID, string compNome)
        {
            LimparDadosPopup();
            txtNomeComp.Text = compNome;
            hdnCompID.Value = compID.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirPopup", "ShowModal();", true);
        }

        private void LimparDadosPopup()
        {
            txtNomeComp.Text = String.Empty;
            txtNivelValor.Text = String.Empty;
            hdnCompID.Value = String.Empty;
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

        private void LimparDados()
        {
            ddlNomeFuncionario.Text = String.Empty;

            grvGridCompetencias.DataSource = new List<Persistencia.DTO.CompetenciaNivelDTO>();
            grvGridCompetencias.DataBind();
        }

        private void LoadCompetencias()
        {
            fachada = new Fachada();
            try
            {
                if (!String.IsNullOrEmpty(ddlNomeFuncionario.SelectedItem.Value))
                {
                    IDFuncionario = Convert.ToInt32(ddlNomeFuncionario.SelectedItem.Value);
                }

                ListaCompetenciasComNivel = fachada.LoadCompetenciasComNiveis(IDFuncionario);

                if (ListaCompetenciasComNivel.Count > 0)
                {
                    grvGridCompetencias.DataSource = ListaCompetenciasComNivel;
                    grvGridCompetencias.DataBind();
                }

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        protected void btnAjustarNivel_Click(object sender, EventArgs e)
        {
            AjustarNivel();
        }

        private void AjustarNivel()
        {
            if (!String.IsNullOrEmpty(txtNivelValor.Text))
            {
                int nivel = Convert.ToInt32(txtNivelValor.Text);
                int compId = Convert.ToInt32(hdnCompID.Value);

                foreach (CompetenciaNivelDTO compNivel in ListaCompetenciasComNivel)
                {
                    if (compNivel.CompID == compId)
                    {
                        compNivel.CompNivel = nivel;
                        break;
                    }
                }

                grvGridCompetencias.DataSource = ListaCompetenciasComNivel;
                grvGridCompetencias.DataBind();

            }
            else
            {
                Master.OpenWarningModal("Nível não pode ser vazio!");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                AlterarNivel();
            }
        }

        private void AlterarNivel()
        {
            fachada = new Fachada();
            try
            {
                int? idGerente = Master.UsuarioAtual.UsuarioID;

                bool competenciasAlteradas = fachada.AlterarCompetencias(idGerente, IDFuncionario, ListaCompetenciasComNivel);

                if (competenciasAlteradas)
                {
                    // abre a popup de sucesso.
                    Master.OpenSuccesModal("Competências alteradas com Sucesso!");
                }
                else
                {
                    Master.OpenWarningModal("Houve um problema ao alterar as competências!");
                }

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }
	}
}