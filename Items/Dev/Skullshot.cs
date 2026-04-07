using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic.Items.Dev
{
    public class Skullshot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Skullshot");
            /* Tooltip.SetDefault(@"fires a massive spread of bullets at your foes
Right click to fire spinning bones at your foe
Uses Bullets and Bones as ammo
'I have an irrational hatred for gods`
-Gibs"); */
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.knockBack = 7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.width = 46;
            Item.height = 20;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item36;
            Item.damage = 90;
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.value = 100000;
            Item.rare = ItemRarityID.Cyan;
            Item.DamageType = DamageClass.Ranged;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name);
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                new Color(255, 128, 0),
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 15;
                Item.useTime = 5;
                Item.reuseDelay = 17;
                Item.useAmmo = AAMod.BoneAmmo;
                Item.damage = 375;
            }
            else
            {
                Item.useAnimation = 28;
                Item.useTime = 28;
                Item.reuseDelay = 0;
                Item.useAmmo = AmmoID.Bullet;
                Item.damage = 95;
            }
            return base.CanUseItem(player);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 2) || player.altFunctionUse != 2;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                float spread = Main.rand.Next(20, 30) * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Main.rand.Next(5, 11); i++)
                {
                    float randomSpeed = baseSpeed + Main.rand.NextFloat() * 1.5f;
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, randomSpeed * (float)Math.Sin(offsetAngle), randomSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, Main.myPlayer);
                }
            }
            else
            {
                int proj = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ProjectileID.BoneGloveProj, damage, knockback, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GibsFemur>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
