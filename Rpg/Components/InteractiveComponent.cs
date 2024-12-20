using Microsoft.Xna.Framework;
using Rpg.Core.Components;
using Rpg.Core.Services.Interfaces;
using Rpg.Models;

namespace Rpg.Components
{
    public class InteractiveComponent(Entity owner, IGraphicsService graphicsService) : BaseComponent(owner)
    {
        protected readonly IGraphicsService _graphicsService = graphicsService;

        public Rectangle InteractiveZone { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            InteractiveZone = Owner.GetRectangle();
        }


        public virtual void Enable()
        {

        }
    }
}
