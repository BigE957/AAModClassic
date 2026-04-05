using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    public class InfinityCore : InfinityZero
    {
		
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Zero");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 38;
            NPC.height = 44;
        }

        public int varTime = 0;

        public int YvarOld = 0;

        public int XvarOld = 0;
        public NPC Body;
        public InfinityZero iz = null;
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        private int CoreFrame;
        private int CoreCounter;

        public override void AI()
        {
            Body = Main.npc[(int)NPC.ai[0]];
            NPC.realLife = (int)NPC.ai[0];

            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (iz == null)
            {
                NPC npcBody = Main.npc[(int)NPC.ai[0]];
                if (npcBody.type == ModContent.NPCType<InfinityZero>())
                {
                    iz = (InfinityZero)npcBody.ModNPC;
                }
            }

            if (!Body.active)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghost heads'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }
            
            if (!player.active || player.dead || !Body.active)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead || !Body.active)
                {
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }
            
            Vector2 moveTo = Body.Center - NPC.Center;
            NPC.velocity = moveTo;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if(NPC.frameCounter > 5)
            {
                NPC.frameCounter = 0;
                CoreCounter += 1;
            }
            if (CoreCounter > 4)
            {
                CoreCounter = 0;
            }
            NPC.frame.Y = CoreCounter * frameHeight;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                //TODOIZ
                //damage /= 2;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }

    }
}
