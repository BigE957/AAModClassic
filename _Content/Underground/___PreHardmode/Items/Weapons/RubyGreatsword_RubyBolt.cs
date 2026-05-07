using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Weapons
{
    public class RubyGreatsword_RubyBolt : DiamondGreatsword_DiamondBolt
    {
        public override Color LightColor => new Color((255 - Projectile.alpha) * 1f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * 0f / 255f);
        public override Color DustColor => Color.Red;
        public override Color DrawColor => Color.White;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruby Bolt");
        }
    }
}