using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using training.models;
using training.repository;

namespace training.application
{
    static class Program
    {
        static void Main(string[] args)
        {
            string dbConnString = "Server=localhost;Initial Catalog=Training;Integrated Security=True";
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(dbConnString);
            //optionsBuilder.UseInMemoryDatabase("training");

            using (var db = new BloggingContext(optionsBuilder.Options))
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                for (int i = 1; i <= 5; i++)
                { 
                    int blogId = db.Blogs.Count() + 1;
                    var blog = new Blog(blogId, string.Format("{0}{1}", name, blogId));
                    /*
                    var blogpost = new Post(blogId + 1, "post", "test", blogId);
                    blog.Posts = new List<Post>();
                    blog.Posts.Add(blogpost);
                    */
                    db.Blogs.Add(blog);
                    db.SaveChanges();
                }
            }

            using (var db = new BloggingContext(optionsBuilder.Options))
            {
                // Display all Blogs from the database
                /*
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;
                */

                var query = db.Blogs.OrderBy(x => x.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
