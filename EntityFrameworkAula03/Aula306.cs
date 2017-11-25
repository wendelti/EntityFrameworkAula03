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
    class Aula306
    {
        [Table("Students")]
        public class Estudante
        {
            public Estudante()
            {

            }
            
            public int ChaveEstudante { get; set; }
            public DateTime? DataNascimento { get; set; }
            public int CPF { get; set; }
            public decimal Altura { get; set; }
            public string Nome { get; set; }
            public Nullable<float> Peso { get; set; }

            public int NotaId { get; set; }
            public Nota Nota { get; set; }

            public byte[] Foto { get; set; }
            

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

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

                EntityTypeConfiguration<Estudante> ecEstudante = modelBuilder.Entity<Estudante>();
                //entityConfigurationEstudante.ToTable("DadosEstudantes");

                ecEstudante.ToTable("Estudantes");
                ecEstudante.HasKey<int>(e => e.ChaveEstudante);
                ecEstudante.Property(p => p.DataNascimento)
                            .HasColumnName("Nascimento")
                            .HasColumnOrder(2)
                            .HasColumnType("datetime2")
                            .IsRequired();

                ecEstudante.Property(p => p.Altura)
                            .HasPrecision(2, 2);

                ecEstudante.Property(p => p.Nome)
                           .IsConcurrencyToken();



                base.OnModelCreating(modelBuilder);
            }


        }

        public static void ExecutarExercicio()
        {
            using (var ctx = new EscolaContext())
            {

                Estudante stud = new Estudante() { CPF = 999999,  Nome = "Novo Aluno", DataNascimento = DateTime.Now };
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
