using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire.__Hardmode.NPCs._Underground._Desert
{
    public class ShadowGhoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadow Ghoul");
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
            Banner = Item.NPCtoBanner(NPCID.DesertGhoul);
            BannerItem = ItemID.DesertGhoulBanner;
            SpawnModBiomes = [ModContent.GetInstance<UndergroundMireBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && !spawnInfo.Player.ZoneSurface() && spawnInfo.Player.ZoneDesert && spawnInfo.Player.ZoneAnyMire() && !NPCUtils.AnyEvents(spawnInfo.Player))
                return ContentReplacementSystem.NeedToReplaceContent ? 0.25f : .025f;

            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.AbyssiumDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ItemID.AncientCloth, 10));

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Bogtoxin>(), 3));

            npcLoot.Add(unofficialRule);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 420);
        }
    }
}
