using Onyx.Domain.Entities;

namespace Onyx.Data.DBContexts
{
    public static class ProductDBContextDBSeedData
    {
        static object synchlock = new object();
        static volatile bool seeded = false;

        public static void EnsureSeedData(this ProductContext context)
        {
            if (!seeded && context.Products.Count()==0)
            {
                lock (synchlock)
                {
                    if (!seeded)
                    {
                        context.Products.AddRange(
                            new ProductEntity { Id = 1, Name = "Product A", Color = "Red" },
                            new ProductEntity { Id = 2, Name = "Product B", Color = "Blue" },
                            new ProductEntity { Id = 3, Name = "Product C", Color = "White" },
                            new ProductEntity { Id = 4, Name = "Product D", Color = "Blue" },
                            new ProductEntity { Id = 5, Name = "Product E", Color = "Violet" },
                            new ProductEntity { Id = 6, Name = "Product F", Color = "Yellow" },
                            new ProductEntity { Id = 7, Name = "Product G", Color = "White" },
                            new ProductEntity { Id = 8, Name = "Product H", Color = "White" },
                            new ProductEntity { Id = 9, Name = "Product I", Color = "Black" },
                            new ProductEntity { Id = 10, Name = "Product I", Color = "Blue" },
                            new ProductEntity { Id = 11, Name = "Product I", Color = "White" },
                            new ProductEntity { Id = 12, Name = "Product I", Color = "Green" }
                            );
                        context.SaveChanges();
                        seeded = true;
                    }
                }
            }
        }
    }
}
