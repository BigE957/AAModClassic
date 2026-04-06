using AAModClassic.Items.Dev;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ReaperImmune_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe immunity");
			// Description.SetDefault("You are immune to damage and deal 10x damage");
			Main.debuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != ModContent.ItemType<GrimReaperScythe>())
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.GetDamage(DamageClass.Melee) += 10f;
		}
	}
}
