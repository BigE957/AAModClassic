using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.WorldGen;
using AAModClassic._Unreleased.Content.Desert.__Hardmode.NPCs.__BossAnubis;
using AAModClassic._Content.Desert.__Hardmode._BossAnubis;
using AAModClassic._Content.Desert._PostMoonlord._BossAnubisA;

namespace AAModClassic._Content.Desert.__Hardmode.Items._BossAnubis
{
    public class RasScepter : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ra's Scepter");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Summons Anubis
Can only be used in the desert on the surface
'I uh...borrowed this from a bird friend of mine.'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = 0;
            Item.rare = ItemRarityID.LightPurple;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (!player.ZoneDesert && !player.ZoneUndergroundDesert)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ScepterBossFalse1"), Color.Gold, false);
                return true;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<Anubis>()))
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.ScepterBossFalse2"), Color.Gold, false);
                return true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<FATransition>()) || NPC.AnyNPCs(ModContent.NPCType<FATransition2>()) || NPC.AnyNPCs(ModContent.NPCType<ForsakenAnubis>()))
            {
                return true;
            }

            int anubis = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unreleased) ? ModContent.NPCType<AnubisUnreleased>() : ModContent.NPCType<Anubis>();
            int a = NPC.NewNPC(NPC.GetBossSpawnSource(player.whoAmI), (int)player.position.X + Main.rand.Next(-300, 300), (int)player.position.Y - 400, anubis);
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            NPC npc = Main.npc[a];

            Vector2 position = npc.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Granite, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = npc.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 50, default, 3.7f);
                Main.dust[num88].position = npc.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 25, default, 1.5f);
                Main.dust[num88].position = npc.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = npc.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += npc.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = npc.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(npc.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += npc.DirectionTo(Main.dust[num92].position) * 3f;
            }
            return true;
        }
    }
}