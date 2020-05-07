namespace GTMY.IO
{
    public interface IReader : System.IDisposable
    {
        T Read<T>() where T : new();
    }
}