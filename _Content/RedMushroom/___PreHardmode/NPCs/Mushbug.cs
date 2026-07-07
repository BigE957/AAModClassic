using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs
{
    public class Mushbug : ModNPC, IBannerNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushbug");
            Main.npcFrameCount[NPC.type] = 6;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = -2
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.MushiLadybug);
            NPC.width = 30;
            NPC.height = 24;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.damage = 10;
            NPC.defense = 9;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit45;
            NPC.DeathSound = SoundID.NPCDeath47;
            NPC.knockBackResist = 0.3f;
            AnimationType = NPCID.MushiLadybug;
            NPC.value = 1000f;
            NPC.buffImmune[31] = false;
            NPC.npcSlots = 0.3f;
            Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.MushbugBanner>();
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.Player.GetModPlayer<ZAAPlayer>().ZoneMush ? 1f : 0f;
        }

		public override void HitEffect(NPC.HitInfo hit)
		{

            int dust1 = ModContent.DustType<Dusts.MushDust>();
            if (NPC.life <= 0)
			{
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, dust1, 0f, 0f, 0);
            }
		}

		public override void OnKill()
		{
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Mushroom);
        }
	}
}