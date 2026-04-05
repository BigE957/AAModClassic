using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ManaOverload_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Overload");
            // Description.SetDefault("Double magic attack speed");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
        
    }
    public class ManaOverloadEffect : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (Player.HasBuff(ModContent.BuffType<ManaOverload>()) && Player.HeldItem.CountsAsClass(DamageClass.Magic))
            {
                if (Player.itemAnimation > 0)
                {
                    Player.itemAnimation--;
                }
                else
                {
                    Player.itemAnimation = 0;
                }
                if (Player.itemTime > 0)
                {
                    Player.itemTime--;
                }
                else
                {
                    Player.itemTime = 0;
                }
            }
        }
    }
}
