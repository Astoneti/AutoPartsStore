using Godel.AutoPartsStore.Common.Models;
using Godel.AutoPartsStore.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Godel.AutoPartsStore.PartsStore.Initialize
{
    public static class DbInitializer 
    {
        public static void Initialize(DAL.PartsStoreDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Parts.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                 new Category { Name="Motor oils"},
                 new Category { Name="Breaking fluids"},
                 new Category { Name="Engine and accessories"},
                 new Category { Name="Brake system"},
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var parts = new Part[]
            {
                new Part {CategoryId=1,Name="Motor oil Shell Ultra-5W30",PartNumber=1,Price=13,Quantity=13,IsActive=true},
                new Part {CategoryId=2,Name="Breaking fluid Shell-Dot 4",PartNumber=2,Price=5,Quantity=2,IsActive=true},
                new Part {CategoryId=3,Name="Spark plugs-NGK Quick Range",PartNumber=3,Price=7,Quantity=3,IsActive=false},
                new Part {CategoryId=3,Name="Spark plugs-Bosch Super",PartNumber=4,Price=8,Quantity=2,IsActive=true},
                new Part {CategoryId=3,Name="Spark plugs-Bosch Super Plus",PartNumber=5,Price=9,Quantity=4,IsActive=false},
                new Part {CategoryId=3,Name="Spark plugs-Valeo-Silver",PartNumber=6,Price=5,Quantity=1,IsActive=true},
                new Part {CategoryId=3,Name="Belt drive-Bosch Universal",PartNumber=7,Price=10,Quantity=16,IsActive=false},
                new Part {CategoryId=3,Name="Belt drive-NSK Premium",PartNumber=8,Price=13,Quantity=11,IsActive=true},
                new Part {CategoryId=3,Name="Tension roller-SKF",PartNumber=9,Price=14,Quantity=10,IsActive=true},
                new Part {CategoryId=4,Name="Brake pads-Bosch Premium",PartNumber=10,Price=25,Quantity=8,IsActive=false},
                new Part {CategoryId=4,Name="Brake pads-Brembo Ultra Sport",PartNumber=11,Price=28,Quantity=6,IsActive=true},
                new Part {CategoryId=4,Name="Brake discs-Brembo Sport",PartNumber=12,Price=130,Quantity=30,IsActive=true},
            };

            foreach (Part p in parts)
            {
                context.Parts.Add(p);
            }
            context.SaveChanges();

            var users = new List<User>
            {
                 new User { Email ="a.pashkun@godeltech.com",Password = "12345678"},
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var roles = new Role[]
           
            {
                 new Role {Name="admin",Users=users},
                 new Role {Name="user",Users=null},
            };

            foreach (Role r in roles)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();
        }
    }
}
