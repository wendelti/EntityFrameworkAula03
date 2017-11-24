using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAula03
{
    class Aula303
    {
        public class Estudante
        {
            public Estudante()
            {

            }
            public DateTime? DataNascimento { get; set; }

            [NotMapped]
            public bool EhMaiorDeIdade
            {
                get
                {
                    return DataNascimento.Value.Year - DateTime.Now.Year > 18;
                }
            }

            public decimal? Altura { get; set; }
            public Nullable<float> Peso { get; set; }

            public int NotaId { get; set; }
            public Nota Nota { get; set; }

            public string Nome { get; set; }
            public byte[] Foto { get; set; }

            public int EstudanteId { get; set; }


        }

        public class Nota
        {
            public Nota()
            {

            }
            public int NotaId { get; set; }
            public string Descricao { get; set; }

            public ICollection<Estudante> Alunos { get; set; }

        }

        public class EscolaContext : DbContext
        {
            public EscolaContext() : base()
            {

            }

            public DbSet<Estudante> Alunos { get; set; }
            public DbSet<Nota> Notas { get; set; }

        }

        public static void ExecutarExercicio()
        {
            using (var ctx = new EscolaContext())
            {

                Estudante stud = new Estudante() { Nome = "Novo Aluno", DataNascimento = DateTime.Now };
                stud.Nota = new Nota() { Descricao = "A+" };

                ctx.Alunos.Add(stud);
                ctx.SaveChanges();

                var students = from s in ctx.Alunos select s;

                foreach(Estudante s in students)
                {
                    Console.WriteLine(s.Nome);
                }

            }
        }

    }
}
