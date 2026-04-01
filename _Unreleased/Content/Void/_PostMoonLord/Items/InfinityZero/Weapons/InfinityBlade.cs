using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using System;
using AAModClassic;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic._Unreleased.Dusts;
using AAModClassic._Unreleased.Projectiles.Zero;

namespace AAModClassic._Unreleased.Items.Boss.Infinity

{
    public class InfinityBlade : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Blade");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 400;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 94;
			Item.height = 94;
			Item.useTime = 13;
            Item.shoot = ModContent.ProjectileType<Rift_Unreleased>();
            Item.shootSpeed = 14f;
            Item.useAnimation = 13;
			Item.useStyle = 1;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(1, 0, 0, 0);
            //TODOIZ
            //Item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
			Item.autoReuse = true;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 30f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 5; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "RiftShredder", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<VoidDust_Unreleased>(), 0f, 0f, 46, default(Color), 1.25f);
			dust.noGravity = true;
        }
	}
}
