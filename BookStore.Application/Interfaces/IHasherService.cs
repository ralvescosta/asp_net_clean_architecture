namespace BookStore.Application.Interfaces
{
    public interface IHasherService
    {
        string Hashe(string value);

        bool CompareHashe(string real, string digest);
    }
}
