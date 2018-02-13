using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GrandeGift.Models;

namespace GrandeGift.Services
{
    public static class SeedHelper
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            DataService<Profile> profileDataService = new DataService<Profile>();
            DataService<Category> categoryDataService = new DataService<Category>();
            DataService<Hamper> hamperdataService = new DataService<Hamper>();

            //add admin role
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //add customer role
            if (await roleManager.FindByNameAsync("Customer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            //add Admin user
            if (await userManager.FindByNameAsync("Anna") == null)
            {
                IdentityUser userAdmin = new IdentityUser("Anna");
                await userManager.CreateAsync(userAdmin, "aaa");
                await userManager.AddToRoleAsync(userAdmin, "Admin");
                profileDataService.Create(new Profile { Username = "Anna" });
            }
            //add Customer user
            if (await userManager.FindByNameAsync("Joe") == null)
            {
                IdentityUser userCustomer = new IdentityUser("Joe");
                await userManager.CreateAsync(userCustomer, "jjj");
                await userManager.AddToRoleAsync(userCustomer, "Customer");
                profileDataService.Create(new Profile { Username = "Joe", FirstName = "Joe", LastName = "Doe" });
            }
            //add category samples
            if (categoryDataService.GetSingle(c => c.Name == "Christmas") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "Christmas",
                    Details = "Grande Gift provides the best collection for Corporate and Individual Christmas Hampers.  We streamline the ordering process and ensure that your Christmas Gifts are perfectly presented and delivered on time.",
                    Picture = "Anna/Xmascategory.jpg" });
            }
            if (categoryDataService.GetSingle(c => c.Name == "Baby Hampers") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "Baby Hampers",
                    Details = "Choose Grande Gift baby hamper for the most perfect baby shower hampers and gifts, maternity gifts and hampers, mother and baby hampers and baby gift baskets. Our baby hampers include some organic cosmetics and leading brands for newborn babies, the softest and sweetest toys and leading champagnes for Mum or Dad.",
                    Picture = "Anna/Babycategory.jpg"
                });
            }
            if (categoryDataService.GetSingle(c => c.Name == "For Her") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "For Her",
                    Details = "Grande Gifts has a wide range of Gifts for Her and Gift Hampers for Women. Whether it's for a good friend, your girlfriend or sister, gifts for wife, or gifts for mum. Grande Gifts has created a range of gifts and hampers for all ages, pamper hampers for her, birthday hampers for her, and exquisite gifts for her.",
                    Picture = "Anna/Hercategory.jpg"
                });
            }
            if (categoryDataService.GetSingle(c => c.Name == "For Him") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "For Him",
                    Details = "Grande Gift has designed a unique and luxurious range of Gifts and Hampers for Him. Grande Gift and Hampers for Him are designed to suit any occasion - anniversaries, birthdays, Father's day or congratulations. Hampers for him include deluxe body products, luxurious robes, to the finest wines and champagnes, chocolate hampers, gourmet hampers and beer.",
                    Picture = "Anna/Himcategory.jpg"
                });
            }
            if (categoryDataService.GetSingle(c => c.Name == "Birthday") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "Birthday",
                    Details = "Grande Gift hampers are the way to go when searching for the perfect birthday present for a work colleague, friend or loved one. Unleash your inner creativity with our wide range of birthday hampers suitable for both him and her.",
                    Picture = "Anna/Birthdaycategory.jpg"
                });
            }
            if (categoryDataService.GetSingle(c => c.Name == "Chocolate Hampers") == null)
            {
                categoryDataService.Create(new Category
                {
                    Name = "Chocolate Hampers",
                    Details = "Grande Gift famous chocolate hamper is the best gift for sympathy, birthday, anniversary or congratulations. Our luxurious chocolate hampers are divine to celebrate any special occasion.",
                    Picture = "Anna/Chocolatecategory.jpg"
                });
            }        

            //add hamper samples            
            if (hamperdataService.GetSingle(h => h.Name == "Luxury Baby Girl") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Baby Hampers").CategoryId,
                    Name = "Luxury Baby Girl",
                    Details = "some details will be here",
                    Price = 159,
                    Picture = "Anna/Babygirl.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "New arrival baby boy") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Baby Hampers").CategoryId,
                    Name = "New arrival baby boy",
                    Details = "Soft Toy Bunny 28cm high;Uimi Merino Wool Bassinet Blanket Blue;Uimi White Beanie Size 00;Uimi White Booties Size 00",
                    Price = 239,
                    Picture = "Anna/Babyboy.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Newborn") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Baby Hampers").CategoryId,
                    Name = "Newborn",
                    Details = "Soft Toy Bunny 28cm high;Aromababy Pure Baby Moisture Cream Aroma Free with Rosehip Oil 125ml;Aromababy Mother and Child Massage Oil 125ml;Aromababy Pure Baby Wash Aroma free with Rosehip Oil 150ml;Sophie the Giraffe",
                    Price = 149,
                    Picture = "Anna/Babyneutral.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Golden Prosecco") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "For Her").CategoryId,
                    Name = "Golden Prosecco",
                    Details = "Calappiano 18K Gold Prosecco 750ml;Cristina Re Lucille Tea Cup presented in a divine Gift Box;Voluspa Prosecco Bellini Hand Poured Coconut Wax Candle with a 100 hour burn time;OP Therapy Hand Creme with Emu Oil 150g",
                    Price = 199,
                    Picture = "Anna/Her.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Myrtle And Moss") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "For Her").CategoryId,
                    Name = "Myrtle And Moss",
                    Details = "Myrtle and Moss Mandarin, Lemon Myrtle and Orange Peel Essential Oil Diffuser 125ml;Myrtle and Moss Mandarin, Lemon Myrtle and Orange Peel Hand and Body Wash 500ml;Myrtle and Moss Mandarin, Lemon Myrtle and Orange Peel Hand Cream 75ml;Myrtle and Moss Mandarin, Lemon Myrtle and Orange Peel Soap 185g",
                    Price = 129,
                    Picture = "Anna/Her2.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Glenmorangie") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "For Him").CategoryId,
                    Name = "Glenmorangie",
                    Details = "Glenmorangie The Original Scotch Whisky;2 Riedel Whisky Glasses;Michel Cluizel Salted Caramel Chocolate Bar 100g;La Maison d'Armorine Salted Butter Caramels in a Wooden Box 50g;Dark Chocolate Macadamia Nuts 150g",
                    Price = 259,
                    Picture = "Anna/Him.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Cheery Christmas") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Christmas").CategoryId,
                    Name = "Cheery Christmas",
                    Details = "Bruny Island Rich Chocolate Fudge 95g;Charlie's Raspberry and White Chocolate Mini Melting Moments 125g;Simon Johnson Salted Caramel Wafers 80g;Paul and Pippa Organic Tomato and Oregano Biscuits 150g",
                    Price = 99,
                    Picture = "Anna/Xmas.jpg"
                });
            }     
            if (hamperdataService.GetSingle(h => h.Name == "Chocolate Treats") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Chocolate hampers").CategoryId,
                    Name = "Chocolate Treats",
                    Details = "Organic Times Chocolate Coated Macadamia Nuts 150g;Charlie's Choc Noir Mint Mini Melting Moments 125g;Paul and Pippa Cocoa Chips - Organic Cocoa and Salt Flakes 150g;Michel Cluizel Salted Caramel Chocolate Bar 100g / Valrhona Caramelia with Pearls Chocolate Bar 75g",
                    Price = 89,
                    Picture = "Anna/Chocolatetreats.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Brownie Hamper") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Chocolate hampers").CategoryId,
                    Name = "Brownie Hamper",
                    Details = "some details will be here",
                    Price = 149,
                    Picture = "Anna/Chocolatebrownie.jpg"
                });
            }
            if (hamperdataService.GetSingle(h => h.Name == "Champange and Chocolate") == null)
            {
                hamperdataService.Create(new Hamper
                {
                    CategoryId = categoryDataService.GetSingle(c => c.Name == "Birthday").CategoryId,
                    Name = "Champange and Chocolate",
                    Details = "some details will be here",
                    Price = 149,
                    Picture = "Anna/Birthday.jpg"
                });
            }       
        }
    }
}
