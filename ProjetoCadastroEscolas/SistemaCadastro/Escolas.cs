using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastro
{
    class Banda
    {

        string nome;
        int alunos;
        DateTime dataHora;
        int oficinas;

        public string Nome { get => nome; set => nome = value; }
        public int alunos { get => alunos; set => alunos = value; }
        public DateTime dataHora { get => dataHora; set => dataHora = value; }
        public int oficinas { get => oficinas; set => oficinas = value; }
    }
}
