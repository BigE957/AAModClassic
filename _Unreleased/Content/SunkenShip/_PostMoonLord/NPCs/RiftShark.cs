using AAModClassic._Content.Void._PostMoonlord.Items.Accessories.Vanity;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Unreleased.Content.SunkenShip.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.AAConditions;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs
{
    public class RiftShark : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Trench Shark");
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new Vector2(32, 0),
                Velocity = -2
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 56;
            NPC.aiStyle = -1;
            NPC.damage = 80;
            NPC.defense = 30;
            NPC.lifeMax = 700;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.knockBackResist = 0f;
            NPC.buffImmune[31] = false;
            NPC.noGravity = true;
            AnimationType = NPCID.Shark;
            SpawnModBiomes = [ModContent.GetInstance<SunkenShipBiome>().Type];
            //Banner = NPC.type;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.downedEquinox && spawnInfo.Water && spawnInfo.Player.AAPlayer().ZoneShip && !NPCUtils.AnyEvents(spawnInfo.Player))
                return 0.7f;

            return 0f;
        }

        public override void AI()
        {
            BaseAI.AIFish(NPC, ref NPC.ai, true, true, false, 5f, 5f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
                NPC.width = 44;
                NPC.height = 78;
                NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
                int dust1 = ModContent.DustType<CthulhuDust>();
                int dust2 = ModContent.DustType<CthulhuDust>();
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            List<IItemDropRule> sharkRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.Shark, false);

            LeadingConditionRule unofficialRule = new(new Unofficial());

            foreach (var v in sharkRules)
            {
                unofficialRule.OnSuccess(v);

                npcLoot.Add(unofficialRule);
            }
        }
    }

}
