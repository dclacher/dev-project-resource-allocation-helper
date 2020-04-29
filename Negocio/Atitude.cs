using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Atitude
    {
        private Adaptador adaptador;

        public int? AtitudeID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? TipoCompetencia { get; set; }

        public Atitude(int? atID, string nome, string descricao, int? tipoComp)
        {
            this.AtitudeID = atID;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TipoCompetencia = tipoComp;
        }

        public Atitude()
        {

        }

        public List<DropDownItem> LoadAtitudes()
        {
            adaptador = new Adaptador();
            return adaptador.LoadAtitudes();
        }

        public bool CadastrarAtitude(string atNome, string atDesc)
        {
            adaptador = new Adaptador();
            return adaptador.CadastrarAtitude(atNome, atDesc);

        }
    }
}
