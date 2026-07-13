using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.NPCs.__BossSagittarius;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class ProtocolR : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Protocol-R");
            //Tooltip.SetDefault(@"fires a barrage of special rockets");
        }       

        public override void SetDefaults()
		{
			Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 30;
			Item.useTime = 3;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; 
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/Dayshot");
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 6;
            Item.reuseDelay = 100;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, -9);
        }

        int shoot = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
                        //for (int i = 0; i < 3; i++)
            //{
            //    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(60));
            //    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SagRocketF>(), damage, knockBack, player.whoAmI);
            //}
            if (shoot++ > 6) shoot = 0;

            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(30)) * .5f;
                Projectile.NewProjectile(Item.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<ProtocolR_RaiderRocket>(), damage, knockback, player.whoAmI);
            }

            if (Main.rand.Next(3) == 0)
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                //Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<SagRocketF>(), damage * 2, knockBack, player.whoAmI);
                //shoot = 0;
                for (int i = 0; i < Main.rand.Next(2); i++)
                {
                    Vector2 perturbedSpeed2 = velocity.RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile.NewProjectile(Item.GetSource_FromThis(), position, perturbedSpeed2, ModContent.ProjectileType<ProtocolR_RaiderRocket>(), (int)(damage * 1.5f), knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 40);
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
