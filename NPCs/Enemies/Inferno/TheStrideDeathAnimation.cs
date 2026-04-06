using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Inferno
{
    public class TheStrideDeathAnimation : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flamebrute");
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.width = 62;
            NPC.height = 88;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = false;
            NPC.aiStyle = -1;
            NPC.timeLeft = 48;

            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (NPC.ai[0] > 0)
            {
                TheStrideGore();
                NPC.life = 0;
            }
            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {
            Player player = Main.player[NPC.target];

            if (NPC.frameCounter++ > 7)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = NPC.frame.Y + frameHeight;
            }
            if (NPC.frame.Y >= frameHeight * 6)
            {
                NPC.ai[0]++;
                NPC.frame.Y = 0;
                return;
            }
        }
        public void TheStrideGore()
        {
          for(int i = 0; i<20; i++)
          {
            int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.RealityDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(0, -10), 6, new Color(255, 0, 0, 255), 1f);
            Main.dust[num].noGravity = false;
            Main.dust[num].velocity *= 2.5f;
            Main.dust[num].noLight = true;
          }
          //TODO: Fake projectile, gore, thing...
            //Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2 (Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), ModContent.ProjectileType<FlamebruteProjectileGore5>(), NPC.damage/2, 4f);
            //Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), ModContent.ProjectileType<FlamebruteProjectileGore4>(), NPC.damage / 2, 4f);
            //Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), ModContent.ProjectileType<FlamebruteProjectileGore3>(), NPC.damage / 2, 4f);            
        }


    }
}
