using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    public class IceCrystal : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 600;
            NPC.defense = 10;
            NPC.width = 30;
            NPC.height = 30;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            NPC.value = 0;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.Item30;
            NPC.DeathSound = SoundID.Item27;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ice Crystal");
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[2]++;
            }
            if (NPC.alpha > 40)
            {
                NPC.alpha -= 3;
            }
            else
            {
                //TODO: speed based on distance or something
                int p = BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<IceSpike>(), ref NPC.ai[0], 80, NPC.damage / 2, 7, true);
                if (p != -1)
                {
                    Main.projectile[p].ai[1] = NPC.ai[1];
                    int pieCut = 8;
                    float radians = MathHelper.TwoPi / pieCut;
                    for (int i = 0; i < pieCut; i++)
                    {
                        int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 0.6f);
                    }
                }

                NPC.alpha = 40;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight * (int)NPC.ai[1];
        }

        public override void OnKill()
        {
            SoundEngine.PlaySound(SoundID.Item50, NPC.position);
            int pieCut = 20;
            float radians = MathHelper.TwoPi / pieCut;
            for (int i = 0; i < pieCut; i++)
            {
                int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = Vector2.Normalize(new Vector2(6, 0).RotatedBy(radians * i));
            }
            for (int i = 0; i < pieCut; i++)
            {
                int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = Vector2.Normalize(new Vector2(9, 0).RotatedBy(radians * i));
                Main.dust[dustID].noLight = false;
            }
        }
    }
}
