namespace BookStore.Application.Interfaces
{
    public interface IHasher
    {
        string Hashe(string value);

        bool CompareHashe(string real, string digest);
    }
}
