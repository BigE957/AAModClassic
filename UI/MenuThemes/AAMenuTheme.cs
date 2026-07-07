using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Assets;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.UI.MenuThemes
{
    public class AAMenuTheme : ModMenu
    {
        private static int Phase => Main.moonPhase % 3;
        public override string DisplayName => "Ancients Awakened: Classic Style";

        private static Asset<Texture2D> MireLogo;
        private static Asset<Texture2D> InfernoLogo;
        private static Asset<Texture2D> VoidLogo;
        private static Asset<Texture2D> BlankTex;

        public override void SetStaticDefaults()
        {
            MireLogo = ModContent.Request<Texture2D>("AAModClassic/UI/MenuThemes/LogoMire");
            InfernoLogo = ModContent.Request<Texture2D>("AAModClassic/UI/MenuThemes/LogoInferno");
            VoidLogo = ModContent.Request<Texture2D>("AAModClassic/UI/MenuThemes/LogoVoid");
            BlankTex = ModContent.Request<Texture2D>(AssetDirectory.General.Nothing);
        }

        public override Asset<Texture2D> Logo 
        { 
            get 
            {
                return Phase switch
                {
                    2 => VoidLogo,
                    1 => Main.dayTime ? InfernoLogo : MireLogo,
                    _ => Main.dayTime ? TextureAssets.Logo3 : TextureAssets.Logo4,
                };
            }
        }
        public override ModSurfaceBackgroundStyle MenuBackgroundStyle
        {
            get
            {
                return Phase switch
                {
                    2 => ModContent.GetInstance<VoidSurfaceBgStyle>(),
                    1 => Main.dayTime ? ModContent.GetInstance<InfernoSurfaceBgStyle>() : ModContent.GetInstance<MireSurfaceBgStyle>(),
                    _ => base.MenuBackgroundStyle,
                };
            }
        }
        public override Asset<Texture2D> SunTexture
        {
            get
            {
                return Phase switch
                {
                    2 => BlankTex,
                    //1 => Sun,
                    _ => base.SunTexture
                };
            }
        }
        public override Asset<Texture2D> MoonTexture
        {
            get
            {
                return Phase switch
                {
                    2 => BlankTex,
                    //1 => Moon,
                    _ => base.MoonTexture,
                };
            }
        }
        public override int Music => MusicID.Title;
        public override void Update(bool isOnTitleScreen)
        {
            if (Phase == 1 || Phase == 2)
            {
                Main.numClouds = 0;
                if (Phase == 2)
                {
                    if (!SkyManager.Instance["AAModClassic:VoidSky"].IsActive())
                        SkyManager.Instance.Activate("AAModClassic:VoidSky");
                    SkyManager.Instance["AAModClassic:VoidSky"].Update(null);

                    if (SkyManager.Instance["AAModClassic:InfernoSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:InfernoSky", [true]);
                    if (SkyManager.Instance["AAModClassic:MireSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:MireSky", [true]);
                }
                else if (Main.dayTime)
                {
                    if (!SkyManager.Instance["AAModClassic:InfernoSky"].IsActive())
                        SkyManager.Instance.Activate("AAModClassic:InfernoSky");
                    SkyManager.Instance["AAModClassic:InfernoSky"].Update(null);

                    if (SkyManager.Instance["AAModClassic:VoidSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:VoidSky", [true]);
                    if (SkyManager.Instance["AAModClassic:MireSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:MireSky", [true]);
                }
                else
                {
                    if (!SkyManager.Instance["AAModClassic:MireSky"].IsActive())
                        SkyManager.Instance.Activate("AAModClassic:MireSky");
                    SkyManager.Instance["AAModClassic:MireSky"].Update(null);

                    if (SkyManager.Instance["AAModClassic:InfernoSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:InfernoSky", [true]);
                    if (SkyManager.Instance["AAModClassic:VoidSky"].IsActive())
                        SkyManager.Instance.Deactivate("AAModClassic:VoidSky", [true]);
                }
            }
            else
            {
                Main.numClouds = 10;
                if (SkyManager.Instance["AAModClassic:MireSky"].IsActive())
                    SkyManager.Instance.Deactivate("AAModClassic:MireSky", [true]);
                if (SkyManager.Instance["AAModClassic:InfernoSky"].IsActive())
                    SkyManager.Instance.Deactivate("AAModClassic:InfernoSky", [true]);
                if (SkyManager.Instance["AAModClassic:VoidSky"].IsActive())
                    SkyManager.Instance.Deactivate("AAModClassic:VoidSky", [true]);
            }
        }

        public override void OnDeselected()
        {
            if (SkyManager.Instance["AAModClassic:MireSky"].IsActive())
                SkyManager.Instance.Deactivate("AAModClassic:MireSky", [true]);
            if (SkyManager.Instance["AAModClassic:InfernoSky"].IsActive())
                SkyManager.Instance.Deactivate("AAModClassic:InfernoSky", [true]);
            if (SkyManager.Instance["AAModClassic:VoidSky"].IsActive())
                SkyManager.Instance.Deactivate("AAModClassic:VoidSky", [true]);
        }
    }
}
