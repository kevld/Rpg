using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Rpg.Exceptions;
using Rpg.Models;
using Rpg.Scenes;
using Rpg.Services;
using SharpDX.XAudio2;

namespace Rpg.Test.Models
{
    [TestClass]
    public class CameraTest
    {

        [TestMethod]
        public void Name_Default()
        {
            Camera camera = new();

            Assert.IsFalse(string.IsNullOrEmpty(camera.Name));
        }

        [TestMethod]
        public void ScreenPosition_Default()
        {
            Camera camera = new();

            Assert.AreEqual(Vector2.Zero, camera.ScreenPosition);
        }

        [TestMethod]
        public void Size_Default()
        {
            Camera camera = new();

            Assert.AreEqual(Vector2.Zero, camera.Size);
        }

        [TestMethod]
        public void WorldPosition_Default()
        {
            Camera camera = new();

            Assert.AreEqual(Vector2.Zero, camera.WorldPosition);
        }

        [TestMethod]
        public void FollowPercentage_Default()
        {
            Camera camera = new();

            Assert.AreEqual(.1f, camera.FollowPercentage);
        }

        [TestMethod]
        public void Zoom_Default()
        {
            Camera camera = new();

            Assert.AreEqual(1f, camera.Zoom);
        }

        [TestMethod]
        public void BackgroundColour_Default()
        {
            Camera camera = new();

            Assert.AreEqual(default, camera.BackgroundColour);
        }

        [TestMethod]
        public void BorderColour_Default()
        {
            Camera camera = new();

            Assert.AreEqual(default, camera.BorderColour);
        }

        [TestMethod]
        public void BorderThickness_Default()
        {
            Camera camera = new();

            Assert.AreEqual(0, camera.BorderThickness);
        }

        [TestMethod]
        public void TrackedEntity_Default()
        {
            Camera camera = new();

            Assert.IsNull(camera.TrackedEntity);
        }

        [TestMethod]
        public void OwnerEntity_Default()
        {
            Camera camera = new();

            Assert.IsNull(camera.OwnerEntity);
        }

        [TestMethod]
        public void Name_Set()
        {
            string guid = Guid.NewGuid().ToString();
            Camera camera = new(guid);

            Assert.AreEqual(guid, camera.Name);
        }

        [TestMethod]
        public void ScreenPosition_Set()
        {
            Vector2 v = new(50, 50);
            Camera camera = new(screenPosition: v);

            Assert.AreEqual(v, camera.ScreenPosition);
        }

        [TestMethod]
        public void Size_Set()
        {
            Vector2 v = new(50, 50);
            Camera camera = new(size: v);

            Assert.AreEqual(v, camera.Size);
        }

        [TestMethod]
        public void ScreenPosition_SetPositionInferiorToZero()
        {
            Vector2 v = new(-50, 50);
            Assert.ThrowsException<NegativeVectorException>(() => new Camera(size: v), "Screen size cannot have negative value");
        }

        [TestMethod]
        public void WorldPosition_Set()
        {
            Vector2 v = new(50, 50);
            Camera camera = new(worldPosition: v);

            Assert.AreEqual(v * -1, camera.WorldPosition);
        }

        [TestMethod]
        public void FollowPercentage_Set()
        {
            Camera camera = new(followPercentage: 5.5f);

            Assert.AreEqual(5.5f, camera.FollowPercentage);
        }

        [TestMethod]
        public void Zoom_Set()
        {
            Camera camera = new(zoom: 2f);

            Assert.AreEqual(2f, camera.Zoom);
        }

        [TestMethod]
        public void Zoom_SetNegativeNumber()
        {
            Assert.ThrowsException<NegativeNumberException>(() => new Camera(zoom: -2f), "Zoom cannot be inferior to 0.1");
        }

        [TestMethod]
        public void Zoom_SetTooLowNumber()
        {
            Assert.ThrowsException<NegativeNumberException>(() => new Camera(zoom: 0.01f), "Zoom cannot be inferior to 0.1");
        }

        [TestMethod]
        public void BackgroundColour_Set()
        {
            Camera camera = new(backgroundColour: Color.Aqua);

            Assert.AreEqual(Color.Aqua, camera.BackgroundColour);
        }

        [TestMethod]
        public void BorderColour_Set()
        {
            Camera camera = new(borderColour: Color.Aqua);

            Assert.AreEqual(Color.Aqua, camera.BorderColour);
        }

        [TestMethod]
        public void BorderThickness_Set()
        {
            Camera camera = new(borderThickness: 5);

            Assert.AreEqual(5, camera.BorderThickness);
        }

        [TestMethod]
        public void BorderThickness_NegativeNumber()
        {
            Assert.ThrowsException<NegativeNumberException>(() => new Camera(borderThickness: -1), "Border thickness cannot be inferior to 0.1");
        }

        [TestMethod]
        public void TrackedEntity_Set()
        {
            Entity e = new();
            Camera camera = new(trackedEntity: e);

            Assert.AreEqual(e, camera.TrackedEntity);
        }

        [TestMethod]
        public void OwnerEntity_Set()
        {
            Entity e = new();
            Camera camera = new(ownerEntity: e);

            Assert.AreEqual(e, camera.OwnerEntity);
        }

        [TestMethod]
        public void DefaultCtor_Fields()
        {
            Camera camera = new();

            Assert.AreEqual(default, camera.PreviousWorldPosition);
            Assert.AreEqual(default, camera.TargetWorldPosition);
            Assert.AreEqual(default, camera.TargetZoom);
        }

        [TestMethod]
        public void SetWorldPosition_Default()
        {
            Camera camera = new();
            camera.SetWorldPosition(Vector2.Zero);

            Assert.AreEqual(Vector2.Zero, camera.TargetWorldPosition);
            Assert.AreEqual(Vector2.Zero, camera.WorldPosition);
            Assert.AreEqual(Vector2.Zero, camera.PreviousWorldPosition);
            Assert.AreEqual(camera.TargetWorldPosition, camera.WorldPosition);
            Assert.AreEqual(camera.TargetWorldPosition, camera.PreviousWorldPosition);
        }

        [TestMethod]
        public void SetWorldPosition_SetValue()
        {
            Camera camera = new();
            Vector2 newPosition = new(50, 50);

            camera.SetWorldPosition(newPosition);

            Vector2 resut = newPosition * -1;
            Assert.AreEqual(resut, camera.WorldPosition);
            Assert.AreEqual(resut, camera.TargetWorldPosition);
            Assert.AreEqual(resut, camera.PreviousWorldPosition);
        }

        [TestMethod]
        public void SetZoom_Default()
        {
            Camera camera = new();

            camera.SetZoom(0);

            Assert.AreEqual(1, camera.Zoom);
            Assert.AreEqual(1, camera.TargetZoom);
        }

        [TestMethod]
        public void SetZoom_NumberInferiorToZero()
        {
            Camera camera = new();

            camera.SetZoom(-1);

            Assert.AreEqual(1, camera.Zoom);
            Assert.AreEqual(1, camera.TargetZoom);
        }

        [TestMethod]
        public void SetZoom_NumberBetweenZeroAndPointOne()
        {
            Camera camera = new();

            camera.SetZoom(0.005f);

            Assert.AreEqual(1, camera.Zoom);
            Assert.AreEqual(1, camera.TargetZoom);
        }

        [TestMethod]
        public void SetZoom_NumberSuperiorToOne()
        {
            Camera camera = new();

            camera.SetZoom(2.5f);

            Assert.AreEqual(2.5f, camera.Zoom);
            Assert.AreEqual(2.5f, camera.TargetZoom);
        }

        [TestMethod]
        public void GetScreenMiddle_Default()
        {
            Camera camera = new();

            Assert.AreEqual(Vector2.Zero, camera.GetScreenMiddle());
        }

        [TestMethod]
        public void GetScreenMiddle_Get()
        {
            Vector2 position = new(10, 30);
            Vector2 size = new(800, 600);
            Camera camera = new(screenPosition: position, size: size);

            Vector2 expected = new(410, 330);
            Assert.AreEqual(expected, camera.GetScreenMiddle());
        }

        [TestMethod]
        public void GetViewPort_Default()
        {
            Camera camera = new();

            Viewport expected = new(0, 0, 0, 0);
            Assert.AreEqual(expected, camera.GetViewport());
        }

        [TestMethod]
        public void GetViewPort_Values()
        {
            Vector2 position = new(10, 30);
            Vector2 size = new(800, 600);
            Camera camera = new(screenPosition: position, size: size);

            Viewport expected = new((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Assert.AreEqual(expected, camera.GetViewport());
        }

        [TestMethod]
        public void GetTransformMatrix_IsOk()
        {
            try
            {
                Camera camera = new();
                Assert.IsNotNull(camera.GetTransformMatrix());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Initialize_SceneIsNull()
        {
            Camera camera = new();
            Assert.ThrowsException<ArgumentNullException>(() => camera.Initialize(), "Scene is null");
        }

        [TestMethod]
        public void Initialize_SceneMapIsNull()
        {
            Camera camera = new();

            Assert.ThrowsException<ArgumentNullException>(() => camera.Initialize(), "Scene.Map is null");
        }

        // TODO:Obsolete
        //[TestMethod]
        //public void Initialize_SceneIsOk()
        //{
        //    Camera camera = new Camera();

        //    DebugScene debugScene = new DebugScene(new ContentService(), new GraphicsService(null));
        //    //debugScene.Map = new MonoGame.Extended.Tiled.TiledMap();
        //    //camera.LinkScene(debugScene);

        //    try
        //    {
        //        camera.Initialize();
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail(e.Message);
        //    }
        //}

        [TestMethod]
        public void Update_IsOk()
        {
            Camera camera = new();

            try
            {
                camera.Update(new GameTime());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
