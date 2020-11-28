namespace StateManagement
{
    public delegate T Reducer<T>(in T state, in FluxAction action);

    public interface IReducer<T>
    {
        Reducer<T> Reduce { get; }
    }
}