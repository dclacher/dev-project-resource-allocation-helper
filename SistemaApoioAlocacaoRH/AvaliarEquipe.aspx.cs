using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Persistencia;
using Persistencia.DTO;

namespace SistemaApoioAlocacaoRH
{
	public partial class AvaliarEquipe : System.Web.UI.Page
	{
        private Fachada fachada;

        private int? IDProjeto
        {
            get
            {
                return (int?)ViewState["IDProjeto"];
            }
            set
            {
                ViewState["IDProjeto"] = value;
            }
        }

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

        private int NumeroFuncionariosEquipe
        {
            get
            {
                return (int)ViewState["NumeroFuncionariosEquipe"];
            }
            set
            {
                ViewState["NumeroFuncionariosEquipe"] = value;
            }
        }

        private int NumeroFuncionariosAvaliados
        {
            get
            {
                return (int)ViewState["NumeroFuncionariosAvaliados"];
            }
            set
            {
                ViewState["NumeroFuncionariosAvaliados"] = value;
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

        private List<DropDownItem> ListaRecursosProjeto
        {
            get
            {
                return (List<DropDownItem>)ViewState["ListaRecursosProjeto"];
            }
            set
            {
                ViewState["ListaRecursosProjeto"] = value;
            }
        }

        private List<int?> ListaRecursosProjetoPorID
        {
            get
            {
                return (List<int?>)ViewState["ListaRecursosProjetoPorID"];
            }
            set
            {
                ViewState["ListaRecursosProjetoPorID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtNomeGerente.Text = Master.UsuarioAtual.NomeUsuario + " " + Master.UsuarioAtual.SobrenomeUsuario;
                LoadCombos();
                IDProjeto = Convert.ToInt32(Request.QueryString["IdProjeto"]);
                NumeroFuncionariosAvaliados = 1; //Por causa do Gerente, que entra no combo, preciso setar para 1.
                NumeroFuncionariosEquipe = 0;
                if (IDProjeto > 0)
                {
                    LoadRecursosProjeto(IDProjeto);
                }
            }
        }

        protected void ddlProjeto_TextChanged(object sender, EventArgs e)
        {
            LimparDadosCompetencias();
            LoadRecursosProjeto();
        }

        private void LoadRecursosProjeto()
        {
            if (ddlProjeto.SelectedItem.Value != null && !String.IsNullOrEmpty(ddlProjeto.SelectedItem.Value))
            {
                IDProjeto = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
                try
                {
                    fachada = new Fachada();
                    ListaRecursosProjeto = fachada.LoadRHProjetoDDL((int) IDProjeto);
                    NumeroFuncionariosEquipe = ListaRecursosProjeto.Count;
                    ListaRecursosProjeto.Insert(0, new DropDownItem(null, ""));
                    ddlNomeFuncionario.DataSource = ListaRecursosProjeto;
                    ddlNomeFuncionario.DataBind();
                    lblNomeFuncionario.Visible = true;
                    ddlNomeFuncionario.Visible = true;
                    //btnSalvar.Visible = true;
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
                }
            }
            else
            {
                ddlNomeFuncionario.DataSource = null;
                ddlNomeFuncionario.DataBind();
                lblNomeFuncionario.Visible = false;
                ddlNomeFuncionario.Visible = false;
                LimparDadosPropriedades();
            }
        }

        private void LimparDadosPropriedades()
        {
            IDProjeto = null;
            IDFuncionario = null;
            ListaCompetenciasComNivel = null;
            ListaRecursosProjeto = null;
            NumeroFuncionariosAvaliados = 1;
            NumeroFuncionariosEquipe = 0;
            ListaRecursosProjetoPorID = null;
        }

        private void LoadRecursosProjeto(int? idProjeto)
        {
            try
            {
                fachada = new Fachada();
                ddlProjeto.ClearSelection();
                ddlProjeto.Items.FindByValue(idProjeto.ToString()).Selected = true;
                ListaRecursosProjeto = fachada.LoadRHProjetoDDL((int) idProjeto);
                NumeroFuncionariosEquipe = ListaRecursosProjeto.Count;
                ListaRecursosProjeto.Insert(0, new DropDownItem(null, ""));
                ddlNomeFuncionario.DataSource = ListaRecursosProjeto;
                ddlNomeFuncionario.DataBind();
                lblNomeFuncionario.Visible = true;
                ddlNomeFuncionario.Visible = true;
                //btnSalvar.Visible = true;
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
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
            else
            {
                LimparDadosCompetencias();
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
            int? idGerente = Master.UsuarioAtual.UsuarioID;
            if (idGerente != null)
            {
                fachada = new Fachada();
                try
                {
                    if (idGerente != null)
                    {
                        List<DropDownItem> listaProjetos = fachada.LoadProjetosNaoAvaliadosGerente(idGerente);
                        listaProjetos.Insert(0, new DropDownItem(null, ""));
                        ddlProjeto.DataSource = listaProjetos;
                        ddlProjeto.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
                }
            }
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

                ListaCompetenciasComNivel = fachada.LoadCompetenciasProjetoComNiveis(IDFuncionario, IDProjeto);

                if (ListaCompetenciasComNivel.Count > 0)
                {
                    grvGridCompetencias.DataSource = ListaCompetenciasComNivel;
                    grvGridCompetencias.DataBind();
                    btnSalvar.Visible = true;
                }
                else
                {
                    LimparDadosCompetencias();
                }

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void LimparDadosCompetencias()
        {
            grvGridCompetencias.DataSource = null;
            grvGridCompetencias.DataBind();
            btnSalvar.Visible = false;
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
                if (ListaCompetenciasComNivel != null && ListaCompetenciasComNivel.Count > 0)
                {
                    AlterarNivel();
                }
                else
                {
                    Master.OpenWarningModal("Não há competências a serem alteradas!");
                }
                
            }
        }

        private void AlterarNivel()
        {
            fachada = new Fachada();
            try
            {
                int? idGerente = Master.UsuarioAtual.UsuarioID;
                bool competenciasAlteradas = fachada.AlterarCompetencias(idGerente, IDFuncionario, ListaCompetenciasComNivel);
                if (ListaRecursosProjetoPorID == null)
                {
                    ListaRecursosProjetoPorID = new List<int?>();
                }
                if (!ListaRecursosProjetoPorID.Contains(IDFuncionario))
                {
                    NumeroFuncionariosAvaliados = NumeroFuncionariosAvaliados + 1;
                    ListaRecursosProjetoPorID.Add(IDFuncionario);
                }
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

        protected void btnFinalizarAvaliacao_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                ConfirmarFinalizacaoAvaliacao();
            }
        }

        private void ConfirmarFinalizacaoAvaliacao()
        {
            if (NumeroFuncionariosAvaliados < NumeroFuncionariosEquipe)
            {
                AbrirConfirmarPopup();
            }
            else
            {
                FinalizarAvaliacao();
            }
        }

        private void AbrirConfirmarPopup()
        {
            fachada = new Fachada();
            string nomes = "";
            int idGerente = (int) Master.UsuarioAtual.UsuarioID;
            List<ProjetoUsuarioDTO> listaRecursos = fachada.LoadRHProjeto((int) IDProjeto);
            if (ListaRecursosProjetoPorID == null)
            {
                ListaRecursosProjetoPorID = new List<int?>();
            }
            foreach (ProjetoUsuarioDTO dto in listaRecursos) {
                if (!ListaRecursosProjetoPorID.Contains(dto.IDFuncionario) && dto.IDFuncionario != idGerente)
                {
                    nomes = nomes + String.Concat(dto.Nome + " " + dto.Sobrenome + ", ");
                }
            }
            nomes = nomes.Substring(0, nomes.Length - 2);
            lblConfirmaFinalizarMsg.Text = "Você ainda não avaliou: " + nomes + ". Deseja finalizar assim mesmo?";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirConfirmarPopup", "ShowConfirmarModal();", true);
        }

        protected void btnConfirmaFinalizar_Click(object sender, EventArgs e)
        {
            FinalizarAvaliacao();
        }

        private void FinalizarAvaliacao()
        {
            fachada = new Fachada();
            try
            {
                int? idGerente = Master.UsuarioAtual.UsuarioID;
                bool projetoAvaliado = fachada.MarcarProjetoAvaliado(IDProjeto);

                if (projetoAvaliado)
                {
                    // abre a popup de sucesso.
                    Master.LoadProjetosTerminados();
                    //Response.Redirect("~/AvaliarEquipe.aspx");
                    Master.OpenSuccesModal("Equipe avaliada com Sucesso!");
                    LimparDadosCompetencias();
                    LimparDadosPropriedades();
                    ddlProjeto.Text = String.Empty;
                    ddlNomeFuncionario.Visible = false;
                    lblNomeFuncionario.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }
	}
}