using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;
using Persistencia.DTO;

namespace Negocio
{
    public class Equipe
    {
        private Adaptador adaptador;

        public Equipe()
        {

        }

        public int BuscarMembrosEquipe(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.BuscarMembrosEquipe(idProjeto);
        }

        public bool SalvarEquipe(int idProjeto, UsuarioDTO gerenteProj, List<UsuarioDTO> listaUsuarios)
        {
            adaptador = new Adaptador();
            return adaptador.SalvarEquipe(idProjeto, gerenteProj, listaUsuarios);
        }

        public bool RemoverEquipe(int idProjeto)
        {
            adaptador = new Adaptador();
            return adaptador.RemoverEquipe(idProjeto);
        }
    }
}
