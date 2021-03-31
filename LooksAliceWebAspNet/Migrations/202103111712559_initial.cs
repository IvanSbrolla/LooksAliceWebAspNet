namespace LooksAliceWebAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Categoria_Nome = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Compra_StatusCompra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComprasId = c.Int(nullable: false),
                        StatusCompraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compras", t => t.ComprasId, cascadeDelete: true)
                .ForeignKey("dbo.StatusCompra", t => t.StatusCompraId, cascadeDelete: true)
                .Index(t => t.ComprasId)
                .Index(t => t.StatusCompraId);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        CompraId = c.Int(nullable: false, identity: true),
                        CodTransacao = c.String(unicode: false),
                        Preco = c.Double(nullable: false),
                        Forma_Pagamento = c.String(unicode: false),
                        Data = c.DateTime(nullable: false, precision: 0),
                        UserName = c.String(unicode: false),
                        Logradouro = c.String(unicode: false),
                        Cep = c.String(unicode: false),
                        NumeroResidencial = c.String(unicode: false),
                        Uf = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CompraId);
            
            CreateTable(
                "dbo.StatusCompra",
                c => new
                    {
                        StatusCompraId = c.Int(nullable: false, identity: true),
                        Status = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.StatusCompraId);
            
            CreateTable(
                "dbo.Compras_Pedidos",
                c => new
                    {
                        Compras_PedidosId = c.Int(nullable: false, identity: true),
                        PedidosId = c.Int(nullable: false),
                        ComprasId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Compras_PedidosId)
                .ForeignKey("dbo.Compras", t => t.ComprasId, cascadeDelete: true)
                .ForeignKey("dbo.Pedidos", t => t.PedidosId, cascadeDelete: true)
                .Index(t => t.PedidosId)
                .Index(t => t.ComprasId);
            
            CreateTable(
                "dbo.Pedidos",
                c => new
                    {
                        IdPedido = c.Int(nullable: false, identity: true),
                        AppUserName = c.String(unicode: false),
                        ProdutoId = c.Int(nullable: false),
                        Produto_Qntd = c.Int(nullable: false),
                        Concluido = c.Boolean(nullable: false),
                        Preco = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdPedido)
                .ForeignKey("dbo.Produtos", t => t.ProdutoId, cascadeDelete: true)
                .Index(t => t.ProdutoId);
            
            CreateTable(
                "dbo.Produtos",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                        Preco = c.Double(nullable: false),
                        TecidoId = c.Int(nullable: false),
                        Tamanho = c.String(unicode: false),
                        CorId = c.Int(nullable: false),
                        Imagem = c.Binary(),
                        Promocao = c.Boolean(nullable: false),
                        DestaqueHome = c.Boolean(nullable: false),
                        Preco_Promocao = c.Double(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        RecomendadoHome = c.Boolean(nullable: false),
                        RecomendadoCarrinho = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProdutoId)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Cores", t => t.CorId, cascadeDelete: true)
                .ForeignKey("dbo.Tecidos", t => t.TecidoId, cascadeDelete: true)
                .Index(t => t.TecidoId)
                .Index(t => t.CorId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Cores",
                c => new
                    {
                        CorId = c.Int(nullable: false, identity: true),
                        Cor_Nome = c.String(unicode: false),
                        CodRgba = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.CorId);
            
            CreateTable(
                "dbo.Tecidos",
                c => new
                    {
                        TecidoId = c.Int(nullable: false, identity: true),
                        Tecido_Nome = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.TecidoId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        PrimeiroNome = c.String(maxLength: 30, storeType: "nvarchar"),
                        Sobrenome = c.String(maxLength: 50, storeType: "nvarchar"),
                        Telefone = c.String(unicode: false),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Compras_Pedidos", "PedidosId", "dbo.Pedidos");
            DropForeignKey("dbo.Pedidos", "ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.Produtos", "TecidoId", "dbo.Tecidos");
            DropForeignKey("dbo.Produtos", "CorId", "dbo.Cores");
            DropForeignKey("dbo.Produtos", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Compras_Pedidos", "ComprasId", "dbo.Compras");
            DropForeignKey("dbo.Compra_StatusCompra", "StatusCompraId", "dbo.StatusCompra");
            DropForeignKey("dbo.Compra_StatusCompra", "ComprasId", "dbo.Compras");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Produtos", new[] { "CategoriaId" });
            DropIndex("dbo.Produtos", new[] { "CorId" });
            DropIndex("dbo.Produtos", new[] { "TecidoId" });
            DropIndex("dbo.Pedidos", new[] { "ProdutoId" });
            DropIndex("dbo.Compras_Pedidos", new[] { "ComprasId" });
            DropIndex("dbo.Compras_Pedidos", new[] { "PedidosId" });
            DropIndex("dbo.Compra_StatusCompra", new[] { "StatusCompraId" });
            DropIndex("dbo.Compra_StatusCompra", new[] { "ComprasId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tecidos");
            DropTable("dbo.Cores");
            DropTable("dbo.Produtos");
            DropTable("dbo.Pedidos");
            DropTable("dbo.Compras_Pedidos");
            DropTable("dbo.StatusCompra");
            DropTable("dbo.Compras");
            DropTable("dbo.Compra_StatusCompra");
            DropTable("dbo.Categorias");
        }
    }
}
