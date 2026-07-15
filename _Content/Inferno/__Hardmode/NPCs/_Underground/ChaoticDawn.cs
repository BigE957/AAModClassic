using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.UI.World;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground
{
    public class ChaoticDawn : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaotic Dawn");
            //Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 68;
            NPC.damage = 60;
			NPC.defense = 25;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 24000f;
            NPC.knockBackResist = .30f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<ChaoticDawnBanner>();
            SpawnModBiomes = [ModContent.GetInstance<UndergroundInfernoBiome>().Type];
        }

        public override void AI()
        {
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);

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

        /*public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 3)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 6)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 9)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 12)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }*/

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.Player.GetModPlayer<ZAAPlayer>().ZoneInferno && spawnInfo.SpawnTileY > Main.worldSurface && Main.hardMode ? .1f : 0f;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{
            int dust1 = ModContent.DustType<Dusts.BroodmotherDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ItemID.Nazar, 100));

            npcLoot.Add(unofficialRule);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlazingDawn>(), 10));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Main.rand.NextBool(3))
                target.AddBuff(BuffID.Cursed, 240, false);
        }
    }
}