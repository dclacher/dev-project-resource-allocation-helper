using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.DTO;

namespace Persistencia
{
    class TipoCompetenciaDAOSQL
    {
        public TipoCompetenciaDAOSQL()
        {

        }

        public List<DropDownItem> LoadTipoCompetencia()
        {
            List<DropDownItem> listaTipoCompetencia = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from tipoCompetencia in db.Tipo_Competencia
                                select tipoCompetencia;

                    foreach (Tipo_Competencia tipo in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(tipo.tcom_id, tipo.tcom_nome);
                        listaTipoCompetencia.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de tipos de competências! ");
            }

            return listaTipoCompetencia;
        }

        public List<CompetenciaNivelDTO> LoadCompetenciasProjeto(int idProjeto)
        {
            List<CompetenciaNivelDTO> listaCompetenciaProjeto = new List<CompetenciaNivelDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from proj in db.Projetoes
                                where proj.proj_id == idProjeto
                                select proj;                    

                    // instancia um objeto DTO que é a competência com nível.
                    foreach (Competencia comp in query.Single().Competencias.ToList<Competencia>())
                    {
                        CompetenciaNivelDTO compNivel = new CompetenciaNivelDTO();
                        compNivel.CompID = comp.comp_id;
                        compNivel.CompNome = comp.comp_nome;
                        compNivel.CompDesc = comp.comp_descricao;
                        compNivel.CompNivel = 0;
                        listaCompetenciaProjeto.Add(compNivel);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de competências deste projeto! ");
            }
            return listaCompetenciaProjeto;
        }

        # region PerfilPessoal
        
        public List<CompetenciaNivelDTO> LoadCompetenciasFuncionario(int? idFuncionario)
        {
            List<CompetenciaNivelDTO> listaCompetenciaFuncionario = new List<CompetenciaNivelDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from usuAvalComp in db.Usu_Aval_Comp
                                join avaliacao in db.Avaliacaos on usuAvalComp.uac_aval_id equals avaliacao.aval_id
                                join competencia in db.Competencias on usuAvalComp.uac_comp_id equals competencia.comp_id
                                where usuAvalComp.uac_usu_id == idFuncionario
                                && avaliacao.aval_atual == true
                                select new
                                {
                                    competencia.comp_id,
                                    competencia.comp_nome,
                                    competencia.comp_descricao,
                                    avaliacao.aval_nivel,
                                };

                    foreach (var item in query.ToList())
                    {
                        CompetenciaNivelDTO compNivel = new CompetenciaNivelDTO(item.comp_id, item.comp_nome, item.comp_descricao, Convert.ToInt32(item.aval_nivel));
                        listaCompetenciaFuncionario.Add(compNivel);
                    }                    
                }
            }
            catch (Exception)
            {                
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a sua lista de competências!");
            }
            return listaCompetenciaFuncionario;
        }

        public List<CompetenciaNivelDTO> CarregarCompetencias(int? tipo)
        {
            List<CompetenciaNivelDTO> listaCompetencias = new List<CompetenciaNivelDTO>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from comp in db.Competencias
                                join tipoComp in db.Tipo_Competencia on comp.comp_tcom_id equals tipoComp.tcom_id
                                orderby comp.comp_nome
                                where (!tipo.HasValue || comp.comp_tcom_id == tipo)
                                select new
                                {
                                    comp.comp_id,
                                    comp.comp_nome,
                                    comp.comp_descricao,
                                    tipoComp.tcom_nome,
                                };

                    foreach (var item in query.ToList())
                    {
                        CompetenciaNivelDTO competencia = new CompetenciaNivelDTO();
                        competencia.CompID = item.comp_id;
                        competencia.CompNome = item.comp_nome;
                        competencia.CompDesc = item.comp_descricao;
                        competencia.CompTipo = item.tcom_nome;
                        listaCompetencias.Add(competencia);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de competências!");
            }
            return listaCompetencias;
        }

        //public List<DropDownItem> LoadCompetenciasComboPerfil()
        //{
        //    List<DropDownItem> listaCompetencia = new List<DropDownItem>();

        //    try
        //    {
        //        using (SAARHEntities db = new SAARHEntities())
        //        {
        //            // busca as competências menos as que são do tipo atitude.
        //            var query = from competencia in db.Competencias
        //                        where competencia.comp_tcom_id != 3
        //                        orderby competencia.comp_nome
        //                        select competencia;

        //            foreach (Competencia comp in query.ToList())
        //            {
        //                DropDownItem item = new DropDownItem(comp.comp_id, comp.comp_nome);
        //                listaCompetencia.Add(item);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new Excecoes.TipoCompetenciaExcecao("Ocorreu um problema ao recuperar a lista de tipos de competências! ");
        //    }
        //    return listaCompetencia;
        //}

        #endregion
    }
}
