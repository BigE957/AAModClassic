using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Mire.___PreHardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class KappaFins : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new MovementSpeedEffect(0.15f));
            AddEffect<FlipperEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>());
            recipe.AddIngredient(ItemID.Flipper);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddCondition(Language.GetOrRegister("Mods.AAModClassic.Common.Conditions.Unofficial"), () => WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial));
            recipe.Register();
        }
    }
}