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
    public class TopazGreatsword_TopazBolt : DiamondGreatsword_DiamondBolt
    {
        public override (float, float, float) LightColor => ((255 - Projectile.alpha) * 0.8f / 255f, (255 - Projectile.alpha) * 0.4f / 255f, (255 - Projectile.alpha) * 0f / 255f);
        public override Color DustColor => Color.Yellow;
        public override Color DrawColor => Color.White;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Topaz Bolt");
        }
    }
}