using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Excecoes
{
    public class ConhecimentoExcecao:Exception
    {
        public ConhecimentoExcecao(string msg) : base(msg)
        {

        }
    }
}
