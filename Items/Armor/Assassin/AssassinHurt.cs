using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
    public class AssassinHurt : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("AssassinHirt");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = false;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<AAModGlobalNPC>().AssassinHurt = true;
		}
	}
}
