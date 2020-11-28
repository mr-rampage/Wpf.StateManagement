using System;
using System.Windows;

namespace StateManagement
{
    public static class Enhancer
    {
        public static Action Enhance(this IInputElement element, DispatchEventHandler handler)
        {
            element.AddHandler(FluxAction.DispatchEvent, handler);
            return () => element.RemoveHandler(FluxAction.DispatchEvent, handler);
        } 
    }
}