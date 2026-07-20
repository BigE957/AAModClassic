using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class ShadowBandUnofficialEffect_ShadowStealth : ModBuff
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
            player.moveSpeed += 0.40f;
        }
    }
}
