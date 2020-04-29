using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Excecoes
{
    public class ProjetoExcecao : Exception
    {
        public ProjetoExcecao(string msg) : base(msg)
        {
            
        }
    }
}
