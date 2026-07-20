using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.UI.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic.Utilities
{
    public static class ConditionUtils
    {
        public static Condition Unofficial = new Condition("Mods.AAModClassic.Common.Conditions.Unofficial", () => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial));
        public static Condition NotUnofficial = new Condition("Mods.AAModClassic.Common.Conditions.NotUnofficial", () => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial));
    }
}
