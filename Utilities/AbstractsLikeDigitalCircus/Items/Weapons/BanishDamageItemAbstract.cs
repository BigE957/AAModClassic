using AAModClassic._Content._Dev.Invoker;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Void._PostMoonlord.NPCs._BossZero.Protocol;
using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Weapons
{
    /// <summary>
    /// when making a banish damage item make sure to use SafeSetDefaults idiot you moron you moron idiot
    /// </summary>
    public abstract class BanishDamageItemAbstract : BaseAAItem
    {
        public virtual void SafeSetDefaults()
        {
        }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            Item.DamageType = DamageClass.Generic;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.Scale(InvokerPlayer.ModPlayer(player).BanishDamageMult);
        }

        public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
        {
            knockback.Flat = 0;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit = 0;
        }

        //TODO: make all of this a damage class instead of wtf this is
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.Text = damageValue + " banish " + damageWord;
            }
        }
    }
}