using Microsoft.Xna.Framework;
using Rpg.Core.Interfaces;
using Rpg.Core.System;
using Rpg.Models;
using System;

namespace Rpg.Core.Components
{
    public abstract class BaseComponent : DisposableObject, IInitializable, IUpdatable
    {
        private readonly Entity _owner;

        public Entity Owner => _owner;

        protected BaseComponent(Entity owner)
        {
            ArgumentNullException.ThrowIfNull(owner);

            _owner = owner;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
