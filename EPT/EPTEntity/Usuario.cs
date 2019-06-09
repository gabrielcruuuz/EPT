using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nome { get; set; }

        public int RA{ get; set; }

        public string Senha{ get; set; }

        public int tipoPerfil { get; set; }

    }
}
