using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;

namespace AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis.Runes
{
    public class AnubisCircle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.alpha = 255;
            NPC.dontTakeDamage = true;
            NPC.lifeMax = 1;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0.2f;
            NPC.width = 108;
            NPC.height = 108;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.scale = .001f;
            NPC.friendly = false;
        }

        public float internalAI = 0; //Is Moving

        /* [0] = x
         * [1] = y
         * [2] = Type
         * [3] = Anubis
         */

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI = reader.ReadSingle();
            }
        }

        public override void AI()
        {
            bool anubisAlive = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<AnubisUnreleased>())
                    anubisAlive = true;
            }
            if (!anubisAlive)
            {
                NPC.active = false;
                return;
            }
            
            if (internalAI == 0)
            {
                NPC.alpha = 255;
                for (int num468 = 0; num468 < 10; num468++)
                {
                    int num469 = Dust.NewDust(NPC.Center - new Vector2(8, 8), 16, 16, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default, 1f);
                    Main.dust[num469].noGravity = true;
                }
                Move(new Vector2(NPC.ai[0], NPC.ai[1]));
                if (Vector2.Distance(NPC.Center, new Vector2(NPC.ai[0], NPC.ai[1])) < 10 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI = 1;
                    NPC.velocity *= 0;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (Main.npc[(int)NPC.ai[3]].ai[0] == 0)
                {
                    if (NPC.alpha > 50)
                    {
                        NPC.alpha -= 5;
                    }
                    if (NPC.scale < 1)
                    {
                        NPC.scale += .02f;
                    }
                    NPC.rotation += .05f;
                }
                else
                {
                    if (NPC.alpha < 255)
                    {
                        NPC.alpha += 5;
                    }
                    else
                    {
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                    if (NPC.scale < 1)
                    {
                        NPC.scale *= 1.2f;
                    }
                    NPC.rotation -= .05f;
                }
            }

        }

        public void Move(Vector2 point)
        {
            float Speed = 13;
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < Speed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / Speed);
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= Speed;
            NPC.velocity *= velMultiplier;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D Icon = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Desert/__Hardmode/NPCs/__BossAnubis/Runes/RuneTex").Value;
            Rectangle frame = BaseDrawing.GetFrame((int)NPC.ai[2], Icon.Width, Icon.Height / 5, 0, 0);

            spriteBatch.Draw(Icon, NPC.Center - screenPos, frame, NPC.GetAlpha(Color.Cyan), -NPC.rotation, frame.Size() * 0.5f, NPC.scale, 0, 0);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(Color.White), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            return false;
        }
    }
}