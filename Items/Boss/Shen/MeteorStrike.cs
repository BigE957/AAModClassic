using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AAMod.Items.Boss.Shen   //where is located
{
    public class MeteorStrike : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Meteor Strike");
            /* Tooltip.SetDefault(@"Fires a barrage of meteors at your foes
Hitting enemies causes a smaller, but more damaging explosion
Hitting a tile causes a larger, but less damaging projectile
Inflicts Discordian Inferno"); */

        }


        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("Meteor").Type;
            Item.damage = 400;            
            Item.DamageType = DamageClass.Magic;            //if it's magic
            Item.width = 32;              
            Item.height = 36;             
            Item.useTime = 16;          
            Item.useAnimation = 16;
            Item.useStyle = 5;      
            Item.knockBack = .5f;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.mana = 10;
            Item.UseSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.shootSpeed = 16f;
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

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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
            type = Main.rand.Next(3);

            switch (type)
            {
                case 0:
                    type = Mod.Find<ModProjectile>("Meteor").Type;
                    break;
                case 1:
                    type = Mod.Find<ModProjectile>("MeteorRed").Type;
                    break;
                default:
                    type = Mod.Find<ModProjectile>("MeteorBlue").Type;
                    break;
            }


            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
            return false;
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(Mod.Find<ModBuff>("DiscordInferno").Type, 600);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SunStorm", 1);
            recipe.AddIngredient(null, "Toxibomb", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
