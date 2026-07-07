using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetRangedPlayer_SunSiphon_ManaOverload : ModBuff
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
            if (Player.HasBuff(ModContent.BuffType<StarHelmetRangedPlayer_SunSiphon_ManaOverload>()) && Player.HeldItem.CountsAsClass(DamageClass.Magic))
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
