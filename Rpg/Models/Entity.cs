using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Models
{
    public class Entity : IUpdateable
    {
        private readonly Guid _id;
        private readonly List<string> _tags;

        private readonly List<Component> _components;

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public Vector2 WorldPosition { get; set; }
        public Vector2 Size { get; set; }

        public string Id => _id.ToString();

        public IReadOnlyList<string> Tags => _tags.AsReadOnly();
        public IReadOnlyList<Component> Components => _components.AsReadOnly();

        public int DrawOrder => throw new NotImplementedException();

        public bool Visible => throw new NotImplementedException();

        public bool Enabled => throw new NotImplementedException();

        public int UpdateOrder => throw new NotImplementedException();

        public Entity()
        {
            _id = Guid.NewGuid();
            _tags = new List<string>();
            _components = new List<Component>();
        }

        public bool AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag) || _tags.Contains(tag))
                return false;

            _tags.Add(tag);

            return true;
        }

        public void AddTags(IEnumerable<string> tags)
        {
            if (tags == null)
                return;

            foreach (var tag in tags)
                AddTag(tag);
        }

        public bool RemoveTag(string tag)
        {
            if (string.IsNullOrEmpty(tag) || !_tags.Contains(tag))
                return false;

            _tags.Remove(tag);
            return true;
        }

        public void RemoveTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
                RemoveTag(tag);
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in _components)
            {
                component.Update(gameTime);
            }
        }

        public void AddComponent(Component component)
        {

            component.Initialize();
            _components.Add(component);
        }

        public Rectangle GetRectangle()
        {
            return CalculateRectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y);
        }

        public Rectangle GetProjectionRectangle(Vector2 velocity)
        {
            var projectionLocation = WorldPosition + velocity;
            return CalculateRectangle((int)projectionLocation.X, (int)projectionLocation.Y, (int)Size.X, (int)Size.Y);
        }

        #region private

        private Rectangle CalculateRectangle(int x, int y, int width, int height)
        {
            return new Rectangle(x, y, width, height);
        }

        #endregion
    }
}
