using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Core.Scenes;
using Rpg.Models;

namespace Rpg.Scenes
{
    public class DebugScene : BaseScene
    {
        public DebugScene(GameServiceContainer gameServiceContainer) : base(gameServiceContainer)
        {
            _name = "DebugScene";
        }

        public override void Initialize()
        {
            Vector2 playerPosition = new(250, 250);

            Entity player = _entityService.CreatePlayerEntity(x: (int)playerPosition.X, y: (int)playerPosition.Y, 15, 20, idTag: "localPlayer");
            _entityService.LocalPlayer = player;
            player.AddComponent(new InputComponent(player, _keyboardService, _entityService));
            player.AddComponent(new DirectionComponent(player));
            player.AddComponent(new InteractComponent(player, _graphicsService));


            base.Initialize();
        }
    }
}
