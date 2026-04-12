using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Boss.Grips;
using AAModClassic.Items.Vanity.Mask;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosRed : BaseGripOfChaos
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1600;
            NPC.damage = 32;
            NPC.defense = 15;	
            NPC.buffImmune[BuffID.OnFire] = true;			

			offsetBasePoint = new Vector2(-240f, 0f);			
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("InfernoGripGore4").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/GripOfChaosRed_Glow");
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, Color.White);
            return false;
        }

        public override void OnKill()
        {
            int blueGripExists = NPC.CountNPCS(ModContent.NPCType<GripOfChaosBlue>());
            if (blueGripExists == 0)
                AAWorld.downedGrips = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GripTrophyRed>(), 10));

            LeadingConditionRule notExpert = new(new Conditions.NotExpert());

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 30, 44));

            notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GripMaskRed>(), 7));

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
            if (Main.rand.NextBool(2) || (Main.expertMode && Main.rand.Next(0) == 0))
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(180, 250));
            }
        }
    }
}
