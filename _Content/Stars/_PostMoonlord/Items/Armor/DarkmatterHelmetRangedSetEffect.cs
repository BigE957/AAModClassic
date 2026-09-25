using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class DarkmatterHelmetRangedSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StarHelmetRangedPlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetRangedPlayer>().sunPortal = false;
        }
    }
}