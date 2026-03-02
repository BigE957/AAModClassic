using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Railjaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Railjaw");
        }

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 20;
			Item.useTime = 9;
            Item.reuseDelay = 16;
            Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 1;
			Item.value = 1200;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 5f;
			Item.useAmmo = AmmoID.Bullet;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}
