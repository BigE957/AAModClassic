using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ReaperImmune : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaper Scythe immunity");
			// Description.SetDefault("You are immune to damage and deal 10x damage");
			Main.debuff[Type] = false;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			if (player.HeldItem.type != Mod.Find<ModItem>("GrimReaperScythe").Type)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			player.immune = true;
			player.GetDamage(DamageClass.Melee) += 10f;
		}
	}
}
