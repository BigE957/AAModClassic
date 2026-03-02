using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mushroom
{
    public class Mushbug : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushbug");
            Main.npcFrameCount[NPC.type] = 6;
		}

		public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.MushiLadybug);
            NPC.width = 30;
            NPC.height = 24;
            NPC.aiStyle = 3;
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
			BannerItem = Mod.Find<ModItem>("MushbugBanner").Type;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneMush ? 1f : 0f;
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
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Mushroom);
        }
	}
}