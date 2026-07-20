using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Tinker.___PreHardmode.Items.Accessories
{
    public class MendingBandEffect_MendingStealth : ModBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Glitched");
            // Description.SetDefault("Your head is like 10 feet in front of you");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int index)
        {
            base.Update(player, ref index);
            player.aggro -= 200;
            player.lifeRegen += 6;
        }
    }
}
