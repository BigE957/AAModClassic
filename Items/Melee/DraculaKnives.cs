using System;
using AAModClassic.Items.Boss;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class DraculaKnives : BaseAAItem
	{

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dracula Knives");
            /* Tooltip.SetDefault(@"Rapidly throw life stealing daggers
Vampire Knives EX"); */
        }

        public override void SetDefaults()
		{
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 15f;
            Item.shoot = ProjectileID.VampireKnife;
            Item.damage = 60;
            Item.width = 18;
            Item.height = 20;
            Item.UseSound = SoundID.Item39;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.knockBack = 2.75f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 5; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
            }
            return true;
            /*Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default);
            Vector2 vector3 = Main.MouseWorld - vector2;
            Vector2 vector4 = player.itemRotation.ToRotationVector2() * (float)player.direction;
            int num76 = item.damage;
            int num74 = item.shoot;
            float num75 = item.shootSpeed;
            float num77 = item.knockBack;
            int num149 = 4;
            float num81 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
            float num83 = (float)Math.Sqrt((double)(num81 * num81 + num82 * num82));
            float num84 = num83;
            num81 *= num83;
            num82 *= num83;
            if (Main.rand.Next(2) == 0)
            {
                num149++;
            }
            if (Main.rand.Next(4) == 0)
            {
                num149++;
            }
            if (Main.rand.Next(8) == 0)
            {
                num149++;
            }
            if (Main.rand.Next(16) == 0)
            {
                num149++;
            }
            for (int num150 = 0; num150 < num149; num150++)
            {
                float num151 = num81;
                float num152 = num82;
                float num153 = 0.05f * (float)num150;
                num151 += (float)Main.rand.Next(-35, 36) * num153;
                num152 += (float)Main.rand.Next(-35, 36) * num153;
                num83 = (float)Math.Sqrt((double)(num151 * num151 + num152 * num152));
                num83 = num75 / num83;
                num151 *= num83;
                num152 *= num83;
                float x4 = vector2.X;
                float y4 = vector2.Y;
                Projectile.NewProjectile(x4, y4, num151, num152, num74, num76, num77, i, 0f, 0f);
            }
            return false;*/
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
		    recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
		}
    }
}
