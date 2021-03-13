using FluentMigrator;

namespace BookStore.Infrastructure.Migration
{
    [Migration(1615595977935)]
    public class AddBooksTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("books")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Guid").AsString(36).NotNullable()
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("Subtitle").AsString(255).Nullable()
                .WithColumn("Subject").AsString(255).NotNullable()
                .WithColumn("AuthorId").AsInt64().ForeignKey("authors","Id").NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().NotNullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("books");
        }
    }
}
