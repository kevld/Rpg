using System.Collections.Generic;

namespace Rpg.Helpers
{
    public static class CameraHelper
    {
        private static readonly List<string> CameraTypeList = new()
        {
            "main"
        };

        public static IReadOnlyCollection<string> GetCameraTypes() => CameraTypeList;
    }
}
