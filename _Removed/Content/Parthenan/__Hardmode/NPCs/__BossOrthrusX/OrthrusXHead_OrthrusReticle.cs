using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossOrthrusX
{
    public class OrthrusXHead_OrthrusReticle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orthrus Reticle");
            this.HideFromBestiary();
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
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.GetAlpha(Color.White), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, 0, 0);
            return false;
        }

        public NPC orthrus = null;

        public override void AI()
        {
            OrthrusXHead orthrus = Main.npc[(int)NPC.ai[0]].ModNPC as OrthrusXHead;

            if(orthrus == null)
            {
                NPC.active = false;
                return;
            }

            Player player = Main.player[NPC.target];

            NPC.rotation += .1f;

            if (NPC.target == -1)
            {
                NPC.TargetClosest();
            }

            if (NPC.alpha > 0)
            {
                NPC.scale += .5f;
                NPC.alpha -= 5;
            }
            else
            {
                NPC.alpha = 0;
            }

            if (orthrus.internalAI[0] % 300 > 240)
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