using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Fachada
    {
        private Usuario usuario;
        private Projeto projeto;
        private Conhecimento conhecimento;
        private Habilidade habilidade;
        private Atitude atitude;
        private Avaliacao avaliacao;
        private Equipe equipe;
        private TipoCompetencia tipoCompetencia;
        public Fachada()
        {
            
        }

        #region Usuario

        public List<ConsultarRHGridDTO> LoadRecursosHumanos(List<int?> listacompetencias)
        {
            usuario = new Usuario();
            return usuario.LoadRecursosHumanos(listacompetencias);
        }

        public UsuarioDTO GetUsuario(string nomeUsuario, string senha)
        {

            usuario = new Usuario();
            return usuario.GetUsuario(nomeUsuario, senha);
        }

        public bool CadastrarUsuario(string usuNome, string nome, string sobrenome, int tipo, string email, string senha)
        {
            usuario = new Usuario();
            return usuario.CadastrarUsuario(usuNome, nome, sobrenome, tipo, email, senha);
        }

        public List<DropDownItem> LoadTipoUsuario()
        {
            usuario = new Usuario();
            return usuario.LoadTipoUsuario();
        }

        public List<DropDownItem> LoadGerentes()
        {
            usuario = new Usuario();
            return usuario.LoadGerentes();
        }

        public List<DropDownItem> LoadFuncionarios()
        {
            usuario = new Usuario();
            return usuario.LoadFuncionarios();
        }

        public List<UsuarioDTO> PesquisarRecursos(List<CompetenciaNivelDTO> listaCompetenciaProj, bool calculoMelhores)
        {
            usuario = new Usuario();
            return usuario.PesquisarRecursos(listaCompetenciaProj, calculoMelhores);
        }

        #endregion

        #region TipoCompetencia

        public List<DropDownItem> LoadTipoCompetencia()
        {
            tipoCompetencia = new TipoCompetencia();
            return tipoCompetencia.LoadTipoCompetencia();
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjeto(int idProjeto)
        {
            tipoCompetencia = new TipoCompetencia();
            return tipoCompetencia.LoadCompetenciasProjeto(idProjeto);
        }
        
        //verificar se esse metodo não está duplicado.
        public List<CompetenciaNivelDTO> LoadCompetenciasFuncionario(int? idFuncionario)
        {
            tipoCompetencia = new TipoCompetencia();
            return tipoCompetencia.LoadCompetenciasFuncionario(idFuncionario);
        }

        public List<CompetenciaNivelDTO> CarregarCompetencias(int? tipo)
        {
            tipoCompetencia = new TipoCompetencia();
            return tipoCompetencia.CarregarCompetencias(tipo);
        }

        #endregion

        #region Conhecimento

        public List<DropDownItem> LoadConhecimentos()
        {
            conhecimento = new Conhecimento();
            return conhecimento.LoadConhecimentos();           
        }

        public bool CadastrarConhecimento(string coNome, string coDesc)
        {
            conhecimento = new Conhecimento();
            return conhecimento.CadastrarConhecimento(coNome, coDesc);
        }

        #endregion

        #region Habilidade

        public List<DropDownItem> LoadHabilidades()
        {
            habilidade = new Habilidade();
            return habilidade.LoadHabilidades();
        }

        public bool CadastrarHabilidade(string habNome, string habDesc)
        {
            habilidade = new Habilidade();
            return habilidade.CadastrarHabilidade(habNome, habDesc);
        }

        #endregion

        #region Atitude

        public List<DropDownItem> LoadAtitudes()
        {
            atitude = new Atitude();
            return atitude.LoadAtitudes();            
        }

        public bool CadastrarAtitude(string atNome, string atDesc)
        {
            atitude = new Atitude();
            return atitude.CadastrarAtitude(atNome, atDesc);
        }

        #endregion

        #region Projeto

        public bool CadastrarProjeto(int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            projeto = new Projeto();
            return projeto.CadastrarProjeto(idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

        public List<Persistencia.Projeto> LoadProjetos(string nome, int? idGerente, string status, string prioridade,
            DateTime? dataInicio, DateTime? dataTermino)
        {
            projeto = new Projeto();
            return projeto.LoadProjetos(nome, idGerente, status, prioridade, dataInicio, dataTermino);
        }

        public List<ProjetoUsuarioDTO> LoadRHProjeto(int idProjeto)
        {
            projeto = new Projeto();
            return projeto.LoadRHProjeto(idProjeto);
        }

        public List<DropDownItem> LoadRHProjetoDDL(int idProjeto)
        {
            projeto = new Projeto();
            return projeto.LoadRHProjetoDDL(idProjeto);
        }

        public Persistencia.Projeto CarregarProjeto(int idProjeto)
        {
            projeto = new Projeto();
            return projeto.CarregarProjeto(idProjeto);
        }

        public bool AlterarProjeto(int idProjeto, int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            projeto = new Projeto();
            return projeto.AlterarProjeto(idProjeto, idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

        public List<DropDownItem> LoadProjetosGerente(int? idGerente)
        {
            projeto = new Projeto();
            return projeto.LoadProjetosGerente(idGerente);
        }

        public List<DropDownItem> LoadProjetosNaoAvaliadosGerente(int? idGerente)
        {
            projeto = new Projeto();
            return projeto.LoadProjetosNaoAvaliadosGerente(idGerente);
        }

        public List<Persistencia.Projeto> LoadProjetosTerminados(int? idGerente)
        {
            projeto = new Projeto();
            return projeto.LoadProjetosTerminados(idGerente);
        }

        public bool MarcarProjetoAvaliado(int? idProjeto)
        {
            projeto = new Projeto();
            return projeto.MarcarProjetoAvaliado(idProjeto);
        }

        #endregion

        #region Avaliacao

        public List<CompetenciaNivelDTO> LoadCompetenciasComNiveis(int? idFuncionario)
        {
            avaliacao = new Avaliacao();
            return avaliacao.LoadCompetenciasComNiveis(idFuncionario);
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjetoComNiveis(int? idFuncionario, int? idProjeto)
        {
            avaliacao = new Avaliacao();
            return avaliacao.LoadCompetenciasProjetoComNiveis(idFuncionario, idProjeto);
        }

        public bool AlterarCompetencias(int? idGerente, int? idFuncionario, List<CompetenciaNivelDTO> listaCompetenciasComNivel)
        {
            avaliacao = new Avaliacao();
            return avaliacao.AlterarCompetencias(idGerente, idFuncionario, listaCompetenciasComNivel);
        }

        public bool SalvarPerfil(int idFuncionario, List<CompetenciaNivelDTO> listaNovasCompetencias)
        {
            avaliacao = new Avaliacao();
            return avaliacao.SalvarPerfil(idFuncionario, listaNovasCompetencias);
        }

        public List<CompetenciaNivelDTO> CarregarHistoricoComp(int idCompetencia, int idFuncionario)
        {
            avaliacao = new Avaliacao();
            return avaliacao.CarregarHistoricoComp(idCompetencia, idFuncionario);
        }

        #endregion

        #region Equipe

        public int BuscarMembrosEquipe(int idProjeto)
        {
            equipe = new Equipe();
            return equipe.BuscarMembrosEquipe(idProjeto);
        }

        public bool SalvarEquipe(int idProjeto, UsuarioDTO gerenteProj, List<UsuarioDTO> listaUsuarios)
        {
            equipe = new Equipe();
            return equipe.SalvarEquipe(idProjeto, gerenteProj, listaUsuarios);
        }

        public bool RemoverEquipe(int idProjeto)
        {
            equipe = new Equipe();
            return equipe.RemoverEquipe(idProjeto);
        }

        #endregion
    }
}
