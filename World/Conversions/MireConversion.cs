using AAModClassic.Tiles;
using AAModClassic.Walls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.World.Convertions
{
    public class MireConversion : ModBiomeConversion
    {
        public override void Load()
        {
            if (ModLoader.TryGetMod("SpiritReforged", out var spirit))
            {
                Func<int[]> tileType = () => [ModContent.TileType<MireGrass>()];

                (bool success, int type) = ((bool success, int type))spirit.Call("AddSavannaTree", "AAModClassic/Tiles/Trees/", "BogwoodAcaciaTree", tileType, Mod);
                if (success)
                {
                    spirit.Call("RegisterConversionSet", "AcaciaTree", ModContent.TileType<MireGrass>(), type);
                    ModContent.GetModTile(type).RegisterItemDrop(ModContent.ItemType<AAModClassic.Items.Blocks.Bogwood>());
                }
            }
        }

        public override void PostSetupContent()
        {
            TileLoader.RegisterConversion(TileID.Grass, Type, ModContent.TileType<MireGrass>());
            TileLoader.RegisterConversion(TileID.JungleGrass, Type, ModContent.TileType<MireGrass>());
            WallLoader.RegisterConversion(WallID.Grass, Type, ModContent.WallType<MireJungleWall>());
            TileLoader.RegisterConversion(TileID.Dirt, Type, TileID.Mud); //Unofficial: This just makes sense to me

            WallLoader.RegisterConversion(WallID.GrassUnsafe, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe1, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe2, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe3, Type, ModContent.WallType<MireJungleWall>());
            WallLoader.RegisterConversion(WallID.JungleUnsafe4, Type, ModContent.WallType<MireJungleWall>());
            
            TileLoader.RegisterConversion(TileID.Stone, Type, ModContent.TileType<Depthstone>());
            WallLoader.RegisterConversion(WallID.Stone, Type, ModContent.WallType<DepthstoneWall>());
            
            TileLoader.RegisterConversion(TileID.Sand, Type, ModContent.TileType<Depthsand>());
            TileLoader.RegisterConversion(TileID.HardenedSand, Type, ModContent.TileType<DepthsandHardened>());
            WallLoader.RegisterConversion(WallID.HardenedSand, Type, ModContent.WallType<DepthsandHardenedWall>());
            TileLoader.RegisterConversion(TileID.Sandstone, Type, ModContent.TileType<Depthsandstone>());
            WallLoader.RegisterConversion(WallID.Sandstone, Type, ModContent.WallType<DepthsandstoneWall>());
            
            TileLoader.RegisterConversion(TileID.IceBlock, Type, ModContent.TileType<IndigoIce>());
            
            TileLoader.RegisterConversion(TileID.LivingWood, Type, ModContent.TileType<LivingBogwood>());
            WallLoader.RegisterConversion(WallID.LivingWood, Type, ModContent.WallType<LivingBogwoodWall>());
            WallLoader.RegisterConversion(WallID.LivingWoodUnsafe, Type, ModContent.WallType<LivingBogwoodWall>());
            TileLoader.RegisterConversion(TileID.LeafBlock, Type, ModContent.TileType<LivingBogleaves>());
            WallLoader.RegisterConversion(WallID.LivingLeaf, Type, ModContent.WallType<LivingBogleafWall>());

            TileLoader.RegisterConversion(TileID.LivingMahogany, Type, ModContent.TileType<LivingBogwood>());
            TileLoader.RegisterConversion(TileID.LivingMahoganyLeaves, Type, ModContent.TileType<LivingBogleaves>());

            if(ModLoader.TryGetMod("SpiritReforged", out var spirit))
            {
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrass").Type, Type, ModContent.TileType<MireGrass>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassCorrupt").Type, Type, ModContent.TileType<MireGrass>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassCrimson").Type, Type, ModContent.TileType<MireGrass>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaGrassHallow").Type, Type, ModContent.TileType<MireGrass>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("SavannaDirt").Type, Type, TileID.Mud);

                WallLoader.RegisterConversion(spirit.Find<ModWall>("SavannaDirtWall").Type, Type, WallID.Dirt);
                WallLoader.RegisterConversion(spirit.Find<ModWall>("SavannaDirtWallUnsafe").Type, Type, WallID.MudUnsafe);

                TileLoader.RegisterConversion(spirit.Find<ModTile>("LivingBaobab").Type, Type, ModContent.TileType<LivingBogwood>());
                TileLoader.RegisterConversion(spirit.Find<ModTile>("LivingBaobabLeaf").Type, Type, ModContent.TileType<LivingBogleaves>());
                WallLoader.RegisterConversion(spirit.Find<ModWall>("LivingBaobabWall").Type, Type, ModContent.WallType<LivingBogwoodWall>());
                WallLoader.RegisterConversion(spirit.Find<ModWall>("LivingBaobabLeafWall").Type, Type, ModContent.WallType<LivingBogleafWall>());
            }
        }
    }
}
