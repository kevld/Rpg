using Microsoft.Xna.Framework.Content;

namespace Rpg.Core.Services.Interfaces
{
    public interface IContentService : IBaseService
    {
        public ContentManager ContentManager { get; set; }
    }
}
