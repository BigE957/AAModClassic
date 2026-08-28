using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn;
using AAModClassic._CrossMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Desert.___PreHardmode.NPCs._Day
{
    public class DustDjinn : ModNPC, IBannerNPC
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
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.DustDjinn")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return 0f;

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
        }

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            Shooty = true;
            int Shoot = Main.rand.Next(2);
            switch (Shoot)
            {
                case 0:
                    Shoot = ModContent.ProjectileType<DustDjinn_MagicBlastBlue>();
                    break;
                default:
                    Shoot = ModContent.ProjectileType<DustDjinn_MagicBlastRed>();
                    break;
            }

            BaseAI.FireProjectile(player.Center, npc, Shoot, (int)(npc.damage * 0.25f), 0f, 2f);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (Shooty)
                {
                    if (NPC.frame.Y < frameHeight * 8)
                        NPC.frame.Y = frameHeight * 8;

                    if (NPC.frame.Y > frameHeight * 15)
                    {
                        NPC.frame.Y = 0;
                        Shooty = false;
                    }
                }
                else
                {
                    if (NPC.frame.Y > frameHeight * 7)
                        NPC.frame.Y = 0;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SandDust>());
                Main.dust[d].velocity.X *= 0f;
                Main.dust[d].scale *= 1.3f;
                Main.dust[d].noGravity = false;
                d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<SandDust>());
                Main.dust[d].velocity.X *= 0f;
                Main.dust[d].scale *= 1.3f;
                Main.dust[d].noGravity = false;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertLamp>(), 4));
        }
    }
}
