using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class DoomiteAssaultBlaster : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 19;
            Item.useTime = 19;
            Item.width = 52;
            Item.height = 20;
            Item.UseSound = SoundID.Item12;
            Item.knockBack = 2;
            Item.damage = 15;
            Item.shootSpeed = 9f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.value = 20000;
            Item.shoot = ModContent.ProjectileType<Projectiles.DoomiteVortex>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Assault Blaster");
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Doomite", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
