using System.Windows;

namespace StateManagement
{
    public static class Dispatcher
    {
        public static void Dispatch(this IInputElement element, FluxAction action)
        {
            element.RaiseEvent(action);
        }
    }
}