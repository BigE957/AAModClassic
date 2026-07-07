using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class MidnightAssassinHelmetSetEffect_AssassinHurt : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Assassin Hirt");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().AssassinHurt = true;
		}
	}
}
