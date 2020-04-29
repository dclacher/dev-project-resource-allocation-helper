using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Habilidade
    {
        private Adaptador adaptador;

        public int? HabilidadeID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? TipoCompetencia { get; set; }

        public Habilidade()
        {

        }

        public Habilidade(int? habID, string nome, string descricao, int? tipoComp)
        {
            this.HabilidadeID = habID;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TipoCompetencia = tipoComp;
        }

        public List<DropDownItem> LoadHabilidades()
        {
            adaptador = new Adaptador();
            return adaptador.LoadHabilidades();
        }

        public bool CadastrarHabilidade(string habNome, string habDesc)
        {
            adaptador = new Adaptador();
            return adaptador.CadastrarHabilidade(habNome, habDesc);

        }
        
    }

}
