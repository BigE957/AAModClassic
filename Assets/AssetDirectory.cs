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

            public static readonly string Barrier = FilePath + "Barrier";

            public static readonly string Ritual_Inner1 = FilePath + "Ritual_Inner1";
            public static readonly string Ritual_Inner2 = FilePath + "Ritual_Inner2";
            public static readonly string Ritual_Outer1 = FilePath + "Ritual_Outer1";
            public static readonly string Ritual_Outer2 = FilePath + "Ritual_Outer2";

            public static readonly string Bloom_Medium = FilePath + "Bloom_Medium";

            public static readonly string LensFlare_Small = FilePath + "LensFlare_Small";
            public static readonly string LensFlare_SmallBlank = FilePath + "LensFlare_SmallBlank";
            public static readonly string LensFlare_Medium = FilePath + "LensFlare_Medium";

            public static readonly string HollowCircle_HardEdge = FilePath + "HollowCircle_HardEdge";
        }

        public class Items
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "Items/";

            public static readonly string BiomePrism = FilePath + "BiomePrism";
        }

        public class Particles
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "Particles/";

            public static readonly string CircleGlow = FilePath + "CircleGlow";
            public static readonly string CircleGlow_White = FilePath + "CircleGlow_White";
            public static readonly string CircleSolid = FilePath + "CircleSolid";

            public static readonly string PillGlow = FilePath + "PillGlow";
            public static readonly string PillGlow_White = FilePath + "PillGlow_White";
            public static readonly string PillSolid = FilePath + "PillSolid";

            public static readonly string StripGlow = FilePath + "StripGlow";
            public static readonly string StripGlow_White = FilePath + "StripGlow_White";
            public static readonly string StripSolid = FilePath + "StripSolid";
        }

        public class Projectiles
        {
            public static readonly string FilePath = AssetDirectory.FilePath + "Projectiles/";

            public static readonly string FireProj = FilePath + "FireProj";
        }
    }
}
