using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class OceanWhaler : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.knockBack = 6f;
            Item.width = 30;
            Item.height = 10;
            Item.damage = 34;
            Item.shoot = ModContent.ProjectileType<Projectiles.OceanWhaler>();
            Item.shootSpeed = 11f;
            Item.UseSound = SoundID.Item10;
            Item.rare = ItemRarityID.Green;
            Item.value = 27000;
            Item.DamageType = DamageClass.Ranged;
        }

		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int num17 = 0; num17 < 1000; num17++)
            {
                if (Main.projectile[num17].active && Main.projectile[num17].owner == Main.myPlayer && Main.projectile[num17].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.Harpoon);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
