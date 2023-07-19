using E_Market.Core.Domain.Entites;
using E_Market.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Market.Core.Application.ViewModel.User;
using Microsoft.AspNetCore.Http;
using E_Market.Core.Application.Helpers;

namespace E_Market.Infrastructure.Persistence.Context
{
    public class E_MarketContext:DbContext
    {
        public DbSet<Anuncio> anuncios { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Imagen> imagen { get; set; }
        private readonly UserViewModel _userViewModel;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public E_MarketContext(DbContextOptions<E_MarketContext> options, IHttpContextAccessor httpContext) : base(options)
        {
            _httpContextAccessor = httpContext;
            _userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _userViewModel.Nombre;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        entry.Entity.ModifiedBy = _userViewModel.Nombre;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region Table
            model.Entity<Anuncio>().ToTable("Anuncio");
            model.Entity<User>().ToTable("User");
            model.Entity<Category>().ToTable("Category");
            model.Entity<Imagen>().ToTable("Imagen");
            #endregion
            #region PrimaryKey
            model.Entity<Anuncio>().HasKey(a => a.Id);
            model.Entity<User>().HasKey(u => u.Id);
            model.Entity<Category>().HasKey(c => c.Id);
            model.Entity<Imagen>().HasKey(c => c.Id);
            #endregion
            #region Relation
            model.Entity<Category>()
                .HasMany<Anuncio>(a => a.Anuncio)
                .WithOne(u => u.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Anuncio>()
                .HasMany<Imagen>(a => a.Imagen)
                .WithOne(u => u.anuncio)
                .HasForeignKey(a => a.idAnuncio)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<User>()
                .HasMany<Anuncio>(a => a.Anuncios)
                .WithOne (c => c.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Property
            #region Category
            model.Entity<Category>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            model.Entity<Category>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(100);
            #endregion
            #region Anuncio
            model.Entity<Anuncio>()
                .Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            model.Entity<Anuncio>()
                .Property(a => a.Descripcion)
                .IsRequired();
            model.Entity<Anuncio>()
                .Property(a => a.Precio)
                .IsRequired();
            #endregion
            #region User
            model.Entity<User>()
                .Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            model.Entity<User>()
                .Property(u => u.Apellido)
                .IsRequired()
                .HasMaxLength(100);
            model.Entity<User>()
                .Property(u => u.Correo)
                .IsRequired();
            model.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
            model.Entity<User>()
                .Property(u => u.Telefono)
                .IsRequired()
                .HasMaxLength(100);
            model.Entity<User>()
                .Property(u => u.NombreUsuario)
                .IsRequired();
            #endregion
            #region Imagen
            model.Entity<Imagen>()
                .Property(i => i.Nombre);
            model.Entity<Imagen>()
                .Property(i => i.ImageUrl);
            #endregion

            #endregion
        }
    }
}
