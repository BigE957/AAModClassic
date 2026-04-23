using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Banners;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs
{

    public class Skulker : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skulker");

            Main.npcFrameCount[NPC.type] = 11;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 140;
            NPC.damage = 8;
            NPC.defense = 14;
            NPC.value = Item.sellPrice(0, 0, 6, 45);
            NPC.aiStyle = -1;
            NPC.width = 56;
            NPC.height = 28;
            NPC.npcSlots = 1f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.knockBackResist = .2f;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<MireSkulkerBanner>();
        }

        private bool Shell = false;
        private int ShellTimer = 0;

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {

            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (!Shell)
            {
                if (NPC.frameCounter++ > 9)
                {
                    NPC.frame.Y += 40;
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y > 200)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (ShellTimer < 180)
                {
                    if (NPC.frame.Y < 40 * 6)
                    {
                        NPC.frame.Y = 40 * 6;
                    }

                    if (NPC.frameCounter++ > 9)
                    {
                        NPC.frame.Y += 40;
                        NPC.frameCounter = 0;
                        if (NPC.frame.Y > 320)
                        {
                            NPC.frame.Y = 320;
                        }
                    }
                }
                else
                {
                    if (NPC.frameCounter++ > 9)
                    {
                        NPC.frame.Y += 40;
                        NPC.frameCounter = 0;
                    }
                }
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.defense = 14;
            NPC.reflectsProjectiles = false;

            if (NPC.velocity.X > 0) // so it faces the player
            {
                NPC.spriteDirection = -1;
            }
            else
            {
                NPC.spriteDirection = 1;
            }

            if (!Shell)
            {
                ShellTimer++;
                if (ShellTimer > 500)
                {
                    NPC.frame.Y = 40 * 6;
                    ShellTimer = 0;
                    Shell = true;
                    NPC.netUpdate = true;
                }
                
                BaseAI.AIZombie(NPC, ref NPC.ai, true, true, -1, 0.08f, 1f, 2, 3, 120);
            }
            else
            {
                NPC.defense = 999;
                NPC.knockBackResist = 0;
                NPC.reflectsProjectiles = true;
                NPC.velocity *= 0;
                ShellTimer++;
                if (ShellTimer >= 180)
                {
                    NPC.defense = 14;
                    NPC.reflectsProjectiles = false;

                    if (NPC.frameCounter++ > 9)
                    {
                        if (NPC.frame.Y > 400)
                        {
                            Shell = false;
                            NPC.netUpdate = true;
                            ShellTimer = 0;
                        }
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MirePod>(), 1, 5, 15));
        }
    }
}


