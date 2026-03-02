using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class ZeroDeath1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero");
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public bool linesaid = false;
        public override void AI()
        {
            if (Main.expertMode && !AAWorld.downedZero && !linesaid)
            {
                if (Main.netMode != 1)
                {
                    AAMod.Chat(Lang.BossChat("ZeroDeath1"), Color.Red.R, Color.Red.G, Color.Red.B);
                    linesaid = true;
                }
            }
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 7)
                {
                    Projectile.Kill();
                   
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y += 0.00f;
           
        }
        public override void OnKill(int timeLeft)
        {
            if (!AAWorld.downedZero && Main.expertMode)
            {
                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("ZeroDeath3"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
            int p = Projectile.NewProjectile(Projectile.Center, new Vector2(0f, 0f), Mod.Find<ModProjectile>("ZeroDeath2").Type, 0, 0);
            Main.projectile[p].Center = Projectile.Center;
        }
    }
}