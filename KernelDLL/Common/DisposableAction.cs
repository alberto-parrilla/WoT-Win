using System;

namespace KernelDLL.Common
{
    public class DisposableAction : IDisposable
    {        
        public static DisposableAction InvokeOnDispose(Action action)
        {        
            return new DisposableAction(action);
        }

        private readonly Action _action;

        private DisposableAction(Action action)
        {
            _action = action;
        }
     
        public void Dispose()
        {
            _action();
        }
    }
}
