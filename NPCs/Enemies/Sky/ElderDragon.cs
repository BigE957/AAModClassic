using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Armor.Chaos;
using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AAModClassic.NPCs.Enemies.Sky
{
    public class ElderDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elder Dragon");
            Main.npcFrameCount[NPC.type] = 5;
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
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.ElderDragonBanner>();
        }

        public override void AI()
        {
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
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 96;
                if (NPC.frame.Y > (96 * 3))
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
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Materials.DragonSpirit>());
        }
    }
}