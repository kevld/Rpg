using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rpg.Core.Services.Interfaces;
using Rpg.Managers;
using Rpg.Scenes;
using Rpg.Services;
using System.Diagnostics.CodeAnalysis;

namespace Rpg
{
    [ExcludeFromCodeCoverage]
    public class Game1 : Game
    {
        private readonly GraphicsService _graphicsService;
        private readonly ContentService _contentService;
        private readonly KeyboardService _keyboardService;
        private readonly EntityService _entityService;
        private readonly ConfigService _configService;

        private readonly string _configFilePath = "Settings.json";

        private SceneManager SceneManager { get; set; }
        private KeyboardManager KeyboardManager { get; set; }

        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphicsService = new GraphicsService(new GraphicsDeviceManager(this));
            _contentService = new ContentService();
            _keyboardService = new KeyboardService();
            _entityService = new EntityService();
            _configService = new ConfigService(_configFilePath);
        }

        protected override void Initialize()
        {
            base.Initialize();

            InitializeGraphics();
            InitializeContentService();

            RegisterServices();

            RegisterManagers();

            SceneManager.Initialize();
            SceneManager.ChangeScene<DebugScene>();
        }

        protected override void LoadContent()
        {

            _graphicsService.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SceneManager.Update(gameTime);
            KeyboardManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        #region private

        #region InitServices

        private void InitializeGraphics()
        {
            _graphicsService.GraphicsDevice = GraphicsDevice;
            _graphicsService.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _graphicsService.SceneRenderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents
            );
            _graphicsService.Window = Window;
        }

        private void InitializeContentService()
        {
            _contentService.ContentManager = Content;
        }

        #endregion

        #region Registers

        private void RegisterServices()
        {
            Services.AddService<IGraphicsService>(_graphicsService);
            Services.AddService<IContentService>(_contentService);
            Services.AddService<IKeyboardService>(_keyboardService);
            Services.AddService<IEntityService>(_entityService);
            Services.AddService<IConfigService>(_configService);
        }

        private void RegisterManagers()
        {
            SceneManager = new SceneManager(Services);
            KeyboardManager = new KeyboardManager(Services);
        }

        #endregion

        #endregion
    }
}
