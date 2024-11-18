//using Rpg.Models;
//using System;

//namespace Rpg.Helpers
//{
//    public static class PlayerEntityHelper
//    {
//        public static Entity Create(int x, int y, int width, int height,
//            string defaultState = "idle_right", float speed = 50, string idTag = null)
//        {
//            Entity playerEntity;


//            // Otherwise create a new player entity
//            playerEntity = EngineGlobals.EntityManager.CreateEntity();

//            if (!string.IsNullOrEmpty(idTag))
//            {
//                playerEntity.Tags.Id = idTag;
//                if (idTag == "localPlayer")
//                    EngineGlobals.EntityManager.SetLocalPlayer(playerEntity);
//            }
//            else
//            {
//                // Generate a new unique player id
//                Guid guid = Guid.NewGuid();
//                // Generate a new player guid if it already exists?
//                playerEntity.Tags.Id = "player" + guid;
//            }

//            playerEntity.AddTag("player");

//            // Add transform and sprites
//            //playerEntity.AddComponent(new TransformComponent(x, y, width, height));

//            EngineGlobals.EntityManager.SetLocalPlayer(playerEntity);

//            return playerEntity;
//        }
//    }
//}
