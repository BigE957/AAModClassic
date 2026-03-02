
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedSpawn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spark of Desire");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
			NPC.TargetClosest();			
            Player player = Main.player[NPC.target];
			
			if(Main.netMode != NetmodeID.Server)
			{
                if (NPC.ai[0] > 175)
				{
					NPC.alpha -= 3;
					if (NPC.alpha < 0)
					{
						NPC.alpha = 0;
					}
				}

                if (NPC.ai[0] >= 570)
                {
                    Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Greed");
                }

            }
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0]++;

				if (NPC.ai[0] == 175)    
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed1"), Color.Goldenrod);
					NPC.netUpdate = true;
				}else
				if (NPC.ai[0] == 350)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed2"), Color.Goldenrod);
				}else
				if (NPC.ai[0] == 500)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed3"), Color.Goldenrod);
                    NPC.netUpdate = true;
				}else
				if (NPC.ai[0] == 610)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed4"), Color.Goldenrod);
				}else
				if (NPC.ai[0] >= 755 && !NPC.AnyNPCs(Mod.Find<ModNPC>("Greed").Type))
				{
					AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("Greed").Type, true, NPC.Center, Lang.BossChat("GreedName"), false);
					if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Greed5"), Color.Goldenrod);

                    NPC.netUpdate = true;
					NPC.active = false;				
				}
			}
        }

        public override bool CheckActive()
        {
            if (!NPC.AnyNPCs(Mod.Find<ModNPC>("Greed").Type))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (++NPC.frameCounter >= 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Rectangle SunFrame = new Rectangle(0, 0, 70, 70);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("NPCs/Bosses/Greed/GreedSpawn"), 0, NPC.position + new Vector2(0, NPC.gfxOffY), NPC.width, NPC.height, NPC.scale, 0, NPC.spriteDirection, 4, SunFrame, NPC.GetAlpha(AAColor.COLOR_WHITEFADE1), true);
            return false;
        }
    }
}