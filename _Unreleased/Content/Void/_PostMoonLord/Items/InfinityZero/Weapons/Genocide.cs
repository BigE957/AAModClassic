using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;
using System;
using AAModClassic.Globals;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class Genocide : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Genocide");
            /* Tooltip.SetDefault(@"Fires a spread of infinitely piercing lasers that ignores tiles and home in on enemies
Gets stronger the more the laser pierces
Doesn't require ammo"); */
        }

        public override void SetDefaults()
        {
            
            Item.damage = 500;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 74;
            Item.height = 24;
            Item.useTime = 20;
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.shoot = ModContent.ProjectileType<Antimatter_Unreleased>();
            Item.knockBack = 12;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            //TODOIZ
            //Item.UseSound = new LegacySoundStyle(2, 75, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
            Item.crit = 5; 
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

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("_Unreleased/Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
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
                Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            //TODOIZ this item doesnt exist anymore
            //recipe.AddIngredient(null, "AntimatterRifle", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}