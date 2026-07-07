using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento de Equipamento (Suporte nativo a JSONB no PostgreSQL)
            modelBuilder.Entity<Equipamento>(entity =>
            {
                entity.Property(e => e.Especificacoes)
                      .HasColumnType("jsonb");
            });

            // Relacionamentos de Pedido (Prevenir conflito de múltiplas chaves estrangeiras com cascata)
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.AgenciaOrigem)
                .WithMany()
                .HasForeignKey(p => p.AgenciaOrigemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.AgenciaDestino)
                .WithMany()
                .HasForeignKey(p => p.AgenciaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Solicitante)
                .WithMany()
                .HasForeignKey(p => p.SolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}