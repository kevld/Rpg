using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rpg.Core.Services.Interfaces
{
    public interface IContentService : IBaseService
    {
        public ContentManager ContentManager { get; set; }

        public SpriteFont DialogFont { get; set; }
    }
}
