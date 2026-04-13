using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void.___PreHardmode.NPCs
{
    public class ShadowScout : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadow Scout");
            Main.npcFrameCount[NPC.type] = 12;
        }
		
		public override void SetDefaults()
		{
            NPC.noGravity = true;
            NPC.noTileCollide = true;
			NPC.aiStyle = -1;
            NPC.width = 24;
            NPC.height = 40;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.alpha = 70;
            NPC.value = 7000f;
            NPC.knockBackResist = 0.7f;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<ShadowScoutBanner>();
        }

		public int frameCount = 0;
		public int frameCounter = 0;
        public int IdleTimer = 0;

		public override void PostAI()
		{
			NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
		}

        public override void AI()
        {
            BaseAI.AIElemental(NPC, ref NPC.ai, ref IdleTimer, null, 1, false, true, 800f, 600f, 180, 2f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 7)
            {
                if (NPC.ai[0] == 2f)
                {
                    if (NPC.frame.Y < 44 * 3)
                    {
                        NPC.frame.Y = 44 * 3;
                    }
                    if (NPC.frame.Y > 44 * 8)
                    {
                        NPC.frame.Y = 44 * 6;
                    }
                }
                else
                {
                    if (NPC.frame.Y >= 44 * 6)
                    {
                        NPC.frame.Y = 44 * 9;
                    }
                    if (NPC.frame.Y > 44 * 11 || NPC.frame.Y == 44 * 3 )
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture("Glowmasks/SagittariusMini_Glow"), 0, NPC, AAColor.ZeroShield);
            return false;
        }

        public override void OnKill()
        {
            //Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DoomiteScrap>(), 1);
        }

        
    }
}