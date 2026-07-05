using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace AAModClassic.Utilities
{
    public class FilePathUtils
    {
        public static string FilePath<T>()
        {
            return typeof(T).Namespace.Replace('.', '/');
        }

        public static string TexturePath<T>()
        {
            return $"{FilePath<T>()}/{typeof(T).Name}";
        }

        public static string RemoveModNameHeaderFromFilePath(string input)
        {
            return input.Remove(0, 13);
        }
    }
}
