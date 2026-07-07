using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    public class ZeroNeutralizer_SingularityArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singularity Arrow");    
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = ProjAIStyleID.Arrow;        
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.arrow = true;
            Projectile.extraUpdates = 2;
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, .5f, .5f, 6, false, 0f, 0f, AAColor.ZeroShield);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, lightColor, false);
            return true;
        }

        int a = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient) a++;
            if (a == 40)
            {
                Projectile.tileCollide = true;
                Projectile.netUpdate = true;
            }
            if (a < 40)
            {
                Projectile.tileCollide = false;
            }
        }
    }
}
