using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories
{
    public class Equinox : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox");
            /* Tooltip.SetDefault(@"'True balance'"); */
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void RegisterEquipStats()
        {
            AddEffect<MoonCharmEffect>();
            AddEffect<NeptunesShellEffect>();
            AddEffect<EquinoxEffect>();
            AddEffect<NightOwlEffect>();
            AddEffect(new EmitLightFromPlayerEffect(1f, 0.95f, 0.8f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialShell, 1);
            recipe.AddIngredient(ModContent.ItemType<RadiantStar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DarkVoid>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 20);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}