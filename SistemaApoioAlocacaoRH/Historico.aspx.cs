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
    public partial class Historico : System.Web.UI.Page
    {
        private Fachada fachada;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(!String.IsNullOrEmpty(Request.QueryString["IdCompetencia"]) && !String.IsNullOrEmpty(Request.QueryString["NomeCompetencia"]))
                {
                    int idCompetencia = Convert.ToInt32(Request.QueryString["IdCompetencia"]);
                    string nomeCompetencia = Request.QueryString["NomeCompetencia"];
                    lblLegend.Text = "Visualize a evolução da competência " + nomeCompetencia;

                    int idFuncionario;
                    if (String.IsNullOrEmpty(Request.QueryString["IdFuncionario"]))
                    {
                        idFuncionario = Convert.ToInt32(Master.UsuarioAtual.UsuarioID);
                    }
                    else
                    {
                        idFuncionario = Convert.ToInt32(Request.QueryString["IdFuncionario"]);
                    }
                    CarregarHistoricoComp(idCompetencia, nomeCompetencia, idFuncionario);
                }                
            }
        }

        private void CarregarHistoricoComp(int idCompetencia, string nomeCompetencia, int idFuncionario)
        {
            fachada = new Fachada();
            try
            {
                List<CompetenciaNivelDTO> listaCompetencias = fachada.CarregarHistoricoComp(idCompetencia, idFuncionario);
                if (listaCompetencias.Count > 0)
                {
                    string script = "Morris.Bar({{element: 'divGrafico', data: [{0}], xkey: 'y', ykeys: ['a'], labels: ['{1}'], hideHover: true, resize: true}});";
                    string item = "{{ y: '{0}', a: {1}}},";
                    string dados = String.Empty;

                    foreach (CompetenciaNivelDTO compNivel in listaCompetencias)
                    {
                        string data = Convert.ToString(compNivel.CompAvalData.ToShortDateString(), Master.Cultura);
                        dados = dados + String.Format(item, data, compNivel.CompNivel);
                    }

                    string scriptFinal = String.Format(script, dados, nomeCompetencia);
                    ScriptManager.RegisterStartupScript(this, GetType(), "PlotarGrafico", "$(function() {" + scriptFinal + " });", true);
                }
            }
            catch (Exception ex)
            {
                Master.OpenErrorModal(ex.Message);
            }
        }
    }
}