using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BladeOfEvil : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 52;
            Item.height = 52;
            Item.useTime = 30;
            Item.useAnimation = 30;     
            Item.useStyle = 1;
            Item.knockBack = 4;
            Item.value = 10000;        
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.EvilFlare>();
            Item.shootSpeed = 9;
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 14);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 117);
			}
		}
		
		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("Blade of Evil");
		  /* Tooltip.SetDefault(@"The perfect balance between Corruption and Crimson
Shoots alternating fireballs of Ichor and Cursed Flames"); */
		}

        int Shot = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Shot++;
            int proj = 0;
            if (Shot % 2 == 0)
            {
                proj = 1;
            }
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, proj);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }
}
