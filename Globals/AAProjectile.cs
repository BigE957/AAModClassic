using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Base.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;

using Terraria.Audio;
using Terraria.GameContent;

namespace AAModClassic.Globals
{
    public abstract class AAProjectile : ParentProjectile
    {
        protected override bool CloneNewInstances => true;

        public string name
        {
            get
            {
                return Projectile.Name;
            }
            set
            {
                Projectile.Name = value;
            }
        }

        public int frameWidth = 0;
        public int frameHeight = 0;
        public int nextFrameCounter = 0;
        public int frameCount = 0;
        public bool invertFrames = false;
        public Color? lightColor = null, drawColor = null;
        public int drawColorType = -1;
        public float lightIntensity = 1f;
        public override Vector4 GetFrameV4() { return new Vector4(0, 0, frameWidth, frameHeight + 2); }

        public bool drawCentered = false, drawCenteredX = false, hurtsTiles = true, firstTick = false;

        public SoundStyle? spawnSound = null;
        public short immunityID = -1; //allows for projectiles to _not_ override player attacks

        public virtual void SetMaster(params object[] args) { }
        public virtual void OnSpawnEffects() { }

        public override bool? CanCutTiles()
        {
            return !hurtsTiles ? false : (bool?)null;
        }

        

        public override bool PreAI()
        {
            if (!firstTick)
            {
                OnSpawnEffects();
                if (spawnSound != null)
                {
                    SoundEngine.PlaySound(spawnSound, Projectile.Center);
                }
                firstTick = true;
            }
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (lightColor != null) BaseDrawing.AddLight(Projectile.Center, (Color)lightColor, lightIntensity);
            if (drawCentered || drawCenteredX)
            {
                Vector2 oldPos = Projectile.position;
                if (drawCenteredX)
                {
                    Projectile.position.X += Projectile.Center.X - Projectile.position.X;
                }
                else
                {
                    Projectile.position += Projectile.Center - Projectile.position;
                }
                BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, GetAlpha(lightColor));
                Projectile.position = oldPos;
                return false;
            }
            return true;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            if (drawColor != null && drawColorType != -1)
            {
                if (drawColorType == 1)
                {
                    Color drawColor2 = (Color)drawColor;
                    if (drawColor.R > drawColor2.R) { drawColor2.R = drawColor.R; }
                    if (drawColor.G > drawColor2.G) { drawColor2.G = drawColor.G; }
                    if (drawColor.B > drawColor2.B) { drawColor2.B = drawColor.B; }
                    //drawColor2.A = (Color)drawColor.A;
                    return drawColor2;
                }
                return (Color)drawColor;
            }
            return base.GetAlpha(drawColor);
        }
    }
}