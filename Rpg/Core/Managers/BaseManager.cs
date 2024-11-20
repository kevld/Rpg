using Microsoft.Xna.Framework;
using Rpg.Core.System;
using System;

namespace Rpg.Core.Managers
{
    public abstract class BaseManager : DisposableObject
    {

        protected GameServiceContainer _services;

        protected BaseManager(GameServiceContainer gameServiceContainser)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(gameServiceContainser));
            _services = gameServiceContainser;
        }
    }
}
