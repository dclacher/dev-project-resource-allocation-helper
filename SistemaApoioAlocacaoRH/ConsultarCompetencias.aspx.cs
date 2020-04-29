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
    public partial class ConsultarCompetencias : System.Web.UI.Page
    {
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LoadCombos();
                CarregarCompetencias();
            }
        }

        protected void ddlTipoCompetencia_TextChanged(object sender, EventArgs e)
        {
            CarregarCompetencias();
        }

        private void LoadCombos()
        {
            fachada = new Fachada();
            try
            {
                List<DropDownItem> listaTipoCompetencia = fachada.LoadTipoCompetencia();
                listaTipoCompetencia.Insert(0, new DropDownItem(null, ""));
                ddlTipoCompetencia.DataSource = listaTipoCompetencia;
                ddlTipoCompetencia.DataBind();
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }

        private void CarregarCompetencias()
        {
            fachada = new Fachada();
            try
            {
                int? tipo = null;
                if(!String.IsNullOrEmpty(ddlTipoCompetencia.SelectedItem.Value))
                {
                    tipo = Convert.ToInt32(ddlTipoCompetencia.SelectedItem.Value);
                }
                
                
                List<CompetenciaNivelDTO> listaCompetencia = fachada.CarregarCompetencias(tipo);

                if(listaCompetencia.Count > 0)
                {
                    grvCompetencias.DataSource = listaCompetencia;
                    grvCompetencias.DataBind();
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);                
            }
        }

        
    }
}