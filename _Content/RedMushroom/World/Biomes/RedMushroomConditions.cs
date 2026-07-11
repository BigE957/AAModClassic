using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic._Content.RedMushroom.World.Biomes
{
    public static class RedMushroomConditions
    {
        public static Condition InAnyRedMushroom = new Condition("Mods.AAModClassic.Common.Conditions.InAnyRedMushroom", () => Main.LocalPlayer.InModBiome<RedMushroomBiome>());
    }
}
