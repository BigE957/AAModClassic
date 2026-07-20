using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic._Unofficial.Content.Void._PostMoonlord.Items._BossZero.BossStandard;
using AAModClassic.UI.World;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static AAModClassic._Content.Inferno.___PreHardmode.NPCs.DragonClaw_NPC;
using static AAModClassic.Utilities.ItemDropRuleConditionUtils;

namespace AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground._Desert
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class InfernalGhoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infernal Ghoul");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = -2
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.DesertGhoul);
            AnimationType = NPCID.DesertGhoul;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
			Banner = Item.NPCtoBanner(NPCID.DesertGhoul);
			BannerItem = ItemID.DesertGhoulBanner;
            SpawnModBiomes = [ModContent.GetInstance<UndergroundInfernoBiome>().Type];
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IncineriteDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ItemID.AncientCloth, 10));

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DragonFire>(), 3));

            npcLoot.Add(unofficialRule);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 420);
        }
    }
}
