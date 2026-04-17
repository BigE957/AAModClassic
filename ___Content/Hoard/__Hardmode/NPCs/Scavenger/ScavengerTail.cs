using Microsoft.Xna.Framework;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.___Content.Hoard.__Hardmode.NPCs.Scavenger
{
    public class ScavengerTail : ScavengerHead
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scavenger");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 44;
            NPC.height = 44;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Gold, hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Gold, hit.HitDirection, -1f, 0, default, 1f);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 1, NPC.frame, drawColor, true);
            return false;
        }
    }
}

