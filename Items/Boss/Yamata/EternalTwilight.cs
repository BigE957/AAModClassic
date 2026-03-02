using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.Projectiles.Yamata;

namespace AAMod.Items.Boss.Yamata
{
    public class EternalTwilight : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eternal Twilight");
            // Tooltip.SetDefault("Falling Twilight EX");
        }

        public override void SetDefaults()
        {
            Item.damage = 237;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 76;
            Item.useAnimation = 17;
            Item.useTime = 5;
            Item.reuseDelay = 7;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = 1;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            Item.expert = true; Item.expertOnly = true;
            Item.rare = 9;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation - 1);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!(player.itemAnimation == 1))
            {
                float SpeedX = speedX + Main.rand.Next(-25, 26) * 0.05f;
                float SpeedY = speedY + Main.rand.Next(-25, 26) * 0.05f;
                Projectile.NewProjectile(position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<YamataPhantom>(), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FallingTwilight");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}
