using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class TheSquirter : BaseAAItem
    {

        public override void SetDefaults()
        {

            Item.damage = 84;
            Item.noMelee = true;

            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 26;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<Squirt>();
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = false;
            Item.shootSpeed = 14f;

        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, -2);
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

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Squirter");
            // Tooltip.SetDefault("Doesnt use ammo");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SlimeGun, 1);
            recipe.AddIngredient(ItemID.Gel, 200);
            recipe.AddIngredient(null, "DeepAbyssium", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
