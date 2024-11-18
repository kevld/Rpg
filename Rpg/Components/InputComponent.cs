using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rpg.EventsArgsModels;
using Rpg.Helpers;
using Rpg.Interfaces;
using Rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Components
{
    public class InputComponent : Component, IInitializable, IDisposable
    {
        private List<Keys> intentions;

        private readonly IDictionary<Keys, bool> _pressedKeys;
        private readonly IKeyboardService _keyboardService;
        private readonly IEntityService _entityService;
        private bool _disposed;

        public InputComponent(Entity owner, IKeyboardService keyboardService, IEntityService entityService) : base(owner)
        {
            _keyboardService = keyboardService;
            _entityService = entityService;
            _pressedKeys = new Dictionary<Keys, bool>();
            foreach (Keys k in KeyboardHelper.TriggerableKeys)
            {
                _pressedKeys.Add(k, false);
            }

            intentions = new List<Keys>();
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
            if (GetPressedKeys().Intersect(new List<Keys>() { Keys.Z, Keys.Up }).Any())
                direction.Y -= 1;
            if (GetPressedKeys().Intersect(new List<Keys>() { Keys.S, Keys.Down }).Any())
                direction.Y += 1;
            if (GetPressedKeys().Intersect(new List<Keys>() { Keys.Q, Keys.Left }).Any())
                direction.X -= 1;
            if (GetPressedKeys().Intersect(new List<Keys>() { Keys.D, Keys.Right }).Any())
                direction.X += 1;

            float speed = 50; // units/second
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 velocity = direction * 1.2f * speed * deltaTime;

            bool hasCollision = false;
            Vector2 oldPosition = Owner.WorldPosition;

            //TODO:Handle collisions
            _entityService.GetEntities()
                .Where(x => x.Id != Owner.Id && x.Components.Any(x => typeof(CollisionComponent) == x.GetType()))
                .ToList()
                .ForEach(x =>
                {
                    if (Owner.GetProjectionRectangle(velocity).Intersects(x.GetRectangle()))
                    {
                        hasCollision = true;
                    }
                });

            if (!hasCollision)
            {
                Owner.WorldPosition += velocity;
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

        ~InputComponent() => Dispose();

        public void Dispose()
        {
            _keyboardService.KeyboardEvent -= HandleKeyboard;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
