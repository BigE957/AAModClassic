using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Weapons
{
    public class Bubbleshot : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Bubbleshot");
        }

		public override void SetDefaults()
		{
			Item.damage = 29;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 20;
			Item.useTime = 5;
			Item.useAnimation = 13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = 50000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item85;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Bubbleshot_FuckingBubbles>();
			Item.shootSpeed = 4f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
