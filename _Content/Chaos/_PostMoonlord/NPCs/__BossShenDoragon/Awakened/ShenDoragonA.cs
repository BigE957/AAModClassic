using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Music;
using AAModClassic.UI.Core.BestiaryBackgrounds;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.Bestiary;
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
            NPC.value = Item.buyPrice(1, 0, 0, 0);
            Music = MusicManagementSystem.MusicSlots["Shen_Awakened"];
            SceneEffectPriority = (SceneEffectPriority)11;
            //IsAwakened = true;
            if(!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.boss = true;
            SpawnModBiomes = [ModContent.GetInstance<InfernoBiome>().Type, ModContent.GetInstance<MireBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.AwakenedShenDoragon")]);

            bestiaryEntry.AddTags([new AwakenedShenDoragonBestiaryBackground()]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule expert = new(new Conditions.IsExpert());

            expert.OnSuccess(ItemDropRule.BossBag(ModContent.ItemType<ShenDoragonTreasureBag>()));

            expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ShenDoragonATrophy>(), 10));

            expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EXSoul>()));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ShenDoragonRelic>()));

            npcLoot.Add(masterMode);

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
