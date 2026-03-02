using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ManaOverload : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mana Overload");
            // Description.SetDefault("Double magic attack speed");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
        }
        
    }
    public class ManaOverloadEffect : ModPlayer
    {
        public override void PostItemCheck()
        {
            if (Player.HasBuff(Mod.Find<ModBuff>("ManaOverload").Type) && Player.HeldItem.CountsAsClass(DamageClass.Magic))
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
