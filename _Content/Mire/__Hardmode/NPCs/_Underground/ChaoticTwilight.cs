using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.NPCs._Underground
{
    public class ChaoticTwilight : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaotic Twilight");
            Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
        {
            NPC.width = 74;
            NPC.height = 76;
            NPC.damage = 90;
			NPC.defense = 10;
			NPC.lifeMax = 200;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 24000f;
            NPC.knockBackResist = .30f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<ChaoticTwilightBanner>();
            SpawnModBiomes = [ModContent.GetInstance<UndergroundMireBiome>().Type];
        }

        public override void AI()
        {
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            Lighting.AddLight((int)((NPC.position.X + NPC.width / 2) / 16f), (int)((NPC.position.Y + NPC.height / 2) / 16f), 0f, 0f, 0.3f);

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.ai[0] == 0f)
            {
                float num312 = 9f;
                Vector2 vector32 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float num313 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vector32.X;
                float num314 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vector32.Y;
                float num315 = (float)Math.Sqrt(num313 * num313 + num314 * num314);
                num315 = num312 / num315;
                num313 *= num315;
                num314 *= num315;
                NPC.velocity.X = num313;
                NPC.velocity.Y = num314;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 0.785f;
                NPC.ai[0] = 1f;
                NPC.ai[1] = 0f;
                NPC.netUpdate = true;
                return;
            }
            if (NPC.ai[0] == 1f)
            {
                if (NPC.justHit)
                {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                }
                NPC.velocity *= 0.99f;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 100f)
                {
                    NPC.netUpdate = true;
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                    NPC.velocity.X = 0f;
                    NPC.velocity.Y = 0f;
                    return;
                }
            }
            else
            {
                if (NPC.justHit)
                {
                    NPC.ai[0] = 2f;
                    NPC.ai[1] = 0f;
                }
                NPC.velocity *= 0.96f;
                NPC.ai[1] += 1f;
                float num316 = NPC.ai[1] / 120f;
                num316 = 0.1f + num316 * 0.4f;
                NPC.rotation += num316 * NPC.direction;
                if (NPC.ai[1] >= 120f)
                {
                    NPC.netUpdate = true;
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter < 3)
            {
                NPC.frame.Y = 0 * frameHeight;
            }
            else if (NPC.frameCounter < 6)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 9)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.frameCounter < 12)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else
            {
                NPC.frameCounter = 0;
            }
        }
        

		public override void HitEffect(NPC.HitInfo hit)
		{

            int dust1 = ModContent.DustType<Dusts.MireBubbleDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AbyssalTwilight>(), 10));
        }
    }
}