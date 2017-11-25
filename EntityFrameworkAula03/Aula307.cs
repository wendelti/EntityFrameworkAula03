using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAula03
{
    class Aula307
    {

        public class Estudante
        {
            public Estudante()
            {
            }

            public int EstudanteId { get; set; }
            public string Nome { get; set; }
            public int NotaId { get; set; }
            public EnderecoEstudante Endereco { get; set; }
            public Nota Nota { get; set; }
            public ICollection<Curso> Cursos { get; set; }
        }

        public class EnderecoEstudante
        {
            public EnderecoEstudante()
            {
            }
            public int EnderecoEstudanteId { get; set; }
            public string CEP { get; set; }
            public string Numero { get; set; }

            public int EstudanteId { get; set; }
            public Estudante Estudante { get; set; }
        }

        public class Nota
        {
            public Nota()
            {
            }
            public int NotaId { get; set; }
            public string Descricao { get; set; }

            public int EstudanteId { get; set; }
            public Estudante Aluno { get; set; }

        }

        public class Curso
        {
            public Curso()
            {
            }
            public int CursoId { get; set; }
            public string Descricao { get; set; }
            public ICollection<Estudante> Alunos { get; set; }

            public int ProfessorId { get; set; }
            public Professor Professor { get; set; }
        }

        public class Professor
        {
            public Professor()
            {
            }
            public int ProfessorId { get; set; }
            public string Nome { get; set; }
            public ICollection<Curso> Cursos { get; set; }
        }

        public class EscolaContext : DbContext
        {
            public EscolaContext() : base()
            {

            }

            public DbSet<Estudante> Alunos { get; set; }
            public DbSet<Nota> Notas { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

                EntityTypeConfiguration<Estudante> ecEstudante = modelBuilder.Entity<Estudante>();

                // 1 para 1 ou 0
                ecEstudante
                        .HasOptional<EnderecoEstudante>(e => e.Endereco)
                        .WithRequired(endereco => endereco.Estudante);

                // 1 para 1
                ecEstudante
                       .HasRequired<Nota>(e => e.Nota)
                       .WithRequiredPrincipal(e => e.Aluno);

                // 1 para MUITOS
                modelBuilder.Entity<Curso>()
                    .HasRequired<Professor>(p => p.Professor)
                    .WithMany(p => p.Cursos)
                    .HasForeignKey<int>(c => c.ProfessorId);

                //MUTOS para MUITOS
                modelBuilder.Entity<Estudante>()
                   .HasMany<Curso>(s => s.Cursos)
                   .WithMany(c => c.Alunos)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("EstudanteId");
                       cs.MapRightKey("CursoId");
                       cs.ToTable("EstudanteCurso");
                   });


                base.OnModelCreating(modelBuilder);
            }


        }

        public static void ExecutarExercicio()
        {
            using (var ctx = new EscolaContext())
            {

                Estudante stud = new Estudante() { Nome = "Novo Aluno" };
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
