using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Persistencia;
using Persistencia.DTO;
using System.Web.UI.HtmlControls;

namespace SistemaApoioAlocacaoRH
{
    public partial class Perfil : System.Web.UI.Page
    {

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

        private List<CompetenciaNivelDTO> ListaCompetenciaFuncionario
        {
            get
            {
                return (List<CompetenciaNivelDTO>)ViewState["ListaCompetenciaFuncionario"];
            }
            set
            {
                ViewState["ListaCompetenciaFuncionario"] = value;
            }
        }

        private List<CompetenciaNivelDTO> ListaCompetenciaOriginal
        {
            get
            {
                return (List<CompetenciaNivelDTO>)ViewState["ListaCompetenciaOriginal"];
            }
            set
            {
                ViewState["ListaCompetenciaOriginal"] = value;
            }
        }

        private List<CompetenciaNivelDTO> ListaNovasCompetencias
        {
            get
            {
                return (List<CompetenciaNivelDTO>)ViewState["ListaNovasCompetencias"];
            }
            set
            {
                ViewState["ListaNovasCompetencias"] = value;
            }
        }

        private int IndiceBloqueados
        {
            get
            {
                return (int)ViewState["IndiceBloqueados"];
            }
            set
            {
                ViewState["IndiceBloqueados"] = value;
            }
        }

        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fachada = new Fachada();
                IDFuncionario = Master.UsuarioAtual.UsuarioID;
                try
                {
                    List<CompetenciaNivelDTO> listaCompetenciasFuncionario = fachada.LoadCompetenciasFuncionario(IDFuncionario);
                    if (listaCompetenciasFuncionario.Count > 0)
                    {
                        Master.OpenWarningModal("Você já possui um perfil com competências! Para adicionar novas competências, clique sobre a opção Adicionar Competência no menu");
                        btnSalvar.Enabled = false;
                        btnLimparDados.Enabled = false;
                        ddlConhecimentos.Enabled = false;
                        ddlHabilidades.Enabled = false;
                        
                    }
                    ListaNovasCompetencias = new List<CompetenciaNivelDTO>();
                    LoadInfoPerfil();
                    LoadCombos();
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
                }
            }
        }

        protected void custCompFuncionario_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ListaCompetenciaFuncionario != null && ListaCompetenciaFuncionario.Count > 0)
            {
                args.IsValid = true;
                foreach (CompetenciaNivelDTO compNivel in ListaCompetenciaFuncionario)
                {
                    if (compNivel.CompNivel == 0)
                    {
                        args.IsValid = false;
                        break;
                    }
                }
            }
        }

        protected void ddlConhecimentos_TextChanged(object sender, EventArgs e)
        {
            SelecionarConhecimento();
        }

        protected void ddlHabilidades_TextChanged(object sender, EventArgs e)
        {
            SelecionarHabilidade();
        }

        protected void grvCompFuncionario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AjustarNivel"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int compID = Convert.ToInt32(args[0]);
                string compNome = args[1];
                AbrirPopupNivel(compID, compNome);
            }
            else if (e.CommandName.Equals("HistoricoComp"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int compID = Convert.ToInt32(args[0]);
                string compNome = args[1];
                Response.Redirect("~/Historico.aspx?IdCompetencia=" + compID + "&NomeCompetencia=" + compNome);
            }
        }

        protected void grvCompFuncionario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItemIndex <= IndiceBloqueados - 1)
                {
                    LinkButton linkPopupNivel = e.Row.FindControl("btnAjustarnivel") as LinkButton;
                    linkPopupNivel.ToolTip = "Essa competência só pode ser modificada pelo gerente";
                    linkPopupNivel.Enabled = false;
                    HtmlGenericControl iconNivel = e.Row.FindControl("iconNivel") as HtmlGenericControl;
                    iconNivel.Attributes["class"] = "fa fa-fw fa-lock";
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ListaNovasCompetencias.Count > 0)
                {
                    // abre a popup de confirmação
                    AbrirPopupConfirmacao();
                }
                else
                {
                    Master.OpenWarningModal("Você não adicionou nenhuma nova competência ao seu perfil!");
                }
            }
        }

        protected void btnConfirmarPerfil_Click(object sender, EventArgs e)
        {
            SalvarPerfil();
        }

        protected void btnAjustarNivel_Click(object sender, EventArgs e)
        {
            AjustarNivel();
        }

        protected void btnLimparDados_Click(object sender, EventArgs e)
        {
            LimparDados();
        }


        private void LoadInfoPerfil()
        {
            txtNomeFuncionario.Text = Master.UsuarioAtual.NomeUsuario + " " + Master.UsuarioAtual.SobrenomeUsuario;

            int? idFuncionario = Master.UsuarioAtual.UsuarioID;
            if (idFuncionario != null)
            {
                fachada = new Fachada();
                try
                {
                    List<CompetenciaNivelDTO> listaCompetenciasFuncionario = fachada.LoadCompetenciasFuncionario(idFuncionario);
                    ListaCompetenciaFuncionario = listaCompetenciasFuncionario;
                    ListaCompetenciaOriginal = listaCompetenciasFuncionario;
                    IndiceBloqueados = listaCompetenciasFuncionario.Count;
                    grvCompFuncionario.DataSource = listaCompetenciasFuncionario;
                    grvCompFuncionario.DataBind();
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
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
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void SalvarPerfil()
        {
            fachada = new Fachada();
            try
            {
                int idFuncionario = Convert.ToInt32(Master.UsuarioAtual.UsuarioID);

                if (idFuncionario > 0)
                {
                    if (ListaNovasCompetencias.Count > 0)
                    {
                        bool salvo = fachada.SalvarPerfil(idFuncionario, ListaNovasCompetencias);
                        if (salvo)
                        {
                            LoadInfoPerfil();
                            ListaNovasCompetencias = new List<CompetenciaNivelDTO>();
                            Master.OpenSuccesModal("Perfil salvo com sucesso!");
                        }
                    }
                    else
                    {
                        Master.OpenWarningModal("Você não adicionou nenhuma nova competência ao seu perfil!");
                    }
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void SelecionarConhecimento()
        {
            if (!String.IsNullOrEmpty(ddlConhecimentos.SelectedItem.Text))
            {
                string desc = ddlConhecimentos.SelectedItem.Text;
                int code = Convert.ToInt32(ddlConhecimentos.SelectedItem.Value);

                int count = 0;
                foreach (CompetenciaNivelDTO compDTO in ListaCompetenciaFuncionario)
                {
                    if (compDTO.CompID == code)
                    {
                        Master.OpenWarningModal("Este conhecimento já faz parte do seu perfil!");
                        break;
                    }
                    count++;
                }

                if (count == ListaCompetenciaFuncionario.Count)
                {
                    // chegou ao fim da lista e a competência selecionada não faz parte do perfil.
                    CompetenciaNivelDTO comp = new CompetenciaNivelDTO();
                    comp.CompID = code;
                    comp.CompNome = desc;
                    comp.CompNivel = 0;

                    ListaNovasCompetencias.Add(comp);
                    ListaCompetenciaFuncionario.Add(comp);
                    grvCompFuncionario.DataSource = ListaCompetenciaFuncionario;
                    grvCompFuncionario.DataBind();
                }
            }
        }

        private void SelecionarHabilidade()
        {
            if (!String.IsNullOrEmpty(ddlHabilidades.SelectedItem.Text))
            {
                string desc = ddlHabilidades.SelectedItem.Text;
                int code = Convert.ToInt32(ddlHabilidades.SelectedItem.Value);

                int count = 0;
                foreach (CompetenciaNivelDTO compDTO in ListaCompetenciaFuncionario)
                {
                    if (compDTO.CompID == code)
                    {
                        Master.OpenWarningModal("Esta habilidade já faz parte do seu perfil!");
                        break;
                    }
                    count++;
                }

                if (count == ListaCompetenciaFuncionario.Count)
                {
                    // chegou ao fim da lista e a competência selecionada não faz parte do perfil.
                    CompetenciaNivelDTO comp = new CompetenciaNivelDTO();
                    comp.CompID = code;
                    comp.CompNome = desc;
                    comp.CompNivel = 0;

                    ListaNovasCompetencias.Add(comp);
                    ListaCompetenciaFuncionario.Add(comp);
                    grvCompFuncionario.DataSource = ListaCompetenciaFuncionario;
                    grvCompFuncionario.DataBind();
                }
            }
        }

        private void AjustarNivel()
        {
            if (!String.IsNullOrEmpty(txtNivelValor.Text))
            {
                int nivel = Convert.ToInt32(txtNivelValor.Text);
                int compId = Convert.ToInt32(hdnCompID.Value);

                foreach (CompetenciaNivelDTO compNivel in ListaCompetenciaFuncionario)
                {
                    if (compNivel.CompID == compId)
                    {
                        compNivel.CompNivel = nivel;
                        break;
                    }
                }

                // atualiza também a lista de novas competências
                foreach (CompetenciaNivelDTO compNivel in ListaNovasCompetencias)
                {
                    if (compNivel.CompID == compId)
                    {
                        compNivel.CompNivel = nivel;
                        break;
                    }
                }

                grvCompFuncionario.DataSource = ListaCompetenciaFuncionario;
                grvCompFuncionario.DataBind();

            }
            else
            {
                Master.OpenWarningModal("Nível não pode ser vazio!");
            }
        }

        private void AbrirPopupConfirmacao()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirPopup", "ShowModalConfirmar();", true);
        }

        private void AbrirPopupNivel(int compID, string compNome)
        {
            LimparDadosPopupNível();
            txtNomeComp.Text = compNome;
            hdnCompID.Value = compID.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirPopup", "ShowModalNivel();", true);
        }

        private void LimparDadosPopupNível()
        {
            txtNomeComp.Text = String.Empty;
            txtNivelValor.Text = String.Empty;
            hdnCompID.Value = String.Empty;
        }

        private void LimparDados()
        {
            ddlConhecimentos.Text = String.Empty;
            ddlHabilidades.Text = String.Empty;

            ListaNovasCompetencias = new List<CompetenciaNivelDTO>();
            ListaCompetenciaFuncionario = ListaCompetenciaOriginal;

            grvCompFuncionario.DataSource = ListaCompetenciaOriginal;
            grvCompFuncionario.DataBind();

        }

    }
}