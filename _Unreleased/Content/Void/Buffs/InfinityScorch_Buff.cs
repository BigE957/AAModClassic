using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void.Buffs
{
    public class InfinityScorch_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Scorch");
			// Description.SetDefault("Your health is burning away");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ZAAPlayer>().InfinityScorch = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<AAModGlobalNPC>().InfinityScorch = true;
        }
    }
}
