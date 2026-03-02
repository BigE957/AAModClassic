using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Rajah
{

    public class Bunzooka : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bunzooka");
            // Tooltip.SetDefault("Fires Rabbit Rockets");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 66;
            Item.height = 28;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 7.5f;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = Mod.Find<ModProjectile>("RabbitRocket3").Type;
            Item.useAmmo = AmmoID.Rocket;
            Item.rare = ItemRarityID.Yellow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20, -6);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Mod.Find<ModProjectile>("RabbitRocket3").Type, damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}