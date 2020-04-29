using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;
using System.Web.Helpers;


namespace Negocio
{
    public class Usuario
    {
        private Adaptador adaptador;

        public int? UsuarioID { get; set; }
        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int? TipoUsuario { get; set; }
        public string TipoUsuarioDesc { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {

        }

        public Usuario(int? usuID, string usuNome, string nome, string sobrenome, int? tipo, string tipoDesc, string email, string senha)
        {
            this.UsuarioID = usuID;
            this.NomeUsuario = usuNome;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.TipoUsuario = tipo;
            this.TipoUsuarioDesc = tipoDesc;
            this.Email = email;
            this.Senha = senha;
        }
        
        public UsuarioDTO GetUsuario(string nomeUsuario, string senha)
        {
            adaptador = new Adaptador();
            UsuarioDTO usuario = adaptador.GetUsuario(nomeUsuario);

            if (usuario != null)
            {
                string salt = usuario.Salt;
                string senhaSalt = senha + salt;

                bool senhaCorrecta = Crypto.VerifyHashedPassword(usuario.Senha, senhaSalt);
                if(!senhaCorrecta)
                {
                    return null;
                }
            }

            return usuario;
        }

        public List<ConsultarRHGridDTO> LoadRecursosHumanos(List<int?> listacompetencias)
        {
            adaptador = new Adaptador();
            return adaptador.LoadRecursosHumanos(listacompetencias);
        }

        public bool CadastrarUsuario(string usuNome, string nome, string sobrenome, int tipo, string email, string senha)
        {
            // faz o hash da senha e chama o adaptador
            string salt = Crypto.GenerateSalt();
            string senhaSalt = senha + salt;
            string senhaHash = Crypto.HashPassword(senhaSalt);

            adaptador = new Adaptador();
            return adaptador.CadastrarUsuario(usuNome, nome, sobrenome, tipo, email, senhaHash, salt);
                                    
        }

        public List<DropDownItem> LoadTipoUsuario()
        {
            adaptador = new Adaptador();
            return adaptador.LoadTipoUsuario();
        }

        public List<DropDownItem> LoadGerentes()
        {
            adaptador = new Adaptador();
            return adaptador.LoadGerentes();
        }

        public List<DropDownItem> LoadFuncionarios()
        {
            adaptador = new Adaptador();
            return adaptador.LoadFuncionarios();
        }

        public List<UsuarioDTO> PesquisarRecursos(List<CompetenciaNivelDTO> listaCompetenciaProj, bool calculoMelhores)
        {
            adaptador = new Adaptador();
            List<UsuarioDTO> listaRecursos = adaptador.PesquisarRecursos(listaCompetenciaProj);

            if(calculoMelhores)
            {
                return PesquisaMelhores(listaRecursos, listaCompetenciaProj);
            }
            else
            {
                return PesquisaMelhorAjuste(listaRecursos, listaCompetenciaProj);
            }  
            
        }

        private List<UsuarioDTO> PesquisaMelhores(List<UsuarioDTO> listaRecursos, List<CompetenciaNivelDTO> listaCompetenciaProj)
        {
            List<UsuarioDTO> listaMelhores = new List<UsuarioDTO>();

            // para cada usuário que tenha pelo menos uma competência requerida pelo projeto.
            foreach (UsuarioDTO recurso in listaRecursos)
            {
                // para cada competência do usuário.
                int posicao = 0;
                foreach (CompetenciaNivelDTO competenciaUsuario in recurso.ListaCompetenciaNivel)
                {      
                    // compara as competências e calcula a classificação do usuário.
                    foreach (CompetenciaNivelDTO competenciaProj in listaCompetenciaProj)
                    {
                        if(competenciaUsuario.CompID == competenciaProj.CompID)
                        {
                            posicao += competenciaUsuario.CompNivel * competenciaProj.CompNivel;
                        }
                    }
                }
                recurso.classificacao = posicao;
            }
            // ordena a lista de recursos pela classificação dos usuários (maior para o menor).
            listaMelhores = listaRecursos.OrderByDescending(r => r.classificacao).ToList();            
            return listaMelhores;
        }

        private List<UsuarioDTO> PesquisaMelhorAjuste(List<UsuarioDTO> listaRecursos, List<CompetenciaNivelDTO> listaCompetenciaProj)
        {
            List<UsuarioDTO> listaMelhorAjuste = new List<UsuarioDTO>();

            // Para cada usuário que tenha pelo menos uma competência requerida pelo projeto.
            foreach (UsuarioDTO recurso in listaRecursos)
            {
                // Para cada competência do usuário.
                int posicao = 0;
                int posicaoAux = 0;

                // Lista auxiliar de competencias do projeto que serve para controlar as competências que o usuário não possui. 
                List<CompetenciaNivelDTO> listaCompProjAux = new List<CompetenciaNivelDTO>(listaCompetenciaProj);  
                foreach (CompetenciaNivelDTO competenciaUsuario in recurso.ListaCompetenciaNivel)
                {
                    // compara as competências e calcula a classificação do usuário.
                    foreach (CompetenciaNivelDTO competenciaProj in listaCompetenciaProj)
                    {
                        if (competenciaUsuario.CompID == competenciaProj.CompID)
                        {                            
                            posicao += Math.Abs(competenciaUsuario.CompNivel - competenciaProj.CompNivel);
                            posicaoAux += (competenciaUsuario.CompNivel - competenciaProj.CompNivel);

                            // Remove da lista auxiliar a competência do projeto já onde foi calculado a classificação.
                            listaCompProjAux.Remove(competenciaProj);
                        }                        
                    }
                }
                // para cada competência que o usuário não tem é adicionado o valor do nível dela.
                foreach(CompetenciaNivelDTO compProjAux in listaCompProjAux)
                {
                    posicao += compProjAux.CompNivel;
                    posicaoAux -= compProjAux.CompNivel;
                }
                recurso.classificacao = posicao;
                if (posicaoAux < 0) 
                {
                    recurso.Negativado = true;
                }
            }
            // ordena a lista de recursos pela classificação dos usuários (maior para o menor).
            listaMelhorAjuste = listaRecursos.OrderBy(r => r.classificacao).ThenBy(n => n.Negativado).ToList();
            return listaMelhorAjuste;
        }

    }
}
