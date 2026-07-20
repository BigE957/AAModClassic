using AAModClassic._Content.Sky.__Hardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static AAModClassic.Utilities.ItemDropRuleConditionUtils;

namespace AAModClassic._Content.Sky.__Hardmode.NPCs
{
    public class ElderDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elder Dragon");
            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Position = new(0, 18)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 38;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = 30;
            NPC.defense = 30;
            NPC.lifeMax = 800;
            NPC.HitSound = SoundID.DD2_WyvernHurt;
            NPC.DeathSound = SoundID.DD2_WyvernDeath;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.05f;
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.ElderDragonBanner>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
            ]);
        }

        public override void AI()
        {
            if(WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.3f, 0.08f, 7f, 6f, false, 300);
            else
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.8f, 0.04f, 8f, 7f, false, 300);
            Player player = Main.player[NPC.target];
            if (player.Center.X > NPC.Center.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !Main.hardMode)
            {
                return 0f;
            }
            return SpawnCondition.Sky.Chance * 0.10f;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DragonSpirit>());
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule notUnofficialRule = new(new NotUnofficial());

            notUnofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DragonSpirit>()));

            npcLoot.Add(notUnofficialRule);

            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DragonSpirit>(), 1, 5, 9));

            npcLoot.Add(unofficialRule);
        }
    }
}