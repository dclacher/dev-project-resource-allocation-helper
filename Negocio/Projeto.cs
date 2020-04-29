using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Projeto
    {
        private Adaptador adaptador;

        public Projeto()
        {

        }

        public bool CadastrarProjeto(int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio, 
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            adaptador = new Adaptador();
            return adaptador.CadastrarProjeto(idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

        //Especificar exatamente que tipo de classe "projeto" é (da camada de Persistencia ou de Negócio)
        public List<Persistencia.Projeto> LoadProjetos(string nome, int? idGerente, string status, string prioridade,
            DateTime? dataInicio, DateTime? dataTermino)
        {
            adaptador = new Adaptador();
            return adaptador.LoadProjetos(nome, idGerente, status, prioridade, dataInicio, dataTermino);
        }

        public List<ProjetoUsuarioDTO> LoadRHProjeto(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.LoadRHProjeto(idProjeto);
        }

        public List<DropDownItem> LoadRHProjetoDDL(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.LoadRHProjetoDDL(idProjeto);
        }

        public Persistencia.Projeto CarregarProjeto(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.CarregarProjeto(idProjeto);
        }

        public bool AlterarProjeto(int idProjeto, int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
           DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            adaptador = new Adaptador();
            return adaptador.AlterarProjeto(idProjeto, idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

        public List<DropDownItem> LoadProjetosGerente(int? idGerente)
        {
            adaptador = new Adaptador();
            return adaptador.LoadProjetosGerente(idGerente);
        }

        public List<DropDownItem> LoadProjetosNaoAvaliadosGerente(int? idGerente)
        {
            adaptador = new Adaptador();
            return adaptador.LoadProjetosNaoAvaliadosGerente(idGerente);
        }

        public List<Persistencia.Projeto> LoadProjetosTerminados(int? idGerente)
        {
            adaptador = new Adaptador();
            return adaptador.LoadProjetosTerminados(idGerente);
        }

        internal bool MarcarProjetoAvaliado(int? idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.MarcarProjetoAvaliado(idProjeto);
        }
    }
}
