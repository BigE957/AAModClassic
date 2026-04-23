using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Summoning;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Consumables
{
    public class VoidCrate : CrateAbstract
    {
        public override int Tile => ModContent.TileType<VoidCrate_Tile>();
        public override IItemDropRule[] TopLoot =>
        [
            ItemDropRule.OneFromOptionsNotScalingWithLuck
            (
                1,
                ModContent.ItemType<Voidsaber>(),
                ModContent.ItemType<DoomGun>(),
                ModContent.ItemType<DoomStaff>(),
                ModContent.ItemType<ProbeControlUnit>()
            )
        ];
        public override IItemDropRule[] BottomLoot =>
        [
            ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DoomiteScrap>(), 2, 2, 5),
        ];
    }
}