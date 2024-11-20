namespace Rpg.Core.Services.Interfaces
{
    public interface IConfigService : IBaseService
    {
        public bool IsDebug { get; }
    }
}
