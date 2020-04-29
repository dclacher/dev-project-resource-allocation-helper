using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    class AvaliacaoDAOSQL
    {
        public AvaliacaoDAOSQL()
        {

        }

        public List<CompetenciaNivelDTO> LoadCompetenciasComNiveis(int? idFuncionario)
        {
            List<CompetenciaNivelDTO> listaCompetenciasComNiveis = new List<CompetenciaNivelDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // pego todos os registro daquele funcionario na Usu_Aval_Comp
                    var query01 = from usuAvalComp in db.Usu_Aval_Comp
                                  where idFuncionario == usuAvalComp.uac_usu_id
                                  orderby usuAvalComp.uac_comp_id descending, usuAvalComp.uac_aval_id descending
                                  select usuAvalComp;

                    List<int> listaAuxiliar = new List<int>();
                    foreach (Usu_Aval_Comp uac in query01.ToList())
                    {
                        if (!listaAuxiliar.Contains(uac.uac_comp_id))
                        {
                            CompetenciaNivelDTO compNivelDTO = new CompetenciaNivelDTO();
                            compNivelDTO.CompID = uac.uac_comp_id;
                            compNivelDTO.CompNome = uac.Competencia.comp_nome;
                            compNivelDTO.CompDesc = uac.Competencia.comp_descricao;
                            compNivelDTO.CompNivel = (int)uac.Avaliacao.aval_nivel;
                            listaCompetenciasComNiveis.Add(compNivelDTO);

                            listaAuxiliar.Add(compNivelDTO.CompID);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de competências deste funcionário! ");
            }
            return listaCompetenciasComNiveis;
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjetoComNiveis(int? idFuncionario, int? idProjeto)
        {
            List<CompetenciaNivelDTO> listaCompetenciasComNiveis = new List<CompetenciaNivelDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // pego todos os registro daquele funcionario na Usu_Aval_Comp
                    var query01 = from usuAvalComp in db.Usu_Aval_Comp
                                  where idFuncionario == usuAvalComp.uac_usu_id
                                  orderby usuAvalComp.uac_comp_id descending, usuAvalComp.uac_aval_id descending
                                  select usuAvalComp;

                    // pego o projeto
                    var query02 = from proj in db.Projetoes
                                  where idProjeto == proj.proj_id
                                  select proj;

                    // pego as competencias do projeto e coloco os ids delas em uma lista
                    List<Competencia> listaCompetenciasProjeto = query02.ToList()[0].Competencias.ToList();
                    List<int> listaCompIds = new List<int>();
                    foreach (Competencia comp in listaCompetenciasProjeto)
                    {
                        listaCompIds.Add(comp.comp_id);
                    }
                    List<int> listaAuxiliar = new List<int>();
                    foreach (Usu_Aval_Comp uac in query01.ToList())
                    {
                        if (!listaAuxiliar.Contains(uac.uac_comp_id))
                        {
                            // so vou adicionar a competencia na lista final se ela pertencer ao projeto
                            if (listaCompIds.Contains(uac.uac_comp_id))
                            {
                                CompetenciaNivelDTO compNivelDTO = new CompetenciaNivelDTO();
                                compNivelDTO.CompID = uac.uac_comp_id;
                                compNivelDTO.CompNome = uac.Competencia.comp_nome;
                                compNivelDTO.CompDesc = uac.Competencia.comp_descricao;
                                compNivelDTO.CompNivel = (int) uac.Avaliacao.aval_nivel;
                                listaCompetenciasComNiveis.Add(compNivelDTO);
                                listaAuxiliar.Add(compNivelDTO.CompID);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de competências deste funcionário! ");
            }
            return listaCompetenciasComNiveis;
        }

        public bool AlterarCompetencias(int? idGerente, int? idFuncionario, List<CompetenciaNivelDTO> listaCompetenciasComNivel)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    List<int> idsCompetencias = new List<int>();

                    foreach (CompetenciaNivelDTO comp in listaCompetenciasComNivel)
                    {
                        idsCompetencias.Add(comp.CompID);
                    }
                    
                    int idFuncionarioQuery = Convert.ToInt32(idFuncionario);
                    var query = from uac in db.Usu_Aval_Comp
                                where idFuncionarioQuery == uac.uac_usu_id
                                && idsCompetencias.Contains(uac.uac_comp_id)
                                select uac;

                    foreach (Usu_Aval_Comp uacomp in query.ToList())
                    {
                        uacomp.Avaliacao.aval_atual = false;
                        db.SaveChanges();
                    }

                    foreach (CompetenciaNivelDTO comp in listaCompetenciasComNivel)
                    {
                        Avaliacao novaAvaliacao = new Avaliacao();
                        novaAvaliacao.aval_nivel = comp.CompNivel;
                        novaAvaliacao.aval_data = DateTime.Now;
                        novaAvaliacao.aval_usu_id = Convert.ToInt32(idGerente);
                        novaAvaliacao.aval_atual = true;
                        novaAvaliacao.Usu_Aval_Comp = new List<Usu_Aval_Comp>();
                        int idNovaAvaliacao = novaAvaliacao.aval_id;
                        Usu_Aval_Comp novoUAC = new Usu_Aval_Comp();
                        novoUAC.uac_aval_id = idNovaAvaliacao;
                        novoUAC.uac_comp_id = comp.CompID;
                        novoUAC.uac_usu_id = Convert.ToInt32(idFuncionario);
                        novaAvaliacao.Usu_Aval_Comp.Add(novoUAC);
                        db.Avaliacaos.Add(novaAvaliacao);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao atualizar as competências deste funcionário! ");
            }
        }

        #region Perfil

        public bool SalvarPerfil(int idFuncionario, List<CompetenciaNivelDTO> listaNovasCompetencias)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    foreach (CompetenciaNivelDTO comp in listaNovasCompetencias)
                    {
                        // verifica se não existe alguma avaliação para esta competência, para este funcionario.  
                        var query = from uac in db.Usu_Aval_Comp
                                    where uac.uac_usu_id == idFuncionario
                                    && uac.uac_comp_id == comp.CompID
                                    select uac;

                        if(query.Count() > 0)
                        {                            
                            return false;
                        }
                        else
                        {
                            Avaliacao novaAval = new Avaliacao();
                            novaAval.aval_usu_id = idFuncionario;
                            novaAval.aval_nivel = comp.CompNivel;
                            novaAval.aval_data = DateTime.Now;
                            novaAval.aval_atual = true;

                            db.Avaliacaos.Add(novaAval);
                            db.SaveChanges();

                            int idNewAval = novaAval.aval_id;

                            Usu_Aval_Comp novaUac = new Usu_Aval_Comp();
                            novaUac.uac_aval_id = idNewAval;
                            novaUac.uac_usu_id = idFuncionario;
                            novaUac.uac_comp_id = comp.CompID;

                            db.Usu_Aval_Comp.Add(novaUac);
                            db.SaveChanges();
                        }
                    }                    
                }
            }
            catch (Exception)
            {
                throw new Excecoes.AvaliacaoExcecao("Ocorreu um problema ao salvar o perfil!");              
            }
            return true;
        }

        #endregion

        #region Histórico

        public List<CompetenciaNivelDTO> CarregarHistoricoComp(int idCompetencia, int idFuncionario)
        {
            List<CompetenciaNivelDTO> listaCompetencia = new List<CompetenciaNivelDTO>();
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from usuAvalComp in db.Usu_Aval_Comp
                                join avaliacao in db.Avaliacaos on usuAvalComp.uac_aval_id equals avaliacao.aval_id
                                join competencia in db.Competencias on usuAvalComp.uac_comp_id equals competencia.comp_id
                                where usuAvalComp.uac_usu_id == idFuncionario
                                && usuAvalComp.uac_comp_id == idCompetencia
                                select new
                                {
                                    competencia.comp_id,
                                    competencia.comp_nome,
                                    competencia.comp_descricao,
                                    avaliacao.aval_nivel,
                                    avaliacao.aval_data,
                                };

                    foreach (var item in query.ToList())
                    {
                        CompetenciaNivelDTO compNivel = new CompetenciaNivelDTO();
                        compNivel.CompID = item.comp_id;
                        compNivel.CompNome = item.comp_nome;
                        compNivel.CompDesc = item.comp_descricao;
                        compNivel.CompAvalData = item.aval_data;
                        compNivel.CompNivel = Convert.ToInt32(item.aval_nivel);

                        listaCompetencia.Add(compNivel);
                    }
                }
            }
            catch (Exception)
            {                
                throw new Excecoes.AvaliacaoExcecao("Ocorreu um problema ao recuperar o histórico desta competência!");
            }
            return listaCompetencia;
        }

        #endregion
    }
}
