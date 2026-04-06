using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu
{
    public class Portal : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rift Portal");
        }
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 120;
            NPC.alpha = 255;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }


        public bool Spawned = false;

        public override void AI()
        {
            NPC.scale = 1f - NPC.alpha / 255f;
            NPC.rotation += .05f;
            NPC.velocity.X = NPC.ai[0];
            NPC.velocity.Y = NPC.ai[1];

            if (NPC.alpha <= 0 && !Spawned)
            {
                SummonEnemy();
                Spawned = true;
            }
            if (!Spawned)
            {
                NPC.alpha -= 3;
            }
            if (Spawned)
            {
                NPC.alpha += 3;
                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
            }
        }

        public override void OnKill()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, 1, ModContent.DustType<Dusts.AkumaADust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.AkumaADust>(), -NPC.velocity.X * 0.2f,
                    -NPC.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public void SummonEnemy()
        {
            int Enemy = Main.rand.Next(3);

            switch (Enemy)
            {
                case 0:
                    Enemy = ModContent.NPCType<DeityDragon>();
                    break;
                case 1:
                    Enemy = ModContent.NPCType<EoA>();
                    break;
                default:
                    Enemy = ModContent.NPCType<RiftVision>();
                    break;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Enemy);
                Main.npc[npcID].Center = NPC.Center;
                Main.npc[npcID].netUpdate = true;
            }

            NPC.active = false;
        }
    }
}