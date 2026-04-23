using AAModClassic.___Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Pets;
using AAModClassic.___Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items.Consumables
{
    public class MireCrate : CrateAbstract
    {
        public override int Tile => ModContent.TileType<MireCrate_Tile>();
        public override IItemDropRule[] TopLoot =>
        [
            ItemDropRule.OneFromOptionsNotScalingWithLuck
            (
                1,
                ModContent.ItemType<HydrasSpear>(),
                ModContent.ItemType<Mossket>(),
                ModContent.ItemType<GlowingMossBall>(),
                ModContent.ItemType<ShadowBand>(),
                ModContent.ItemType<GunkWand>()
            )
        ];
    }
}