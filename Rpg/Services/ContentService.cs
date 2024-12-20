using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;

namespace Rpg.Services
{
    public class ContentService : BaseService, IContentService
    {
        public ContentManager ContentManager { get; set; }

        public SpriteFont DialogFont { get; set; }
    }
}
