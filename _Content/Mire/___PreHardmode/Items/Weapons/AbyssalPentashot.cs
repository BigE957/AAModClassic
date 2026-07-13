using Terraria;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Weapons
{
    public class AbyssalPentashot : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Pentashot");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.shootSpeed = 12f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 20f * 0.0174f;
            float deltaAngle = spread / 6f;
		    for (int i = -2; i <= 2; i++)
		    	Projectile.NewProjectile(source, position, velocity.RotatedBy(deltaAngle * i), type, damage, knockback, Main.myPlayer);
		    return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydraTrishot>(), 1);
            recipe.AddIngredient(ModContent.ItemType<OceanWhaler>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DoomiteAssaultBlaster>(), 1);
            recipe.AddIngredient(ItemID.SnowballCannon, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
