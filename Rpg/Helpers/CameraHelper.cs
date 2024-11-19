using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
