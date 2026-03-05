using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.Hallow
{
    public class FatPixie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fat Pixie");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 500;
            NPC.damage = 30;
            NPC.defense = 15;
            NPC.knockBackResist = 0f;
            NPC.value = Item.sellPrice(0, 0, 75, 45);
            NPC.aiStyle = -1;
            NPC.width = 60;
            NPC.height = 36;
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("FatPixieBanner").Type;

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneHallow && Main.hardMode ? .05f : 0f;
        }

		int frameCounter = 0;
        public override void AI()
        {
			NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (NPC.velocity.Y == 0 || NPC.velocity.Y < 0)
            {
				frameCounter--;
				if(frameCounter <= 0)
				{
					frameCounter = NPC.velocity.Y < 0 ? 3 : 10;
					NPC.frame.Y = NPC.frame.Y == 0 ? NPC.frame.Height : 0;
				}
            }else
            {
                if (NPC.velocity.Y > 0)
                {
                    NPC.frame.Y = NPC.frame.Height * 2;
                }
            }
			if(NPC.velocity.X != 0)
			{
				if(NPC.collideX)
					NPC.velocity.X *= -2f;
				if (NPC.velocity.X > 0)
				{
					NPC.spriteDirection = 1;
				}else
				{
					NPC.spriteDirection = -1;
				}
			}
			float jumpWidth = 3f;
			float jumpHeight = -1f;
			if(NPC.whoAmI % 30 == 0) //THE LEGENDARY SUPER FAT PIXIE
			{
				jumpWidth = 8f;
				jumpHeight = -25f;
                if (NPC.ai[0] >= 0)
                {
                    CombatText.NewText(NPC.Hitbox, Color.LightGoldenrodYellow, Lang.BossChat("FatPixie"));
                }
			}
            BaseAI.AISlime(NPC, ref NPC.ai, false, 150, 4f, 2f, jumpWidth, jumpHeight);
			BaseDrawing.AddLight(NPC.Center, new Color(212, 208, 107), 2f);
        }

        public override void OnKill()
        {
			if(Main.netMode != NetmodeID.MultiplayerClient)
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.PixieDust, Main.rand.Next(5, 7));
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.whoAmI % 30 == 0)
            {
                if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
                else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
                BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, auraPercent, 1f, 0f, 0f, Color.Gold);
            }
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, Color.White);
			return false;
		}
    }
}


