using AAModClassic.UI.Titles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    public class FATransition2 : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anubis Legendscribe");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 72;
            NPC.height = 100;
            NPC.npcSlots = 1000;
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.boss = true;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.damage = 0;
            NPC.value = 0;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        readonly int frameHeight = 100;

        public override void AI()
        {
            NPC.dontTakeDamage = true;

            NPC.ai[3] = 39;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
                if (NPC.velocity.Y == 0)
                {
                    for (int a = 0; a < 8; a++)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 200, default, 1.3f);
                    }
                    NPC.ai[1]++;
                    NPC.frameCounter++;
                    if (NPC.frameCounter > 6)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                    }
                    if (NPC.frame.Y > frameHeight * 3)
                    {
                        NPC.frame.Y = 0;
                    }
                    if (NPC.ai[1] >= 90)
                    {
                        int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ShockwaveBoom>(), 0, 0, Main.myPlayer, 0, 10);
                        Main.projectile[b].Center = NPC.Center;
                        NPC.GetGlobalNPC<TitleGlobalNPC>().ShowTitle = true;
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ForsakenAnubis>());
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }
        }
    }
}