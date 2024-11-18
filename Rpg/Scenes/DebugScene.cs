using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Rpg.Interfaces;
using Rpg.Models;
using System;
using System.Linq;
using Rpg.Helpers;
using Rpg.Exceptions;
using Rpg.Components;
using System.Collections.Generic;

namespace Rpg.Scenes
{
    public class DebugScene(GameServiceContainer gameServiceContainer) : IDisposable, IInitializable, IDrawable, IUpdateable
    {
        private readonly string _name = "DebugScene";
        private readonly Color _backgroundColor = Color.Black;
        private readonly IContentService _contentService = gameServiceContainer.GetService<IContentService>();
        private readonly IGraphicsService _graphicsService = gameServiceContainer.GetService<IGraphicsService>();
        private readonly IEntityService _entityService = gameServiceContainer.GetService<IEntityService>();
        private readonly IKeyboardService _keyboardService = gameServiceContainer.GetService<IKeyboardService>();
        private readonly IConfigService _configService = gameServiceContainer.GetService<IConfigService>();

        private bool _disposed;

        private string PathName => $"Maps/{Name}";

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public string Name => _name;

        public Color BackgroundColor => _backgroundColor;

        public double LightLevel { get; private set; } = 1.0f;

        public TiledMap Map { get; private set; }
        public TiledMapRenderer MapRenderer { get; private set; }
        public List<Entity> CollisionTiles = new List<Entity>();
        public List<Rectangle> CollisionTilesBounds = new List<Rectangle>();

        public int DrawOrder => throw new NotImplementedException();
        public bool Visible => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public Camera Camera { get; private set; }

        public void Initialize()
        {
            Vector2 playerPosition = new Vector2(250, 250);

            Entity player = _entityService.CreatePlayerEntity(x: (int)playerPosition.X, y: (int)playerPosition.Y, 15, 20, idTag: "localPlayer");
            _entityService.LocalPlayer = player;
            player.AddComponent(new InputComponent(player, _keyboardService, _entityService));

            AddCamera("main");
            Camera.Initialize();
        }

        #region ManageMap

        public void LoadMap()
        {
            try
            {
                Map = _contentService.ContentManager.Load<TiledMap>(PathName);
                MapRenderer = new TiledMapRenderer(_graphicsService.GraphicsDevice, Map);

                TiledMapObjectLayer collisionLayer = Map.GetLayer<TiledMapObjectLayer>("collision");
                if (collisionLayer != null)
                {
                    CollisionTiles.Clear();
                    CollisionTilesBounds.Clear();
                    foreach (var collisionObject in collisionLayer.Objects)
                    {
                        int rotation = (int)collisionObject.Rotation;
                        int x = (int)collisionObject.Position.X;
                        int y = (int)collisionObject.Position.Y;
                        int width = (int)collisionObject.Size.Width;
                        int height = (int)collisionObject.Size.Height;

                        if (rotation == 90 || rotation == -90)
                        {
                            int tempW = width;
                            width = height;
                            height = tempW;
                            if (rotation == 90)
                                x -= width;
                            else
                                y -= height;
                        }
                        else if (rotation == -180 || rotation == 180)
                        {
                            x -= width;
                            y -= height;
                        }

                        CreateCollisionTile(x, y, width, height);

                    }
                }

            }
            catch (Exception e) //TODO: Ajouter log
            {
                Map = null;
                MapRenderer = null;
            }
        }

        public void DisposeMap()
        {
            Map = null;
            MapRenderer = null;
        }

        #endregion

        public void Draw(GameTime gameTime)
        {
            _graphicsService.GraphicsDevice.SetRenderTarget(_graphicsService.SceneRenderTarget);

            // scene background
            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _graphicsService.SpriteBatch.FillRectangle(new RectangleF(0, 0, _graphicsService.ScreenWidth, _graphicsService.ScreenHeight), _backgroundColor);
            _graphicsService.SpriteBatch.End();

            // Draw camera
            _graphicsService.GraphicsDevice.Viewport = Camera.GetViewport();
            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _graphicsService.SpriteBatch.FillRectangle(0, 0, Camera.Size.X, Camera.Size.Y, Camera.BackgroundColour);
            _graphicsService.SpriteBatch.End();

            // Draw map
            if (Map == null)
            {
                _graphicsService.GraphicsDevice.SetRenderTarget(null);
                return;
            }

            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetTransformMatrix());

            Map.Layers
                .Where(x => x.Properties.Any(p => p.Value.Value == "below"))
                .ToList()
                .ForEach(layer =>
                {
                    MapRenderer.Draw(layer, Camera.GetTransformMatrix());
                });

            if (_configService.IsDebug)
            {
                Map.Layers.Where(x => x.Name == "collision").ToList().ForEach(l =>
                {
                    MapRenderer.Draw(l, Camera.GetTransformMatrix());
                });

                Map.Layers
                    .Where(x => x.Properties.Any(p => p.Value.Value == "collision"))
                    .ToList()
                    .ForEach(layer =>
                    {
                        MapRenderer.Draw(layer, Camera.GetTransformMatrix());

                    });
            }

            _graphicsService.SpriteBatch.End();

            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetTransformMatrix());


            var e = _entityService.LocalPlayer;
            _graphicsService.SpriteBatch.Draw(
                GetDebugTexture(),
                new Rectangle(
                    (int)(e.WorldPosition.X),
                    (int)(e.WorldPosition.Y),
                    (int)e.Size.X,
                    (int)e.Size.Y
                ),
                sourceRectangle: null,
                Color.White,
                rotation: 0.0f,
                origin: Vector2.Zero,
                effects: SpriteEffects.None,
                layerDepth: 0.0f


            );

            _entityService.GetEntities().Where(x => x.Components.Any(x => (x is CollisionComponent)))
                .ToList()
                .ForEach(e =>
                {
                    Rectangle r = new Rectangle(
                        (int)(e.WorldPosition.X),
                        (int)(e.WorldPosition.Y),
                        (int)e.Size.X,
                        (int)e.Size.Y
                    );

                    _graphicsService.SpriteBatch.DrawRectangle(r, Color.Orange, 2);

                });

            _graphicsService.SpriteBatch.End();

            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetTransformMatrix());

            Map.Layers
                .Where(x => x.Properties.Any(p => p.Value.Value == "above"))
                .ToList()
                .ForEach(layer =>
                {
                    MapRenderer.Draw(layer, Camera.GetTransformMatrix());
                });

            _graphicsService.SpriteBatch.End();


            // Light level
            // scene light level
            _graphicsService.GraphicsDevice.SetRenderTarget(null);
            _graphicsService.GraphicsDevice.Viewport = Camera.GetViewport();
            _graphicsService.GraphicsDevice.Clear(Color.Transparent);
            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _graphicsService.SpriteBatch.FillRectangle(
                0, 0,
                _graphicsService.ScreenWidth, _graphicsService.ScreenHeight,
                new Color(0, 0, 0, (int)(255 * (1 - LightLevel)))
            );
            _graphicsService.SpriteBatch.End();

            _graphicsService.GraphicsDevice.SetRenderTarget(_graphicsService.SceneRenderTarget);

            // draw scene
            _graphicsService.GraphicsDevice.SetRenderTarget(null);
            _graphicsService.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _graphicsService.SpriteBatch.Draw(_graphicsService.SceneRenderTarget, _graphicsService.SceneRenderTarget.Bounds, Color.White);
            _graphicsService.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var entity in _entityService.GetEntities())
            {
                entity.Update(gameTime);
            }

        }

        #region IDisposable

        ~DebugScene() => Dispose(false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Map = null;
                MapRenderer = null;
            }

            _disposed = true;
        }


        #endregion

        #region private

        private void AddCamera(string name)
        {
            if (Camera != null && Camera.Name == name)
                return;

            if (!CameraHelper.CameraTypes.Contains(name))
                throw new CameraTypeException("This camera type does not exist");

            // Main player camera
            Camera playerCamera = new(
                name: "main",
                size: new Vector2(_graphicsService.ScreenWidth, _graphicsService.ScreenHeight),
                zoom: 2.5f,
                backgroundColour: Color.Black,
                trackedEntity: _entityService.LocalPlayer,
                ownerEntity: _entityService.LocalPlayer,
                debugScene: this
            );

            Camera = playerCamera;
        }

        private Texture2D GetDebugTexture()
        {
            Color[] az = Enumerable.Range(0, 100).Select(i => Color.White).ToArray();
            Texture2D texture = new Texture2D(_graphicsService.GraphicsDevice, 10, 10, false, SurfaceFormat.Color);
            texture.SetData(az);

            return texture;
        }

        private void CreateCollisionTile(int x, int y, int width, int height)
        {
            Entity collisionTile = _entityService.CreateEntity(x, y, width, height);
            collisionTile.AddTag("collision");
            collisionTile.AddComponent(new CollisionComponent(collisionTile, _graphicsService, width, height));
        }

        #endregion
    }
}
