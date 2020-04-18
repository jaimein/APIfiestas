using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIfiestas.Models
{
    public partial class PalanciaContext : DbContext
    {
        public PalanciaContext()
        {
        }

        public PalanciaContext(DbContextOptions<PalanciaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CodigoPostal> CodigoPostal { get; set; }
        public virtual DbSet<Comunidades> Comunidades { get; set; }
        public virtual DbSet<Fiesta> Fiesta { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<LoginsAttemps> LoginsAttemps { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Poblaciones> Poblaciones { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<TipoUsu> TipoUsu { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=192.168.1.254;database=palancia;uid=jaime;pwd=jaime", x => x.ServerVersion("10.4.12-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodigoPostal>(entity =>
            {
                entity.HasIndex(e => e.IdPoblacion)
                    .HasName("fk_codigo_postal_poblaciones");

                entity.Property(e => e.Calle)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.HasOne(d => d.IdPoblacionNavigation)
                    .WithMany(p => p.CodigoPostal)
                    .HasForeignKey(d => d.IdPoblacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_codigo_postal_poblaciones");
            });

            modelBuilder.Entity<Comunidades>(entity =>
            {
                entity.HasIndex(e => e.IdPais)
                    .HasName("fk_comunidades_pais");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Nombre)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Comunidades)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comunidades_pais");
            });

            modelBuilder.Entity<Fiesta>(entity =>
            {
                entity.HasIndex(e => e.IdCodigoPostal)
                    .HasName("fk_fiesta_codigo_postal");

                entity.HasIndex(e => e.IdGrupo)
                    .HasName("fk_fiesta_grupo");

                entity.HasIndex(e => e.IdTipo)
                    .HasName("fk_fiesta_tipo");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.HasOne(d => d.IdCodigoPostalNavigation)
                    .WithMany(p => p.Fiesta)
                    .HasForeignKey(d => d.IdCodigoPostal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fiesta_codigo_postal");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Fiesta)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fiesta_grupo");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Fiesta)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fiesta_tipo");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Descripcion)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");
            });

            modelBuilder.Entity<LoginsAttemps>(entity =>
            {
                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_logins_attemps_usuarios");

                entity.Property(e => e.Time).HasDefaultValueSql("'current_timestamp()'");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.LoginsAttemps)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_logins_attemps_usuarios");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.Property(e => e.Cod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Nombre)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<Poblaciones>(entity =>
            {
                entity.HasIndex(e => e.IdProvincia)
                    .HasName("fk_poblaciones_provincias");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Nombre)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Poblaciones)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_poblaciones_provincias");
            });

            modelBuilder.Entity<Provincias>(entity =>
            {
                entity.HasIndex(e => e.IdComunidad)
                    .HasName("fk_provincias_comunidades");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Nombre)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.IdComunidadNavigation)
                    .WithMany(p => p.Provincias)
                    .HasForeignKey(d => d.IdComunidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_provincias_comunidades");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Descripcion)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");
            });

            modelBuilder.Entity<TipoUsu>(entity =>
            {
                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Description)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasIndex(e => e.IdTipo)
                    .HasName("fk_usuarios_tipo_usu");

                entity.Property(e => e.Cusualt)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cusumod)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Email)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Falt).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Fmod).HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Password)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Usuario)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usuarios_tipo_usu");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
