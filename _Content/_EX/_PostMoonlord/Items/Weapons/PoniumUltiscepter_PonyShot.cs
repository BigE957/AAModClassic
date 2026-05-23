using AAModClassic.Assets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class PoniumUltiscepter_PonyShot : ModProjectile
    {
        public override string Texture => AssetDirectory.General.Nothing;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pony Shot");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = ProjAIStyleID.Inferno;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                Projectile.localAI[0] += 1f;
            }
            bool flag15 = false;
            bool flag16 = false;
            if (Projectile.velocity.X < 0f && Projectile.position.X < Projectile.ai[0])
            {
                flag15 = true;
            }
            if (Projectile.velocity.X > 0f && Projectile.position.X > Projectile.ai[0])
            {
                flag15 = true;
            }
            if (Projectile.velocity.Y < 0f && Projectile.position.Y < Projectile.ai[1])
            {
                flag16 = true;
            }
            if (Projectile.velocity.Y > 0f && Projectile.position.Y > Projectile.ai[1])
            {
                flag16 = true;
            }
            if (flag15 && flag16)
            {
                Projectile.Kill();
            }
            for (int num457 = 0; num457 < 10; num457++)
            {
                int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 1.2f);
                Main.dust[num458].noGravity = true;
                Main.dust[num458].velocity *= 0.5f;
                Main.dust[num458].velocity += Projectile.velocity * 0.1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PoniumUltiscepter_Ponysplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }

    }
}
