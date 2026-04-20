using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class MidnightAssassinHelmet_AssassinHurtBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("AssassinHirt");
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
