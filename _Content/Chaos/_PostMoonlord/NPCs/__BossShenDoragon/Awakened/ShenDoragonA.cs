using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened
{
    [AutoloadBossHead]
    public class ShenDoragonA : ShenDoragon
    {
        public override string Texture => FilePathUtils.TexturePath<ShenDoragon>();

        public override string BossHeadTexture => FilePathUtils.TexturePath<ShenDoragonA>() + "_Head_Boss";

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Shen Doragon Awakened");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 130;
            NPC.defense = 80;
            NPC.lifeMax = 1000000;
            NPC.value = Item.sellPrice(1, 0, 0, 0);
            Music = MusicManagementSystem.MusicSlots["Shen_Awakened"];
            SceneEffectPriority = (SceneEffectPriority)11;
            IsAwakened = true;
            NPC.alpha = 255;
            NPC.boss = true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule expert = new(new Conditions.IsExpert());

            expert.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<ShenDoragonTreasureBag>()));

            expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ShenDoragonATrophy>(), 10));

            expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>()));

            LeadingConditionRule firstKill = new(new FirstTimeKillingShenA());

            expert.OnSuccess(firstKill.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChaosRune>())));

            npcLoot.Add(expert);
        }

        public class FirstTimeKillingShenA : IItemDropRuleCondition, IProvideItemConditionDescription
        {
            public bool CanDrop(DropAttemptInfo info) => !NPCExtensions.BeenKilled<ShenDoragonA>(true);
            public bool CanShowItemDropInUI() => true;
            public string GetConditionDescription() => null;
        }
    }
}
