using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ChaosBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaotic Fury");
            // Description.SetDefault("Your magic abilities are increased substantially");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 0.20f;
			player.GetCritChance(DamageClass.Magic) += 15;
			player.manaCost -= 0.20f;
			player.statDefense += 12;
        }
    }
}