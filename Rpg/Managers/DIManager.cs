using Microsoft.Xna.Framework;
using System;

namespace Rpg.Managers
{
    public abstract class DIManager : IDisposable
    {
        private bool _disposedValue;

        protected GameServiceContainer _services;

        ~DIManager() => Dispose();

        protected DIManager(GameServiceContainer services)
        {
            if (services == null)
                throw new ArgumentNullException("GameServiceContainser is null");

            _services = services;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
