using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class Sting : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sting");
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Bullet);
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Dust dust1;
            Vector2 position = Projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, 0, 0, ModContent.DustType<Dusts.SnowDust>(), 4f, 0f, 46, default, 1f)];
            dust1.noGravity = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 100);
        }
    }
}