using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models {
    public static class StoreDbContextExtensions {
        public static void CreateSeedData (this StoreAppContext context) {
            if (context.Database.GetMigrations ().Count () > 0 &&
                context.Database.GetPendingMigrations ().Count () == 0 &&
                context.Products.Count () == 0) {
                var s1 = new Supplier {
                Name = "Splash Dudes",
                City = "San Joes", State = "CA"
                };
                var s2 = new Supplier {
                    Name = "Soccer Town",
                    City = "Chicago", State = "IL"
                };
                var s3 = new Supplier {
                    Name = "Chess Co",
                    City = "New York", State = "NyY"
                };

                context.AddRange (
                    new Product {
                        Name = "Kayak",
                            Description = "A bat for one person",
                            Category = "Watersports",
                            Price = 275,
                            Supplier = s1,
                            Ratings = new List<Rating> {
                                new Rating { Stars = 4 },
                                new Rating { Stars = 3 }
                            }
                    },
                    new Product {
                        Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            Category = "Watersports",
                            Price = 48.95m,
                            Supplier = s1,
                            Ratings = new List<Rating> {
                                new Rating { Stars = 2 },
                                new Rating { Stars = 5 }
                            }
                    },
                    new Product {
                        Name = "Bling-Bling King",
                            Description = "Gold-plated, diamond-studded King",
                            Category = "Chess",
                            Price = 1200,
                            Supplier = s3
                    });
                context.SaveChanges ();

            }

        }
    }
}