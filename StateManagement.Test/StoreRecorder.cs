using System.Collections.Generic;
using System.ComponentModel;

namespace StateManagement.Test
{
    public sealed class StoreRecorder: IStore<IList<FluxAction>>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public IList<FluxAction> State { get; } = new List<FluxAction>();
        public void Dispatch(FluxAction action)
        {
            State.Add(action);
        }
    }
}