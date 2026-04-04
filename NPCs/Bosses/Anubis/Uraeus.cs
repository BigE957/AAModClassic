using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Anubis
{
    public class Uraeus : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.damage = 40;
			NPC.npcSlots = 5f;
            NPC.damage = 45;
            NPC.width = 20;
            NPC.height = 20;
            NPC.defense = 20;
            NPC.lifeMax = 500;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
			AAAI.AIWorm(NPC, new int[]{ ModContent.NPCType<Uraeus>(), ModContent.NPCType<UraeusBody>(), ModContent.NPCType<UraeusTail>() }, 7, 0f, 10f, 0.07f, true, false, true, true, true);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin, hit.HitDirection, -1f, 0);
            }
            if (NPC.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin, hit.HitDirection, -1f, 0);
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor, true);
            return false;
        }
    }
}