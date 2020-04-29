using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    public class ProjetoDAOSQL
    {
        public ProjetoDAOSQL()
        {

        }

        #region CadastrarProjeto

        public bool CadastrarProjeto(int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // verifica se existe outro projeto com o mesmo nome para o mesmo gerente.
                    var queryVal = from projeto in db.Projetoes
                                   where projeto.proj_nome == nomeProjeto
                                   && projeto.proj_usu_id == idGerente
                                   select projeto;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Projeto novoProjeto = new Projeto();
                        novoProjeto.proj_usu_id = Convert.ToInt32(idGerente);
                        novoProjeto.proj_nome = nomeProjeto;
                        novoProjeto.proj_descricao = descProjeto;
                        novoProjeto.proj_data_inicio = dataInicio;
                        novoProjeto.proj_data_termino = dataTermino;
                        novoProjeto.proj_prioridade = prioridade;
                        novoProjeto.proj_status = status;
                        novoProjeto.Competencias = new List<Competencia>();

                        int idProjeto = novoProjeto.proj_id;

                        foreach (int id in listaIdCompetencias)
                        {
                            var query = from comp in db.Competencias
                                        where comp.comp_id == id
                                        select comp;

                            Competencia competencia = query.Single();
                            novoProjeto.Competencias.Add(competencia);
                        }

                        db.Projetoes.Add(novoProjeto);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao salvar as informações do Projeto!");
            }
            return true;
        }
        #endregion

        #region ConsultarProjetos

        public List<Projeto> LoadProjetos(string nome, int? idGerente, string status, string prioridade,
            DateTime? dataInicio, DateTime? dataTermino)
        {
            List<Projeto> listaProjetos = new List<Projeto>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // exemplo de query com parâmetros opcionais utilizando a sintaxe LINQ.
                    var query = from projetos in db.Projetoes
                                orderby projetos.proj_nome
                                where (String.IsNullOrEmpty(nome) || projetos.proj_nome == nome)
                                && (!idGerente.HasValue || projetos.proj_usu_id == idGerente)
                                && (String.IsNullOrEmpty(status) || projetos.proj_status == status)
                                && (String.IsNullOrEmpty(prioridade) || projetos.proj_prioridade == prioridade)
                                && (!dataInicio.HasValue || projetos.proj_data_inicio >= dataInicio)
                                && (!dataTermino.HasValue || projetos.proj_data_termino <= dataTermino)
                                select projetos;

                    foreach (Projeto proj in query.ToList())
                    {
                        Projeto item = new Projeto();
                        item.proj_id = proj.proj_id;
                        item.proj_nome = proj.proj_nome;
                        item.proj_status = proj.proj_status;
                        item.proj_prioridade = proj.proj_prioridade;
                        item.proj_data_inicio = proj.proj_data_inicio;
                        item.proj_data_termino = proj.proj_data_termino;
                        item.proj_usu_id = proj.proj_usu_id;

                        listaProjetos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar a lista de projetos!");
            }

            return listaProjetos;
        }

        public List<ProjetoUsuarioDTO> LoadRHProjeto(int idProjeto)
        {
            List<ProjetoUsuarioDTO> listaRecuros = new List<ProjetoUsuarioDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from recursoProj in db.Projeto_Funcionario
                                join usuario in db.Usuarios on recursoProj.prfu_usu_id equals usuario.usu_id
                                where recursoProj.prfu_proj_id == idProjeto
                                select new
                                {
                                    recursoProj.prfu_id,
                                    recursoProj.prfu_data_inicio,
                                    recursoProj.prfu_data_termino,
                                    usuario.usu_id,
                                    usuario.usu_nome,
                                    usuario.usu_sobrenome,
                                    usuario.usu_email,
                                };

                    foreach (var item in query.ToList())
                    {
                        ProjetoUsuarioDTO projUsuarioDTO = new ProjetoUsuarioDTO();
                        projUsuarioDTO.ID = item.prfu_id;
                        projUsuarioDTO.IDFuncionario = item.usu_id;
                        projUsuarioDTO.Nome = item.usu_nome;
                        projUsuarioDTO.Sobrenome = item.usu_sobrenome;
                        projUsuarioDTO.DataInicio = item.prfu_data_inicio;
                        projUsuarioDTO.DataTermino = item.prfu_data_termino;
                        projUsuarioDTO.Email = item.usu_email;
                        listaRecuros.Add(projUsuarioDTO);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar a lista de recursos!");
            }

            return listaRecuros;
        }

        #endregion

        #region AlterarProjeto

        public Projeto CarregarProjeto(int idProjeto)
        {
            Projeto projeto;
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from proj in db.Projetoes
                                where proj.proj_id == idProjeto
                                select proj;

                    projeto = query.Single();
                    projeto.Competencias = query.Single().Competencias;
                }
            }
            catch (Exception)
            {
                throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar as informações do Projeto!");
            }

            return projeto;
        }

        public bool AlterarProjeto(int idProjeto, int? idGerente, string nomeProjeto, string descProjeto, DateTime dataInicio,
            DateTime dataTermino, string prioridade, string status, List<int?> listaIdCompetencias)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // verifica se existe outro projeto com o mesmo nome para o mesmo gerente.
                    var queryVal = from projeto in db.Projetoes
                                   where projeto.proj_nome == nomeProjeto
                                   && projeto.proj_id != idProjeto
                                   && projeto.proj_usu_id == idGerente
                                   select projeto;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        var querySelect = from projeto in db.Projetoes
                                          where projeto.proj_id == idProjeto
                                          select projeto;


                        Projeto projetoAlterar = querySelect.Single();
                        projetoAlterar.proj_nome = nomeProjeto;
                        projetoAlterar.proj_descricao = descProjeto;
                        projetoAlterar.proj_data_inicio = dataInicio;
                        projetoAlterar.proj_data_termino = dataTermino;
                        projetoAlterar.proj_prioridade = prioridade;
                        projetoAlterar.proj_status = status;
                        //projetoAlterar.Competencias = new List<Competencia>();
                        projetoAlterar.Competencias.Clear();

                        foreach (int id in listaIdCompetencias)
                        {
                            var query = from comp in db.Competencias
                                        where comp.comp_id == id
                                        select comp;

                            Competencia competencia = query.Single();
                            projetoAlterar.Competencias.Add(competencia);
                        }

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao salvar as informações do Projeto!");
            }
            return true;
        }

        #endregion

        #region BuscarEquipe

        public List<DropDownItem> LoadProjetosGerente(int? idGerente)
        {
            List<DropDownItem> listaProjeto = new List<DropDownItem>();

            if (idGerente != null)
            {
                try
                {
                    using (SAARHEntities db = new SAARHEntities())
                    {
                        var query = from projeto in db.Projetoes
                                    orderby projeto.proj_nome
                                    where projeto.proj_usu_id == idGerente
                                    select projeto;

                        foreach (Projeto proj in query.ToList())
                        {
                            DropDownItem item = new DropDownItem(proj.proj_id, proj.proj_nome);
                            listaProjeto.Add(item);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar a lista de Projetos");
                }
            }

            return listaProjeto;
        }              

        #endregion

        #region Master

        public List<Projeto> LoadProjetosTerminados(int? idGerente)
        {
            List<Projeto> listaProjeto = new List<Projeto>();

            if(idGerente != null)
            {
                try
                {
                    using (SAARHEntities db = new SAARHEntities())
                    {
                        var query = from projeto in db.Projetoes
                                    orderby projeto.proj_nome
                                    where projeto.proj_usu_id == idGerente
                                    && projeto.proj_data_termino <= DateTime.Today
                                    && projeto.proj_avaliado == false
                                    select projeto;

                        listaProjeto = query.ToList();
                    }
                }
                catch (Exception)
                {                    
                    throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar a lista de Projetos terminados");
                }
            }

            return listaProjeto;
        }

        #endregion

        #region AvaliarProjeto

        public List<DropDownItem> LoadRHProjetoDDL(int idProjeto)
        {
            List<DropDownItem> listaFuncionarios = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from recursoProj in db.Projeto_Funcionario
                                join usuario in db.Usuarios on recursoProj.prfu_usu_id equals usuario.usu_id
                                where recursoProj.prfu_proj_id == idProjeto
                                select new
                                {
                                    recursoProj.prfu_usu_id,
                                    usuario.usu_nome,
                                    usuario.usu_sobrenome
                                };


                    foreach (var user in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(user.prfu_usu_id, user.usu_nome + " " + user.usu_sobrenome);
                        listaFuncionarios.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de recursos.");
            }

            return listaFuncionarios;
        }

        internal bool MarcarProjetoAvaliado(int? idProjeto)
        {
            bool projetoAvaliado = false;

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from proj in db.Projetoes
                                where proj.proj_id == idProjeto
                                select proj;

                    foreach (Projeto projeto in query.ToList())
                    {
                        projeto.proj_avaliado = true;
                        projetoAvaliado = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de recursos.");
            }

            return projetoAvaliado;
        }

        public List<DropDownItem> LoadProjetosNaoAvaliadosGerente(int? idGerente)
        {
            List<DropDownItem> listaProjeto = new List<DropDownItem>();

            if (idGerente != null)
            {
                try
                {
                    using (SAARHEntities db = new SAARHEntities())
                    {
                        var query = from projeto in db.Projetoes
                                    orderby projeto.proj_nome
                                    where projeto.proj_usu_id == idGerente
                                    && projeto.proj_avaliado == false
                                    && projeto.proj_data_termino <= DateTime.Today
                                    select projeto;

                        foreach (Projeto proj in query.ToList())
                        {
                            DropDownItem item = new DropDownItem(proj.proj_id, proj.proj_nome);
                            listaProjeto.Add(item);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw new Excecoes.ProjetoExcecao("Ocorreu um problema ao recuperar a lista de Projetos");
                }
            }

            return listaProjeto;
        }

        #endregion
    }
}
