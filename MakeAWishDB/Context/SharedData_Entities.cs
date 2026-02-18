using System;
using System.Collections.Generic;
using MakeAWishDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeAWishDB.Context;

public partial class SharedData_Entities : DbContext
{
    public SharedData_Entities()
    {
    }

    public SharedData_Entities(DbContextOptions<SharedData_Entities> options)
        : base(options)
    {
    }

    public virtual DbSet<CakeAllergen> CakeAllergens { get; set; }

    public virtual DbSet<CakeFilling> CakeFillings { get; set; }

    public virtual DbSet<CakeRecipe> CakeRecipes { get; set; }

    public virtual DbSet<CelebrationCake> CelebrationCakes { get; set; }

    public virtual DbSet<CelebrationCakeSize> CelebrationCakeSizes { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientReservation> IngredientReservations { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<LongArticle> LongArticles { get; set; }

    public virtual DbSet<MainPageArticle> MainPageArticles { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleAccess> ModuleAccesses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStep> OrderSteps { get; set; }

    public virtual DbSet<PageHeader> PageHeaders { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<QuoteRequest> QuoteRequests { get; set; }

    public virtual DbSet<Recomendation> Recomendations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockShortage> StockShortages { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<ViewCurrentStockShortage> ViewCurrentStockShortages { get; set; }

    public virtual DbSet<ViewNewOrder> ViewNewOrders { get; set; }

    public virtual DbSet<ViewNewQuoteRequest> ViewNewQuoteRequests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-HI49CHNN\\SQLNOWY;TrustServerCertificate=True;Integrated Security=True;Database=MakeAWishDBProd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CakeAllergen>(entity =>
        {
            entity.HasKey(e => e.CakeAllergenId).HasName("PK__CakeAlle__76ADC3D9FC6080B0");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CelebrationCake).WithMany(p => p.CakeAllergens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CakeAller__Celeb__02C769E9");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.CakeAllergens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CakeAller__Ingre__03BB8E22");
        });

        modelBuilder.Entity<CakeFilling>(entity =>
        {
            entity.HasKey(e => e.CakeFillingId).HasName("PK__CakeFill__71C3570A252C7797");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<CakeRecipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__CakeReci__FDD988D01E35817D");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trigger_CakeRecipes_Delete");
                    tb.HasTrigger("trigger_CakeRecipes_Delete_DeactivateAllergens");
                    tb.HasTrigger("trigger_CakeRecipes_Insert");
                    tb.HasTrigger("trigger_CakeRecipes_Insert_Allergens");
                    tb.HasTrigger("trigger_CakeRecipes_Update");
                    tb.HasTrigger("trigger_CakeRecipes_Update_Allergens");
                });

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CelebrationCake).WithMany(p => p.CakeRecipes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CakeRecip__Celeb__2BFE89A6");

            entity.HasOne(d => d.CelebrationCakeSize).WithMany(p => p.CakeRecipes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CakeRecip__Celeb__2CF2ADDF");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.CakeRecipes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CakeRecip__Ingre__2DE6D218");
        });

        modelBuilder.Entity<CelebrationCake>(entity =>
        {
            entity.HasKey(e => e.CelebrationCakeId).HasName("PK__Celebrat__545A5A522EB28E60");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trigger_DeleteProductBasedOnelebrationCakes");
                    tb.HasTrigger("trigger_InsertCelebrationCakeToProducts");
                    tb.HasTrigger("trigger_UpdateProductsBasedOnCelebrationCakes");
                });

            entity.HasOne(d => d.CakeFillingNavigation).WithMany(p => p.CelebrationCakes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Celebrati__CakeF__60A75C0F");
        });

        modelBuilder.Entity<CelebrationCakeSize>(entity =>
        {
            entity.HasKey(e => e.CelebrationCakeSizeId).HasName("PK__Celebrat__72BBF00B609C94D9");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ContactM__C87C037C2C85FD68");

            entity.Property(e => e.Customer).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsReplied).HasDefaultValue(false);
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.ContactMessages).HasConstraintName("FK__ContactMe__IsRep__09A971A2");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07512BC5E0");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B89F80E383");

            entity.Property(e => e.Country).HasDefaultValue(1);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsCompany).HasDefaultValue(false);
            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CountryNavigation).WithMany(p => p.CustomerCountryNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Count__5441852A");

            entity.HasOne(d => d.DeliveryCountryNavigation).WithMany(p => p.CustomerDeliveryCountryNavigations).HasConstraintName("FK_Customers_DeliveryCountry");

            entity.HasOne(d => d.DeliveryRegionNavigation).WithMany(p => p.CustomerDeliveryRegionNavigations).HasConstraintName("FK_Customers_DeliveryRegion");

            entity.HasOne(d => d.RegionNavigation).WithMany(p => p.CustomerRegionNavigations).HasConstraintName("FK__Customers__Regio__5535A963");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB27ABD481B7C");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.IsVegan).HasDefaultValue(true);

            entity.HasOne(d => d.Unit).WithMany(p => p.Ingredients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__UnitI__25518C17");
        });

        modelBuilder.Entity<IngredientReservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Ingredie__B7EE5F04A724586C");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CelebrationCakeSize).WithMany(p => p.IngredientReservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__Celeb__339FAB6E");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientReservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__Ingre__3493CFA7");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.IngredientReservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__IsCan__32AB8735");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD50FB24066");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            
            entity.Property(e => e.InvoiceNumber).HasComputedColumnSql("((((('FV/'+CONVERT([nvarchar],datepart(year,[InvoiceDate])))+'/')+right('0'+CONVERT([nvarchar],datepart(month,[InvoiceDate])),(2)))+'/')+CONVERT([nvarchar],[InvoiceID]))", true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
          



            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__OrderI__17F790F9");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__Paymen__18EBB532");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.Property(e => e.VatRate)
                .HasColumnName("VATRate")
                .HasColumnType("decimal(4,2)");

            entity.Property(e => e.LineNet)
    .HasComputedColumnSql("([Quantity]*[UnitPrice])", stored: true);

            entity.Property(e => e.LineVat)
                .HasComputedColumnSql("(([Quantity]*[UnitPrice])*[VATRate]/(100))", stored: true);

            entity.Property(e => e.LineGross)
                .HasComputedColumnSql("(([Quantity]*[UnitPrice])+(([Quantity]*[UnitPrice])*[VATRate]/(100)))", stored: true);

        });



        modelBuilder.Entity<LongArticle>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__LongArti__9C6270C8D1CD4465");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.PageHeader).WithMany(p => p.LongArticles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LongArtic__PageH__10566F31");
        });

        modelBuilder.Entity<MainPageArticle>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PK__MainPage__9C6270C8D011781F");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__Modules__2B7477879B416450");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ModuleAccess>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.ModuleId }).HasName("PK__ModuleAc__E84D8942AE4D833D");

            entity.Property(e => e.HasAccess).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleAccesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModuleAcc__Modul__43D61337");

            entity.HasOne(d => d.Role).WithMany(p => p.ModuleAccesses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModuleAcc__RoleI__42E1EEFE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF44AD5B3E");

            entity.ToTable(tb => tb.HasTrigger("trigger_AutoCancelIngredientReservations"));

            entity.Property(e => e.Customer).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPaid).HasDefaultValue(false);
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Customer__74AE54BC");

            entity.HasOne(d => d.ShopNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Shop__75A278F5");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Status__76969D2E");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A183952EF7");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.CelebrationCakeNavigation).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Celeb__7C4F7684");

            entity.HasOne(d => d.CelebrationCakeSizeNavigation).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Celeb__7B5B524B");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__7A672E12");
        });

        modelBuilder.Entity<OrderStep>(entity =>
        {
            entity.HasKey(e => e.OrderStepId).HasName("PK__OrderSte__64CE5BB1327EC3B7");
        });

        modelBuilder.Entity<PageHeader>(entity =>
        {
            entity.HasKey(e => e.PageHeaderId).HasName("PK__PageHead__B6F5E8091F76D526");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsVisible).HasDefaultValue(true);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F34F89F601");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED76A06646");

            entity.HasOne(d => d.CelebrationCake).WithMany(p => p.Products).HasConstraintName("FK_Products_CelebrationCakes");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Produc__5BE2A6F2");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECEE66C212BC");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<QuoteRequest>(entity =>
        {
            entity.HasKey(e => e.QuoteId).HasName("PK__QuoteReq__AF9688E17EDA8951");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Customer).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.QuoteRequests).HasConstraintName("FK__QuoteRequ__Custo__03F0984C");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.QuoteRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuoteRequ__Statu__02FC7413");
        });

        modelBuilder.Entity<Recomendation>(entity =>
        {
            entity.HasKey(e => e.RecomendationId).HasName("PK__Recomend__0EF51DC0B009ED34");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CelebrationCakeNavigation).WithOne(p => p.Recomendation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recomenda__Celeb__6477ECF3");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__Regions__ACD84443E2426DD2");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Regions__Country__4D94879B");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shops__67C55629D721EE63");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__Shopping__488B0B2AB87E3588");

            entity.Property(e => e.AddedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.CelebrationCake).WithMany(p => p.ShoppingCartItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__Celeb__4F47C5E3");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCartItems).HasConstraintName("FK__ShoppingC__UserI__4E53A1AA");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Statuses__C8EE206375D7C1E5");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock__2C83A9E28969CB2B");

            entity.ToTable("Stock", tb => tb.HasTrigger("tr_AutoRestockHandler_Multi"));

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.Stocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__Ingredien__29221CFB");
        });

        modelBuilder.Entity<StockShortage>(entity =>
        {
            entity.HasKey(e => e.ShortageId).HasName("PK__StockSho__6423218244215EAD");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsValid).HasDefaultValue(true);
            entity.Property(e => e.RecordedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.StockShortages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockShor__Ingre__395884C4");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.StockShortages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StockShor__Order__3A4CA8FD");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => e.TeamMemberId).HasName("PK__TeamMemb__C7C09285EC34D6D1");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5EC956539BDD0");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CCAC56D1F1EE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.UserAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAccou__UserR__498EEC8D");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3AD1017CAB");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ViewCurrentStockShortage>(entity =>
        {
            entity.ToView("view_CurrentStockShortages");
        });

        modelBuilder.Entity<ViewNewOrder>(entity =>
        {
            entity.ToView("view_NewOrders");
        });

        modelBuilder.Entity<ViewNewQuoteRequest>(entity =>
        {
            entity.ToView("view_NewQuoteRequests");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
