
using Microsoft.Xna.Framework;
using Rpg.Interfaces;
using Rpg.Models;
using System;

namespace Rpg.Components
{
    public class CollisionComponent : Component
    {
        private readonly IGraphicsService _graphicsService;

        public Rectangle Box { get; private set; }
        public Rectangle Broadphase { get; private set; }

        public Vector2 Size { get; set; }
        public Vector2 Offset { get; set; }

        private CollisionComponent(Entity owner, IGraphicsService graphicsService) : base(owner)
        {
            _graphicsService = graphicsService;
        }

        public CollisionComponent(Entity owner, IGraphicsService graphicsService, Vector2 size, Vector2 offset = default) : this(owner, graphicsService)
        {

            Size = size;
            Offset = offset;
        }

        public CollisionComponent(Entity owner, IGraphicsService graphicsService, int width, int height, int offsetX = 0, int offsetY = 0) : this(owner, graphicsService)
        {
            Size = new Vector2(width, height);
            Offset = new Vector2(offsetX, offsetY);
        }


        public Rectangle GetBoundingBox(int positionX, int positionY)
        {
            Box = new Rectangle(
                positionX + (int)Offset.X,
                positionY + (int)Offset.Y,
                (int)Size.X,
                (int)Size.Y
            );
            return Box;
        }

        public Rectangle GetBoundingBox(Vector2 position)
        {
            Box = GetBoundingBox((int)position.X, (int)position.Y);

            return Box;
        }

        public Rectangle GetBroadphaseBox(Vector2 velocity)
        {
            int x = Box.X;
            int y = Box.Y;
            float vX = velocity.X;
            float vY = velocity.Y;
            int width = (int)Math.Ceiling(Box.Width + Math.Abs(vX) * 2);
            int height = (int)Math.Ceiling(Box.Height + Math.Abs(vY) * 2);

            if (vX < 0)
                x += (int)Math.Floor(vX * 2); // Math.Ceiling

            if (vY < 0)
                y += (int)Math.Floor(vY * 2);

            Broadphase = new Rectangle(x, y, width, height);

            return Broadphase;

        }

        //private bool CollideWith(Entity other)
        //{

        //}

        //public bool CollideWith(Entity other, Vector2 velocity)
        //{
        //    if (other == null) return false;

        //}
    }
}
