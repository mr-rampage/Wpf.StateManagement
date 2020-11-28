using System.ComponentModel;

namespace StateManagement
{
    public interface IStore<out T> : INotifyPropertyChanged
    {
        T State { get; }
        void Dispatch(FluxAction action);
    }
}