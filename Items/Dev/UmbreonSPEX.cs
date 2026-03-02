using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Dev
{
    public class UmbreonSPEX : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Umbra");
			/* Tooltip.SetDefault(@"A dark sword from a dark creature
Blade of Night EX"); */
		}
		
		public override void SetDefaults()
		{
			Item.damage = 436;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 100;
			Item.height = 100;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 7;
			Item.value = Item.sellPrice(1, 1, 50, 0);
			Item.rare = 2;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("UmbreonSPProjectile").Type;
			Item.shootSpeed = 18f;
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
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
		
		public override void AddRecipes()
        {
		    Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UmbreonSP", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(TileID.LunarCraftingStation); // (null, "ModTileID");
		    recipe.Register();
        }
	}
}
