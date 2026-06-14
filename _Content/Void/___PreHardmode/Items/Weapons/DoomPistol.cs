using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Void.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Weapons
{
    public class DoomPistol : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        
        public override void SetDefaults()
        {

            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 22;
            Item.useTime = 22;
            Item.width = 24;
            Item.height = 28;
            Item.UseSound = SoundID.Item12;
            Item.knockBack = 0.75f;
            Item.damage = 20;
            Item.shootSpeed = 25f;
            Item.noMelee = true;
            Item.scale = 0.8f;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Ranged;
            Item.value = 2000;
            Item.shoot = ModContent.ProjectileType<Darkray>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doom Pistol");
        }

		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
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
    }
}
