using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Conhecimento
    {
        private Adaptador adaptador;

        public int? ConhecimentoID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? TipoCompetencia { get; set; }

        public Conhecimento(int? coID, string nome, string descricao, int? tipoComp)
        {
            this.ConhecimentoID = coID;
            this.Nome = nome;
            this.Descricao = descricao;
            this.TipoCompetencia = tipoComp;
        }
        
        public Conhecimento()
        {

        }

        public List<DropDownItem> LoadConhecimentos()
        {
            adaptador = new Adaptador();
            return adaptador.LoadConhecimentos();
        }

        public bool CadastrarConhecimento(string coNome, string coDesc)
        {
            adaptador = new Adaptador();
            return adaptador.CadastrarConhecimento(coNome, coDesc);

        }
    }
}
