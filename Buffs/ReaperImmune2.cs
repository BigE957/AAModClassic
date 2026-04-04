using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ReaperImmune2 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe immunity");
			// Description.SetDefault("You are immune to damage and deal 15x damage");
			Main.debuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != ModContent.ItemType<GrimReaperScytheEX>())
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.GetDamage(DamageClass.Melee) += 15f;
		}
	}
}
