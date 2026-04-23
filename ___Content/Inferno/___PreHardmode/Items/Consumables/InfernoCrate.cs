using AAModClassic.___Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Consumables
{
    public class InfernoCrate : CrateAbstract
    {
        public override int Tile => ModContent.TileType<InfernoCrate_Tile>();
        public override IItemDropRule[] TopLoot =>
        [
            ItemDropRule.OneFromOptionsNotScalingWithLuck
            (
                1, 
                ModContent.ItemType<Pyrosphere>(), 
                ModContent.ItemType<Firebuster>(), 
                ModContent.ItemType<Volley>(), 
                ModContent.ItemType<DragonSoul>(), 
                ModContent.ItemType<DragonsGuard>()
            )
        ];
    }
}