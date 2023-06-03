using DaisyPets.Core.Domain;
using Microsoft.EntityFrameworkCore;


namespace DaisyPets.Infrastructure.Context;

public class PetsContext : DbContext
{
    public DbSet<ConsultaVeterinario> ConsultasVeterinario { get; set; }
    public DbSet<DesparasitanteExterno> DesparasitantesExternos { get; set; }
    public DbSet<DesparasitanteInterno> DesparasitantesInternos { get; set; }
    public DbSet<Especie> Especies { get; set; }
    public DbSet<Esterilizacao> Esterilizacoes { get; set; }
    public DbSet<GaleriaFotos> GaleriasFotos { get; set; }
    //public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<Idade> Idades { get; set; }
    public DbSet<MarcaRacao> MarcasRacoes { get; set; }
    public DbSet<Medicacao> Medicacoes { get; set; }
    public DbSet<Peso> Pesos { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Raca> Racas { get; set; }
    public DbSet<Racao> Racoes { get; set; }
    public DbSet<Temperamento> Temperamentost { get; set; }
    public DbSet<TipoDesparasitanteExterno> TipoDesparasitantesExternos { get; set; }
    public DbSet<TipoDesparasitanteInterno> TipoDesparasitantesInternos { get; set; }
    public DbSet<Vacina> Vacinas { get; set; }
    public PetsContext(DbContextOptions options) : base(options)
    {
    }
}