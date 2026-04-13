using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Items.Boss.Grips;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosBlue : BaseGripOfChaos
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1400;
            NPC.damage = 30;
            NPC.defense = 10;		
            NPC.buffImmune[BuffID.Poisoned] = true;	

			offsetBasePoint = new Vector2(240f, 0f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore4").Type, 1f);
            }
        }

        public override void OnKill()
        {
            int redGripExists = NPC.CountNPCS(ModContent.NPCType<GripOfChaosRed>());
            if (redGripExists == 0)
                AAWorld.downedGrips = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GripTrophyBlue>(), 10));

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AbyssiumOre>(), 1, 30, 44));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GripMaskBlue>(), 7));

            LeadingConditionRule lastStandingAlways = new(new MissingGripAlways());

            lastStandingAlways.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<GripBag>()));

            LeadingConditionRule lastStandingNormal = new(new MissingGripNormal());

            lastStandingNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ClawBaton>(), 4));

            npcLoot.Add(lastStandingAlways);
            npcLoot.Add(lastStandingNormal);
            npcLoot.Add(notExpert);
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.NextBool(2) || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }		
    }
}
