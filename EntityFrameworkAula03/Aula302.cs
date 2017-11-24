using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAula03
{
    class Aula302
    {
        public class Aluno
        {
            public Aluno()
            {

            }
            public int AlunoId { get; set; }
            public string Nome { get; set; }
            public DateTime? DataNascimento { get; set; }
            public byte[] Foto { get; set; }
            public decimal Altura { get; set; }
            public float Peso { get; set; }

            public Nota Nota { get; set; }
        }

        public class Nota
        {
            public Nota()
            {

            }
            public int NotaId { get; set; }
            public string Descricao { get; set; }

            public ICollection<Aluno> Alunos { get; set; }

        }

        public class EscolaContext : DbContext
        {
            public EscolaContext() : base()
            {

            }

            public DbSet<Aluno> Alunos { get; set; }
            public DbSet<Nota> Notas { get; set; }

        }

        public static void ExecutarExercicio()
        {
            using (var ctx = new EscolaContext())
            {

                Aluno stud = new Aluno() { Nome = "Novo Aluno" };
                stud.Nota = new Nota() { Descricao = "A+" };

                ctx.Alunos.Add(stud);
                ctx.SaveChanges();

                var students = from s in ctx.Alunos select s;

                foreach(Aluno s in students)
                {
                    Console.WriteLine(s.Nome);
                }

            }
        }

    }
}
