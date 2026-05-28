using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert._PostMoonlord.NPCs.__BossAnubisA
{
    public class Naddaha : ModNPC
	{
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 16;
		}

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 64;
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
            NPC.noTileCollide = true;
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
				Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.ForsakenDust>(), NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, 1.1f);
			}		
		}

		public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (Shooty == true)
                {
                    if (NPC.frame.Y < frameHeight * 8)
                    {
                        NPC.frame.Y = frameHeight * 8;
                    }
                    if (NPC.frame.Y > frameHeight * 15)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Shooty = false;
                    }
                }
                else
                {
                    if (NPC.frame.Y > frameHeight * 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        public bool Shooty = false;

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            BaseAI.AIEye(NPC, ref NPC.ai, false, true, 0.2f, 0.16f, 6f, 2f);
            BaseAI.Look(NPC, 1);

            if (NPC.ai[3] >= 120)
            {
                FireMagic(NPC);
                NPC.ai[3] = 0;
            }

            if (player.Center.X < NPC.Center.X)
            {
                NPC.direction = NPC.spriteDirection = -1;
            }
            else
            {
                NPC.direction = NPC.spriteDirection = 1;
            }
        }

        public void FireMagic(NPC npc)
        {
            Player player = Main.player[npc.target];
            Shooty = true;

            BaseAI.FireProjectile(player.Center, npc, ModContent.ProjectileType<AnubisA_CurseFlame>(), npc.damage / 2, 0f, 2f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D bodyTex = TextureAssets.Npc[NPC.type].Value;
            Color lightColor = BaseDrawing.GetNPCColor(NPC, null);
            BaseDrawing.DrawTexture(Main.spriteBatch, bodyTex, 0, NPC, lightColor);
            BaseDrawing.DrawTexture(Main.spriteBatch, ModContent.Request<Texture2D>(Texture + "_Glow").Value, 0, NPC, Color.White, true);
            return false;
		}
	}
}