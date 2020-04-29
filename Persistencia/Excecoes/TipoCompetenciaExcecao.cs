using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Excecoes
{
    class TipoCompetenciaExcecao : Exception
    {
        public TipoCompetenciaExcecao(string msg) : base(msg)
        {
            
        }
    }
}
