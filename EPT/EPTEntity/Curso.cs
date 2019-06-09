using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
    public class Curso
    {
        public int id { get; set; }

        public int idCampus{ get; set; }

        public string Nome { get; set; }

        public string Turno { get; set; }

    }

    public class Semestre
    {
        public int id{ get; set; }

        public int semestre { get; set; }

    }
}
