using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Core.Interfaces;
using Rpg.Core.Managers;
using Rpg.Core.Scenes;
using Rpg.Core.Services.Interfaces;
using System;

namespace Rpg.Managers
{
    public class SceneManager : BaseManager, IInitializable, Core.Interfaces.IDrawable, IUpdatable
    {
        private readonly IGraphicsService _graphicService;
        private readonly IEntityService _entityService;

        public BaseScene ActiveScene { get; private set; }

        public SceneManager(GameServiceContainer services) : base(services)
        {
            _graphicService = services.GetService<IGraphicsService>();
            _entityService = services.GetService<IEntityService>();
        }

        #region public

        public void Initialize()
        {
            _graphicService.SceneRenderTarget ??= new RenderTarget2D(
                    _graphicService.GraphicsDevice,
                    _graphicService.GraphicsDevice.PresentationParameters.BackBufferWidth,
                    _graphicService.GraphicsDevice.PresentationParameters.BackBufferHeight,
                    false,
                    _graphicService.GraphicsDevice.PresentationParameters.BackBufferFormat,
                    DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents
                );
        }

        public void Draw(GameTime gameTime)
        {

            if (ActiveScene == null)
                return;

            _graphicService.GraphicsDevice.Clear(Color.Black);
            _graphicService.GraphicsDevice.SetRenderTarget(_graphicService.SceneRenderTarget);

            ActiveScene.Draw(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            if (ActiveScene == null)
                return;

            ActiveScene.Update(gameTime);
        }

        public void ChangeScene<T>()
        {
            _entityService.ClearEntities();

            Type t = typeof(T);

            if (Activator.CreateInstance(t, [_services]) is BaseScene scene)
            {
                ActiveScene = scene;
                ActiveScene.LoadMap();
                ActiveScene.Initialize();
            }
        }

        #endregion
    }
}
