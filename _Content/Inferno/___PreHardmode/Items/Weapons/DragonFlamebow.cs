using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Ammo;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class DragonFlamebow : BaseAAItem
    {

        public override void SetDefaults()
        {

            Item.damage = 14;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 60;
            Item.scale *= .8f;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<DragonArrow_Proj>();
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;

        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Flamebow");
            // Tooltip.SetDefault("Transforms arrows into Dragon Arrows");
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DragonArrow_Proj>(), damage, knockback, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 8);
			recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
