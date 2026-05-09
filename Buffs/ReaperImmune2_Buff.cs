using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ReaperImmune2_Buff : ModBuff
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
			if (player.HeldItem.type != ModContent.ItemType<SoulShredder>())
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.GetDamage(DamageClass.Melee) += 15f;
		}
	}
}
