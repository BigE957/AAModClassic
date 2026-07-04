using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic._Removed.Content._Tinker.__Hardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.Accessories
{
    public class StormClawEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<StormClawPlayer>().effect = true;
        }
    }

    public class StormClawPlayer : EquipmentEffectPlayer
    {
        //TODO: what was he trying to do here
        public virtual float UseTimeMultiplier(Item item, Player player)
        {
            float multiplier = 1f;

            int useTime = item.useTime;

            int useAnimate = item.useAnimation;
            if (effect)
            {
                if (item.autoReuse == false)
                {
                    multiplier *= 2f;
                }
            }
            
            while (useTime / multiplier < 1)
            {
                multiplier -= .1f;
            }

            while (useAnimate / multiplier < 2)
            {
                multiplier -= .1f;
            }

            return multiplier;
        }
    }
}