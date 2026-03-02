using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
	public class MidasGun : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Midas Blaster");
			// Tooltip.SetDefault("Shoot stuff and get more money.");
		}
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
            Item.scale *= .8f;
        }
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, -6);
		}
		
        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddIngredient(ItemID.FlaskofGold, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
            return true; 
        }
    }
}
