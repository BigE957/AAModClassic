using AAModClassic._Content.RedMushroom.World.Biomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.NPCs
{
    public class MushroomJelly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Jelly");
            Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.FungoFish);
            AnimationType = NPCID.FungoFish;
            NPC.noGravity = true;
            NPC.width = 26;
            NPC.height = 26;
            NPC.aiStyle = NPCAIStyleID.Jellyfish;
            NPC.damage = 20;
            NPC.defense = 20;
            NPC.lifeMax = 70;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 1000f;
            NPC.alpha = 20;
            NPC.npcSlots = 0.3f;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.ShroomJellyBanner>();
            SpawnModBiomes = [ModContent.GetInstance<RedMushroomBiome>().Type];
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush && spawnInfo.Water ? .7f : 0f;
        }

        public override void OnKill()
		{
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Mushroom);
        }
	}
}