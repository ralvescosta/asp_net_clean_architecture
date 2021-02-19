namespace BookStore.Shared.Interfaces
{
    public interface IConfigurations
    {
        string JwtScrete { get; }

        int JwtExpiredHours { get; }
    }
}