using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic._Content.Void.World.Biomes
{
    public static class VoidConditions
    {
        public static Condition InAnyVoid = new Condition("Mods.AAModClassic.Common.Conditions.InAnyVoid", () => Main.LocalPlayer.InModBiome<VoidBiome>());
    }
}
