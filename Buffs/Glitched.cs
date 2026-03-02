using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Glitched : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glitched");
			// Description.SetDefault("Your head is like 10 feet in front of you");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = false;

		}


        public override void Update(Player player, ref int index)
        {
            base.Update(player, ref index);
            player.manaCost *= 0;
            player.GetDamage(DamageClass.Magic) += .2f;
            player.GetDamage(DamageClass.Summon) += .2f;
        }
    }
}
