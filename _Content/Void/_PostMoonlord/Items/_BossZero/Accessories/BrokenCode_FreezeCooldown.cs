using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories
{
    public class BrokenCode_FreezeCooldown : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Freeze Cooldown");
			// Description.SetDefault("Atleast your head is still on your body!");
			Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }


        public override void Update(Player player, ref int index)
        {
            
        }
    }
}
