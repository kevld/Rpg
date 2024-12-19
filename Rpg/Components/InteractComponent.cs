using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Rpg.Core.Components;
using Rpg.Core.Services.Interfaces;
using Rpg.Models;

namespace Rpg.Components
{
    public class InteractComponent : BaseComponent
    {
        private readonly IGraphicsService _graphicsService;

        public Rectangle InterationZone { get; private set; }

        private readonly int _izWidth = 32;
        private readonly int _izHeight = 32;


        public InteractComponent(Entity owner, IGraphicsService graphicsService) : base(owner)
        {
            _graphicsService = graphicsService;
        }

        public override void Initialize()
        {
            base.Initialize();

            InterationZone = Rectangle.Empty;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var directionComponent = Owner.GetComponent<DirectionComponent>();
            if (directionComponent != null)
            {
                switch (directionComponent.Direction)
                {
                    case Core.Enums.DirectionType.Up:
                        InterationZone = new Rectangle((int)(Owner.WorldPosition.X - _izWidth / 2 + Owner.Size.X / 2), (int)(Owner.WorldPosition.Y - _izHeight), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.Down:
                        InterationZone = new Rectangle((int)(Owner.WorldPosition.X - _izWidth / 2 + Owner.Size.X / 2), (int)(Owner.WorldPosition.Y + Owner.Size.Y), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.Left:
                        InterationZone = new Rectangle((int)Owner.WorldPosition.X - _izWidth, (int)(Owner.WorldPosition.Y - _izHeight / 2 + Owner.Size.Y / 2), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.Right:
                        InterationZone = new Rectangle((int)(Owner.WorldPosition.X + Owner.Size.X), (int)(Owner.WorldPosition.Y - _izHeight / 2 + Owner.Size.Y / 2), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.UpLeft:
                        InterationZone = new Rectangle((int)Owner.WorldPosition.X - _izWidth, (int)(Owner.WorldPosition.Y - _izHeight), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.DownLeft:
                        InterationZone = new Rectangle((int)Owner.WorldPosition.X - _izWidth, (int)(Owner.WorldPosition.Y + Owner.Size.Y), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.UpRight:
                        InterationZone = new Rectangle((int)(Owner.WorldPosition.X + Owner.Size.X), (int)(Owner.WorldPosition.Y - _izHeight), _izWidth, _izHeight);
                        break;
                    case Core.Enums.DirectionType.DownRight:
                        InterationZone = new Rectangle((int)(Owner.WorldPosition.X + Owner.Size.X), (int)(Owner.WorldPosition.Y + Owner.Size.Y), _izWidth, _izHeight);
                        break;
                    default:
                        
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (InterationZone != Rectangle.Empty)
            {
                _graphicsService.SpriteBatch.DrawRectangle(InterationZone, Color.Black, 2, 0);
            }
        }
    }
}
