using Microsoft.Xna.Framework;
using Rpg.Interfaces;
using Rpg.Models;
using System;

namespace Rpg.Components
{
    public class Component : IInitializable, IUpdateable
    {
        private readonly Entity _owner;

        public Entity Owner => _owner;

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public Component(Entity owner)
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
