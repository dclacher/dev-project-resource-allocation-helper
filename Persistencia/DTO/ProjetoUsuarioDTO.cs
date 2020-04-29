using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DTO
{
    public class ProjetoUsuarioDTO
    {
        public int ID { get; set; }
        public int IDFuncionario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }

        public string Email { get; set; }

        public ProjetoUsuarioDTO()
        {

        }
    }
}
