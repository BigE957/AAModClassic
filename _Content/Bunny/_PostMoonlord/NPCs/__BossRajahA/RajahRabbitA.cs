using AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard;
using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.BossStandard;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahA
{
    [AutoloadBossHead]
    public class RajahRabbitA : RajahRabbit
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah Rabbit; Champion of the Innocent");
            Main.npcFrameCount[NPC.type] = 8;
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
            NPC.value = Item.sellPrice(3, 0, 0, 0);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RajahRabbitATreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RajahRabbitTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RajahRabbitMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ChampionPlate>(), 1, 15, 31));

            int[] lootTable = { ModContent.ItemType<Excalihare>(), ModContent.ItemType<FluffyFury>(), ModContent.ItemType<RabbitsWrath>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, lootTable));

            npcLoot.Add(notExpertRule);
        }
    }

}
