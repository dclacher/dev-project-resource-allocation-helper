using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    public class Adaptador
    {
        private ConhecimentoDAOSQL conhecimentoDAO;
        private HabilidadeDAOSQL habilidadeDAO;
        private AtitudeDAOSQL atitudeDAO;
        private UsuarioDAOSQL usuarioDAO;
        private ProjetoDAOSQL projetoDAO;
        private TipoCompetenciaDAOSQL tipoCompetenciaDAO;
        private AvaliacaoDAOSQL avaliacaoDAO;
        private EquipeDAOSQL equipeDAO;

        public Adaptador()
        {

        }

        #region ConhecimentoDAOSQL

        public List<DropDownItem> LoadConhecimentos()
        {
            conhecimentoDAO = new ConhecimentoDAOSQL();
            return conhecimentoDAO.LoadConhecimentos();
        }

        public bool CadastrarConhecimento(string coNome, string coDescricao)
        {
            conhecimentoDAO = new ConhecimentoDAOSQL();
            return conhecimentoDAO.CadastrarConhecimento(coNome, coDescricao);
        }

        #endregion

        #region HabilidadeDAOSQL

        public List<DropDownItem> LoadHabilidades()
        {
            habilidadeDAO = new HabilidadeDAOSQL();
            return habilidadeDAO.LoadHabilidades();
        }

        public bool CadastrarHabilidade(string habNome, string habDescricao)
        {
            habilidadeDAO = new HabilidadeDAOSQL();
            return habilidadeDAO.CadastrarHabilidade(habNome, habDescricao);
        }

        #endregion 

        #region AtitudeDAOSQL

        public List<DropDownItem> LoadAtitudes()
        {
            atitudeDAO = new AtitudeDAOSQL();
            return atitudeDAO.LoadAtitudes();
        }

        public bool CadastrarAtitude(string atNome, string atDescricao)
        {
            atitudeDAO = new AtitudeDAOSQL();
            return atitudeDAO.CadastrarAtitude(atNome, atDescricao);
        }

        #endregion

        #region UsuarioDAOSQL

        public List<ConsultarRHGridDTO> LoadRecursosHumanos(List<int?> listacompetencias)
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.LoadRecursosHumanos(listacompetencias);
        }

        public UsuarioDTO GetUsuario(string nomeUsuario)
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.GetUsuario(nomeUsuario);
        }

        public bool CadastrarUsuario(string usuNome, string nome, string sobrenome, int tipo, string email, string senha, string salt)
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.CadastrarUsuario(usuNome, nome, sobrenome, tipo, email, senha, salt);
        }

        public List<DropDownItem> LoadTipoUsuario()
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.LoadTipoUsuario();
        }

        public List<DropDownItem> LoadGerentes()
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.LoadGerentes();
        }

        public List<DropDownItem> LoadFuncionarios()
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.LoadFuncionarios();
        }

        public List<UsuarioDTO> PesquisarRecursos(List<CompetenciaNivelDTO> listaCompetenciaProj)
        {
            usuarioDAO = new UsuarioDAOSQL();
            return usuarioDAO.PesquisarRecursos(listaCompetenciaProj);
        }
                
        #endregion

        #region ProjetoDAOSQL

        public bool CadastrarProjeto(int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio, 
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.CadastrarProjeto(idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

         public List<Projeto> LoadProjetos(string nome, int? idGerente, string status, string prioridade,
            DateTime? dataInicio, DateTime? dataTermino)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.LoadProjetos(nome, idGerente, status, prioridade, dataInicio, dataTermino);
        }

         public List<ProjetoUsuarioDTO> LoadRHProjeto(int idProjeto)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.LoadRHProjeto(idProjeto);
        }

         public List<DropDownItem> LoadRHProjetoDDL(int idProjeto)
         {
             projetoDAO = new ProjetoDAOSQL();
             return projetoDAO.LoadRHProjetoDDL(idProjeto);
         }

        public Projeto CarregarProjeto(int idProjeto)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.CarregarProjeto(idProjeto);
        }

        public bool AlterarProjeto(int idProjeto, int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.AlterarProjeto(idProjeto, idGerente, nomeProjeto, descProjeto, dataInicio, dataTermino, prioridade, status, listaIdCompetencias);
        }

        public List<DropDownItem> LoadProjetosGerente(int? idGerente)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.LoadProjetosGerente(idGerente);
        }

        public List<DropDownItem> LoadProjetosNaoAvaliadosGerente(int? idGerente)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.LoadProjetosNaoAvaliadosGerente(idGerente);
        }

        public List<Projeto> LoadProjetosTerminados(int? idGerente)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.LoadProjetosTerminados(idGerente);
        }

        public bool MarcarProjetoAvaliado(int? idProjeto)
        {
            projetoDAO = new ProjetoDAOSQL();
            return projetoDAO.MarcarProjetoAvaliado(idProjeto);
        }

        #endregion

        #region TipoCompetenciaDAOSQL

         public List<DropDownItem> LoadTipoCompetencia()
         {
             tipoCompetenciaDAO = new TipoCompetenciaDAOSQL();
             return tipoCompetenciaDAO.LoadTipoCompetencia();
         }

         public List<CompetenciaNivelDTO> LoadCompetenciasProjeto(int idProjeto)
         {
             tipoCompetenciaDAO = new TipoCompetenciaDAOSQL();
             return tipoCompetenciaDAO.LoadCompetenciasProjeto(idProjeto);
         }

         public List<CompetenciaNivelDTO> LoadCompetenciasFuncionario(int? idFuncionario)
         {
             tipoCompetenciaDAO = new TipoCompetenciaDAOSQL();
             return tipoCompetenciaDAO.LoadCompetenciasFuncionario(idFuncionario);
         }

         public List<CompetenciaNivelDTO> CarregarCompetencias(int? tipo)
         {
             tipoCompetenciaDAO = new TipoCompetenciaDAOSQL();
             return tipoCompetenciaDAO.CarregarCompetencias(tipo);
         }

        #endregion

        #region AvaliacaoDAOSQL

         public List<CompetenciaNivelDTO> LoadCompetenciasComNiveis(int? idFuncionario)
         {
             avaliacaoDAO = new AvaliacaoDAOSQL();
             return avaliacaoDAO.LoadCompetenciasComNiveis(idFuncionario);
         }

         public List<CompetenciaNivelDTO> LoadCompetenciasProjetoComNiveis(int? idFuncionario, int? idProjeto)
         {
             avaliacaoDAO = new AvaliacaoDAOSQL();
             return avaliacaoDAO.LoadCompetenciasProjetoComNiveis(idFuncionario, idProjeto);
         }

         public bool AlterarCompetencias(int? idGerente, int? idFuncionario, List<CompetenciaNivelDTO> listaCompetenciasComNivel)
         {
             avaliacaoDAO = new AvaliacaoDAOSQL();
             return avaliacaoDAO.AlterarCompetencias(idGerente, idFuncionario, listaCompetenciasComNivel);
         }

        public bool SalvarPerfil(int idFuncionario, List<CompetenciaNivelDTO> listaNovasCompetencias)
        {
            avaliacaoDAO = new AvaliacaoDAOSQL();
            return avaliacaoDAO.SalvarPerfil(idFuncionario, listaNovasCompetencias);
        }

        public List<CompetenciaNivelDTO> CarregarHistoricoComp(int idCompetencia, int idFuncionario)
        {
            avaliacaoDAO = new AvaliacaoDAOSQL();
            return avaliacaoDAO.CarregarHistoricoComp(idCompetencia, idFuncionario);
        }

        #endregion

        #region EquipeDAOSQL

        public int BuscarMembrosEquipe(int idProjeto)
        {
            equipeDAO = new EquipeDAOSQL();
            return equipeDAO.BuscarMembrosEquipe(idProjeto);
        }

        public bool SalvarEquipe(int idProjeto, UsuarioDTO gerenteProj, List<UsuarioDTO> listaUsuarios)
        {
            equipeDAO = new EquipeDAOSQL();
            return equipeDAO.SalvarEquipe(idProjeto, gerenteProj, listaUsuarios);
        }

        public bool RemoverEquipe(int idProjeto)
        {
            equipeDAO = new EquipeDAOSQL();
            return equipeDAO.RemoverEquipe(idProjeto);
        }

        #endregion
    }
}
