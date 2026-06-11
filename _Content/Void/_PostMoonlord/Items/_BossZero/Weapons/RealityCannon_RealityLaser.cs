using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class RealityCannon_RealityLaser : UnstablePowerCell_Proj
    {
        public override string Texture => ModContent.GetInstance<UnstablePowerCell_Proj>().Texture;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Laser");
        }
    }
}
