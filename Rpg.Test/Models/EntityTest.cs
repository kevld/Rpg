using Rpg.Models;
using System.Xml.Serialization;

namespace Rpg.Test.Models
{
    [TestClass]
    public class EntityTest
    {
        private Entity? entity;

        [TestInitialize]
        public void Startup()
        {
            entity = new Entity();
            Assert.IsNotNull(entity);
        }

        [TestCleanup]
        public void Cleanup()
        {
            entity = null;
        }

        [TestMethod]
        public void ListOfComponents_NotNull()
        {
            Assert.IsNotNull(entity?.Components);
            Assert.AreEqual(0, entity?.Components.Count);
        }

        [TestMethod]
        public void HasGuid()
        {
            Assert.IsNotNull(entity?.Id);
            Assert.IsTrue(Guid.Empty.ToString() != entity.Id);
        }

        [TestMethod]
        public void HasNoTags()
        {
            Assert.IsNotNull(entity?.Tags);
            Assert.AreEqual(0, entity?.Tags.Count);
        }

        [TestMethod]
        public void HasNoEmptyTag()
        {
            Assert.IsFalse(entity?.AddTag(""));
            Assert.IsFalse(entity?.AddTag(null));

            Assert.AreEqual(0, entity?.Tags.Count);
        }

        [TestMethod]
        public void HasOneTag()
        {
            Assert.IsTrue(entity?.AddTag("Tag1"));
            Assert.AreEqual(1, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
        }

        [TestMethod]
        public void HasTwoTags()
        {
            Assert.IsTrue(entity?.AddTag("Tag1"));
            Assert.IsTrue(entity?.AddTag("Tag2"));
            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
        }

        [TestMethod]
        public void HasNoIdenticalTags()
        {
            Assert.IsTrue(entity?.AddTag("Tag1"));
            Assert.IsFalse(entity?.AddTag("Tag1"));
            Assert.AreEqual(1, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
        }

        [TestMethod]
        public void HasNoIdenticalTagsMultipleTags()
        {
            Assert.IsTrue(entity?.AddTag("Tag1"));
            Assert.IsTrue(entity?.AddTag("Tag2"));
            Assert.IsFalse(entity?.AddTag("Tag1"));
            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
        }

        [TestMethod]
        public void AddTags_EmptyList()
        {
            entity?.AddTags(null);
            Assert.AreEqual(0, entity?.Tags.Count);
        }

        [TestMethod]
        public void AddTags_EmptyList_WithExistingTags()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTags(null);
            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
        }

        [TestMethod]
        public void AddTags_Unique_WithExistingTags()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTags(new List<string>() { "Tag3", "Tag4" });
            Assert.AreEqual(4, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
            Assert.IsTrue(entity?.Tags.Contains("Tag3"));
            Assert.IsTrue(entity?.Tags.Contains("Tag4"));
        }

        [TestMethod]
        public void AddTags_WithDuplicates_WithExistingTags()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTags(new List<string>() { "Tag3", "Tag2" });
            Assert.AreEqual(3, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
            Assert.IsTrue(entity?.Tags.Contains("Tag3"));
        }

        [TestMethod]
        public void RemoveTag_EmptyListOfTags()
        {
            Assert.IsFalse(entity?.RemoveTag(null));
            Assert.IsFalse(entity?.RemoveTag(""));
            Assert.IsFalse(entity?.RemoveTag("Tag1"));
        }

        [TestMethod]
        public void RemoveTag_ExistingListOfTags()
        {
            entity?.AddTag("Tag2");
            entity?.AddTag("Tag3");

            Assert.IsFalse(entity?.RemoveTag(null));
            Assert.IsFalse(entity?.RemoveTag(""));
            Assert.IsFalse(entity?.RemoveTag("Tag1"));

            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
            Assert.IsTrue(entity?.Tags.Contains("Tag3"));
        }

        [TestMethod]
        public void RemoveTag_ExistingListOfTags_ExistingTag()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");

            Assert.IsFalse(entity?.RemoveTag(null));
            Assert.IsFalse(entity?.RemoveTag(""));
            Assert.IsTrue(entity?.RemoveTag("Tag1"));

            Assert.AreEqual(1, entity?.Tags.Count);
            Assert.IsFalse(entity?.Tags.Contains("Tag1"));
            Assert.IsTrue(entity?.Tags.Contains("Tag2"));
        }

        [TestMethod]
        public void RemoveTags_ExistingListOfTags_ExistingTag()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTag("Tag3");
            entity?.AddTag("Tag4");

            entity?.RemoveTags(new List<string>() { "Tag1", "Tag2" });

            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsFalse(entity?.Tags.Contains("Tag1"));
            Assert.IsFalse(entity?.Tags.Contains("Tag2"));
            Assert.IsTrue(entity?.Tags.Contains("Tag3"));
            Assert.IsTrue(entity?.Tags.Contains("Tag4"));
        }

        [TestMethod]
        public void RemoveTags_Diplicate_ExistingListOfTags_ExistingTag()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTag("Tag3");
            entity?.AddTag("Tag4");

            entity?.RemoveTags(new List<string>() { "Tag1", "Tag2", "Tag2" });

            Assert.AreEqual(2, entity?.Tags.Count);
            Assert.IsFalse(entity?.Tags.Contains("Tag1"));
            Assert.IsFalse(entity?.Tags.Contains("Tag2"));
            Assert.IsTrue(entity?.Tags.Contains("Tag3"));
            Assert.IsTrue(entity?.Tags.Contains("Tag4"));
        }

        [TestMethod]
        public void HasTag()
        {
            entity?.AddTag("Tag1");
            entity?.AddTag("Tag2");
            entity?.AddTag("Tag3");
            entity?.AddTag("Tag4");

            Assert.IsTrue(entity?.HasTag("Tag1"));
            Assert.IsTrue(entity?.HasTag("Tag2"));
            Assert.IsFalse(entity?.HasTag(null));
            Assert.IsFalse(entity?.HasTag(""));
        }

        [TestMethod]
        public void Enabled_Exception()
        {
            Assert.ThrowsException<NotImplementedException>(() => entity?.Enabled);
        }

        [TestMethod]
        public void UpdateOrder_Exception()
        {
            Assert.ThrowsException<NotImplementedException>(() => entity?.UpdateOrder);
        }
    }
}
