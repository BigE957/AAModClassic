using AAModClassic.___Content.Inferno.___PreHardmode.Items.Consumables;
using AAModClassic.___Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic.___Content.Mire.__Hardmode.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.__Hardmode.Items.Consumables
{
    public class EventideCrate : MireCrate
    {
        public override int Tile => ModContent.TileType<EventideCrate_Tile>();
        public override bool Hardmode => true;
        public override int? ShimmerInto => ModContent.ItemType<MireCrate>();
        public override IItemDropRule[] BottomLoot =>
        [
            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SoulOfSpite>(), 2, 2, 5),
            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Bogtoxin>(), 2, 2, 5)
        ];
    }
}