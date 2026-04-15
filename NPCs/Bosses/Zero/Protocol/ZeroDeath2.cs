using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Zero.Protocol
{
    public class ZeroDeath2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero");
            Main.projFrames[Projectile.type] = 30;
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
            Projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 30)
                {
                    Projectile.Kill();
                }
            }
            Projectile.velocity.X *= 0.00f;
            Projectile.velocity.Y += 0.00f;
            if (Projectile.timeLeft >= 913)
            {
                if (Projectile.timeLeft == 913)
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.Zero.Awakened.Defeat.First.3"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
        }
    }
}