using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Equinox
{
    public class NCCloud : ModNPC
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nightclawer Cloud");
             Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.friendly = false;
            NPC.damage = 80;
            NPC.lifeMax = 1500;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(body);
                writer.Write(rotValue);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                body = reader.ReadInt32();
                rotValue = reader.ReadSingle();
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public override void AI()
        {
            if (NPC.frameCounter++ > 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 46;
                if (NPC.frame.Y >= 46 * 4)
                {
                    NPC.frame.Y = 0;
                }
            }

            if (NPC.alpha > 0)
            {
                NPC.alpha -= 10;
            }
            else
            {
                NPC.alpha = 0;
            }

            if(NPC.alpha == 205)
            {
                SpawnDust();
            }
            NPC.noGravity = true;
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<NightcrawlerHead>(), 120f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;

            NPC NC = Main.npc[body];
            if (NC == null || NC.life <= 0 || !NC.active || NC.type != ModContent.NPCType<NightcrawlerHead>()) { NPC.active = false; return; }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            if (rotValue == -1f) rotValue = NPC.ai[3];
            rotValue += 0.05f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            NPC.Center = BaseUtility.RotateVector(NC.position, NC.position + new Vector2(140f, 0f), rotValue);

            int aiTimerFire = 0;

            NPC.ai[1]++;

            if (NPC.ai[3] == 1 || NPC.ai[3] == 4 || NPC.ai[3] == 7 || NPC.ai[3] == 10)
            {
                aiTimerFire = 50;
            }
            if (NPC.ai[3] == 2 || NPC.ai[3] == 5 || NPC.ai[3] == 8 || NPC.ai[3] == 11)
            {
                aiTimerFire = 100;
            }
            if (NPC.ai[3] == 3 || NPC.ai[3] == 6 || NPC.ai[3] == 9 || NPC.ai[3] == 12)
            {
                aiTimerFire = 150;
            }

            if (NPC.ai[1] >= 150)
            {
                NPC.ai[1] = 0;
            }

            if (NPC.ai[1] == aiTimerFire && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 speed = new Vector2(1f, 0f).RotatedBy((float)(Main.rand.NextDouble() * 3.1415f)) * 6f;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<NightcrawlerNothing>(), NPC.damage / 4, 0, Main.myPlayer);
            }

            if (Main.dayTime)
            {
                NPC.active = false;
                NPC.NPCLoot();
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }

        public override void OnKill()
        {
            SpawnDust();
            NPC.active = false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Obstructed, 60);
        }

        public void SpawnDust()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.NightcrawlerDust>(), 0, 0, 100, new Color(), 2f);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
        }
    }
}