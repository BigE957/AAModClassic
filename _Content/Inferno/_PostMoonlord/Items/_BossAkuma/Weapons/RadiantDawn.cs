using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class RadiantDawn : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiant Dawn");
            // Tooltip.SetDefault("Hold to fire more arrows");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 450;
            Item.shoot = ModContent.ProjectileType<RadiantDawn_Holdout>();
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.DaedalusStormbow);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}