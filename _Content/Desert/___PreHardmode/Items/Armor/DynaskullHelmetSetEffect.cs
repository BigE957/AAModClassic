using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria.ID;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Armor
{
    public class DynaskullHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<ChampionHelmetRangedSetPlayer>().effect = true;
        }
    }

    public class DynaskullHelmetSetPlayer : EquipmentEffectPlayer
    {
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect && Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Confused, 180);
            }
        }
    }
}