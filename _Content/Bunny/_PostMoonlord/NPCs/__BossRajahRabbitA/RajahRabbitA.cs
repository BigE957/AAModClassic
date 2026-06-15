using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Unofficial.Content.Bunny._PostMoonlord.Items._RajahA.BossStandard;
using AAModClassic.Music;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA
{
    [AutoloadBossHead]
    public class RajahRabbitA : RajahRabbit
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit; Champion of the Innocent");
            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 108),
                PortraitPositionYOverride = 56,
                Scale = 0.75f,
                PortraitScale = 0.6f
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 150;
            NPC.defense = 0;
            NPC.lifeMax = 1200000;
            NPC.life = 1200000;
            Music = MusicManagementSystem.MusicSlots["Rajah_Awakened"];
            isSupreme = true;
            NPC.value = Item.buyPrice(3, 0, 0, 0);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.ChampionRajahRabbit")
            ]);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RajahRabbitATreasureBag>()));

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitARelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitTrophy>(), 10));

            LeadingConditionRule unofficialRule = new(new AAConditions.UnofficialNotExpert());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitAMask>(), 7));

            npcLoot.Add(unofficialRule);

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChampionPlate>(), 1, 15, 31));

            int[] lootTable = { ModContent.ItemType<Excalihare>(), ModContent.ItemType<FluffyFury>(), ModContent.ItemType<RabbitsWrath>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            npcLoot.Add(notExpertRule);
        }
    }

}
