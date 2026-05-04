using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    public class OrthrusLock : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Target Locked.");
        }
        public override void SetDefaults()
        {
            NPC.width = 74;
            NPC.height = 74;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            NPC.scale *= 10;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, NPC.GetAlpha(Color.White), true);
            return false;
        }

        public NPC orthrus = null;

        public override void AI()
        {
            NPC body = Main.npc[BaseAI.GetNPC(NPC.Center, ModContent.NPCType<OrthrusHead1>(), -1)];
            OrthrusHead1 orthrus = (OrthrusHead1)body.ModNPC;

            Player player = Main.player[NPC.target];

            if (Main.netMode != 1)
            {
                NPC.ai[0]++;
            }

            NPC.rotation += .1f;

            if (NPC.target == -1)
            {
                NPC.TargetClosest();
            }

            if (orthrus.internalAI[0] >= 300)
            {
                if (NPC.alpha < 255)
                {
                    NPC.scale += .5f;
                    NPC.alpha += 5;
                }
                else
                {
                    orthrus.internalAI[0] = 0;
                    body.netUpdate = true;
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (orthrus.internalAI[0] > 240)
                {
                    NPC.velocity *= 0;
                    NPC.netUpdate = true;
                }
                else
                {
                    NPC.Center = player.Center;
                }
                if (NPC.scale > 1f)
                {
                    NPC.scale -= .5f;
                }
                else
                {
                    NPC.scale = 1f;
                }
            }
        }
    }
}