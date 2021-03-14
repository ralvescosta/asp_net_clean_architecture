using FluentMigrator;

namespace BookStore.Infrastructure.Migration
{
    [Migration(1615595755301)]
    public class AddAuthorsTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("authors")
                .WithColumn("Id")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("Guid")
                    .AsString(36)
                    .NotNullable()
                .WithColumn("FirstName")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("LastName")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("Description")
                    .AsString(255)
                    .NotNullable()
                .WithColumn("CreatedAt")
                    .AsDateTime()
                    .NotNullable()
                .WithColumn("UpdatedAt")
                    .AsDateTime()
                    .NotNullable()
                .WithColumn("DeletedAt")
                    .AsDateTime()
                    .Nullable();
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
