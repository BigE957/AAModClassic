using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA
{
    public class CursedScarab : ModNPC
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Scarab");
            Main.npcFrameCount[NPC.type] = 3;
		}

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 38;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 1200;
            NPC.defense = 40;
            NPC.damage = 60;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath35;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
        }
        
        public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode == NetmodeID.Server) { return; }
			for (int m = 0; m < (NPC.life <= 0 ? 30 : 8); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, 1.1f);
			}		
		}

		public override void FindFrame(int dummy)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 2)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += dummy;
                if (NPC.frame.Y > dummy * 2)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

		public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			for (int m = NPC.oldPos.Length - 1; m > 0; m--)
			{
				NPC.oldPos[m] = NPC.oldPos[m - 1];
			}
			NPC.oldPos[0] = NPC.position;

			BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.15f, 0.08f, 8f, 7f, false);
            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = NPC.spriteDirection = -1;
            }
            else
            {
                NPC.direction = NPC.spriteDirection  = 1;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D bodyTex = TextureAssets.Npc[NPC.type].Value;
            Color lightColor = BaseDrawing.GetNPCColor(NPC, null);
			if(Main.player[NPC.target] != null && Main.player[NPC.target].active && !Main.player[NPC.target].dead) 
                BaseDrawing.DrawAfterimage(Main.spriteBatch, bodyTex, 0, NPC, 3f, 0.9f, 4, true, 0f, 0f, Color.LightGreen);
            BaseDrawing.DrawTexture(Main.spriteBatch, bodyTex, 0, NPC, lightColor);
			return false;
		}
	}
}