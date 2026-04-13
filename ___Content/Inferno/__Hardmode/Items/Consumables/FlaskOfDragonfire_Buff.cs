using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class FlaskOfDragonfire_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Weapon Imbue: Dragonfire");
			// Description.SetDefault("Melee attacks inflict Dragonfire");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.dead || !player.active)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
