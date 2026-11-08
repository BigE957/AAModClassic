using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged
{
    public class SpiritReforgedManager : ModSystem
    {
        internal static Mod spiritReforged = null;

        public static bool IsEnabled => spiritReforged != null;

        public static object Call(params object[] args) => spiritReforged?.Call(args);

        public override void Load()
        {
            ModLoader.TryGetMod("SpiritReforged", out spiritReforged);

            Call("TrellisVine", AAMod.instance, () => ((int, int)[])[(ModContent.ItemType<ScorchedSeeds>(), 0), (ModContent.ItemType<DankSeeds>(), 1)], "AATrellisVines", "AAModClassic/_CrossMod/SpiritReforged/Textures/AATrellisVines");
        }
    }
}
