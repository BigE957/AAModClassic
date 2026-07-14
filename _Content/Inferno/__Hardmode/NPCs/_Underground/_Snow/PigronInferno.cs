using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Globals;
using AAModClassic.UI.Core;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground._Snow
{
    public class PigronInferno : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pigron");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[170];
		}

		public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 36;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 12;
            NPC.lifeMax = 210;
            NPC.HitSound = SoundID.NPCHit27;
            NPC.DeathSound = SoundID.NPCDeath30;
            NPC.knockBackResist = 0.5f;
            NPC.value = 2000f;
            AnimationType = NPCID.PigronCorruption;
            NPC.buffImmune[31] = false;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            Banner = Item.NPCtoBanner(NPCID.PigronCorruption);
			BannerItem = ItemID.PigronBanner;
            SpawnModBiomes = [ModContent.GetInstance<UndergroundInfernoBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(new FlavorTextBestiaryInfoElement("CommonBestiaryFlavor.Pigron"));
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            if (Main.rand.NextBool(1000))
            {
                SoundEngine.PlaySound(SoundID.Zombie9, NPC.position);
            }
            NPC.noGravity = true;
            if (!NPC.noTileCollide)
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                    {
                        NPC.velocity.X = 2f;
                    }
                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                    {
                        NPC.velocity.X = -2f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                    {
                        NPC.velocity.Y = 1f;
                    }
                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                    {
                        NPC.velocity.Y = -1f;
                    }
                }
            }
            NPC.TargetClosest(true);
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                if (NPC.ai[1] > 0f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[1] = 0f;
                    NPC.ai[0] = 0f;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[1] == 0f)
            {
                NPC.ai[0] += 1f;
            }
            if (NPC.ai[0] >= 300f)
            {
                NPC.ai[1] = 1f;
                NPC.ai[0] = 0f;
                NPC.netUpdate = true;
            }
            if (NPC.ai[1] == 0f)
            {
                NPC.alpha = 0;
                NPC.noTileCollide = false;
            }
            else
            {
                NPC.wet = false;
                NPC.alpha = 200;
                NPC.noTileCollide = true;
            }
            NPC.rotation = NPC.velocity.Y * 0.1f * NPC.direction;
            NPC.TargetClosest(true);
            if (NPC.direction == -1 && NPC.velocity.X > -4f && NPC.position.X > Main.player[NPC.target].position.X + Main.player[NPC.target].width)
            {
                NPC.velocity.X = NPC.velocity.X - 0.08f;
                if (NPC.velocity.X > 4f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.04f;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - 0.2f;
                }
                if (NPC.velocity.X < -4f)
                {
                    NPC.velocity.X = -4f;
                }
            }
            else if (NPC.direction == 1 && NPC.velocity.X < 4f && NPC.position.X + NPC.width < Main.player[NPC.target].position.X)
            {
                NPC.velocity.X = NPC.velocity.X + 0.08f;
                if (NPC.velocity.X < -4f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.04f;
                }
                else if (NPC.velocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f;
                }
                if (NPC.velocity.X > 4f)
                {
                    NPC.velocity.X = 4f;
                }
            }
            if (NPC.directionY == -1 && NPC.velocity.Y > -2.5 && NPC.position.Y > Main.player[NPC.target].position.Y + Main.player[NPC.target].height)
            {
                NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                if (NPC.velocity.Y > 2.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.05f;
                }
                else if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.15f;
                }
                if (NPC.velocity.Y < -2.5)
                {
                    NPC.velocity.Y = -2.5f;
                }
            }
            else if (NPC.directionY == 1 && NPC.velocity.Y < 2.5 && NPC.position.Y + NPC.height < Main.player[NPC.target].position.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                if (NPC.velocity.Y < -2.5)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.05f;
                }
                else if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                }
                if (NPC.velocity.Y > 2.5)
                {
                    NPC.velocity.Y = 2.5f;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
            if (NPC.life > 0)
            {
                int num589 = 0;
                while (num589 < hit.Damage / NPC.lifeMax * 50.0)
                {
                    int num590 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.BroodmotherDust>(), 0f, 0f, 0, default, 1.5f);
                    Main.dust[num590].velocity *= 1.5f;
                    Main.dust[num590].noGravity = true;
                    num589++;
                }
                return;
            }
            for (int num591 = 0; num591 < 10; num591++)
            {
                int num592 = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.BroodmotherDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num592].velocity *= 2f;
                Main.dust[num592].noGravity = true;
            }
            for (int num593 = 0; num593 < 4; num593++)
            {
                int num594 = Gore.NewGore(NPC.GetSource_OnHurt(null), new Vector2(NPC.position.X, NPC.position.Y + NPC.height / 2 - 10f), new Vector2(hit.HitDirection, 0f), 99, NPC.scale);
                Main.gore[num594].velocity *= 0.3f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notUnofficialRule = new(new NotUnofficial());

            notUnofficialRule.OnSuccess(ItemDropRule.Common(ItemID.Bacon, 3));

            npcLoot.Add(notUnofficialRule);

            LeadingConditionRule unofficialRule = new(new Unofficial());

            //TODO: for 1.4.5 uncomment this
            //CloneDropsWithoutInput(NPCID.PigronHallow, [ItemID.Crystallize], unofficialRule, ref npcLoot);
        }

        /// <summary>
        /// Clones the given NPC's loot except anything input and adds it to the given loot pool.
        /// </summary>
        /// <param name="npcToClone">The ID of the npc whose loot is to be cloned.</param>
        /// <param name="itemIdsToExclude">The items present in the former NPC's lootpool you do not wish to clone.</param>
        /// <param name="leadingCondition">The loading condition rule to apply to all cloned loot.</param>
        /// <param name="loot">The loot pool you wish to add the loot to.</param>
        public static void CloneDropsWithoutInput(int npcToClone, int[] itemIdsToExclude, LeadingConditionRule leadingCondition, ref NPCLoot loot)
        {
            List<IItemDropRule> clonedDropRules = Main.ItemDropsDB.GetRulesForNPCID(npcToClone, false);

            foreach (IItemDropRule rule in clonedDropRules)
            {
                int itemID = 0;

                if (rule is ItemDropWithConditionRule conditionDrop)
                {
                    itemID = conditionDrop.itemId;
                }
                else if (rule is CommonDrop commonDrop)
                {
                    itemID = commonDrop.itemId;
                }

                if (itemIdsToExclude.Contains(itemID))
                {
                    continue;
                }

                leadingCondition.OnSuccess(rule);
            }

            loot.Add(leadingCondition);
        }
    }
}