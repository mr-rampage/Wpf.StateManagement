using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StateManagement.Test
{
    [TestClass]
    public class StateManagementTest
    {
        [TestMethod]
        public void ShouldReceiveAllFluxActions()
        {
            var fixture = new StoreRecorder();
            var dispatcher = new ContentControl();
            var provider = new ContentControl {Content = dispatcher};

            provider.ProvideStore(fixture);

            var fluxActions = new List<FluxAction>
                {new Action1(), new Action1(), new Action2(), new Action3(), new Action1()};
            foreach (var fluxAction in fluxActions)
            {
                dispatcher.Dispatch(fluxAction);
            }

            Assert.IsTrue(fixture.State.SequenceEqual(fluxActions));
        }

        [TestMethod]
        public void ShouldEnhanceAllFluxActions()
        {
            var fixture = new StoreRecorder();
            var dispatcher = new ContentControl();
            var enhancer = new ContentControl {Content = dispatcher};
            var provider = new ContentControl {Content = enhancer};

            enhancer.Enhance((sender, action) =>
            {
                if (!Equals(action.OriginalSource, dispatcher)) return;

                action.Handled = true;
                enhancer.Dispatch(new Action3());
            });

            provider.ProvideStore(fixture);

            var fluxActions = new List<FluxAction> {new Action1(), new Action1(), new Action2(), new Action3(), new Action1()};
            foreach (var fluxAction in fluxActions)
            {
                dispatcher.Dispatch(fluxAction);
            }

            Assert.IsTrue(fixture.State.All(action => action is Action3));
        }


        private sealed class Action1 : FluxAction
        {
        }

        private sealed class Action2 : FluxAction
        {
        }

        private sealed class Action3 : FluxAction
        {
        }
    }
}