using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class RealityCannon : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reality Cannon");
            // Tooltip.SetDefault("Rapidly Fires a spread of dark lasers");
        }

        public override void SetDefaults()
        {
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 48;
            Item.height = 26;
            Item.damage = 300;
            Item.UseSound = SoundID.Item12;
            Item.shoot = ModContent.ProjectileType<UnstablePowerCell_Proj>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noUseGraphic = false;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
            }
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
			recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
			recipe.AddIngredient(ItemID.StarCannon);
	        recipe.AddTile(ModContent.TileType<ACS_Tile>());
	        recipe.Register();
		}
	}
}
