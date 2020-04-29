using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    public class UsuarioDAOSQL
    {
        public UsuarioDAOSQL()
        {

        }

        #region Login

        public UsuarioDTO GetUsuario(string nomeUsuario)
        {
            UsuarioDTO usuario = new UsuarioDTO();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {

                    var query = from user in db.Usuarios
                                join tipo in db.Tipo_Usuario on user.usu_tipo_id equals tipo.tipo_id
                                where user.usu_nome_usuario == nomeUsuario
                                // aqui cria um objeto abstrato (não tipado), que contém colunas de duas tabelas.
                                select new
                                {
                                    user.usu_id,
                                    user.usu_nome,
                                    user.usu_sobrenome,
                                    user.usu_senha,
                                    user.usu_salt,
                                    user.usu_email,
                                    user.usu_nome_usuario,
                                    tipo.tipo_id,
                                    tipo.tipo_nome,

                                };
                                    
                    if (query.Count() > 0 && query.Count() < 2)
                    {
                        // userEntity é a variável de retorno do objeto abstrato.
                        var userEntity = query.Single(); // sigle retorna o único valor da query.
                        usuario.UsuarioID = userEntity.usu_id;
                        usuario.NomeUsuario = userEntity.usu_nome;
                        usuario.SobrenomeUsuario = userEntity.usu_sobrenome;
                        usuario.Senha = userEntity.usu_senha;
                        usuario.Salt = userEntity.usu_salt;
                        usuario.Email = userEntity.usu_email;
                        usuario.NomeSistemaUsuario = userEntity.usu_nome_usuario;
                        usuario.TipoUsuario = userEntity.tipo_id;
                        usuario.TipoUsuarioDesc = userEntity.tipo_nome;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar as informações do usuário!");
            }

            return usuario;
        }
        #endregion

        #region ConsultarRecursos
        public List<ConsultarRHGridDTO> LoadRecursosHumanos(List<int?> listacompetencias)
        {
            List<ConsultarRHGridDTO> listaRH = new List<ConsultarRHGridDTO>();

            List<int?> listaAux = new List<int?>(listacompetencias);
            for (int i = 0; i < 10; i++)
            {
                if (i > listaAux.Count - 1)
                {
                    listaAux.Add(null);
                }
            }

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = db.SP_GetRecursosHumanos(listaAux[0], listaAux[1], listaAux[2], listaAux[3], listaAux[4], listaAux[5],
                        listaAux[6], listaAux[7], listaAux[8], listaAux[9]);

                    foreach (SP_GetRecursosHumanos_Result item in query.ToList())
                    {

                        ConsultarRHGridDTO consultaRHGrid = new ConsultarRHGridDTO();
                        consultaRHGrid.IdUsuario = item.tmp_id;
                        consultaRHGrid.Nome = item.tmp_nome;
                        consultaRHGrid.SobreNome = item.tmp_sobrenome;
                        consultaRHGrid.Nivel0 = item.tmp_comp_nivel0;
                        consultaRHGrid.Nivel1 = item.tmp_comp_nivel1;
                        consultaRHGrid.Nivel2 = item.tmp_comp_nivel2;
                        consultaRHGrid.Nivel3 = item.tmp_comp_nivel3;
                        consultaRHGrid.Nivel4 = item.tmp_comp_nivel4;
                        consultaRHGrid.Nivel5 = item.tmp_comp_nivel5;
                        consultaRHGrid.Nivel6 = item.tmp_comp_nivel6;
                        consultaRHGrid.Nivel7 = item.tmp_comp_nivel7;
                        consultaRHGrid.Nivel8 = item.tmp_comp_nivel8;
                        consultaRHGrid.Nivel9 = item.tmp_comp_nivel9;

                        listaRH.Add(consultaRHGrid);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de recursos humanos");
            }

            return listaRH;
        }
        #endregion

        #region CadastrarUsuario

        public bool CadastrarUsuario(string usuNome, string nome, string sobrenome, int tipo, string email, string senha, string salt)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // Verifica se não existe já outro usuario com o mesmo Username.
                    var queryVal = from user in db.Usuarios
                                   where user.usu_nome_usuario == usuNome
                                   select user;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        // UserName não existe ainda no BD e pode ser criado um novo usuário.
                        Usuario novoUsuario = new Usuario();
                        novoUsuario.usu_nome_usuario = usuNome;
                        novoUsuario.usu_nome = nome;
                        novoUsuario.usu_sobrenome = sobrenome;
                        novoUsuario.usu_tipo_id = tipo;
                        novoUsuario.usu_email = email;
                        novoUsuario.usu_senha = senha;
                        novoUsuario.usu_salt = salt;

                        db.Usuarios.Add(novoUsuario);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao salvar as informações deste usuário! ");
            }

            return true;
        }

        public List<DropDownItem> LoadTipoUsuario()
        {
            List<DropDownItem> listaTipoUsuario = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from tipoUsuario in db.Tipo_Usuario
                                select tipoUsuario;

                    foreach (Tipo_Usuario tipo in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(tipo.tipo_id, tipo.tipo_nome);
                        listaTipoUsuario.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de tipos de Usuários! ");
            }

            return listaTipoUsuario;
        }

        #endregion

        #region ConsultarProjetos

        public List<DropDownItem> LoadGerentes()
        {
            List<DropDownItem> listaGerentes = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from user in db.Usuarios
                                orderby user.usu_nome, user.usu_sobrenome
                                where user.usu_tipo_id == 1
                                select user;


                    foreach (Usuario user in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(user.usu_id, user.usu_nome + " " + user.usu_sobrenome);
                        listaGerentes.Add(item);
                    }
                }
            }
            catch (Exception)
            {                
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de gerentes");
            }

            return listaGerentes;
        }

        #endregion

        #region PerfilPessoal

        public List<DropDownItem> LoadFuncionarios()
        {
            List<DropDownItem> listaFuncionarios = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from user in db.Usuarios
                                orderby user.usu_nome, user.usu_sobrenome
                                where user.usu_tipo_id == 2
                                select user;


                    foreach (Usuario user in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(user.usu_id, user.usu_nome + " " + user.usu_sobrenome);
                        listaFuncionarios.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao recuperar a lista de funcionários");
            }

            return listaFuncionarios;
        }       

        #endregion

        #region BuscarEquipe
        
        public List<UsuarioDTO> PesquisarRecursos(List<CompetenciaNivelDTO> listaCompetenciaProj)
        {
            List<UsuarioDTO> listaRecursos = new List<UsuarioDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // instancia e popula um array com os id's das competências existentes no projeto.
                    // esse array é usado para fazer a lógico do "IN" em uma cláusula WHERE no entity framework.
                    int[] idCompetencias = new int[listaCompetenciaProj.Count];

                    int index = 0;
                    foreach (CompetenciaNivelDTO comp in listaCompetenciaProj)
                    {
                        idCompetencias[index] = comp.CompID;
                        index++;
                    }

                    // seleciona todos os usuários que contém pelo menos uma das competências do projeto.
                    var query = (from recurso in db.Usuarios
                                join usuAvalComp in db.Usu_Aval_Comp on recurso.usu_id equals usuAvalComp.uac_usu_id
                                where idCompetencias.Contains(usuAvalComp.uac_comp_id)
                                select recurso).Distinct();

                    foreach (Usuario usuario in query.ToList())
                    {
                        UsuarioDTO usuarioDTO = new UsuarioDTO();
                        usuarioDTO.UsuarioID = usuario.usu_id;
                        usuarioDTO.NomeUsuario = usuario.usu_nome;
                        usuarioDTO.SobrenomeUsuario = usuario.usu_sobrenome;
                        usuarioDTO.TipoUsuario = usuario.usu_tipo_id;
                        usuarioDTO.Email = usuario.usu_email;
                        usuarioDTO.ListaCompetenciaNivel = new List<CompetenciaNivelDTO>();

                        // verifica se o usuário já está alocado em algum projeto.
                        var queryAlocado = from projFunc in db.Projeto_Funcionario
                                           where projFunc.prfu_usu_id == usuarioDTO.UsuarioID
                                           && projFunc.prfu_data_termino >= DateTime.Today
                                           select projFunc;

                        if(queryAlocado.ToList().Count > 0)
                        {
                            usuarioDTO.Alocado = true;
                        }

                        // busca todas as competências do usuário com o respectivo valor do nível setado na avaliação
                        var queryCompetencias = from usuAvalComp in db.Usu_Aval_Comp
                                                join avaliacao in db.Avaliacaos on usuAvalComp.uac_aval_id equals avaliacao.aval_id
                                                join competencia in db.Competencias on usuAvalComp.uac_comp_id equals competencia.comp_id
                                                where usuAvalComp.uac_usu_id == usuarioDTO.UsuarioID
                                                && avaliacao.aval_atual == true
                                                && idCompetencias.Contains(usuAvalComp.uac_comp_id)
                                                select new
                                                {
                                                    competencia.comp_id,
                                                    competencia.comp_nome,
                                                    competencia.comp_descricao,
                                                    avaliacao.aval_nivel,
                                                };

                        foreach (var item in queryCompetencias.ToList())
                        {
                            CompetenciaNivelDTO compNivel = new CompetenciaNivelDTO(item.comp_id, item.comp_nome, item.comp_descricao, Convert.ToInt32(item.aval_nivel));
                            usuarioDTO.ListaCompetenciaNivel.Add(compNivel);
                        }

                        listaRecursos.Add(usuarioDTO);
                    } 
                }
            }
            catch (Exception)
            {
                throw new Excecoes.UsuarioExcecao("Ocorreu um problema ao pesquisar os recursos deste projeto!");
            }
            return listaRecursos;
        }

        #endregion
    }
}


