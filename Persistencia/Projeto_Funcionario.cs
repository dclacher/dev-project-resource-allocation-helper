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
    
    public partial class Projeto_Funcionario
    {
        public int prfu_id { get; set; }
        public System.DateTime prfu_data_inicio { get; set; }
        public System.DateTime prfu_data_termino { get; set; }
        public int prfu_usu_id { get; set; }
        public int prfu_proj_id { get; set; }
    
        public virtual Projeto Projeto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}