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
    public partial class ConsultarRecursos : System.Web.UI.Page
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
                ListaCompetencias = new List<DropDownItem>();
                LoadCombos();
            }
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

       
        protected void ddlConhecimento_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(ddlConhecimentos.SelectedItem.Text))
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
                else if(ListaCompetencias.Contains(item))
                {
                    Master.OpenWarningModal("A competência " +  desc + " já está cadastrada!");
                }
            }            
        }
               
        protected void ddlHabilidades_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(ddlHabilidades.SelectedItem.Text))
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LoadRecursosHumanos();
        }   

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparDados();
        } 
        
        
        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {                
                // criar o onRowDataBound do grid que vai colocar as colunas!
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
            catch(Exception ex)
            {
                Master.OpenErrorModal(ex.Message);                
            }
        }
      
        private void LimparDados()
        {
            grvSelecionados.DataSource = null;
            grvSelecionados.DataBind();
            ddlConhecimentos.Text = String.Empty;
            ddlHabilidades.Text = String.Empty;
            ddlAtitudes.Text = String.Empty;
            ListaCompetencias.Clear();

            for (int i = 3; i < 10; i++)
            {
                grvGridResultado.Columns[i].Visible = false;
                grvGridResultado.Columns[i].HeaderText = "";
            }

            grvGridResultado.DataSource = new List<ConsultarRHGridDTO>();
            grvGridResultado.DataBind();
        }

        private void LoadRecursosHumanos()
        {
            fachada = new Fachada();
            try
            {                
                if (ListaCompetencias.Count <= 10)
                {
                    List<int?> listaIdComp = new List<int?>();
                    foreach (DropDownItem item in ListaCompetencias)
                    {
                        if(item.Code != null)
                        {
                            listaIdComp.Add(item.Code);
                        }
                    }

                    List<ConsultarRHGridDTO> listaRH = fachada.LoadRecursosHumanos(listaIdComp);

                    if (listaRH.Count > 0)
                    {
                        for (int i = 0; i < ListaCompetencias.Count; i++)
                        {
                            grvGridResultado.Columns[i + 3].Visible = true;
                            grvGridResultado.Columns[i + 3].HeaderText = ListaCompetencias[i].Desc;
                        }

                        grvGridResultado.DataSource = listaRH;
                        grvGridResultado.DataBind();                        

                    }
                    else
                    {
                        // limpa o grid
                        grvGridResultado.DataSource = new List<ConsultarRHGridDTO>();
                        grvGridResultado.DataBind();
                        Master.OpenWarningModal("Não foram encontrados funcionários com este conjunto de competências.");
                    }                    

                }
                else
                {
                    Master.OpenErrorModal("Não é possível realizar uma busca com mais de 10 competências selecionadas como filtro.");
                }

            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }
            
    }
}