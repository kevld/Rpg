using DotTiled.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Rpg.Interfaces;
using Rpg.Models;
using Rpg.Scenes;
using Rpg.Services;
using System;

namespace Rpg.Managers
{
    public class SceneManager : DIManager, IInitializable, IDrawable, IUpdateable
    {
        private readonly IGraphicsService _graphicService;
        private readonly IContentService _contentService;
        private readonly GameServiceContainer _services;

        public DebugScene ActiveScene { get; private set; }

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();


        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public SceneManager(GameServiceContainer services) : base(services)
        {
            var graphicService = services.GetService<IGraphicsService>();
            _graphicService = graphicService;

            var contentService = services.GetService<IContentService>();
            _contentService = contentService;
            this._services = services;
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

        public void ChangeScene<TDebugScene>()
        {
            Type t = typeof(TDebugScene);

            if (Activator.CreateInstance(t, [_services]) is DebugScene scene)
            {
                ActiveScene = scene;
                ActiveScene.LoadMap();
                ActiveScene.Initialize();
            }
        }

        #endregion
    }
}
