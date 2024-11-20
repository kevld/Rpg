using System;

namespace Rpg.Core.System
{
    public abstract class DisposableObject : IDisposable
    {
        private bool disposedValue;

        protected virtual void CleanIfDisposing() { }
        protected virtual void Clean() { }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CleanIfDisposing();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
