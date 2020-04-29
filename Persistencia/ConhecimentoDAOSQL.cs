using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Persistencia
{
    public class ConhecimentoDAOSQL
    {
        public ConhecimentoDAOSQL()
        {

        }

        public List<DropDownItem> LoadConhecimentos()
        {
            List<DropDownItem> listaConhecimento = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from conhecimento in db.Competencias
                                join tipo in db.Tipo_Competencia on conhecimento.comp_tcom_id equals tipo.tcom_id
                                orderby conhecimento.comp_nome
                                where tipo.tcom_id == 1
                                select conhecimento;
               
                    foreach (Competencia comp in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(comp.comp_id, comp.comp_nome); 
                        listaConhecimento.Add(item);
                    }                    
                }
            }

            catch (Exception ex)
            {
                throw new Excecoes.ConhecimentoExcecao("Ocorreu um problema ao recuperar a lista de Conhecimentos");
            }

            return listaConhecimento;
        }

        #region CadastrarConhecimento

        public bool CadastrarConhecimento(string coNome, string coDescricao)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // Verifica se não existe já outro conhecimento com o mesmo nome.
                    var queryVal = from conhecimento in db.Competencias
                                   where conhecimento.comp_nome == coNome
                                   select conhecimento;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Competencia novoConhecimento = new Competencia();
                        novoConhecimento.comp_tcom_id = 1;
                        novoConhecimento.comp_nome = coNome;
                        novoConhecimento.comp_descricao = coDescricao;

                        db.Competencias.Add(novoConhecimento);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.HabilidadeExcecao("Ocorreu um problema ao salvar as informações deste conhecimento! ");
            }

            return true;
        }

        #endregion
    }
}
