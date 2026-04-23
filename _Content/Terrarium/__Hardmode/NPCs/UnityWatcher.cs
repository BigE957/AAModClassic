using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs
{
    public class UnityWatcher : ModNPC
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unity Watcher");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 35;
            NPC.height = 35;
            NPC.value = Item.sellPrice(0, 0, 5, 50);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 300;
            NPC.defense = 30;
            NPC.damage = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.TerraWatcherBanner>();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
            }
        }

        public override void AI()
        {
            BaseAI.AISkull(NPC, ref NPC.ai, false, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[NPC.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt(playerActive ? player.Center : NPC.Center + NPC.velocity, NPC, 0);
            if (Main.netMode != NetmodeID.MultiplayerClient && playerActive)
            {
                NPC.ai[2]++;
                if (NPC.ai[2] >= 69)
                {
                    if (NPC.frameCounter++ > 7)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += 28;
                        if (NPC.frame.Y > 28 * 3)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
                if (NPC.ai[2] >= 90)
                {

                    NPC.ai[2] = 0;
                    int projType = ModContent.ProjectileType<UnityWatcher_Sphere>();
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, NPC, projType, (int)(NPC.damage * 0.25f), 0f, 2f);
                    NPC.frame.Y = 0;
                    NPC.frameCounter = 0;
                    NPC.netUpdate2 = true;
                }
            }
            
        }
    }
}