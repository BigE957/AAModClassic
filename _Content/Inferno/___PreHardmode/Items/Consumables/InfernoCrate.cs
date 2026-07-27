using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Unreleased.Content.Inferno.___PreHardmode.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Consumables
{
    public class InfernoCrate : CrateAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Crates";
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
                ModContent.ItemType<DragonsGuard>(),
                ModContent.ItemType<LivingRazewoodWand>(),
                ModContent.ItemType<LivingRazeleafWand>()
            )
        ];
    }
}