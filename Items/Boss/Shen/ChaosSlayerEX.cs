using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic.Globals;
using AAModClassic.Projectiles.Shen;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class ChaosSlayerEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ikari");
            /* Tooltip.SetDefault(@"Unleashes explosive blades of chaos to smite your foes
blades go through tiles
Chaos Slayer EX"); */
        }

        public override void SetDefaults()
        {
            Item.width = 85;
            Item.height = 85;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.UseSound = SoundID.Item103;
            Item.damage = 666;
            Item.knockBack = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.expert = true; Item.expertOnly = true;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ChaosSlayerSwordEX>();
			Item.shootSpeed = 7;
            Item.useTurn = true;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, m == 0 ? ModContent.ProjectileType<ChaosSlayerSwordRedEX>() : ModContent.ProjectileType<ChaosSlayerSwordBlueEX>(), damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaosSlayer>());
            recipe.AddIngredient(ModContent.ItemType<PerfectChaos>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}