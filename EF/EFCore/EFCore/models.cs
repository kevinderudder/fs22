using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace training.models
{
    //[Table("BlogTable", Schema = "BlogBC")]
    public class Blog
    {
        public Blog(int blogId, string name)
        {
            BlogId = blogId;
            Name = name;
        }
        //[Column("BlogIdentifier")]
        public int BlogId { get; private set; }
        public string Name { get; private set; }

        // navig. property
        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public Post(int postId, string title, string content, int blogId)
        {
            PostId = postId;
            BlogId = blogId;
            Title = title;
            Content = content;
        }
        public int PostId { get; private set; }

        //[MaxLength(100)]
        public string Title { get; private set; }
        public string Content { get; private set; }

        public int BlogId { get; private set; }

        // navig. property
        public virtual Blog Blog { get; set; }
    }
}