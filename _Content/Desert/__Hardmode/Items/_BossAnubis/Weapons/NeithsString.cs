using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Desert.__Hardmode.Items.Materials;
using AAModClassic._Content.Desert.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis.Weapons
{
    public class NeithsString : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Neith's String");
            /* Tooltip.SetDefault(@"Shoots 2 arrows at once
Can occasionally shoot ``Judgement arrow``, which lowers enemy defense
Converts wooden arrows into slower, but high-damaging mummy arrows"); */
        }
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 60;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;
            Item.value = Item.buyPrice(0, 1, 0, 0);

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(3);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ModContent.ProjectileType<NeithsString_MummyArrow>();
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X * .7f, perturbedSpeed.Y * .7f, type, (int)(damage * 1.5f), knockback, player.whoAmI);
                }
                else
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
			}
			if (Main.rand.NextBool(5))
			{
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<NeithsString_JudgementArrow>(), damage, knockback, player.whoAmI, 0f, 0f);
			}
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FossilBoneslinger>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ForsakenFragment>(), 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
