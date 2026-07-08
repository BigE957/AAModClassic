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
    public class AmethystGreatsword_AmethystBolt : DiamondGreatsword_DiamondBolt
    {
        public override (float, float, float) LightColor => ((255 - Projectile.alpha) * .5f / 255f, (255 - Projectile.alpha) * 0f / 255f, (255 - Projectile.alpha) * .9f / 255f);
        public override Color DustColor => Color.Purple;
        public override Color DrawColor => Color.White;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amethyst Bolt");
        }
    }
}