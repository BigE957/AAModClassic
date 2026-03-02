using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortswordEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ultima Shortsword");
			// Tooltip.SetDefault("Copper Shortsword EX");
        }
		public override void SetDefaults()
		{
            
			Item.damage = 1000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 10;
			Item.useStyle = 3;
			Item.knockBack =20;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 9;
			Item.expert = true; Item.expertOnly = true;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("TrueCopperShot").Type;
            Item.shootSpeed = 20f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer);
            Main.projectile[proj].usesLocalNPCImmunity = true;
            Main.projectile[proj].localNPCHitCooldown = 6;
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "TrueCopperShortsword", 1);
			recipe.AddIngredient(null, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}
