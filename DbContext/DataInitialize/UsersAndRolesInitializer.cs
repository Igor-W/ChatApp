using ChatApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.DbContext.DataInitialize
{
    public static class UsersAndRolesInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("gmail").Result == null)
            {
                User user = new User();
                user.UserName = "alinuts";
                user.Email = "alin@gmail.com";
                user.FirstName = "Alin";
                user.LastName = "Apinzarasoaie";
                user.Gender = false;
                user.Position = "Administrator";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673115";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            if (userManager.FindByEmailAsync("gabi@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "gabi";
                user.Email = "gabi@gmail.com";
                user.FirstName = "Gabi";
                user.LastName = "Camen";
                user.Gender = false;
                user.Position = "Developer";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673111";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("lidia@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "lidia";
                user.Email = "lidia@gmail.com";
                user.FirstName = "Lidia";
                user.LastName = "Cotelnic";
                user.Gender = true;
                user.Position = "Developer";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673112";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("igor@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "igor";
                user.Email = "igor@gmail.com";
                user.FirstName = "Igor";
                user.LastName = "Stoian";
                user.Gender = false;
                user.Position = "Developer";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673113";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("diana@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "diana";
                user.Email = "diana@gmail.com";
                user.FirstName = "Diana";
                user.LastName = "Precup";
                user.Gender = true;
                user.Position = "Developer";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673114";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("stefania@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "stefania";
                user.Email = "stefania@gmail.com";
                user.FirstName = "Stefania";
                user.LastName = "Burtea";
                user.Gender = true;
                user.Position = "Developer";
                user.ProfileImage = "http://aras.kntu.ac.ir/wp-content/uploads/2019/05/hoodie-.png";
                user.PhoneNumber = "0748673116";
                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
