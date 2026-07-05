using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Armor
{
    public class HoppingHoodlumHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                player.moveSpeed += .5f;
                player.GetDamage(DamageClass.Summon) += .5f;
                player.GetDamage(DamageClass.Melee) += .5f;
            }
        }
    }
}