using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;

using static AAModClassic.Utilities.FilePathUtils;

namespace AAModClassic.Assets
{
    public class AssetDirectory : ModSystem
    {
        public static readonly string FilePath = FilePath<AssetDirectory>() + "/";

        //TODO: add noisemap stuff here, like oblivion noise and fog noise

        public class General
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "General/";

            public static readonly string Nothing = FilePath + "Nothing";
        }

        public class Items
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "Items/";

            public static readonly string BiomePrism = FilePath + "BiomePrism";
        }

        public class Projectiles
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "Projectiles/";

            public static readonly string FireProj = FilePath + "FireProj";
        }
    }
}
