using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Bubbleshot : BaseAAItem
	{
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
			Item.shoot = Mod.Find<ModProjectile>("Bubble").Type;
			Item.shootSpeed = 4f;
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
