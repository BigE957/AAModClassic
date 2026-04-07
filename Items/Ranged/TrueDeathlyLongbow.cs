using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.Projectiles;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Ranged
{
    public class TrueDeathlyLongbow : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Ghastbow");
            /* Tooltip.SetDefault(@"Replaces Arrows with Reaper Arrows
Fires an explosive ghast skull every other shot"); */
        }

        public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 86;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Arrow;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.statLife += damageDone / 8;
            player.HealEffect(damageDone / 8);
        }

        int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ReaperArrow>(), damage, knockback, player.whoAmI);
            }
            shoot++;

            if (shoot % 2 != 0) return false;

            if (shoot >= 2)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<GhastSkull>(), (int)(damage * 1.0), knockback, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DeathlyLongbow>(), 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddIngredient(ModContent.ItemType<HeroShards>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
