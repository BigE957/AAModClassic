using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Armor
{
    public class Atlantean_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Atlantean Power");
            // Description.SetDefault("Your magic abilities are increased");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.manaCost -= 0.15f;
			player.statDefense += 10;
        }
    }
}