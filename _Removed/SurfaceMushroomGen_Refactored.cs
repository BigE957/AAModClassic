using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan;
using AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan.Ancient;
using AAModClassic._Unreleased.Tiles.Fulgurite.Parthenan.Ancient.Walls;
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

namespace AAModClassic._Unreleased.World
{
    // this had to be majorly rewritten to function at any level consistently
    // sorry accuracyheads but the artistic vision wins out
    public class SurfaceMushroomGen_Refactored : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            ushort tileGrass = (ushort)ModContent.TileType<Mycelium>();

            int worldSize = BaseWorldGen.GetWorldSize();
            int biomeWidth = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150, biomeWidthHalf = biomeWidth / 2;
            int biomeHeight = worldSize == 3 ? 200 : worldSize == 2 ? 180 : 150;

            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[]
            {
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Grass, TileID.CorruptGrass, TileID.CrimsonGrass }),
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10),
                new SetModTile(tileGrass, true, true)
            }));
            WorldUtils.Gen(origin, new Shapes.Rectangle(biomeWidth, biomeHeight), Actions.Chain(new GenAction[]
{
                new InWorld(),
                new Modifiers.OnlyTiles(new ushort[]{ TileID.Ebonstone, TileID.Crimstone }),
                new RadialDitherCenter(biomeWidth, biomeHeight, biomeWidthHalf - 10, biomeWidthHalf + 10),
                new SetModTile(TileID.Stone, true, true)
            }));

            return true;
        }
    }
}
