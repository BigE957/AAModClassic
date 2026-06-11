using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class TheVortex : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("The Vortex");
            // Tooltip.SetDefault(@"Spins fast enough to drag all enemies into its gravitational pull");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Terrarian);
            Item.damage = 475;                            
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shoot = ModContent.ProjectileType<TheVortex_Holdout>();
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ItemID.Terrarian);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }

    }
}
