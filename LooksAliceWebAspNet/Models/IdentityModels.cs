using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MySql.Data.EntityFramework;

namespace LooksAliceWebAspNet.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [MaxLength(30), MinLength(3)]
        public string PrimeiroNome { get; set; }

        [MaxLength(50), MinLength(3)]
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNet.Identity.UserManager<ApplicationUser> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Produtos> produtos { get; set; }
        public DbSet<Pedidos> pedidos { get; set; }
        public DbSet<Compras_Pedidos> compras_Pedidos { get; set; }
        public DbSet<Categorias> categorias { get; set; }
        public DbSet<Cores> cores { get; set; }
        public DbSet<Compra_StatusCompra> compra_statusCompra { get; set; }
        public DbSet<StatusCompra> statusCompra { get; set; }
        public DbSet<Compras> compras { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}