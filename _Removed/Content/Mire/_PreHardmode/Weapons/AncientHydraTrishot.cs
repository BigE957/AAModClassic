using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;

namespace AAModClassic._Removed.Content.Mire._PreHardmode.Weapons
{
    public class AncientHydraTrishot : ModItem
    {
        public override void SetDefaults()
        {

            Item.damage = 10;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = 2000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;

        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Hydra Trishot");
            // Tooltip.SetDefault("'It even purifies the corruption!'");

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<HydraTrishot>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<HydraTrishot>()] = Type;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt((velocity.X * velocity.X) + (velocity.Y * velocity.Y));
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Item.playerIndexTheItemIsReservedFor);
            }
            return false;
        }
    }
}