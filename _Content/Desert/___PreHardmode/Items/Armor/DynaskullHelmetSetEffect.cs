using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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