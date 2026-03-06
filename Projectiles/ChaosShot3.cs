namespace AAModClassic.Projectiles
{
    public class ChaosShot3 : ChaosShot1
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Shot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            proType = 2;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            offsetLeft = true;
        }
    }
}