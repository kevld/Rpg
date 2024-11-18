using Microsoft.Xna.Framework.Content;
using Rpg.Interfaces;

namespace Rpg.Test.Mocks
{
    public class ContentServiceMock : IContentService
    {
        public ContentManager ContentManager { get; set; }

        public ContentServiceMock(GameMock gameMock)
        {
            ContentManager = gameMock.Content;
        }
    }
}
