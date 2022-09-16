using FluentMigrator;
using FluentMigrator.Runner.Initialization;
using System.Linq;

namespace migrations
{
    [Migration(1)]
#pragma warning disable S101 // Types should be named in PascalCase
    public class ManageDatabase_V1 : Migration
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public override void Up()
        {
            Create.Table("Blogs")
                .WithColumn("BlogId").AsInt32().NotNullable().Unique().PrimaryKey()
                .WithColumn("Name").AsString(100);

            Create.Table("Posts")
                .WithColumn("PostId").AsInt32().NotNullable().Unique().PrimaryKey()
                .WithColumn("BlogId").AsInt32().NotNullable()
                .ForeignKey("FK_Blogs", "Blogs", "BlogId")
                .WithColumn("Content").AsString()
                .WithColumn("Title").AsString(100);
        }

        public override void Down()
        {
            Delete.Table("Posts");
            Delete.Table("Blogs");
        }
    }

    [Migration(2)]
#pragma warning disable S101 // Types should be named in PascalCase
    public class ManageDatabase_V2 : Migration
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public override void Up()
        {
            Create.Schema("BlogBC");

            Create.Table("BlogTable").InSchema("BlogBC")
                .WithColumn("BlogIdentifier").AsInt32().NotNullable().Unique().PrimaryKey()
                .WithColumn("Name").AsString(100);
        }

        public override void Down()
        {
            Delete.Table("BlogTable").InSchema("BlogBC");
            Delete.Schema("BlogBC");
        }
    }

    [Migration(3)]
#pragma warning disable S101 // Types should be named in PascalCase
    public class ManageDatabase_V3 : Migration
#pragma warning restore S101 // Types should be named in PascalCase
    {
        public override void Up()
        {
            Create.Schema("ModelSchema");

            Create.Table("ModelBlog").InSchema("ModelSchema")
                .WithColumn("CustomBlogIdentifier").AsInt32().NotNullable().Unique().PrimaryKey()
                .WithColumn("Name").AsString(100);
        }

        public override void Down()
        {
            Delete.Table("ModelBlog").InSchema("ModelSchema");
            Delete.Schema("ModelSchema");
        }
    }
}