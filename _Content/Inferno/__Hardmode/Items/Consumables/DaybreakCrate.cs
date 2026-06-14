using AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Consumables
{
    public class DaybreakCrate : InfernoCrate, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Crates";
        public override int Tile => ModContent.TileType<DaybreakCrate_Tile>();
        public override bool Hardmode => true;
        public override int? ShimmerInto => ModContent.ItemType<InfernoCrate>();
        public override IItemDropRule[] BottomLoot =>
        [
            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SoulOfSmite>(), 2, 2, 5),
            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DragonFire>(), 2, 2, 5)
        ];
    }
}