using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Champion
{
    public class RageCool_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rajah's Rage");
            // Description.SetDefault("A champion of Terraria never backs down");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.HasBuff(ModContent.BuffType<Rage_Buff>()))
            {
                player.statDefense -= 15;
                player.moveSpeed *= .2f;
            }
        }
    }
}