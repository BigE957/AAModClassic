using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DoomiteHolobow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Holobow");
        }

        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 64;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item12;
            Item.autoReuse = true;
            Item.shootSpeed = 40f;
            Item.crit = 0;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Doomite", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<Projectiles.HoloArrow>(), Item.damage, knockBack, player.whoAmI);
            return false;
        }
    }
}