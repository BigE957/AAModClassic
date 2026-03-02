namespace AAMod.Projectiles
{
    public class ChaosShot2 : ChaosShot1
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Shot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            proType = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            offsetLeft = false;
        }
    }
}