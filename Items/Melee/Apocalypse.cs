using AAModClassic;
using AAModClassic.Items.Boss;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Apocalypse : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Apocalypse");
            /* Tooltip.SetDefault(@"The Flaming Jacks travel towards the sunset, where
souls travel to reach the afterlife.
Horseman's Blade EX"); */
        }

		public override void SetDefaults()
		{
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.damage = 1000;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 20f;
            Item.width = 54;
			Item.height = 54;    
            Item.knockBack = 6.5f;
            Item.useTime = 5;
			Item.useAnimation = 5;
			Item.value = 1000000;
            Item.expert = true; Item.expertOnly = true;

			glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
			glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
			glowmaskDrawColor = Color.White;  //glowmask draw color
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TheHorsemansBlade);
			recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
			recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
			recipe.Register();
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            float screenX = Main.screenPosition.X;
            if (player.direction < 0)
            {
                screenX += Main.screenWidth;
            }

            //change to make more/less projectiles
            float screenY = Main.screenPosition.Y;
            screenY += Main.rand.Next(Main.screenHeight);
            Vector2 vector = new Vector2(screenX, screenY);
            float velocityX = target.Center.X - vector.X;
            float velocityY = target.Center.Y - vector.Y;
            velocityX += Main.rand.Next(-50, 51) * 0.1f;
            velocityY += Main.rand.Next(-50, 51) * 0.1f;
            int num5 = 24;
            float num6 = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
            num6 = num5 / num6;
            velocityX *= num6;
            velocityY *= num6;
            Projectile p = Projectile.NewProjectileDirect(target.GetSource_OnHurt(null), new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<Projectiles.Apocalypse>(), damageDone, 0f, player.whoAmI);
            p.tileCollide = false;
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
