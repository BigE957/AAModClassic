using AAModClassic._Unreleased.Content.Void.Buffs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    public class InfinityZeroSpawn1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Death");
            Main.npcFrameCount[NPC.type] = 2;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.width = 342;
            NPC.height = 420; 
            NPC.npcSlots = 100;
            NPC.dontCountMe = true;
            NPC.noTileCollide = true;
            NPC.boss = false;
            NPC.noGravity = true;
            //npc.behindTiles = true;
            NPC.aiStyle = -1;
            NPC.scale *= 1.4f;
            NPC.behindTiles = true;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["InfinityZero_Intro"];
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.alpha = 255;
        }

        private int Frame = 0;
        private int FrameCounter = 0;
        private int HoldTimer = 90;
		public int spawnState = 0;
        public int StartTimer = 200;

        public override void AI()
        {
            if (NPC.ai[0] == 1f)
            {
                HoldTimer = 60;
                StartTimer = 100;
                NPC.ai[0] = 2f;
            }
            StartTimer--;
            if (StartTimer <= 0)
            {
                NPC.alpha = 0;
                int endFrame = spawnState == 0 ? 7 : spawnState == 1 ? 4 : spawnState == 2 ? 4 : spawnState == 3 ? 4 : spawnState == 4 ? 3 : 6;
                if (Frame >= endFrame)
                {
                    Frame = endFrame;
                    HoldTimer--;
                    if (HoldTimer == 0)
                    {
                        Frame = 0;
                        FrameCounter = 0;
                        HoldTimer = NPC.ai[0] == 2f ? spawnState >= 3 ? 30 : 40 : spawnState >= 3 ? 50 : 60;
                        spawnState++;
                        if (spawnState >= 5 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            SummonInfinity();
                            NPC.active = false;
                        }
                    }
                }
                else
                {
                    FrameCounter++;
                    if (FrameCounter > 10)
                    {
                        Frame++;
                        FrameCounter = 0;
                    }
                }
            }
			
        }

		public void SummonInfinity()
		{
			//roar is now handled when infinity spawns so his mouth opens
             if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				int npcID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<InfinityZero>());
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player2 = Main.player[i];
                    if (player2 != null && player2.active && !player2.dead && player2.HasBuff(ModContent.BuffType<LockedOn_Buff>()))
                    {
                        Main.npc[npcID].life = player2.GetModPlayer<AAPlayer>().GetIZHealth;
                    }
                }
                Main.npc[npcID].Center = NPC.Center;
				Main.npc[npcID].netUpdate = true;
			}
		}

        public override void DrawBehind(int index)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                NPC.hide = true;
                Main.instance.DrawCacheNPCsMoonMoon.Add(index);
            }
            else
                NPC.hide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D SFrame1 = TextureAssets.Npc[NPC.type].Value;
            Texture2D SFrame2 = ModContent.Request<Texture2D>(Texture.Replace('1', '2')).Value;
            Texture2D SFrame3 = ModContent.Request<Texture2D>(Texture.Replace('1', '3')).Value;
            Texture2D SFrame4 = ModContent.Request<Texture2D>(Texture.Replace('1', '4')).Value;
            Texture2D SFrame5 = ModContent.Request<Texture2D>(Texture.Replace('1', '5')).Value;
            Texture2D SFrame6 = ModContent.Request<Texture2D>(Texture.Replace('1', '6')).Value;
            

            NPC.frame = BaseDrawing.GetFrame(Frame, 171, 210, 0, 0);
			Rectangle darkFrame = BaseDrawing.GetFrame(0, 171, 210, 0, 0);
			Texture2D drawTexture = spawnState == 0 ? SFrame1 : spawnState == 1 ? SFrame2 : spawnState == 2 ? SFrame3 : spawnState == 3 ? SFrame4 : spawnState == 4 ? SFrame5 : SFrame6;
			Texture2D infinityTex = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Void/_PostMoonLord/NPCs/InfinityZero/IInfinityZeroSpawn1_Shadow").Value;

            int offset = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? 144 : 72;
            
            NPC.position.Y += offset;
            if (StartTimer <= 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, infinityTex, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, 3f, NPC.rotation, NPC.spriteDirection, 7, darkFrame, Color.Black);
                BaseDrawing.DrawTexture(spriteBatch, drawTexture, 0, NPC.position + new Vector2(0f, NPC.gfxOffY), NPC.width, NPC.height, 3f, NPC.rotation, NPC.spriteDirection, 7, NPC.frame, InfinityZero.GetGlowAlpha(true));
            }
            NPC.position.Y -= offset;
			return false;
        }
    }
}
