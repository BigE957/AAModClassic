using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class SunPartisan : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Sun Partisan");
            /* Tooltip.SetDefault(@"One of two legendary spears used to divide time into day and night
Inflicts daybroken"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 280;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 96;
            Item.height = 96;
            Item.scale = 1.1f;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.knockBack = 4.7f;
            Item.UseSound = SoundID.Item20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTurn = true;
			Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.shoot = ModContent.ProjectileType<SunPartisan_Holdout>();  //put your Spear projectile name
            Item.shootSpeed = 7f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.NorthPole, 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
