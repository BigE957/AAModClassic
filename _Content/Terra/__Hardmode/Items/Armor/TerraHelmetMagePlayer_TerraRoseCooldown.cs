using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetMagePlayer_TerraRoseCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Rose Cooldown");
            // Description.SetDefault("Cannot plant roses at this time...");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}