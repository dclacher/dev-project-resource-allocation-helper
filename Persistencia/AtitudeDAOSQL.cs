using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Persistencia
{
    public class AtitudeDAOSQL
    {
        public AtitudeDAOSQL()
        {

        }

        public List<DropDownItem> LoadAtitudes()
        {
            List<DropDownItem> listaAtitude = new List<DropDownItem>();

            try
            {
                using(SAARHEntities db = new SAARHEntities()){
                    var query = from atitute in db.Competencias
                                join tipo in db.Tipo_Competencia on atitute.comp_tcom_id equals tipo.tcom_id
                                orderby atitute.comp_nome
                                where tipo.tcom_id == 3
                                select atitute;
                    foreach (Competencia comp in query.ToList())
                    {
                        DropDownItem item = new DropDownItem(comp.comp_id, comp.comp_nome);
                        listaAtitude.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Excecoes.AtitudeExcecao("Ocorreu um problema ao recuperar a lista de Atitudes");
            }

            return listaAtitude;
        }

        #region CadastrarAtitude

        public bool CadastrarAtitude(string atNome, string atDescricao)
        {
            try
            {
                using (SAARHEntities db = new SAARHEntities())
                {
                    // Verifica se não existe já outra atitude com o mesmo nome.
                    var queryVal = from atitude in db.Competencias
                                   where atitude.comp_nome == atNome
                                   select atitude;

                    if (queryVal.Count() > 0)
                    {
                        return false;
                    }
                    else
                    {
                        Competencia novaAtitude = new Competencia();
                        novaAtitude.comp_tcom_id = 3;
                        novaAtitude.comp_nome = atNome;
                        novaAtitude.comp_descricao = atDescricao;

                        db.Competencias.Add(novaAtitude);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw new Excecoes.AtitudeExcecao("Ocorreu um problema ao salvar as informações desta atitude! ");
            }

            return true;
        }

        #endregion
    }
}
