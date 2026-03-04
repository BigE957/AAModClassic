using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;

using Terraria.ModLoader;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.AH.Ashe
{
    public class AsheOrbiter : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flame Vortex");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 46;
            NPC.friendly = false;
            NPC.lifeMax = 1300;
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
                body = reader.ReadInt();
                rotValue = reader.ReadFloat();
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
                NPC.alpha -= 4;
            }
            else
            {
                NPC.alpha = 0;
            }
            NPC.noGravity = true;
            body = (int)NPC.ai[0];
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("Ashe").Type, 120f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;

            NPC ashe = Main.npc[body];
            if (ashe == null || ashe.life <= 0 || !ashe.active || ashe.type != Mod.Find<ModNPC>("Ashe").Type) { NPC.active = false; return; }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            if (rotValue == -1f) rotValue = NPC.ai[3];
            rotValue += 0.05f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            NPC.Center = BaseUtility.RotateVector(ashe.Center, ashe.Center + new Vector2(140f, 0f), rotValue);
        }

        public override void OnKill()
        {
            float spread = 60f * 0.0174f;
            double startAngle = Math.Atan2(NPC.velocity.X, -NPC.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6;
            double offsetAngle;
            for (int i = 0; i < 6; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), ModContent.ProjectileType<AsheMagicSpark>(), NPC.damage / 2, 0, Main.myPlayer, 0f, 0f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, NPC.GetAlpha(Color.White), true);
            return false;
        }
    }
}