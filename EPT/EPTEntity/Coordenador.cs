using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPTEntity
{
    public class Coordenador : Usuario
    {
        public int idCoordenador{ get; set; }

        public List<Curso> listaCurso { get; set; }

        public List<Turma> listaTurma{ get; set; }

    }
}
