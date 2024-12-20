using Microsoft.Xna.Framework;
using Rpg.Components;
using Rpg.Components.Interactives;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;
using Rpg.Models;
using System;
using System.Collections.Generic;

namespace Rpg.Services
{
    public class EntityService : BaseService, IEntityService
    {
        private readonly List<Entity> _entities = new();

        public Entity LocalPlayer { get; set; }

        public void ClearEntities()
        {
            LocalPlayer = null;
            _entities.Clear();
        }

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
                playerEntity.AddTag("player" + playerEntity.Id);
            }

            playerEntity.AddTag("player");

            // Add transform and sprites
            // playerEntity AddComponent new TransformComponent x, y, width, height
            playerEntity.AddComponent(new CollisionComponent(
                    playerEntity, new Vector2(13, 7), new Vector2(0, 12)
                ));


            LocalPlayer = playerEntity;

            return playerEntity;
        }

        public Entity CreateNpcEntity(int x, int y, int width, int height, string defaultState = "idle", float speed = 0, string idTag = null)
        {
            Entity npc = CreateEntity();
            npc.WorldPosition = new Vector2(x, y);
            npc.Size = new Vector2(width, height);

            if (!string.IsNullOrEmpty(idTag))
            {
                npc.AddTag(idTag);
            }
            else
            {
                npc.AddTag($"npc-{npc.Id}");
            }

            npc.AddComponent(new CollisionComponent(npc, new Vector2(13, 7), new Vector2(0, 12)));
            //npc.AddComponent(new DialogueComponent(npc, ));

            return npc;
        }


        public IReadOnlyCollection<Entity> GetEntities()
        {
            return _entities.AsReadOnly();
        }
    }
}
