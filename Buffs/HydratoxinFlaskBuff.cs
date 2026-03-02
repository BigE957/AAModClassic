using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class HydratoxinFlaskBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Weapon Imbue: Hydratoxin");
			// Description.SetDefault("Melee attacks inflict hydratoxin");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = true;
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
