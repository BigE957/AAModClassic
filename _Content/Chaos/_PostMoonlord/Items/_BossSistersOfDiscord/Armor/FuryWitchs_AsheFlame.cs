using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Armor
{
    public class FuryWitchs_AsheFlame : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ashe Flame");
			// Description.SetDefault("You get the flame power of inferno");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.AsheFlame = true;
			player.GetDamage(DamageClass.Magic) += .15f;
			player.GetDamage(DamageClass.Summon) += .15f;
			player.statDefense += 10;
		}
	}
}