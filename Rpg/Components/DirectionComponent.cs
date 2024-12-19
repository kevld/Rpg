using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rpg.Core.Components;
using Rpg.Core.Enums;
using Rpg.Core.Extensions;
using Rpg.Models;
using System;
using System.Linq;

namespace Rpg.Components
{
    public class DirectionComponent(Entity owner) : BaseComponent(owner)
    {
        public DirectionType Direction { get; private set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 direction = Vector2.Zero;

            var inputComponent = Owner.GetComponent<InputComponent>();
            if (inputComponent != null)
            {
                if (inputComponent.GetPressedKeys().Intersect([Keys.Z, Keys.Up]).Any()) // Up
                    direction.Y = (int)DirectionType.Up;
                if (inputComponent.GetPressedKeys().Intersect([Keys.S, Keys.Down]).Any()) // Down
                    direction.Y = (int)DirectionType.Down;
                if (inputComponent.GetPressedKeys().Intersect([Keys.Q, Keys.Left]).Any()) // Left
                    direction.X = (int)DirectionType.Left;
                if (inputComponent.GetPressedKeys().Intersect([Keys.D, Keys.Right]).Any()) // Right
                    direction.X = (int)DirectionType.Right;

                int calculatedMovement = direction.GetXY_AddedAsInt();

                Direction = (DirectionType)calculatedMovement;
            }
        }
    }
}
