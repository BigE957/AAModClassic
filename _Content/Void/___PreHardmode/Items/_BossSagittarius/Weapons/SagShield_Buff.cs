using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons
{
    public class SagShield_Buff : ModBuff
	{
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Shields Up");
            // Description.SetDefault("They can't get in, but your weapons can't get out.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().ShieldUp = true;
            player.lifeRegen += 14;
            player.noItems = true;
        }
    }
}