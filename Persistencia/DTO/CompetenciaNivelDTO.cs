using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DTO
{
    [Serializable]
    public class CompetenciaNivelDTO
    {
        public int CompID { get; set; }
        public string CompNome { get; set; }
        public string  CompDesc { get; set; }
        public int CompNivel { get; set; }
        public string CompTipo { get; set; }
        public DateTime CompAvalData  { get; set; }
                
        public CompetenciaNivelDTO()
        {

        }

        public CompetenciaNivelDTO(int compID, string compNome, string compDesc, int compNivel)
        {
            CompID = compID;
            CompNome = compNome;
            CompDesc = compDesc;
            CompNivel = compNivel;
        }

    }
}
