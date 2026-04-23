using AAModClassic.___Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic.___Content.Inferno.__Hardmode.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Inferno.__Hardmode.Items.Consumables
{
    public class DaybreakCrate : InfernoCrate
    {
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