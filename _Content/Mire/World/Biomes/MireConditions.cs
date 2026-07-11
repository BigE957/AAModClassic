using AAModClassic._Content.Mire.World.Biomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic._Content.Mire.World.Biomes
{
    public static class MireConditions
    {
        public static Condition InAnyMire = new Condition("Mods.AAModClassic.Common.Conditions.InAnyMire", () => Main.LocalPlayer.InModBiome<MireBiome>());
    }
}
