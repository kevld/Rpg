using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Core.Interfaces;
using Rpg.Core.Scenes;
using Rpg.Exceptions;
using System;

namespace Rpg.Models
{
    public class Camera : IInitializable, IUpdatable
    {
        public string Name { get; private set; }
        public Vector2 ScreenPosition { get; private set; }
        public Vector2 Size { get; private set; }
        private Vector2 WorldPosition { get; set; }

        public Vector2 PreviousWorldPosition { get; private set; }
        public Vector2 TargetWorldPosition { get; private set; }

        public float FollowPercentage { get; private set; }

        public float Zoom { get; private set; }
        public float TargetZoom { get; private set; }
        public float ZoomIncrement { get; private set; } = 0.02f;

        public Color BackgroundColour { get; private set; }
        public Color BorderColour { get; private set; }
        public int BorderThickness { get; private set; }

        public Entity TrackedEntity { get; private set; }
        public Entity OwnerEntity { get; private set; }
        private BaseScene Scene { get; set; }

        public Camera(
            string name = "",
            Vector2 screenPosition = default,
            Vector2 size = default,
            Vector2 worldPosition = default,
            float followPercentage = .1f,
            float zoom = 1,
            Color backgroundColour = default,
            Color borderColour = default,
            int borderThickness = 0,
            Entity trackedEntity = null,
            Entity ownerEntity = null,
            BaseScene scene = null
            )
        {
            Name = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : name;
            ScreenPosition = screenPosition;

            if (size.X < 0 || size.Y < 0)
                throw new NegativeVectorException("Screen size cannot have negative value");

            Size = size;
            WorldPosition = worldPosition * -1;
            FollowPercentage = followPercentage;

            if (zoom < 0.1)
                throw new NegativeNumberException("Zoom cannot be inferior to 0.1");

            Zoom = zoom;
            BackgroundColour = backgroundColour;
            BorderColour = borderColour;

            if (borderThickness < 0)
                throw new NegativeNumberException("Border thickness cannot be inferior to 0.1");

            BorderThickness = borderThickness;
            TrackedEntity = trackedEntity;
            OwnerEntity = ownerEntity;
            Scene = scene;
        }

        public void SetWorldPosition(Vector2 position)
        {
            TargetWorldPosition = position * -1;
            WorldPosition = TargetWorldPosition;
            PreviousWorldPosition = WorldPosition;
        }

        public void SetZoom(float zoom)
        {
            if (zoom < .1f)
            {
                zoom = 1.0f;
            }

            Zoom = zoom;
            TargetZoom = zoom;
        }

        public Vector2 GetScreenMiddle()
        {
            return new Vector2(ScreenPosition.X + Size.X / 2, ScreenPosition.Y + Size.Y / 2);
        }

        public Viewport GetViewport()
        {
            return new Viewport((int)ScreenPosition.X, (int)ScreenPosition.Y, (int)Size.X, (int)Size.Y);
        }

        public Matrix GetTransformMatrix()
        {
            Vector2 test = WorldPosition;
            test.X = WorldPosition.X + (Size.X / 2) / Zoom;
            test.Y = WorldPosition.Y + (Size.Y / 2) / Zoom;

            return Matrix.CreateTranslation(
                    new Vector3(test.X, test.Y, 0.0f)) *
                    Matrix.CreateRotationZ(0.0f) *
                    Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                    Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f)
            );
        }

        public void Initialize()
        {
            ArgumentNullException.ThrowIfNull(Scene);
        }

        public void Update(GameTime gameTime)
        {
            PreviousWorldPosition = WorldPosition;

            if (TrackedEntity != null)
            {
                SetWorldPosition(new Vector2(
                    TrackedEntity.WorldPosition.X + (TrackedEntity.Size.X / 2),
                    TrackedEntity.WorldPosition.Y + (TrackedEntity.Size.Y / 2))
                );
            }


            float x = (WorldPosition.X * (1 - FollowPercentage)) + (TargetWorldPosition.X * FollowPercentage);
            float y = (WorldPosition.Y * (1 - FollowPercentage)) + (TargetWorldPosition.Y * FollowPercentage);

            WorldPosition = new Vector2(x, y);

            if (Scene?.Map == null)
                return;

            // Calculate map bounds
            WorldPosition = new Vector2(CalculateWidth(), CalculateHeight());

            // update camera zoom
            if (Math.Abs(Zoom - TargetZoom) < 0.1f)
            {
                double f = ZoomIncrement * Math.Log(Zoom);
                if (Zoom < TargetZoom)
                    Zoom = (float)Math.Min(TargetZoom, Zoom + f);
                else
                    Zoom = (float)Math.Max(TargetZoom, Zoom - f);
            }
        }

        #region private

        private float CalculateWidth()
        {
            float width = WorldPosition.X;

            // width
            // if camera is bigger than map
            if (Size.X > (Scene.Map.Width * Scene.Map.TileWidth * Zoom))
            {
                width = (Scene.Map.Width * Scene.Map.TileWidth / 2) * -1;
            }
            else
            {
                // clamp to left
                if (WorldPosition.X * -1 < (Size.X / Zoom / 2))
                {
                    width = (Size.X / Zoom / 2) * -1;
                }
                // clamp to right
                if (WorldPosition.X * -1 > (Scene.Map.Width * Scene.Map.TileWidth) - (Size.X / Zoom / 2))
                {
                    width = ((Scene.Map.Width * Scene.Map.TileWidth) - (Size.X / Zoom / 2)) * -1;
                }
            }

            return width;
        }

        private float CalculateHeight()
        {
            float height = WorldPosition.Y;

            // height
            // if camera is bigger than map
            if (Size.Y > (Scene.Map.Height * Scene.Map.TileHeight * Zoom))
            {
                height = (Scene.Map.Height * Scene.Map.TileHeight / 2) * -1;
            }
            else
            {
                // clamp to top
                if (WorldPosition.Y * -1 < (Size.Y / Zoom / 2))
                {
                    height = (Size.Y / Zoom / 2) * -1;
                }
                // clamp to bottom
                if (WorldPosition.Y * -1 > (Scene.Map.Height * Scene.Map.TileHeight) - (Size.Y / Zoom / 2))
                {
                    height = ((Scene.Map.Height * Scene.Map.TileHeight) - (Size.Y / Zoom / 2)) * -1;
                }
            }

            return height;
        }

        #endregion
    }
}
