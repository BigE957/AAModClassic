using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetSummonerPlayer : EquipmentEffectPlayer
    {
        public int CrystalMode = 0;

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (effect)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    CrystalMode++;
                    if (CrystalMode > 2)
                    {
                        CrystalMode = 0;
                    }
                }
                if (CrystalMode == 2)
                {
                    Player.lifeRegen += 12;
                    Player.statDefense.FinalMultiplier *= 1.2f;
                    Player.GetDamage(DamageClass.Generic) /= 2;
                }
            }
        }
    }
}