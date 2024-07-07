namespace Inventorium.API.Data
{
    public static class Extensions
    {
        // This method triggers the initializer method which will seed data if database is empty
        public static void CreateDbIfNotExists(this IHost host)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<InventoriumDbContext>();
                    context.Database.EnsureCreated(); // after verifying database exist, continue
                    DbInitializer.Initialize(context);
                }
            }
        }
    }
}
