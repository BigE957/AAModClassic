using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCode_Glitched : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glitched");
			// Description.SetDefault("Your head is like 10 feet in front of you");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;

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
