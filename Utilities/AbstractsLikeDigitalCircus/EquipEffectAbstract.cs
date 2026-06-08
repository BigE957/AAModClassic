using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus
{
    public abstract class EquipEffectAbstract : ModPlayer
    {
        public bool effect = false;
        public override void ResetEffects()
        {
            effect = false;
        }

        /// <summary>
        /// procs whenever the player takes damage from npc or proj
        /// </summary>
        /// <param name="hurtInfo"></param>
        /// <param name="npc"></param>
        /// <param name="proj"></param>
        public virtual void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {

        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            base.OnHitByNPC(npc, hurtInfo);

            OnHitByAnything(hurtInfo, npc, null);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            base.OnHitByProjectile(proj, hurtInfo);

            OnHitByAnything(hurtInfo, null,  proj);
        }
    }
}
