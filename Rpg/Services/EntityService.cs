using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Interfaces;
using Rpg.Models;
using System;
using System.Collections.Generic;

namespace Rpg.Services
{
    public class EntityService : IEntityService
    {
        private readonly List<Entity> _entities = new();

        public Entity LocalPlayer { get; set; }

        public Entity CreateEntity()
        {
            Entity entity = new();
            _entities.Add(entity);
            return entity;
        }

        public Entity CreateEntity(int x, int y, int width, int height)
        {
            Entity e = new()
            {
                WorldPosition = new Vector2(x, y),
                Size = new Vector2(width, height)
            };
            _entities.Add(e);
            return e;

        }

        public Entity CreatePlayerEntity(int x, int y, int width, int height, string defaultState = "idle_right", float speed = 50, string idTag = null)
        {
            Entity playerEntity = CreateEntity();
            playerEntity.WorldPosition = new Vector2(x, y);
            playerEntity.Size = new Vector2(width, height);

            if (!string.IsNullOrEmpty(idTag))
            {
                playerEntity.AddTag(idTag);
                if (idTag == "localPlayer")
                    LocalPlayer = playerEntity;
            }
            else
            {
                // Generate a new unique player id
                Guid guid = Guid.NewGuid();
                // Generate a new player guid if it already exists?
                playerEntity.AddTag("player" + guid);
            }

            playerEntity.AddTag("player");

            // Add transform and sprites
            //TODO:playerEntity.AddComponent(new TransformComponent(x, y, width, height));
            playerEntity.AddComponent(new CollisionComponent(
                    playerEntity, new Vector2(13, 7), new Vector2(0, 12)
                ));

            LocalPlayer = playerEntity;

            return playerEntity;
        }

        public IReadOnlyCollection<Entity> GetEntities()
        {
            return _entities.AsReadOnly();
        }
    }
}
