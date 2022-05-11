using System.Collections.Generic;
using System.Linq;
using Domain.Task;
using Infrastructure.Auth;
using TaskManager.Auth;

namespace TaskManager
{
    public class DbSeed
    {
        internal static void SeedDb()
        {
            #region Create Admin user
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                var adminUser = context.Users.FirstOrDefault(e => e.Username == "admin");
                if (adminUser == null)
                {
                    context.Users.Add(new User
                    {
                        Password = "password",
                        Role = UserRoles.ROLE_ADMIN,
                        Username = "admin"
                    });

                    context.SaveChanges();
                }   
            }
            #endregion

            #region Create dummy task
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                var user = context.Users.First();
                var tasks = context.Tasks.Select(x => x).ToList();

                if (tasks.Count() == 0)
                {
                    context.Tasks.Add(new Todo
                    {
                        Title = "Dummy Task",
                        Description = "This task will only be generated in development mode and only if no tasks exists in database.",
                        CreatedBy = user,
                        Comments = new List<TodoComment>
                        {
                            new TodoComment
                            {
                                Text = "This is a dummy comment.",
                                CreatedBy = user
                            }
                        }
                    });

                    context.SaveChanges();
                }
            }
            #endregion
        }
    }
}
