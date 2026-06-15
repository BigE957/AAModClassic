using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class ScytheOfTheGrimReaper_ReaperScytheImmunity : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe Immunity");
			// Description.SetDefault("You are immune to damage and deal greatly increased melee damage");
			Main.debuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != ModContent.ItemType<ScytheOfTheGrimReaper>() && player.HeldItem.type != ModContent.ItemType<SoulShredder>())
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;

			if (player.HeldItem.type == ModContent.ItemType<SoulShredder>())
				player.GetDamage(DamageClass.Melee) += 15f;
			else if (player.HeldItem.type == ModContent.ItemType<ScytheOfTheGrimReaper>())
                player.GetDamage(DamageClass.Melee) += 10f;
        }
	}
}
