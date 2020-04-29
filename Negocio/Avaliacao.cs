using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    class Avaliacao
    {
        private Adaptador adaptador;
        
        public List<CompetenciaNivelDTO> LoadCompetenciasComNiveis(int? idFuncionario)
        {
            adaptador = new Adaptador();
            return adaptador.LoadCompetenciasComNiveis(idFuncionario);
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjetoComNiveis(int? idFuncionario, int? idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.LoadCompetenciasProjetoComNiveis(idFuncionario, idProjeto);
        }

        public bool AlterarCompetencias(int? idGerente, int? idFuncionario, List<CompetenciaNivelDTO> listaCompetenciasComNivel)
        {
            adaptador = new Adaptador();
            return adaptador.AlterarCompetencias(idGerente, idFuncionario, listaCompetenciasComNivel);
        }

        public bool SalvarPerfil(int idFuncionario, List<CompetenciaNivelDTO> listaNovasCompetencias)
        {
            adaptador = new Adaptador();
            return adaptador.SalvarPerfil(idFuncionario, listaNovasCompetencias);
        }

        public List<CompetenciaNivelDTO> CarregarHistoricoComp(int idCompetencia, int idFuncionario)
        {
            adaptador = new Adaptador();
            return adaptador.CarregarHistoricoComp(idCompetencia, idFuncionario);
        }
    }
}
