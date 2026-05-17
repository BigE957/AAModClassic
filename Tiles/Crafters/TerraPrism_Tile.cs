using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Tiles.Crafters
{
    public class TerraPrism_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            DustType = DustID.Terra;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Infinity Core");
            AddMapEntry(new Color(40, 100, 40), name);
            AdjTiles = new int[]
            {
                TileID.WorkBenches,
                TileID.Anvils,
                TileID.Furnaces,
                TileID.Hellforge,
                TileID.Bookcases,
                TileID.Sinks,
                TileID.Solidifier,
                TileID.Blendomatic,
                TileID.MeatGrinder,
                TileID.Loom,
                TileID.LivingLoom,
                TileID.FleshCloningVat,
                TileID.GlassKiln,
                TileID.BoneWelder,
                TileID.SteampunkBoiler,
                TileID.Bottles,
                TileID.LihzahrdFurnace,
                TileID.ImbuingStation,
                TileID.DyeVat,
                TileID.Kegs,
                TileID.HeavyWorkBench,
                TileID.Tables,
                TileID.Chairs,
                TileID.CookingPots,
                TileID.DemonAltar,
                TileID.Sawmill,
                TileID.CrystalBall,
                TileID.AdamantiteForge,
                TileID.MythrilAnvil,
                TileID.TinkerersWorkbench,
                TileID.Autohammer,
                TileID.IceMachine,
                TileID.SkyMill,
                TileID.HoneyDispenser,
                TileID.AlchemyTable,
                TileID.LunarCraftingStation,
                ModContent.TileType<HellstoneAnvil_Tile>(),
                ModContent.TileType<HallowedAnvil_Tile>(),
                ModContent.TileType<HallowedForge_Tile>(),
                ModContent.TileType<QuantumFusionAccelerator_Tile>(),
                ModContent.TileType<ACS_Tile>(),
            };
            TileID.Sets.DisableSmartCursor[Type] = true;
            AnimationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 10)
            {
                frameCounter = 0;
                if (++frame >= 10) frame = 0;
            }
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.TileFrameX, tile.TileFrameY + (Main.tileFrame[Type] * 54), false, false, false, null, AAGlobalTile.GetRainbowColorBright);
        }
    }
}