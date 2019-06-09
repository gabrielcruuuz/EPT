using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
    public class Turma
    {
        public int id{ get; set; }
        public string Nome{ get; set; }
        public int idCurso { get; set; }
        public string Semestre { get; set; }
    }
}
