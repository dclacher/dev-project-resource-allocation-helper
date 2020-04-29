using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    public class EquipeDAOSQL
    {
        public EquipeDAOSQL()
        {

        }

        #region BuscarEquipe

        public int BuscarMembrosEquipe(int idProjeto)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from equipe in db.Projeto_Funcionario
                                where equipe.prfu_proj_id == idProjeto
                                select equipe;

                    return query.Count();
                }
            }
            catch (Exception)
            {                
                throw new Excecoes.EquipeExcecao("Ocorreu um problema ao verificar a existência de uma equipe para este projeto!");
            }            
        }

        public bool SalvarEquipe(int idProjeto, UsuarioDTO gerenteProj, List<UsuarioDTO> listaUsuarios)
        {            
            try
            {    
                if(listaUsuarios.Count > 0 && idProjeto != null)
                {
                    using (SAARHEntities db = new SAARHEntities())
                    {
                        // Insere cada usuário na tabela Projeto_Funcionário.
                        foreach (UsuarioDTO usuario in listaUsuarios)
                        {
                            Projeto_Funcionario recurso = new Projeto_Funcionario();
                            recurso.prfu_proj_id = idProjeto;
                            recurso.prfu_usu_id = Convert.ToInt32(usuario.UsuarioID);
                            recurso.prfu_data_inicio = usuario.DataInicio;
                            recurso.prfu_data_termino = usuario.DataTermino;

                            db.Projeto_Funcionario.Add(recurso);
                            //db.SaveChanges();
                        }

                        // Adiciona o gerente do projeto a equipe
                        Projeto_Funcionario gerente = new Projeto_Funcionario();
                        gerente.prfu_proj_id = idProjeto;
                        gerente.prfu_usu_id = Convert.ToInt32(gerenteProj.UsuarioID);
                        gerente.prfu_data_inicio = gerenteProj.DataInicio;
                        gerente.prfu_data_termino = gerenteProj.DataTermino;

                        db.Projeto_Funcionario.Add(gerente);
                        db.SaveChanges();

                        return true;
                    }                    
                }
                
            }
            catch (Exception)
            {                
                throw new Excecoes.EquipeExcecao("Ocorreu um problema ao salvar a equipe para este projeto!");
            }
            return false;
        }
       
        public bool RemoverEquipe(int idProjeto)
        {
            try
            {
                // remove todos os usuários da tabela Projeto_Funcionário para antes de salvar uma nova equipe.
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from recurso in db.Projeto_Funcionario
                                where recurso.prfu_proj_id == idProjeto
                                select recurso;

                    if (query.Count() > 0)
                    {
                        foreach (Projeto_Funcionario recursoProj in query.ToList())
                        {
                            db.Projeto_Funcionario.Remove(recursoProj);
                        }
                        db.SaveChanges();
                        return true;
                    }
                }                
            }
            catch (Exception)
            {
                throw new Excecoes.EquipeExcecao("Ocorreu um problema ao remover a antiga equipe para este projeto!");
            }
            return false;
        }

        #endregion
    }
}
