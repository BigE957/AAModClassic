using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace AAModClassic._Content.Inferno.World.Biomes
{
    public static class InfernoConditions
    {
        public static Condition InAnyInferno = new Condition("Mods.AAModClassic.Common.Conditions.InAnyInferno", () => Main.LocalPlayer.InModBiome<InfernoBiome>());
    }
}
