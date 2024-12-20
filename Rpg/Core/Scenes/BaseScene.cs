﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Rpg.Components;
using Rpg.Core.Interfaces;
using Rpg.Core.Services.Interfaces;
using Rpg.Core.System;
using Rpg.Exceptions;
using Rpg.Helpers;
using Rpg.Models;
using System;
using System.Linq;

namespace Rpg.Core.Scenes
{
    public abstract class BaseScene(GameServiceContainer gameServiceContainer) : DisposableObject, IInitializable, Core.Interfaces.IDrawable, IUpdatable
    {
        protected string _name;

        protected readonly Color _backgroundColor = Color.Black;
        protected readonly IContentService _contentService = gameServiceContainer.GetService<IContentService>();
        protected readonly IGraphicsService _graphicsService = gameServiceContainer.GetService<IGraphicsService>();
        protected readonly IEntityService _entityService = gameServiceContainer.GetService<IEntityService>();
        protected readonly IKeyboardService _keyboardService = gameServiceContainer.GetService<IKeyboardService>();
        protected readonly IConfigService _configService = gameServiceContainer.GetService<IConfigService>();

        protected string PathName => $"Maps/{Name}";

        public string Name => _name;
        public Color BackgroundColor => _backgroundColor;
        public double LightLevel { get; protected set; } = 1.0f;

        public TiledMap Map { get; protected set; }
        public TiledMapRenderer MapRenderer { get; protected set; }
        public Camera Camera { get; protected set; }

        #region IDisposable

        protected override void CleanIfDisposing()
        {
            Map = null;
            MapRenderer = null;
        }

        #endregion

        #region public
        public void LoadMap()
        {
            try
            {
                Map = _contentService.ContentManager.Load<TiledMap>(PathName);
                MapRenderer = new TiledMapRenderer(_graphicsService.GraphicsDevice, Map);

                TiledMapObjectLayer collisionLayer = Map.GetLayer<TiledMapObjectLayer>(MapHelper.LayoutCollision);
                if (collisionLayer != null)
                {
                    foreach (var collisionObject in collisionLayer.Objects)
                    {
                        int rotation = (int)collisionObject.Rotation;
                        int x = (int)collisionObject.Position.X;
                        int y = (int)collisionObject.Position.Y;
                        int width = (int)collisionObject.Size.Width;
                        int height = (int)collisionObject.Size.Height;

                        if (rotation == 90 || rotation == -90)
                        {
                            (height, width) = (width, height);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Map = null;
                MapRenderer = null;
            }
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var entity in _entityService.GetEntities())
            {
                entity.Update(gameTime);
            }

        }

        public virtual void Initialize()
        {
            AddCamera("main");
            Camera.Initialize();
        }

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
                Map.Layers.Where(x => x.Name == MapHelper.LayoutCollision).ToList().ForEach(l =>
                {
                    MapRenderer.Draw(l, Camera.GetTransformMatrix());
                });

                Map.Layers
                    .Where(x => x.Properties.Any(p => p.Value.Value == MapHelper.LayoutCollision))
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

            foreach (var item in e.Components)
            {
                item.Draw(gameTime);
            }


            _entityService.GetEntities().Where(x => x.Components.Any(x => (x is CollisionComponent)))
                .ToList()
                .ForEach(e =>
                {
                    Rectangle r = new(
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

        #endregion

        #region private

        private void AddCamera(string name)
        {
            if (Camera != null && Camera.Name == name)
                return;

            if (!CameraHelper.GetCameraTypes().Contains(name))
                throw new CameraTypeException("This camera type does not exist");

            // Main player camera
            Camera playerCamera = new(
                name: "main",
                size: new Vector2(_graphicsService.ScreenWidth, _graphicsService.ScreenHeight),
                zoom: 2.5f,
                backgroundColour: Color.Black,
                trackedEntity: _entityService.LocalPlayer,
                ownerEntity: _entityService.LocalPlayer,
                scene: this
            );

            Camera = playerCamera;
        }

        private void CreateCollisionTile(int x, int y, int width, int height)
        {
            Entity collisionTile = _entityService.CreateEntity(x, y, width, height);
            collisionTile.AddTag(MapHelper.LayoutCollision);
            collisionTile.AddComponent(new CollisionComponent(collisionTile, width, height));
        }

        #endregion

        #region Debug

        protected Texture2D GetDebugTexture()
        {
            Color[] az = Enumerable.Range(0, 100).Select(i => Color.White).ToArray();
            Texture2D texture = new(_graphicsService.GraphicsDevice, 10, 10, false, SurfaceFormat.Color);
            texture.SetData(az);

            return texture;
        }

        #endregion
    }
}
