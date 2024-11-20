using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Core.Scenes;
using Rpg.Models;

namespace Rpg.Scenes
{
    public class DebugScene2 : BaseScene
    {

        public DebugScene2(GameServiceContainer gameServiceContainer) : base(gameServiceContainer)
        {
            _name = "DebugScene2";
        }

        public override void Initialize()
        {
            Vector2 playerPosition = new(350, 350);

            Entity player = _entityService.CreatePlayerEntity(x: (int)playerPosition.X, y: (int)playerPosition.Y, 15, 20, idTag: "localPlayer");
            _entityService.LocalPlayer = player;
            player.AddComponent(new InputComponent(player, _keyboardService, _entityService));

            base.Initialize();
        }
    }
}
