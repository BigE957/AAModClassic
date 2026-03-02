using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Serpent
{
    public class SubzeroSlasher : BaseAAItem
    {
        private static int shoot;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Slasher");
            // Tooltip.SetDefault("Has a chance to shoot a subzero projectile on swing");
        }

        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.CrystalBullet;
            Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 2 != 0) return false;
            shoot = 0;
            Main.projectile[type].DamageType = DamageClass.Melee;
            Main.projectile[type].ranged = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockback);
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 120);
        }
    }
}