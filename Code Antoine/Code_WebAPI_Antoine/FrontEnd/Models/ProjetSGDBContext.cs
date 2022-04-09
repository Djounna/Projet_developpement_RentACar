using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FrontEnd.Models
{
    public partial class ProjetSGDBContext : DbContext
    {
        public ProjetSGDBContext()
        {
        }

        public ProjetSGDBContext(DbContextOptions<ProjetSGDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Depot> Depots { get; set; } = null!;
        public virtual DbSet<Forfait> Forfaits { get; set; } = null!;
        public virtual DbSet<Notoriete> Notorietes { get; set; } = null!;
        public virtual DbSet<Pays> Pays { get; set; } = null!;
        public virtual DbSet<Prix> Prixes { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Ville> Villes { get; set; } = null!;
        public virtual DbSet<Voiture> Voitures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-FK2QPRH\\SQLDB2022;Database=ProjetSGDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Idclient);

                entity.ToTable("Client");

                entity.HasIndex(e => e.Mail, "UK_Mail")
                    .IsUnique();

                entity.Property(e => e.Idclient).HasColumnName("IDClient");

                entity.Property(e => e.Mail).HasMaxLength(50);

                entity.Property(e => e.Nom).HasMaxLength(50);

                entity.Property(e => e.Prenom).HasMaxLength(50);
            });

            modelBuilder.Entity<Depot>(entity =>
            {
                entity.HasKey(e => e.Iddepot);

                entity.ToTable("Depot");

                entity.Property(e => e.Iddepot).HasColumnName("IDDepot");

                entity.Property(e => e.Idville).HasColumnName("IDVille");

                entity.HasOne(d => d.IdvilleNavigation)
                    .WithMany(p => p.Depots)
                    .HasForeignKey(d => d.Idville)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Depot_Ville");
            });

            modelBuilder.Entity<Forfait>(entity =>
            {
                entity.HasKey(e => e.Idforfait);

                entity.ToTable("Forfait");

                entity.HasIndex(e => e.Idforfait, "U_IDForfait_Prix_DateDebut")
                    .IsUnique();

                entity.Property(e => e.Idforfait)
                    .ValueGeneratedNever()
                    .HasColumnName("IDForfait");

                entity.Property(e => e.DateDebut)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Debut");

                entity.Property(e => e.DateFin)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Fin");

                entity.Property(e => e.Iddepot1).HasColumnName("IDDepot_1");

                entity.Property(e => e.Iddepot2).HasColumnName("IDDepot_2");

                entity.Property(e => e.Prix).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Iddepot1Navigation)
                    .WithMany(p => p.ForfaitIddepot1Navigations)
                    .HasForeignKey(d => d.Iddepot1)
                    .HasConstraintName("FK_Forfait_Depot_1");

                entity.HasOne(d => d.Iddepot2Navigation)
                    .WithMany(p => p.ForfaitIddepot2Navigations)
                    .HasForeignKey(d => d.Iddepot2)
                    .HasConstraintName("FK_Forfait_Depot_2");
            });

            modelBuilder.Entity<Notoriete>(entity =>
            {
                entity.HasKey(e => e.Idnotoriete);

                entity.ToTable("Notoriete");

                entity.Property(e => e.Idnotoriete).HasColumnName("IDNotoriete");

                entity.Property(e => e.CoefficientMultiplicateur)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Coefficient_Multiplicateur");

                entity.Property(e => e.Libelle).HasMaxLength(50);
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.Idpays);

                entity.Property(e => e.Idpays).HasColumnName("IDPays");

                entity.Property(e => e.Nom).HasMaxLength(50);

                entity.Property(e => e.ReferencePrix).HasColumnName("Reference_Prix");
            });

            modelBuilder.Entity<Prix>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Prix");

                entity.HasIndex(e => new { e.ReferencePrix, e.PrixKm, e.DateDebut }, "IX_IDPrix_Prix_DateDebut")
                    .IsUnique();

                entity.Property(e => e.DateDebut)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Debut");

                entity.Property(e => e.DateFin)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Fin");

                entity.Property(e => e.PrixKm)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Prix_Km");

                entity.Property(e => e.ReferencePrix).HasColumnName("Reference_Prix");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Idreservation);

                entity.ToTable("Reservation");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.CoefficientMultiplicateur)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("Coefficient_Multiplicateur");

                entity.Property(e => e.DateDepart).HasColumnType("datetime");

                entity.Property(e => e.DateReservation).HasColumnType("datetime");

                entity.Property(e => e.DateRetour).HasColumnType("datetime");

                entity.Property(e => e.Idclient).HasColumnName("IDClient");

                entity.Property(e => e.IddepotDepart).HasColumnName("IDDepotDepart");

                entity.Property(e => e.IddepotRetour).HasColumnName("IDDepotRetour");

                entity.Property(e => e.Idforfait).HasColumnName("IDForfait");

                entity.Property(e => e.Idvoiture).HasColumnName("IDVoiture");

                entity.Property(e => e.KilometrageDepart).HasColumnName("Kilometrage_Depart");

                entity.Property(e => e.KilometrageRetour).HasColumnName("Kilometrage_Retour");

                entity.HasOne(d => d.IdclientNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Idclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Client");

                entity.HasOne(d => d.IddepotDepartNavigation)
                    .WithMany(p => p.ReservationIddepotDepartNavigations)
                    .HasForeignKey(d => d.IddepotDepart)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_DepotD");

                entity.HasOne(d => d.IddepotRetourNavigation)
                    .WithMany(p => p.ReservationIddepotRetourNavigations)
                    .HasForeignKey(d => d.IddepotRetour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_DepotR");

                entity.HasOne(d => d.IdvoitureNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Idvoiture)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Voiture");
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.Idville);

                entity.ToTable("Ville");

                entity.Property(e => e.Idville).HasColumnName("IDVille");

                entity.Property(e => e.Idpays).HasColumnName("IDPays");

                entity.Property(e => e.Nom).HasMaxLength(50);

                entity.HasOne(d => d.IdpaysNavigation)
                    .WithMany(p => p.Ville)
                    .HasForeignKey(d => d.Idpays)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pays_Ville");
            });

            modelBuilder.Entity<Voiture>(entity =>
            {
                entity.HasKey(e => e.Idvoiture);

                entity.ToTable("Voiture");

                entity.HasIndex(e => e.Immatriculation, "UK_Immatriculation")
                    .IsUnique();

                entity.Property(e => e.Idvoiture).HasColumnName("IDVoiture");

                entity.Property(e => e.Iddepot).HasColumnName("IDDepot");

                entity.Property(e => e.Idnotoriete).HasColumnName("IDNotoriete");

                entity.Property(e => e.Immatriculation)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Marque).HasMaxLength(50);

                entity.HasOne(d => d.IdnotorieteNavigation)
                    .WithMany(p => p.Voitures)
                    .HasForeignKey(d => d.Idnotoriete)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voiture_Notoriete");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
