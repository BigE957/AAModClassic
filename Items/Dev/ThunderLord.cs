using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class ThunderLord : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Rifle");
            /* Tooltip.SetDefault(@"Fires off static shots
'NUM'
-BlazenBreaker"); */
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, Terraria.ModLoader.ModContent.ProjectileType<Projectiles.ThunderSpark>(), damage, knockBack, Main.myPlayer, 0, 0);
            return false;
        }

        public override void SetDefaults()
        {
            Item.damage = 175;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged; 
            Item.width = 70; 
            Item.height = 24;
            Item.useTime = 20; 
            Item.useAnimation = 20; 
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = Mod.Find<ModProjectile>("ThunderSpark").Type;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item92;
            Item.autoReuse = true; 
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Bullet;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
			glowmaskDrawType = GLOWMASKTYPE_GUN;
			glowmaskDrawColor = AAColor.COLOR_WHITEFADE1;
			customNameColor = new Color(0, 0, 255);			
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}