using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.FishingItem
{
    public class SwimmingHydra : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Swimming Hydra");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 36;
			Item.height = 40;
			Item.useTime = 27;
			Item.useAnimation = 27;  
			Item.useStyle = 1;
            Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 4, 0, 0);
			Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
            Item.shootSpeed = 10;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.HydraSlash>();
		}

        int shoot = 0;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 3 != 0) return false;

            shoot = 0;
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Terraria.ModLoader.ModContent.BuffType<Buffs.HydraToxin>(), 180);
		}
	}
}
