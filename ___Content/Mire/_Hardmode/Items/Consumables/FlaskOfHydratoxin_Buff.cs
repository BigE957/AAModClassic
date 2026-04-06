using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Consumables
{
    public class HydratoxinFlask_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Weapon Imbue: Hydratoxin");
			// Description.SetDefault("Melee attacks inflict hydratoxin");
			Main.persistentBuff[Type] = true;
			Main.meleeBuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
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
