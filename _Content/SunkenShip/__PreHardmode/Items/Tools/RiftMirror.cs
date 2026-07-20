using AAModClassic._Unofficial.Content.SunkenShip.___PreHardmode.Items;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.SunkenShip.__PreHardmode.Items.Tools
{
    public class RiftMirror : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rift Mirror");
            /* Tooltip.SetDefault(@"Pressing the rift hotkey returns you home
Pressing the rift return hotkey brings you back to your most recent rift location"); */
        }    

		public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicMirror);
			Item.useAnimation = 15;
            Item.useTime = 15;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.IceMirror);
            recipe.AddTile(TileID.Anvils);
            recipe.AddCondition(Language.GetOrRegister("Mods.AAModClassic.Common.Conditions.NotUnofficial"), () => !WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial));
			recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ShatteredMirror>());
            recipe2.AddIngredient(ItemID.Glass, 20);
            recipe2.AddIngredient(ItemID.Bone, 15);
            recipe2.AddIngredient(ItemID.ShadowScale, 15);
            recipe2.AddTile(TileID.DemonAltar);
            recipe2.AddCondition(ConditionUtils.Unofficial);
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<ShatteredMirror>());
            recipe3.AddIngredient(ItemID.Glass, 20);
            recipe3.AddIngredient(ItemID.Bone, 15);
            recipe3.AddIngredient(ItemID.TissueSample, 15);
            recipe3.AddTile(TileID.DemonAltar);
            recipe3.AddCondition(ConditionUtils.Unofficial);
            recipe3.Register();
        }
    }
}
