using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelDLL.Common
{
    public class Util
    {
        public static int MovRange = 4;
        public static int CanvasWidth = 1152;
        public static int CanvasHeight = 768;
        public static string TestPortraitPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Player\Saved\1"); } }
        public static string TestCharasetPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Player\Saved\1"); } }
        public static string GeneratorPortraitPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Player\Generator\Portrait"); } }
        public static string GeneratorCharasetPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Player\Generator\Charaset"); } }
        public static string SkillIconPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Icons\Skills"); } }
        public static string WeaveIconPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Icons\Weaves"); } }
        public static string SavedPlayerPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Player\Saved"); } }
        public static string ObjectsIconPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Icons\Objects"); } }
        public static string ItemsIconPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Icons\Items"); } }
        public static string AreasPath { get { return Path.Combine(System.Environment.CurrentDirectory, @"Resources\Areas"); } }
    }
}
