using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Inferno
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class TheStride : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flamebrute");
			Main.npcFrameCount[NPC.type] = 6;
		}

		public override void SetDefaults()
		{
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
			Banner = NPC.type;
            NPC.width = 62;
            NPC.height = 90;
            NPC.damage = 12;
            NPC.defense = 4;
            NPC.lifeMax = 90;
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
                    NPC.frame.Y = 0;
                    return;
                }
        }

        public override void AI()
		{
            NPC.ai[0] = 0.2f;
            NPC.ai[1] = 1.2f;
            NPC.TargetClosest(false);
            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center + new Vector2(NPC.spriteDirection * (NPC.width * 4), 0);
            NPC.TargetClosest(true);
            float accel2 = Math.Abs(NPC.Center.X - player.Center.X) / 140;
            if (accel2 > 0.6f)
                accel2 = 0.6f;
            if (NPC.Center.X < player.Center.X)
            {
                NPC.velocity.X += NPC.ai[0] * accel2;
            }

            if (NPC.Center.X > player.Center.X)
            {
                NPC.velocity.X -= NPC.ai[0] * accel2;
            }

            if (Math.Abs(NPC.velocity.X) == NPC.ai[1])
                NPC.velocity.X = NPC.ai[1] * NPC.spriteDirection;
            base.AI();
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
            if(NPC.life <= 0)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y + 45, Mod.Find<ModNPC>("TheStrideDeathAnimation").Type);
            }
		}
	}
}
