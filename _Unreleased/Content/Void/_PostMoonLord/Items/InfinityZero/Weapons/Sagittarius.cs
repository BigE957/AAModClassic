using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Sagittarius : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sagittarius");
            // Tooltip.SetDefault("Throws out 6 razor sharp flails");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 54;
            Item.damage = 470;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item116;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.shoot = ModContent.ProjectileType<Sagittarius_Proj>();
            Item.shootSpeed = 22f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EventHorizon>());
			recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 12);
	        recipe.AddTile(ModContent.TileType<ACS_Tile>());
	        recipe.Register();
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
	    	float ai3X = (Main.rand.NextFloat() - 0.50f) * 0.7853982f; //0.5
            float ai3Y = (Main.rand.NextFloat() - 0.25f) * 0.7853982f; //0.5
            float ai3Z = (Main.rand.NextFloat() - 0.12f) * 0.7853982f;
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, player.whoAmI, 0.0f, ai3);

            }
            return false;
        }
	}
}
