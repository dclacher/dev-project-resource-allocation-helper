﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DTO
{
    public class AlterarProjetoDTO
    {
        public Projeto Projeto { get; set; }
        public List<int?> ListaIdCompetencias { get; set; }

        public AlterarProjetoDTO()
        {

        }
    }
}
