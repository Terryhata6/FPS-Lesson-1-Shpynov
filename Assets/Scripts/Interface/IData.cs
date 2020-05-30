namespace Game
{
    public interface IData<T>
    {
        void Save(T obj, string path = null);
        T Load(string path = null);
    }
}
