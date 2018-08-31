namespace RP.Data.Core
{
    public interface IContext
    {
        void SaveChanges();

        void Dispose();
    }
}
