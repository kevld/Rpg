using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rpg.Core.Components;
using Rpg.Core.Interfaces;
using Rpg.Core.Services.Interfaces;
using Rpg.EventsArgsModels;
using Rpg.Helpers;
using Rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Components
{
    public class InputComponent : Component, IInitializable
    {
        private readonly Dictionary<Keys, bool> _pressedKeys;
        private readonly IKeyboardService _keyboardService;
        private readonly IEntityService _entityService;

        public InputComponent(Entity owner, IKeyboardService keyboardService, IEntityService entityService) : base(owner)
        {
            _keyboardService = keyboardService;
            _entityService = entityService;
            _pressedKeys = new Dictionary<Keys, bool>();
            foreach (Keys k in KeyboardHelper.GetTriggerableKeys())
            {
                _pressedKeys.Add(k, false);
            }
        }

        public IReadOnlyList<Keys> GetPressedKeys()
        {
            return _pressedKeys
                .Where(x => x.Value)
                .Select(x => x.Key)
                .ToList()
                .AsReadOnly();
        }

        public override void Initialize()
        {
            _keyboardService.KeyboardEvent += HandleKeyboard;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 direction = Vector2.Zero;
            if (GetPressedKeys().Intersect([Keys.Z, Keys.Up]).Any())
                direction.Y -= 1;
            if (GetPressedKeys().Intersect([Keys.S, Keys.Down]).Any())
                direction.Y += 1;
            if (GetPressedKeys().Intersect([Keys.Q, Keys.Left]).Any())
                direction.X -= 1;
            if (GetPressedKeys().Intersect([Keys.D, Keys.Right]).Any())
                direction.X += 1;

            float speed = 50; // units/second
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 velocity = direction * 1.2f * speed * deltaTime;
            Vector2 velocityX = new Vector2(velocity.X, 0);
            Vector2 velocityY = new Vector2(0, velocity.Y);

            bool hasCollisionX = false;
            bool hasCollisionY = false;

            var collisionEntities = _entityService.GetEntities()
                .Where(x => x.Id != Owner.Id && x.Components.Any(x => typeof(CollisionComponent) == x.GetType()));

            foreach (var x in collisionEntities)
            {
                if (Owner.GetProjectionRectangle(velocityX).Intersects(x.GetRectangle()))
                {
                    hasCollisionX = true;
                }
                if (Owner.GetProjectionRectangle(velocityY).Intersects(x.GetRectangle()))
                {
                    hasCollisionY = true;
                }
            }

            if (!hasCollisionX)
            {
                Owner.WorldPosition += velocityX;
            }
            if (!hasCollisionY)
            {
                Owner.WorldPosition += velocityY;
            }
        }

        #region Private

        private void HandleKeyboard(object sender, EventArgs e)
        {
            KeyboardEventArgs k = e as KeyboardEventArgs;

            if (k != null)
            {
                _pressedKeys[k.Keys] = k.IsPressed;
            }
        }

        #endregion

        #region Dispose

        protected override void CleanIfDisposing()
        {
            _keyboardService.KeyboardEvent -= HandleKeyboard;
        }

        #endregion
    }
}
