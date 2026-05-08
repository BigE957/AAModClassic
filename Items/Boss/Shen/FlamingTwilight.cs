using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic.Globals;
using AAModClassic.Projectiles.Shen;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Shen
{
    public class FlamingTwilight : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.damage = 400;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 76;
			Item.height = 36;
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 6;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<DiscordianInfernoF>();
			Item.shootSpeed = 11f;
			Item.useAmmo = AmmoID.Gel;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flaming Twilight");
			/* Tooltip.SetDefault(@"Left click to blasts a discordian fireball at your foes 
Right click to rain fire and fury at your cursor position
Consumes gel as ammo
33% chance not to consume gel"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<DiscordianInfernoF>();
            if (player.altFunctionUse == 2)
            {
                float num72 = Item.shootSpeed;
                int num112 = 5;
                for (int num113 = 0; num113 < num112; num113++)
                {
                    Vector2 vector2 = new Vector2(player.position.X + (player.width * 0.5f) + (Main.rand.Next(201) * -player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
                    vector2.Y -= 100 * num113;
                    float num78 = Main.mouseX + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
                    float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (num79 < 0f)
                    {
                        num79 *= -1f;
                    }
                    if (num79 < 20f)
                    {
                        num79 = 20f;
                    }
                    float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
                    num80 = num72 / num80;
                    num78 *= num80;
                    num79 *= num80;
                    float num114 = num78;
                    float num115 = num79 + (Main.rand.Next(-40, 41) * 0.02f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num114 * 0.75f, num115 * 0.75f, type, damage, knockback, player.whoAmI, 0f, 0.5f + (float)(Main.rand.NextDouble() * 0.3f));
                }
                return false;
            }
            else
            {
                float Angle = Main.rand.Next(15, 46);
                float spread = Angle * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
                }
            }
            return false;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Dawnstrike>());
            recipe.AddIngredient(ModContent.ItemType<Darksprayer>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}
