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
    public partial class BuscarEquipe : System.Web.UI.Page
    {
        private List<CompetenciaNivelDTO> ListaCompetenciaProjNivel
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

        private List<UsuarioDTO> ListaUsuariosDTO
        {
            get
            {
                return (List<UsuarioDTO>)ViewState["ListaUsuariosDTO"];
            }
            set
            {
                ViewState["ListaUsuariosDTO"] = value;
            }
        }

        private List<UsuarioDTO> ListaUsuariosSelecionados
        {
            get
            {
                return (List <UsuarioDTO>) ViewState["ListaUsuariosSelecionados"];
            }
            set
            {
                ViewState["ListaUsuariosSelecionados"] = value;
            }
        }
        
        private DateTime DataInicioProjeto
        {
            get
            {
                return (DateTime)ViewState["DataInicioProjeto"];
            }
            set
            {
                ViewState["DataInicioProjeto"] = value;
            }
        }

        private DateTime DataTerminoProjeto
        {
            get
            {
                return (DateTime)ViewState["DataTerminoProjeto"];
            }
            set
            {
                ViewState["DataTerminoProjeto"] = value;
            }
        }
        
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                txtNomeGerente.Text = Master.UsuarioAtual.NomeUsuario + " " + Master.UsuarioAtual.SobrenomeUsuario;
                lblCompetências.Visible = false;
                LoadCombos();
            }
        }

        protected void ddlProjeto_TextChanged(object sender, EventArgs e)
        {
            LoadCompetenciasProjeto();
        }        

        protected void grvCompetenciasProj_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AjustarNivel"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] {'~'});
                int compID = Convert.ToInt32(args[0]);
                string compNome = args[1];
                AbrirPopup(compID, compNome);
            }
        }

        protected void grvResultadoPesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("MostrarCompetencias"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int idUsuario = Convert.ToInt32(args[0]);
                string nomeCompleto = args[1];
                MostrarCompetenciasRecurso(idUsuario, nomeCompleto);
            }
        }        

        protected void grvResultadoPesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                if(!String.IsNullOrEmpty(txtNumeroRecursos.Text))
                {
                    int NumeroRecursos = Convert.ToInt32(txtNumeroRecursos.Text);
                    
                    if(e.Row.DataItemIndex <= NumeroRecursos - 1)
                    {
                        LinkButton linkMarca = e.Row.FindControl("lnkMarca") as LinkButton;
                        linkMarca.ToolTip = "Recurso recomendado";
                        HtmlGenericControl iconMarca = e.Row.FindControl("marcaIco") as HtmlGenericControl;
                        iconMarca.Attributes["class"] = "fa fa-fw fa-thumbs-up text-success";
                    }                   
                }               
            }
        }

        protected void grvEquipeSelecionada_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AjustarDatas"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '~' });
                int idUsuario = Convert.ToInt32(args[0]);
                string nomeCompleto = args[1];
                DateTime dataInicio = Convert.ToDateTime(args[2], Master.Cultura);
                DateTime dataTermino = Convert.ToDateTime(args[3], Master.Cultura);
                AjustarDatasRecurso(idUsuario, nomeCompleto, dataInicio, dataTermino);
            }
        }

        //protected void grvEquipeSelecionada_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if(e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DateTime dataInicioProj = ProjetoSelecionado.proj_data_inicio;
        //        DateTime dataTerminoProj = ProjetoSelecionado.proj_data_termino;

        //        e.Row.Cells[5].Text = dataInicioProj.ToString();
        //        e.Row.Cells[6].Text = dataTerminoProj.ToString();
        //    }
        //}      

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            VoltarConfigPesquisa();
        }

        protected void btnSelecionarEquipe_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
              SelecionarEquipe();
            }
            
        }
                
        protected void custNivelComp_ServerValidate(object source, ServerValidateEventArgs args)
        {            
            if(ListaCompetenciaProjNivel != null && ListaCompetenciaProjNivel.Count > 0)
            {
                args.IsValid = true;
                foreach (CompetenciaNivelDTO compNivel in ListaCompetenciaProjNivel)
                {
                    if (compNivel.CompNivel == 0)
                    {
                        args.IsValid = false;
                        break;
                    }
                }
            }            
        }

        protected void custRecursos_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int numeroRecursos = Convert.ToInt32(txtNumeroRecursos.Text);
            int count = 0;
            foreach (GridViewRow row in grvResultadoPesquisa.Rows)
            {
                CheckBox chkSelecionado = row.FindControl("chkSelecionarRecurso") as CheckBox;
                if (chkSelecionado.Checked)
                {
                    count += 1;
                }
            }
            if(count != numeroRecursos)
            {
                custRecursos.ErrorMessage = String.Format("É necessário selecionar {0} recursos para montar uma equipe!", numeroRecursos);
                args.IsValid = false;
            }

        }

        protected void btnAjustarNivel_Click(object sender, EventArgs e)
        {
            AjustarNivel();
        }

        protected void btnAvançar_Click(object sender, EventArgs e)
        {
            Page.Validate(); // colocar o grupo como parâmetro!
            if(Page.IsValid)
            {
               PesquisarRecursos();
            }            
        }

        protected void radMelhores_CheckedChanged(object sender, EventArgs e)
        {
            PesquisarRecursos();
        }

        protected void radMelhorAjuste_CheckedChanged(object sender, EventArgs e)
        {
            PesquisarRecursos();
        }

        protected void btnVoltarResultadoPesquisa_Click(object sender, EventArgs e)
        {
            VoltarResultadoPesquisa();
        }

        protected void btnConfirmarEquipe_Click(object sender, EventArgs e)
        {
            ConfirmaEquipe();
        }

        protected void btnAjustarData_Click(object sender, EventArgs e)
        {
            AjustarDatas();
        }

        protected void btnAtualizarEquipe_Click(object sender, EventArgs e)
        {
            AtualizarEquipe();
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
                        List<DropDownItem> listaProjetos = fachada.LoadProjetosGerente(idGerente);
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

        private void LoadCompetenciasProjeto()
        {
            if (ddlProjeto.SelectedItem.Value != null && !String.IsNullOrEmpty(ddlProjeto.SelectedItem.Value))
            {
                int idProjeto = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
                fachada = new Fachada();
                try
                {
                    ListaCompetenciaProjNivel = fachada.LoadCompetenciasProjeto(idProjeto);

                    if(ListaCompetenciaProjNivel.Count > 0)
                    {
                        grvCompetenciasProj.DataSource = ListaCompetenciaProjNivel;
                        grvCompetenciasProj.DataBind();
                        lblCompetências.Visible = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
                }
            }       
        }

        private void AbrirPopup(int compID, string compNome)
        {
            LimparDadosPopup();
            txtNomeComp.Text = compNome;
            hdnCompID.Value = compID.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirPopup", "ShowModal();", true);
        }     

        private void AjustarNivel()
        {
            if (!String.IsNullOrEmpty(txtNivelValor.Text))
            {
                int nivel = Convert.ToInt32(txtNivelValor.Text);
                int compId = Convert.ToInt32(hdnCompID.Value);

                foreach (CompetenciaNivelDTO compNivel in ListaCompetenciaProjNivel)
                {
                    if (compNivel.CompID == compId)
                    {
                        compNivel.CompNivel = nivel;
                        break;
                    }
                }

                grvCompetenciasProj.DataSource = ListaCompetenciaProjNivel;
                grvCompetenciasProj.DataBind();

            }
            else
            {
                Master.OpenWarningModal("Nível não pode ser vazio!");
            }
        }

        private void MostrarCompetenciasRecurso(int idUsuario, string nomeCompleto)
        {            
            foreach (UsuarioDTO usuario in ListaUsuariosDTO)
            {
                if(usuario.UsuarioID == idUsuario)
                {
                    lblNomeRecurso.Text = nomeCompleto;
                    grvCompetenciaRecurso.DataSource = usuario.ListaCompetenciaNivel;
                    grvCompetenciaRecurso.DataBind();
                    AbrirCompetenciaRecursoPopup();
                }
            }
        }

        private void AbrirCompetenciaRecursoPopup()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirCompetenciaRecursoPopup", "ShowCompetenciaModal();", true);
        }

        private void AjustarDatas()
        {
            if(!String.IsNullOrEmpty(txtDataInicio.Text) && !String.IsNullOrEmpty(txtDataTermino.Text))
            {
                DateTime dataInicio = Convert.ToDateTime(txtDataInicio.Text, Master.Cultura);
                DateTime dataTermino = Convert.ToDateTime(txtDataTermino.Text, Master.Cultura);

                if(dataInicio < DataInicioProjeto)
                {
                    Master.OpenWarningModal("Data de início no projeto do recurso não pode ser menor que a data de início do projeto");
                }
                else if(dataTermino > DataTerminoProjeto)
                {
                    Master.OpenWarningModal("Data de término no projeto do recurso não pode ser maior que a data de término do projeto");
                }
                else if(dataInicio > dataTermino)
                {
                    Master.OpenWarningModal("Data de início no projeto do recurso não pode ser maior que a data de término");
                }
                else
                {
                    int idUsuario = Convert.ToInt32(hdnRecursoId.Value);
                    foreach (UsuarioDTO usuario in ListaUsuariosSelecionados)
                    {
                        if(usuario.UsuarioID == idUsuario)
                        {
                            usuario.DataInicio = dataInicio;
                            usuario.DataTermino = dataTermino;
                            break;
                        }
                    }

                    grvEquipeSelecionada.DataSource = ListaUsuariosSelecionados;
                    grvEquipeSelecionada.DataBind();
                }
            }
            else
            {
                Master.OpenWarningModal("Data de início ou data de término não podem ser vazias!");
            }
        }

        private void AjustarDatasRecurso(int idUsuario, string nomeCompleto, DateTime dataInicio, DateTime dataTermino)
        {
            foreach (UsuarioDTO usuario in ListaUsuariosSelecionados)
            {
                if(usuario.UsuarioID == idUsuario)
                {
                    LimparDadosPopupDatas();
                    lblNomeRecursoDatas.Text = nomeCompleto;
                    hdnRecursoId.Value = idUsuario.ToString();
                    txtDataInicio.Text = Convert.ToString(dataInicio.ToShortDateString(), Master.Cultura);
                    txtDataTermino.Text = Convert.ToString(dataTermino.ToShortDateString(), Master.Cultura);
                    AbrirAjusteDatasPopup();
                }
            }
        }

        private void AbrirAjusteDatasPopup()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirAjusteDatasPopup", "ShowDatasModal();", true);
        }

        private void PesquisarRecursos()
        {
            fachada = new Fachada();
            bool calculoMelhores = radMelhores.Checked;            
            try
            {
                List<UsuarioDTO> listaUsuarios = fachada.PesquisarRecursos(ListaCompetenciaProjNivel, calculoMelhores);

                if(listaUsuarios.Count > 0)
                {
                    ListaUsuariosDTO = listaUsuarios;
                    grvResultadoPesquisa.DataSource = listaUsuarios;
                    grvResultadoPesquisa.DataBind();

                    lblLegend.Text = "Resultado da Pesquisa";
                    divConfiguraPesquisa.Visible = false;
                    divResultadoPesquisa.Visible = true;
                }
                else
                {
                    Master.OpenWarningModal("Não foi encontrado nenhum recurso com pelos menos uma das competências do exigidas pelo projeto!");
                }
                
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void SelecionarEquipe()
        {
            List<UsuarioDTO> listaUsuariosSelecionados = new List<UsuarioDTO>();
            int index = 0;
            foreach (GridViewRow row in grvResultadoPesquisa.Rows)
            {
                CheckBox chkSelecionado = row.FindControl("chkSelecionarRecurso") as CheckBox;
                if (chkSelecionado.Checked)
                {
                    UsuarioDTO usuarioselecionado = new UsuarioDTO();
                    // colunas invisiveis no grid não guardam valores pela propriedade row.Cell, portanto foi utilizada para recuperar o Id do usuário os DataKeys.
                    usuarioselecionado.UsuarioID = Convert.ToInt32(grvResultadoPesquisa.DataKeys[index].Value);
                    usuarioselecionado.NomeUsuario = row.Cells[1].Text;
                    usuarioselecionado.SobrenomeUsuario = row.Cells[2].Text;
                    usuarioselecionado.Email = row.Cells[3].Text;
                    usuarioselecionado.Alocado = Convert.ToBoolean(row.Cells[4].Text);
                    listaUsuariosSelecionados.Add(usuarioselecionado);
                }
                index += 1;
            }

            // validação adicional, pois já foi validado no evento.
            int NumeroRecursos = Convert.ToInt32(txtNumeroRecursos.Text);
            if (listaUsuariosSelecionados.Count == NumeroRecursos)
            {
                fachada = new Fachada();
                int projetoID = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
                
                if(projetoID > 0)
                {
                    Persistencia.Projeto projetoSelecionado = fachada.CarregarProjeto(projetoID);
                    ListaUsuariosSelecionados = listaUsuariosSelecionados;

                    foreach (UsuarioDTO usuario in ListaUsuariosSelecionados)
                    {
                        usuario.DataInicio = projetoSelecionado.proj_data_inicio;
                        usuario.DataTermino = projetoSelecionado.proj_data_termino;
                    }

                    DataInicioProjeto = projetoSelecionado.proj_data_inicio;
                    DataTerminoProjeto = projetoSelecionado.proj_data_termino;

                    grvEquipeSelecionada.DataSource = ListaUsuariosSelecionados;
                    grvEquipeSelecionada.DataBind();

                    lblLegend.Text = "Confirmar Equipe";
                    string nomeProjeto = ddlProjeto.SelectedItem.Text;
                    lblEquipeSelecionada.Text = String.Format("Abaixo está a equipe selecionada. Você deseja confirmar esta equipe para o projeto {0} ?", nomeProjeto);
                    divResultadoPesquisa.Visible = false;
                    divSelecionados.Visible = true;
                }
                else
                {
                    Master.OpenWarningModal("É preciso ter um projeto selecionado!");
                }         
            }
            
        }

        private void ConfirmaEquipe()
        {
            fachada = new Fachada();
            try
            {
                // primeiro valida se existe algum recurso já alocado para este projeto.
                int quantidadeMembros = BuscarMembrosEquipe();
                if (quantidadeMembros > 0)
                {
                    // abre uma popup de confirmação para atualizar a equipe.
                    AbrirConfirmarPopup();
                }
                else
                {
                    // salva no banco a nova equipe.
                    int projetoID = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
                    // verifica se o usuário logado é um gerente para adicioná-lo ao projeto, é uma validação adicional
                    if (projetoID > 0)
                    {
                        if (Master.UsuarioAtual.TipoUsuario == 1)
                        {
                            UsuarioDTO gerente = Master.UsuarioAtual;
                            gerente.DataInicio = DataInicioProjeto;
                            gerente.DataTermino = DataTerminoProjeto;
                            bool equipeSalva = fachada.SalvarEquipe(projetoID, gerente, ListaUsuariosSelecionados);

                            if (equipeSalva)
                            {
                                Master.OpenSuccesModal("Equipe salva com sucesso!");
                                LimparDadosPesquisa();
                                VoltarConfigPesquisa();
                            }
                        }                  
                    }                       
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void AtualizarEquipe()
        {
            fachada = new Fachada();
            try
            {
                int projetoID = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
                if (projetoID > 0)
                {
                    bool equipeRemovida = fachada.RemoverEquipe(projetoID);
                    if (equipeRemovida)
                    {
                        if (Master.UsuarioAtual.TipoUsuario == 1)
                        {
                            UsuarioDTO gerente = Master.UsuarioAtual;
                            gerente.DataInicio = DataInicioProjeto;
                            gerente.DataTermino = DataTerminoProjeto;
                            bool equipeSalva = fachada.SalvarEquipe(projetoID, gerente, ListaUsuariosSelecionados);

                            if (equipeSalva)
                            {
                                Master.OpenSuccesModal("Equipe atualizada com sucesso!");
                                LimparDadosPesquisa();
                                VoltarConfigPesquisa();
                            }
                        }                  
                    }
                }

            }
            catch (Exception ex)
            {
               Master.OpenErrorModal(ex.Message);
            }
        }

        private int BuscarMembrosEquipe()
        {
            fachada = new Fachada();
            int projetoID = Convert.ToInt32(ddlProjeto.SelectedItem.Value);
            int quantidadeMembros = 0;
            if (projetoID > 0)
            {
                try
                {
                    quantidadeMembros = fachada.BuscarMembrosEquipe(projetoID);                    
                }
                catch (Exception ex)
                {
                    Master.OpenErrorModal(ex.Message);
                }
            }
            else
            {
                Master.OpenWarningModal("É preciso ter um projeto selecionado!");
            }

            return quantidadeMembros;
                       
        }

        private void AbrirConfirmarPopup()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirConfirmarPopup", "ShowConfirmarModal();", true);
        }

        private void VoltarConfigPesquisa()
        {
            lblLegend.Text = "Configure a Pesquisa";
            divResultadoPesquisa.Visible = false;
            divConfiguraPesquisa.Visible = true;
            divSelecionados.Visible = false;
        }

        private void VoltarResultadoPesquisa()
        {
            lblLegend.Text = "Resultado da Pesquisa";
            divSelecionados.Visible = false;
            divConfiguraPesquisa.Visible = false;
            divResultadoPesquisa.Visible = true;            
        }

        private void LimparDadosPopup()
        {
            txtNomeComp.Text = String.Empty;
            txtNivelValor.Text = String.Empty;
            hdnCompID.Value = String.Empty;
        }
      
        private void LimparDadosPopupDatas()
        {
            txtDataInicio.Text = String.Empty;
            txtDataTermino.Text = String.Empty;
            hdnRecursoId.Value = String.Empty;
        }

        private void LimparDadosPesquisa()
        {
            ddlProjeto.Text = String.Empty;
            txtNumeroRecursos.Text = String.Empty;
            grvCompetenciasProj.DataSource = null;
            grvCompetenciasProj.DataBind();
            ListaCompetenciaProjNivel = null;
            ListaUsuariosDTO = null;
            ListaUsuariosSelecionados = null;
            DataInicioProjeto = DateTime.MinValue;
            DataTerminoProjeto = DateTime.MaxValue;
        }

       
    }
}