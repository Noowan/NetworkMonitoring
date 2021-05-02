using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NetworkMonitoring
{
    public partial class NetworkMonitoringContext : DbContext
    {
        public NetworkMonitoringContext()
        {
        }

        public NetworkMonitoringContext(DbContextOptions<NetworkMonitoringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Value> Values { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=NetworkMonitoring;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Config>(entity =>
            {
                entity.Property(e => e.ConfigId).HasColumnName("configID");

                entity.Property(e => e.ConfigString).HasColumnName("configString");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Configs)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_Configs_Devices");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.CredentialsId);

                entity.Property(e => e.CredentialsId).HasColumnName("credentialsID");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_Credentials_Devices");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.Ipaddress)
                    .IsRequired()
                    .HasColumnName("IPAddress");

                entity.Property(e => e.Manufacturer)
                    .HasColumnName("manufacturer")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Model)
                    .HasColumnName("model")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Value>(entity =>
            {
                entity.Property(e => e.ValueId).HasColumnName("valueID");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.MetricName)
                    .IsRequired()
                    .HasColumnName("metricName");

                entity.Property(e => e.Value1)
                    .HasColumnName("value")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValueStr)
                    .HasColumnName("valueStr")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Values)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_Values_Devices");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
