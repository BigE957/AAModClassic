using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.BossSummons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.Desert
{
    public class MiniDjinn : ModNPC
    {
        private bool Shooty = false;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Djinn");
            Main.npcFrameCount[NPC.type] = 16;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.defense = 20;
            NPC.damage = 20;
            NPC.width = 42;
            NPC.height = 66;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.MiniDjinnBanner>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneDesert || spawnInfo.Player.ZoneUndergroundDesert) &&
                NPC.downedBoss3 && !spawnInfo.Player.ZoneBeach 
                && Main.dayTime ? .1f : 0f;
        }

        public float[] shootAI = new float[4];

        public override void AI()
        {
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;

            }
            else
            {
                NPC.spriteDirection = 1;
            }
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            BaseAI.AIFloater(NPC, player, ref NPC.ai, true, 0.2f, 3, 1.5f, .05f, 1.3f, 4);
            NPC.ai[3]++;

            if (NPC.ai[3] >= 120)
            {
                FireMagic(NPC, NPC.velocity);
                NPC.ai[3] = 0;
            }
            
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 66;
                if (Shooty == true)
                {
                    if (NPC.frame.Y < 66 * 8)
                    {
                        NPC.frame.Y = 66 * 8;
                    }
                    if (NPC.frame.Y > (66 * 15) )
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                        Shooty = false;
                    }
                }
                else
                {
                    if (NPC.frame.Y > (66 * 7))
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            Shooty = true;
            int Shoot = Main.rand.Next(2);
            switch (Shoot)
            {
                case 0:
                    Shoot = ModContent.ProjectileType<DjinnMagic1>();
                    break;
                default:
                    Shoot = ModContent.ProjectileType<DjinnMagic2>();
                    break;
            }

            BaseAI.FireProjectile(player.Center, npc, Shoot, (int)(npc.damage * 0.25f), 0f, 2f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.position.X + NPC.width / 2;
                NPC.position.Y = NPC.position.Y + NPC.height / 2;
                NPC.width = 42;
                NPC.height = 66;
                NPC.position.X = NPC.position.X - NPC.width / 2;
                NPC.position.Y = NPC.position.Y - NPC.height / 2;
                int dust1 = ModContent.DustType<Dusts.SandDust>();
                int dust2 = ModContent.DustType<Dusts.SandDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity.X *= 0f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity.X *= 0f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].noGravity = false;
            }
        }

        public override void OnKill()
        {
            if (Main.rand.NextBool(4))
            {
                NPC.DropLoot(ModContent.ItemType<DjinnLamp>());
            }
        }
    }
}
