using FluentMigrator;

namespace BookStore.Infrastructure.Migration
{
    [Migration(20201203071900)]
    public class AddUserTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Guid").AsString(36).NotNullable()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("LastName").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("Permission").AsInt64().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().NotNullable()
                .WithColumn("DeletedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
