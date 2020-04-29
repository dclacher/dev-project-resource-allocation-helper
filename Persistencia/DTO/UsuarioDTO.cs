using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DTO
{
    [Serializable]
    public class UsuarioDTO
    {
        public int? UsuarioID { get; set; }
        public string NomeUsuario { get; set; }
        public string SobrenomeUsuario { get; set; }
        public string NomeSistemaUsuario { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public int? TipoUsuario { get; set; }
        public string TipoUsuarioDesc { get; set; }
        public string Email { get; set; }
        public bool Alocado { get; set; }
        public int classificacao { get; set; }
        public bool Negativado { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
                
        // lista de competencias com o respectivo valor do nível setado na avaliação.
        public List<CompetenciaNivelDTO> ListaCompetenciaNivel { get; set; }       
        
        public UsuarioDTO()
        {

        }

        public UsuarioDTO(int ID, string nome, string sobrenome, string senha, string salt, int tipo, string tipoDesc)
        {
            UsuarioID = ID;
            NomeUsuario = nome;
            SobrenomeUsuario = sobrenome;
            Senha = senha;
            Salt = salt;
            TipoUsuario = tipo;
            TipoUsuarioDesc = tipoDesc;
        }

    }
}
