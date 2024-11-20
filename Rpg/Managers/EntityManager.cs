using Microsoft.Xna.Framework;
using Rpg.Core.Managers;
using Rpg.Models;
using System.Collections.Generic;

namespace Rpg.Managers
{
    public class EntityManager(GameServiceContainer services) : BaseManager(services)
    {
        private readonly List<Entity> _entityList = new();

        public IReadOnlyCollection<Entity> EntityList => _entityList.AsReadOnly();

        public Entity CreateEntity()
        {
            Entity e = new Entity();
            AddEntity(e);
            return e;
        }

        // Add the entity to the list and mapper
        private void AddEntity(Entity e)
        {
            _entityList.Add(e);
        }
    }
}
