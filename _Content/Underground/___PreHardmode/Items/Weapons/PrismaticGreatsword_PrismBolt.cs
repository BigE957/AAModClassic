using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Content.Underground.___PreHardmode.Items.Weapons
{
    public class PrismaticGreatsword_PrismBolt : DiamondGreatsword_DiamondBolt
    {
        public override Color LightColor => new Color((Main.DiscoR - Projectile.alpha) * 0.8f / 255f, (Main.DiscoG - Projectile.alpha) * 0.4f / 255f, (Main.DiscoB - Projectile.alpha) * 0f / 255f);
        public override Color DustColor => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        public override Color DrawColor => Main.DiscoColor;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Prism Bolt");
        }
    }
}