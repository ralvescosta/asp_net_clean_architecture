using FluentMigrator;
using System.Data;

namespace BookStore.Infrastructure.Migration
{
    [Migration(1615596989182)]
    public class AddUsersBooksTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("users_books")
                .WithColumn("Id")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                .WithColumn("Guid")
                    .AsString(36)
                    .NotNullable()
                .WithColumn("UserId")
                    .AsInt64()
                    .ForeignKey("users", "Id")
                    .OnDelete(Rule.Cascade)
                    .OnUpdate(Rule.Cascade)
                    .NotNullable()
                .WithColumn("BookId")
                    .AsInt64()
                    .ForeignKey("books", "Id")
                    .OnDelete(Rule.Cascade)
                    .OnUpdate(Rule.Cascade)
                    .NotNullable()
                .WithColumn("ExpiredAt")
                    .AsDateTime()
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
            Delete.Table("users_books");
        }
    }
}
