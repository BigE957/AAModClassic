using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased
{
    public class TexGenAssets_Unreleased : ModSystem
    {
        public static TexGenData ParthenanTileData;
        public static TexGenData ParthenanWallData;

        public static TexGenData ShipTileData;
        public static TexGenData ShipWallData;
        public static TexGenData ShipLiquidData;

        public override void OnModLoad()
        {
            ParthenanTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/World/ParthenanGen", AssetRequestMode.ImmediateLoad).Value);
            ParthenanWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/Parthenan/World/ParthenanGen_Walls", AssetRequestMode.ImmediateLoad).Value);

            ShipTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/SunkenShipGen", AssetRequestMode.ImmediateLoad).Value);
            ShipWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/SunkenShipGen_Walls", AssetRequestMode.ImmediateLoad).Value);
            ShipLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Content/SunkenShip/World/SunkenShipGen_Liquid", AssetRequestMode.ImmediateLoad).Value);
        }
    }
}
