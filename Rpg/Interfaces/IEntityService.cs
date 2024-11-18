﻿using Rpg.Models;
using System.Collections.Generic;

namespace Rpg.Interfaces
{
    public interface IEntityService
    {
        public IReadOnlyCollection<Entity> GetEntities();
        public Entity LocalPlayer { get; set; }
        public Entity CreatePlayerEntity(int x, int y, int width, int height,
            string defaultState = "idle_right", float speed = 50, string idTag = null);

        public Entity CreateEntity();
        public Entity CreateEntity(int x, int y, int width, int height);
    }
}