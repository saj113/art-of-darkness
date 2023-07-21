namespace Core
{
    public interface IInstanceContainer
    {
        void Set<T>(T instance);
        T Resolve<T>();
    }
}
