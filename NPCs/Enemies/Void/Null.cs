using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using AAModClassic.Items.Boss.Zero;

namespace AAModClassic.NPCs.Enemies.Void
{
    public class Null : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Null");
            Main.npcFrameCount[NPC.type] = 4;
        }
		
		public override void SetDefaults()
		{
            NPC.CloneDefaults(NPCID.Poltergeist);
            NPC.noGravity = true;
            NPC.noTileCollide = true;
			NPC.aiStyle = -1;
            NPC.width = 24;
            NPC.height = 40;
            NPC.damage = 50;
            NPC.defense = 9999999;
            NPC.lifeMax = 100;
            NPC.HitSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Glitch");
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.alpha = 70;
            NPC.value = 7000f;
            NPC.knockBackResist = 0.7f;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.NullBanner>();
        }

		public int frameCount = 0;
		public int frameCounter = 0;
		public override void PostAI()
		{
			
			NPC.frame = new Rectangle(0, frameCount * 40, 36, 38);
			NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
			NPC.rotation = NPC.velocity.X * 0.25f;
		}

        public override void AI()
        {
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            for (int m = 0; m < 2; m++)
            {
                BaseAI.AIEye(NPC, ref NPC.ai, false, true, 0.13f, 0.08f, 2f, 1.1f, 1.2f, 1.2f);
                BaseAI.Look(NPC, 1);
            }
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<UnstableSingularity>(), 1);

            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Ono>(), 1);
            }
        }

        
    }
}