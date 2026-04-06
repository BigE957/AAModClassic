using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Projectiles;
using AAModClassic;

namespace AAModClassic.Items.Ranged
{
    public class Blastshot : BaseAAItem
    {
        
        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 62;
            Item.height = 24;
            Item.useTime = 7;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Gel;
            Item.shoot = ModContent.ProjectileType<Projectiles.DragonfireProj>();
            Item.knockBack = 0;
            Item.value = 100000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shootSpeed = 14f;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blastshot");
            // Tooltip.SetDefault("Consumes Gel");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DragonFire", 5);
            recipe.AddIngredient(null, "IncineriteBar", 10);
            recipe.AddIngredient(null, "SoulOfSmite", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
