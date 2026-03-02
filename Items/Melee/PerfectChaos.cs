using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Melee
{
    public class PerfectChaos : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Perfect Chaos");
			// Tooltip.SetDefault("Chaos EX");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 375;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 120;
			Item.height = 120;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 10;
            Item.value = Item.sellPrice(5, 0, 0, 0);
            Item.rare = 8;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("ChaosShotP").Type;
            Item.shootSpeed = 16f;
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, "ReignOfFire", 1);
			recipe.AddIngredient(Mod, "Masamune", 1);
            recipe.AddIngredient(Mod, "Chaos", 1);
            recipe.AddIngredient(Mod, "EXSoul", 1);
            recipe.AddIngredient(Mod, "ChaosCrystal", 1);
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 20f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
		    double deltaAngle = spread / 6f;
		    double offsetAngle;
		    for (int i = 0; i < 3; i++)
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockBack, Main.myPlayer);
		    }
		    return false;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 500);
			target.AddBuff(Mod.Find<ModBuff>("Moonraze").Type, 500);
        }
	}
}
