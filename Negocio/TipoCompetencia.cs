using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class TipoCompetencia
    {
        private Adaptador adaptador;

        public int? TipoCompetenciaID { get; set; }
        public string Nome { get; set; }

        public TipoCompetencia()
        {

        }

        public TipoCompetencia(int? tipoID, string nome)
        {
            this.TipoCompetenciaID = tipoID;
            this.Nome = nome;
        }

        public List<DropDownItem> LoadTipoCompetencia()
        {
            adaptador = new Adaptador();
            return adaptador.LoadTipoCompetencia();
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjeto(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.LoadCompetenciasProjeto(idProjeto);
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasFuncionario(int? idFuncionario)
        {
            adaptador = new Adaptador();
            return adaptador.LoadCompetenciasFuncionario(idFuncionario);
        }

        public List<CompetenciaNivelDTO> CarregarCompetencias(int? tipo)
        {
            adaptador = new Adaptador();
            return adaptador.CarregarCompetencias(tipo);
        }
    }
}
