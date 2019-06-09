using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
   public class Professor : Usuario
    {
        public int idProfessor{ get; set; }

        public List<Turma> listaTurmas { get; set; }

        public List<Curso> listaCurso { get; set; }

    }
}
