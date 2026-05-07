using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol
{
    public class NullZP : ModNPC
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Null");
            Main.npcFrameCount[NPC.type] = 4;
            this.HideFromBestiary();
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
            NPC.lifeMax = 10;
            NPC.HitSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Glitch");
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.alpha = 70;
            NPC.value = 7000f;
            NPC.knockBackResist = 0.1f;
            NPC.noGravity = true;
        }

		public int frameCount = 0;
		public int frameCounter = 0;
		public override void PostAI()
		{
            if (!NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()))
            {
                NPC.alpha++;

                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
            }
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

        
    }
}