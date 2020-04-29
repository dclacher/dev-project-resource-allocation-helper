using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Excecoes
{
    public class UsuarioExcecao : Exception
    {
        public UsuarioExcecao(string msg) : base(msg)
        {
            
        }
    }
}
