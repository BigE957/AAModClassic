using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert.__Hardmode.NPCs.__BossAnubis
{
    public class Scarab : ModNPC
	{
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 3;
		}

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 38;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 400;
            NPC.defense = 30;
            NPC.damage = 40;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath35;
            NPC.knockBackResist = 0.2f;
            NPC.noGravity = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            ]);
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
            BaseAI.AIFlier(NPC, ref NPC.ai, false, 0.3f, 0.2f, 6f, 4.5f, false, 250);
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
                DrawingUtils.DrawAfterimageWithVelocity(spriteBatch, bodyTex, NPC.Center - Main.screenPosition, NPC.velocity, 4, NPC.frame, lightColor, NPC.scale, [NPC.rotation], NPC.frame.Size() * 0.5f, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 3, 0.9f);
            BaseDrawing.DrawAfterimage(spriteBatch, bodyTex, 0, NPC, 3f, 0.9f, 4, true, 0f, 0f, lightColor);
            spriteBatch.Draw(bodyTex, NPC.Center - screenPos, NPC.frame, lightColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
			return false;
		}
	}
}