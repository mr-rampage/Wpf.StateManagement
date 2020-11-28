using System.Windows;

namespace StateManagement
{
    public delegate void DispatchEventHandler(object sender, FluxAction e);

    public class FluxAction : RoutedEventArgs
    {
        internal static readonly RoutedEvent DispatchEvent = EventManager.RegisterRoutedEvent(nameof(FluxAction),
            RoutingStrategy.Bubble, typeof(DispatchEventHandler), typeof(FluxAction));

        protected FluxAction() : base(DispatchEvent)
        {
        }
    }
}