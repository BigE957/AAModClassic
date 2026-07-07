using AAModClassic._Content.Underground.___PreHardmode.Items.Weapons;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Weapons
{
    public class AmberGreatsword_AmberBolt : DiamondGreatsword_DiamondBolt
    {
        public override Color LightColor => new Color((255 - Projectile.alpha) * 1f / 255f, (255 - Projectile.alpha) * 0.7f / 255f, (255 - Projectile.alpha) * 0f / 255f);
        public override Color DustColor => Color.Orange;
        public override Color DrawColor => Color.White;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amber Bolt");
        }
    }
}