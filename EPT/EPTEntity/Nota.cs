using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
    public class Nota
    {
        public int idAluno { get; set; }
        public int Semestre { get; set; }
        public float nota{ get; set; }
        public int acertos { get; set; }

    }

    public class NotaTurma
    {
        public int idTurma { get; set; }
        public int idCurso { get; set; }
        public int Semestre { get; set; }
        public float nota { get; set; }
        public int acertos { get; set; }

    }

}
