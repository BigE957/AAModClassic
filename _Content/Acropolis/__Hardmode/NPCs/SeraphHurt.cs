using AAModClassic._Content.Acropolis.__Hardmode.NPCs.__BossAthena;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.NPCs
{
    public class SeraphHurt : ModNPC, IBannerNPC
	{
        public int OverrideBannerNPCType => ModContent.NPCType<Seraph>();

        public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 5;
            this.HideFromBestiary();
        }			
		
        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 40;
            NPC.value = 0;
            NPC.npcSlots = 1;
			NPC.aiStyle = -1;
            NPC.lifeMax = 120;
            NPC.defense = 20;
            NPC.damage = 55;
            NPC.knockBackResist = 0.3f;
			NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = false;
            Banner = ModContent.NPCType<Seraph>();
            NPC.dontTakeDamage = true;
        }

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override void AI()
		{
            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            NPC.ai[0]++;

            if (NPC.ai[0] == 120 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 180)
            {
                CombatText.NewText(NPC.Hitbox, Color.CadetBlue, SeraphBitching(), true);
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] >= 240 && NPC.dontTakeDamage && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.dontTakeDamage = false;
                NPC.netUpdate = true;
            }

            if (NPC.ai[0] >= 120 && NPC.ai[0] < 240)
            {
                NPC.velocity *= .97f;
            }
            else if (NPC.ai[0] >= 240)
            {
                NPC.noTileCollide = true;
                NPC.noGravity = true;
                NPC.dontTakeDamage = false;
                NPC.velocity.Y -= 0.5f;
                if (NPC.velocity.Y < -8f) NPC.velocity.Y = -8f;

                if (player.Center.X > NPC.Center.X)
                {
                    NPC.velocity.X -= 0.2f;
                    if (NPC.velocity.X < -8f) NPC.velocity.Y = -8f;
                    NPC.spriteDirection = 1;
                }
                else
                {
                    NPC.velocity.X += 0.2f;
                    if (NPC.velocity.X > 8f) NPC.velocity.Y = 8f;
                    NPC.spriteDirection = -1;
                }

                Vector2 Acropolis = new Vector2(Origin.X + 80 * 16, Origin.Y + 79 * 16);

                if (Vector2.Distance(NPC.Center, Acropolis) > 90 * 16 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int a = 0; a < 8; a++)
                    {
                        Dust.NewDust(NPC.Center, 60, 40, ModContent.DustType<FeatherDust>(), Main.rand.Next(-1, 2), 1, 0);
                    }
                    if (player.GetModPlayer<AAPlayer>().ZoneAcropolis)
                    {
                        AcropolisAltar_Tile.SpawnBoss(player, ModContent.NPCType<Athena>(), player.Center, Language.GetTextValue("Mods.AAModClassic.Common.Athena"), false);
                    }
                    BaseAI.KillNPC(NPC); 
                    NPC.netUpdate = true; 
                }
            }
            
            if (NPC.ai[0] < 120 && NPC.collideY)
            {
                NPC.rotation += NPC.velocity.X * 0.05f;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;
                NPC.rotation = NPC.velocity.X * 0.05f;
            }
        }

        public static string SeraphBitching()
        {
            switch (Main.rand.Next(5))
            {
                case 0: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphHurtChat1");
                case 1: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphHurtChat2");
                case 2: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphHurtChat3");
                case 3: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphHurtChat4");
                default: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphHurtChat5");
            }
        }

		public override void FindFrame(int frameHeight)
		{
            if (NPC.ai[0] < 120)
            {
                NPC.frame.Y = 0;
            }
            else
            {
                if (NPC.velocity.X > 0f)
                {
                    NPC.spriteDirection = 1;
                }
                else
                {
                    NPC.spriteDirection = -1;
                }
                NPC.rotation = NPC.velocity.X * 0.1f;
                NPC.frameCounter++;
                if (NPC.frameCounter >= 6)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0;
                }
                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = frameHeight;
                }
            }
        }
    }
}