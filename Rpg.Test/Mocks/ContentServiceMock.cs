using Microsoft.Xna.Framework.Content;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;

namespace Rpg.Test.Mocks
{
    public class ContentServiceMock : BaseService, IContentService
    {
        public ContentManager ContentManager { get; set; }

        public ContentServiceMock(GameMock gameMock)
        {
            ContentManager = gameMock.Content;
        }
    }
}
