using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Persistencia.DTO;

namespace Persistencia
{
    public class HabilidadeDAOSQL
    {
        public HabilidadeDAOSQL()
        {

        }

        public List<DropDownItem> LoadHabilidades()
        {
            List<DropDownItem> listaHabilidades = new List<DropDownItem>();

            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    var query = from habilidade in db.Competencias
                                join tipo in db.Tipo_Competencia on habilidade.comp_tcom_id equals tipo.tcom_id
                                orderby habilidade.comp_nome
                                where tipo.tcom_id == 2
                                select habilidade;

                    foreach (Competencia comp in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(comp.comp_id, comp.comp_nome);
                        listaHabilidades.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.HabilidadeExcecao("Ocorreu um problema ao recuperar a lista de Conhecimentos");
            }

            return listaHabilidades;
        }

        #region CadastrarHabilidade

        public bool CadastrarHabilidade(string habNome, string habDescricao)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // Verifica se não existe já outra habilidade com o mesmo nome.
                    var queryVal = from hab in db.Competencias
                                   where hab.comp_nome == habNome
                                   select hab;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Competencia novaHabilidade = new Competencia();
                        novaHabilidade.comp_tcom_id = 2;
                        novaHabilidade.comp_nome = habNome;
                        novaHabilidade.comp_descricao = habDescricao;

                        db.Competencias.Add(novaHabilidade);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.HabilidadeExcecao("Ocorreu um problema ao salvar as informações desta habilidade! ");
            }

            return true;
        }

        #endregion
    }
}
