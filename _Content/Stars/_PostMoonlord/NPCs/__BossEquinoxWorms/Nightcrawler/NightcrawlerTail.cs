using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAModClassic._Content.Stars._PostMoonlord.NPCs.__BossEquinoxWorms.Nightcrawler
{
    //[AutoloadBossHead]
    public class NightcrawlerTail : NightcrawlerHead
	{
        public override void SetStaticDefaults()
        {
            //base.SetStaticDefaults();
            this.HideFromBestiary();
        }
        public override void SetDefaults()
		{
            base.SetDefaults();
            NPC.dontCountMe = true;
			nightcrawler = true;
            NPC.npcSlots = 0;
        }

        public override bool PreKill()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<NightcrawlerHead>()))
            {
                return false;
            }
            return true;
        }
    }
}