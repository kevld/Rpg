using Microsoft.Xna.Framework;
using Rpg.Core.Components;
using Rpg.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Models
{
    public class Entity : IUpdatable
    {
        private readonly Guid _id;
        private readonly List<string> _tags;

        private readonly List<BaseComponent> _components;

        public Vector2 WorldPosition { get; set; }
        public Vector2 Size { get; set; }

        public string Id => _id.ToString();

        public IReadOnlyList<string> Tags => _tags.AsReadOnly();
        public IReadOnlyList<BaseComponent> Components => _components.AsReadOnly();

        public Entity()
        {
            _id = Guid.NewGuid();
            _tags = new List<string>();
            _components = new List<BaseComponent>();
        }

        #region public

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
            foreach (BaseComponent component in _components)
            {
                component.Update(gameTime);
            }
        }

        public void AddComponent(BaseComponent component)
        {

            component.Initialize();
            _components.Add(component);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Size.X, (int)Size.Y);
        }

        public Rectangle GetProjectionRectangle(Vector2 velocity)
        {
            var projectionLocation = WorldPosition + velocity;
            return new Rectangle((int)projectionLocation.X, (int)projectionLocation.Y, (int)Size.X, (int)Size.Y);
        }

        public T GetComponent<T>() where T : class
        {
            return Components.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }

        #endregion

        #region private

        #endregion

    }
}
