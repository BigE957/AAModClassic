using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic;
using Terraria.Localization;

namespace AAModClassic.NPCs.Enemies.Sky
{
	public class Seraph : ModNPC
	{
        public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 4;		
		}			
		
        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 40;
            NPC.value = BaseUtility.CalcValue(0, 0, 10, 0);
            NPC.npcSlots = 1;
			NPC.aiStyle = -1;
            NPC.lifeMax = 500;
            NPC.defense = 20;
            NPC.damage = 55;
            NPC.knockBackResist = 0.3f;
			NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = true;
            if (NPC.type == ModContent.NPCType<SeraphA>())
            {
                NPC.alpha = 255;
            }
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("SeraphBanner").Type;
        }

        public override bool PreAI()
        {
            if (NPC.type == ModContent.NPCType<SeraphA>() && !(NPC.AnyNPCs(ModContent.NPCType<Athena>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaA>())))
            {
                NPC.velocity.Y -= .2f;
                NPC.velocity.X *= .95f;
                if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
                return false;
            }
            return true;
        }

		public override void AI()
		{

            if (!NPC.HasPlayerTarget)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.15f, 0.08f, 8f, 7f, false, 300);

            if (NPC.alpha > 0)
            {
                NPC.alpha -= 4;
            }
            else
            {
                NPC.alpha = 0;
            }

            if (NPC.ai[3]++ > 30 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int projType = ModContent.ProjectileType<SeraphFeather>();
                float spread = 30f * 0.0174f;
                Vector2 dir = Vector2.Normalize(player.Center - NPC.Center);
                dir *= 14f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 3; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, NPC.damage / 4, 2, Main.myPlayer);
                }
                NPC.ai[3] = 0;
                NPC.netUpdate = true;
            }

            if (!player.GetModPlayer<AAPlayer>().ZoneAcropolis || player.dead)
            {
                NPC.TargetClosest();
                if (!player.GetModPlayer<AAPlayer>().ZoneAcropolis || player.dead)
                {
                    if (!player.GetModPlayer<AAPlayer>().ZoneAcropolis)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.CadetBlue, SeraphBitching(), true);
                    }
                    else if (player.dead)
                    {
                        CombatText.NewText(NPC.Hitbox, Color.CadetBlue, SeraphBitchingKill(), true);
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        Dust.NewDust(NPC.Center, 60, 40, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0);
                    }
                    BaseAI.KillNPC(NPC);
                }
            }

            NPC.spriteDirection = NPC.direction;
            NPC.rotation = NPC.velocity.X * 0.05f;
        }

		public override void FindFrame(int frameHeight)
		{
            if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = 1;
            }
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
            }
            NPC.rotation = NPC.velocity.X * 0.1f;
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter >= 6.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }

        public override void OnKill()
        {
            if (Main.rand.Next(30) <= SeraphChance.SeraphKills && !NPC.AnyNPCs(ModContent.NPCType<SeraphHurt>()))
            {
                SeraphChance.SeraphKills = 0;
                int a = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<SeraphHurt>());
                Main.npc[a].velocity = NPC.velocity;
            }
            SeraphChance.SeraphKills++;
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SeraphFeather").Type);
        }

        public static string SeraphBitching()
        {
            switch (Main.rand.Next(5))
            {
                case 0: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphChat1");
                case 1: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphChat2");
                case 2: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphChat3");
                case 3: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphChat4");
                default: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphChat5");
            }
        }
        public static string SeraphBitchingKill()
        {
            switch (Main.rand.Next(5))
            {
                case 0: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphKillChat1");
                case 1: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphKillChat2");
                case 2: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphKillChat3");
                case 3: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphKillChat4");
                default: return Language.GetTextValue("Mods.AAModClassic.NPCs.EnemyChat.SeraphKillChat5");
            }
        }
    }

    public class SeraphChance : ModSystem
    {
        public static int SeraphKills = 0;

        public override void OnWorldLoad()/* tModPorter Suggestion: Also override OnWorldUnload, and mirror your worldgen-sensitive data initialization in PreWorldGen */
        {
            SeraphKills = 0;
        }

        public override void PostUpdateWorld()
        {
            if (SeraphKills > 30)
            {
                SeraphKills = 30;
            }
        }
    }
}