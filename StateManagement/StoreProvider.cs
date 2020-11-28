using System;
using System.Windows;

namespace StateManagement
{
    public static class StoreProvider
    {
        public static Action ProvideStore<T>(this IInputElement storeProvider, in IStore<T> store)
        {
            var dispatcher = CreateDispatcher(store);
            var removeHandler = storeProvider.Enhance(dispatcher);
            return () => removeHandler();
        }

        private static DispatchEventHandler CreateDispatcher<T>(IStore<T> store)
        {
            return delegate(object sender, FluxAction action)
            {
                store.Dispatch(action);
            };
        } 
    }
}