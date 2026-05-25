using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossShenDoragon.BossStandard;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.FuryAshe;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.SistersOfAnarchy.WrathHaruka;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.UI.Titles;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
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
