//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Persistencia
{
    using System;
    using System.Collections.Generic;
    
    public partial class Competencia
    {
        public Competencia()
        {
            this.Usu_Aval_Comp = new HashSet<Usu_Aval_Comp>();
            this.Projetoes = new HashSet<Projeto>();
        }
    
        public int comp_id { get; set; }
        public string comp_nome { get; set; }
        public string comp_descricao { get; set; }
        public int comp_tcom_id { get; set; }
    
        public virtual Tipo_Competencia Tipo_Competencia { get; set; }
        public virtual ICollection<Usu_Aval_Comp> Usu_Aval_Comp { get; set; }
        public virtual ICollection<Projeto> Projetoes { get; set; }
    }
}
