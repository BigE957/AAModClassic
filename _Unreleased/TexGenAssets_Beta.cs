using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles;
using AAModClassic.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

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
            ParthenanTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/World/ParthenanGen", AssetRequestMode.ImmediateLoad).Value);
            ParthenanWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/World/ParthenanGen_Walls", AssetRequestMode.ImmediateLoad).Value);

            ShipTileData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/World/ShipGen", AssetRequestMode.ImmediateLoad).Value);
            ShipWallData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/World/ShipGen_Walls", AssetRequestMode.ImmediateLoad).Value);
            ShipLiquidData = TexGenData.FromTexture2D(ModContent.Request<Texture2D>("AAModClassic/_Unreleased/World/ShipGen_Liquid", AssetRequestMode.ImmediateLoad).Value);
        }
    }
}
