using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using AAModClassic.Tiles.Boss;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Tiles.Plants;
using AAModClassic.Items.Potions.LuckyPotions;
using AAModClassic.Tiles;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Tiles.Altar;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Tiles;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System.Linq;

namespace AAModClassic.Globals
{
    public class AAGlobalTile : GlobalTile
    {
        public static int glowTick = 0;
        public static int glowMax = 100;

        public override void AnimateTile()
        {
            glowTick++;
            if (glowTick >= glowMax)
            {
                glowTick = 0;
            }
        }

        #region Tile Colors

        public static Color GetIncineriteColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAPlayer.IncineriteColor, color, min, max, clamp);
        public static Color GetIncineriteColorDim(Color color) => GetIncineriteColor(color, 0.4f, 1f, false);
        public static Color GetIncineriteColorBright(Color color) => GetIncineriteColor(color, 0.6f, 1f, false);
        public static Color GetIncineriteColorBrightInvert(Color color) => GetIncineriteColor(color, 1f, 0.6f, true);

        public static Color GetZeroColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.ZeroShield, color, min, max, clamp);
        public static Color GetZeroColorDim(Color color) => GetZeroColor(color, 0.4f, .6f, false);
        public static Color GetZeroColorBright(Color color) => GetZeroColor(color, 0.6f, 1f, false);
        public static Color GetZeroColorBrightInvert(Color color) => GetZeroColor(color, 1f, 0.6f, true);

        public static Color GetTerraColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.LimeGreen, color, min, max, clamp);
        public static Color GetTerraColorDim(Color color) => GetTerraColor(color, 0.4f, 1f, false);
        public static Color GetTerraColorBright(Color color) => GetTerraColor(color, 0.6f, 1f, false);
        public static Color GetTerraColorBrightInvert(Color color) => GetTerraColor(color, 1f, 0.6f, true);

        public static Color GetTerra2Color(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.YellowGreen, color, min, max, clamp);
        public static Color GetTerra2ColorDim(Color color) => GetTerra2Color(color, 0.4f, 1f, false);
        public static Color GetTerra2ColorBright(Color color) => GetTerra2Color(color, 0.6f, 1f, false);
        public static Color GetTerra2ColorBrightInvert(Color color) => GetTerra2Color(color, 1f, 0.6f, true);

        public static Color GetUraniumColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DarkSeaGreen, color, min, max, clamp);
        public static Color GetUraniumColorDim(Color color) => GetUraniumColor(color, 0.4f, 1f, false);
        public static Color GetUraniumColorBright(Color color) => GetUraniumColor(color, 0.6f, 1f, false);
        public static Color GetUraniumColorBrightInvert(Color color) => GetUraniumColor(color, 1f, 0.6f, true);

        public static Color GetStormColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Violet, color, min, max, clamp);
        public static Color GetStormColorDim(Color color) => GetStormColor(color, 0.4f, 1f, false);
        public static Color GetStormColorBright(Color color) => GetStormColor(color, 0.6f, 1f, false);
        public static Color GetStormColorBrightInvert(Color color) => GetStormColor(color, 1f, 0.6f, true);

        public static Color GetAkumaColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DeepSkyBlue, color, min, max, clamp);
        public static Color GetAkumaColorDim(Color color) => GetAkumaColor(color, 0.4f, 1f, false);
        public static Color GetAkumaColorBright(Color color) => GetAkumaColor(color, 0.6f, 1f, false);
        public static Color GetAkumaColorBrightInvert(Color color) => GetAkumaColor(color, 1f, 0.6f, true);

        public static Color GetDarkmatterColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Nightcrawler, color, min, max, clamp);
        public static Color GetDarkmatterColorDim(Color color) => GetDarkmatterColor(color, 0.4f, 1f, false);
        public static Color GetDarkmatterColorBright(Color color) => GetDarkmatterColor(color, 0.6f, 1f, false);
        public static Color GetDarkmatterColorBrightInvert(Color color) => GetDarkmatterColor(color, 1f, 0.6f, true);

        public static Color GetRadiumColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Daybringer, color, min, max, clamp);
        public static Color GetRadiumColorDim(Color color) => GetRadiumColor(color, 0.4f, 1f, false);
        public static Color GetRadiumColorBright(Color color) => GetRadiumColor(color, 0.6f, 1f, false);
        public static Color GetRadiumColorBrightInvert(Color color) => GetRadiumColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Maroon, color, min, max, clamp);
        public static Color GetYamataColorDim(Color color) => GetYamataColor(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright(Color color) => GetYamataColor(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert(Color color) => GetYamataColor(color, 1f, 0.6f, true);

        public static Color GetYamataColor2(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.Violet, color, min, max, clamp);
        public static Color GetYamataColorDim2(Color color) => GetYamataColor2(color, 0.4f, 1f, false);
        public static Color GetYamataColorBright2(Color color) => GetYamataColor2(color, 0.6f, 1f, false);
        public static Color GetYamataColorBrightInvert2(Color color) => GetYamataColor2(color, 1f, 0.6f, true);

        public static Color GetCthulhuColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Color.DarkCyan, color, min, max, clamp);
        public static Color GetCthulhuColorDim(Color color) => GetCthulhuColor(color, 0.4f, 1f, false);
        public static Color GetCthulhuColorBright(Color color) => GetCthulhuColor(color, 0.6f, 1f, false);
        public static Color GetCthulhuColorBrightInvert(Color color) => GetCthulhuColor(color, 1f, 0.6f, true);

        public static Color GetShenColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Shen2, color, min, max, clamp);
        public static Color GetShenColorDim(Color color) => GetShenColor(color, 0.4f, 1f, false);
        public static Color GetShenColorBright(Color color) => GetShenColor(color, 0.6f, 1f, false);
        public static Color GetShenColorBrightInvert(Color color) => GetShenColor(color, 1f, 0.6f, true);

        public static Color GetSkyColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.Sky, color, min, max, clamp);
        public static Color GetSkyColorDim(Color color) => GetSkyColor(color, 0.4f, 1f, false);
        public static Color GetSkyColorBright(Color color) => GetSkyColor(color, 0.6f, 1f, false);
        public static Color GetSkyColorBrightInvert(Color color) => GetSkyColor(color, 1f, 0.6f, true);

        public static Color GetBlankColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(AAColor.COLOR_WHITEFADE1, color, min, max, clamp);
        public static Color GetBlankColorDim(Color color) => GetBlankColor(color, 0.4f, 1f, false);
        public static Color GetBlankColorBright(Color color) => GetBlankColor(color, 0.6f, 1f, false);
        public static Color GetBlankColorBrightInvert(Color color) => GetBlankColor(color, 1f, 0.6f, true);

        public static Color GetRainbowColor(Color color, float min, float max, bool clamp) => GetTimedrawColor(Main.DiscoColor, color, min, max, clamp);
        public static Color GetRainbowColorDim(Color color) => GetRainbowColor(color, 0.4f, 1f, false);
        public static Color GetRainbowColorBright(Color color) => GetRainbowColor(color, 0.6f, 1f, false);
        public static Color GetRainbowColorBrightInvert(Color color) => GetRainbowColor(color, 1f, 0.6f, true);

        #endregion

        public override void Drop(int i, int j, int type)/* tModPorter Suggestion: Use CanDrop to decide if items can drop, use this method to drop additional items. See documentation. */
        {
            if (type == TileID.Dirt && TileID.Sets.BreakableWhenPlacing[TileID.Dirt]) //placing grass
            {
                return;
            }

            if (type == TileID.Mud && TileID.Sets.BreakableWhenPlacing[TileID.Mud]) //placing grass
            {
                return;
            }

            if (type == 28)
            {
                if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManSpawn)
                {
                    PotsDropMethod(i, j);
                }
            }

            if (Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManOre)
            {
                if(TileID.Sets.Conversion.Stone[type])
                {
                    int k = DropOreMethod(i, j, type);
                    if(k != 0) Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, k, 1, false, 0, false, false);
                }
            }

            if (Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().AncientGoldBody)
            {
                if(TileID.Sets.Conversion.Stone[type] && Main.rand.NextBool(50))
                {
                    Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 32, 32, ItemID.GoldCoin, 1, false, 0, false, false);
                }
            }
        }

        public static Color GetTimedrawColor(Color tColor, Color color, float min, float max, bool clamp)
        {
            Color glowColor = BaseUtility.ColorMult(tColor, BaseUtility.MultiLerp(glowTick / (float)glowMax, min, max, min));

            if (clamp)
            {
                if (color.R > glowColor.R) { glowColor.R = color.R; }
                if (color.G > glowColor.G) { glowColor.G = color.G; }
                if (color.B > glowColor.B) { glowColor.B = color.B; }
            }

            return glowColor;
        }

        public static Color GetGradientColor(Color tColor1, Color tColor2, Color color, bool clamp)
        {
            Color glowColor = Color.Lerp(tColor1, tColor2, BaseUtility.MultiLerp(glowTick / (float)glowMax, 0f, 1f, 0f));

            if (clamp)
            {
                if (color.R > glowColor.R)
                {
                    glowColor.R = color.R;
                }

                if (color.G > glowColor.G)
                {
                    glowColor.G = color.G;
                }

                if (color.B > glowColor.B)
                {
                    glowColor.B = color.B;
                }
            }

            return glowColor;
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

            Tile t = Main.tile[i, j - 1];

            if(!t.HasTile)
                return base.CanKillTile(i, j, type, ref blockDamaged);

            if ((t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<GreedAltar_Tile>() || t.TileType == ModContent.TileType<AcropolisAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<AcropolisAltar_Tile>()))
                return false;

            if ((t.TileType == ModContent.TileType<StarAltar_Tile>() || t.TileType == ModContent.TileType<GravAltar_Tile>() || t.TileType == ModContent.TileType<WormAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<StarAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<GravAltar_Tile>() || Main.tile[i, j].TileType == ModContent.TileType<WormAltar_Tile>()))
                return false;

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanExplode(int i, int j, int type)
        {
            Tile t = Main.tile[i, j - 1];
            if (t.HasTile && (t.TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (t.TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || t.TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
                return false;

            if (TileProtectionSystem.UnbreakableTiles.Contains(new(i, j)))
                return false;

            return base.CanExplode(i, j, type);
        }

        public override bool Slope(int i, int j, int type)
        {
            if (Main.tile[i, j - 1].HasTile && (Main.tile[i, j - 1].TileType == ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j - 1].TileType == ModContent.TileType<DragonAltarUnsafe_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<AbyssAltarUnsafe_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<DragonAltarUnsafe_Tile>()))
            {
                return false;
            }

            if (Main.tile[i, j - 1].HasTile && (Main.tile[i, j - 1].TileType == ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j - 1].TileType == ModContent.TileType<AcropolisAltar_Tile>()) && (Main.tile[i, j].TileType != ModContent.TileType<GreedAltar_Tile>() || Main.tile[i, j].TileType != ModContent.TileType<AcropolisAltar_Tile>()))
            {
                return false;
            }

            return base.Slope(i, j, type);
        }

        public override void RandomUpdate(int i, int j, int type)
        {
            if (Main.tile[i, j].TileType == TileID.MushroomGrass)
            {
                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(1000))
                {
                    int style = Main.rand.Next(5);

                    if (PlaceObject(i, j - 1, ModContent.TileType<MadnessShroom_Tile>(), false, style))
                    {
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<MadnessShroom_Tile>(), style, 0, -1, -1);
                    }
                }
            }

            if (Main.tile[i, j].TileType == TileID.Grass && Main.hardMode)
            {
                if (!Framing.GetTileSafely(i, j - 1).HasTile && Main.rand.NextBool(800))
                {
                    if (PlaceObject(i, j - 1, ModContent.TileType<Carrot_Tile>(), false, 0))
                    {
                        NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<Carrot_Tile>(), 0, 0, -1, -1);
                    }
                }
            }

            if(Main.player[Main.myPlayer].GetModPlayer<AAPlayer>().StripeManSpawn)
            {
                if(Main.rand.NextBool(800) && j >= GenVars.worldSurfaceLow)
                {
                    if (Main.tile[i, j + 1].HasTile && Main.tileSolid[Main.tile[i, j].TileType] && Main.tile[i, j - 1].LiquidType != LiquidID.Lava)
                    {
                        int style = WorldGen.genRand.Next(0, 4);
                        int tiletype = 0;
                        if (j < Main.maxTilesY - 5)
                        {
                            tiletype = Main.tile[i, j + 1].TileType;
                        }
                        if (tiletype == 147 || tiletype == 161 || tiletype == 162)
                        {
                            style = WorldGen.genRand.Next(4, 7);
                        }
                        if (tiletype == 60)
                        {
                            style = WorldGen.genRand.Next(7, 10);
                        }
                        if (Main.wallDungeon[Main.tile[i, j].WallType])
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 41 || tiletype == 43 || tiletype == 44)
                        {
                            style = WorldGen.genRand.Next(10, 13);
                        }
                        if (tiletype == 22 || tiletype == 23 || tiletype == 25)
                        {
                            style = WorldGen.genRand.Next(16, 19);
                        }
                        if (tiletype == 199 || tiletype == 203 || tiletype == 204 || tiletype == 200)
                        {
                            style = WorldGen.genRand.Next(22, 25);
                        }
                        if (tiletype == 367)
                        {
                            style = WorldGen.genRand.Next(31, 34);
                        }
                        if (tiletype == 226)
                        {
                            style = WorldGen.genRand.Next(28, 31);
                        }
                        if (j > Main.maxTilesY - 200)
                        {
                            style = WorldGen.genRand.Next(13, 16);
                        }
                        if (WorldGen.PlacePot(i, j, 28, style))
                        {
                            NetMessage.SendObjectPlacement(-1, i, j, 28, 0, 0, -1, -1);
                        }
                    }
                }
                
            }
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int random = -1, int direction = -1)
        {
            if (!TileObject.CanPlace(x, y, type, style, direction, out TileObject toBePlaced, false))
            {
                return false;
            }

            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
            }

            return false;
        }

        public static void PotsDropMethod(int i, int j)
        {
            int itemcreat = 0;
            if (WorldGen.genRand.NextBool(30) || (Main.rand.NextBool(30) && Main.expertMode))
            {
                if (WorldGen.genRand.NextBool(20))
                {
                    int rand = WorldGen.genRand.Next(100);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 678, 1, false, 0, false, false);
                    }
                    else
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if(rand2 == 0)
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2352, 1, false, 0, false, false);
                        }
                        if(rand2 == 1)
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2353, 1, false, 0, false, false);
                        }
                        if(rand2 == 2)
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2756, 1, false, 0, false, false);
                        }
                    }
                }
                if(Main.rand.NextBool(200))
                {
                    int k = Config.LuckyPotion.Keys.Count;
                    foreach (int itempotion in Config.LuckyPotion.Keys)
			        {
                        if(Main.rand.Next(k) == 0)
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, itempotion, 1, false, 0, false, false);
                            break;
                        }
                        k -= 1;
                    }
                }
                else if (Main.tile[i, j].LiquidAmount > 0)
                {
                    int rand = WorldGen.genRand.Next(3);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2354, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2355, 1, false, 0, false, false);
                    }
                    if (rand >= 2)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2356, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.worldSurface)
                {
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyIronskinPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 292, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckySwiftnessPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyCalmingPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2324, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2325, 1, false, 0, false, false);
                    }
                    if (rand == 7 || rand == 8)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<LuckyWrathPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2349, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 9)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.rockLayer)
                {
                    if (Main.player[Main.myPlayer].ZoneJungle)
                    {
                        int rand2 = WorldGen.genRand.Next(3);
                        if (rand2 == 0)
                        {
                            if(Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckySummoningPotion>();
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2328, 1, false, 0, false, false);
                            }
                        }
                        else
                        {
                            if(Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyThornsPotion>();
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                            }
                        }
                    }
                    else if (Main.player[Main.myPlayer].ZoneSnow)
                    {
                        if (WorldGen.genRand.NextBool(2))
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2359, 1, false, 0, false, false);
                        }
                    }
                    int rand = WorldGen.genRand.Next(12);
                    if (rand == 0)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyRegenerationPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 289, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 298, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckySwiftnessPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 290, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 291, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyEndurancePotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2346, 1, false, 0, false, false);
                        }
                    }
                    if (rand >= 10)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else if (j < Main.maxTilesY - 200)
                {
                    int rand = WorldGen.genRand.Next(15);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 299, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 303, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyThornsPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2322, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2327, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2329, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2350, 1, false, 0, false, false);
                    }
                }
                else
                {
                    int rand = WorldGen.genRand.Next(16);
                    if (rand == 0)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 296, 1, false, 0, false, false);
                    }
                    if (rand == 1)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 295, 1, false, 0, false, false);
                    }
                    if (rand == 2)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 293, 1, false, 0, false, false);
                    }
                    if (rand == 3)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 4)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 294, 1, false, 0, false, false);
                    }
                    if (rand == 5)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 297, 1, false, 0, false, false);
                    }
                    if (rand == 6)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 304, 1, false, 0, false, false);
                    }
                    if (rand == 7)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 305, 1, false, 0, false, false);
                    }
                    if (rand == 8)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyThornsPotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 301, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 9)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 302, 1, false, 0, false, false);
                    }
                    if (rand == 10)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 288, 1, false, 0, false, false);
                    }
                    if (rand == 11)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 300, 1, false, 0, false, false);
                    }
                    if (rand == 12)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                    }
                    if (rand == 13)
                    {
                        itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                    }
                    if (rand == 14)
                    {
                        if(Main.rand.NextBool(100))
                        {
                            int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyRagePotion>();
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2347, 1, false, 0, false, false);
                        }
                    }
                    if (rand == 15)
                    {
                        if(Main.rand.NextBool(5))
                        {
                            if(Main.rand.NextBool(100))
                            {
                                int rarepotion = ModContent.ItemType<Items.Potions.LuckyPotions.LuckyLifeforcePotion>();
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, rarepotion, 1, false, 0, false, false);
                            }
                            else
                            {
                                itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2345, 1, false, 0, false, false);
                            }
                        }
                        else if (Main.rand.NextBool(2))
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2326, 1, false, 0, false, false);
                        }
                        else
                        {
                            itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), i * 16, j * 16, 16, 16, 2323, 1, false, 0, false, false);
                        }
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
            }
        }

        public static int DropOreMethod(int x, int y, int type)
        {
            float ChanceBalance = 1;
            int SecondDrop = Config.LuckyOre.Keys.Count;
            foreach (int itemtype in Config.LuckyOre.Keys)
			{
				float chance = Config.LuckyOre[itemtype];
                chance -= Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].pick;
                chance = chance/ChanceBalance * 100f;
                if(chance < 100 && !(itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre || itemtype == ModContent.ItemType<AbyssiumOre>() || itemtype == ModContent.ItemType<IncineriteOre>() || itemtype == ModContent.ItemType<ApocalyptiteOre>()))
                {
                    if(Utils.NextFloat(Main.rand, SecondDrop) < 1)
                    {
                        int itemcreat = Item.NewItem(Item.GetSource_NaturalSpawn(), x * 16, y * 16, 32, 32, itemtype, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    SecondDrop -= 1;
                    continue;
                }
                else if(itemtype == ItemID.DemoniteOre || itemtype == ItemID.CrimtaneOre)
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && (type == TileID.Crimstone || type == TileID.Ebonstone))
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<AbyssiumOre>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Depthstone_Tile>())
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<IncineriteOre>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Torchstone_Tile>())
                    {
                        return itemtype;
                    }
                }
                else if(itemtype == ModContent.ItemType<ApocalyptiteOre>())
                {
                    if(Utils.NextFloat(Main.rand, chance) < 1 && type == ModContent.TileType<Doomstone_Tile>() && AAWorld.downedZero)
                    {
                        return itemtype;
                    }
                }
                else if(!Main.hardMode)
                {
                    if(Config.LuckyOre[itemtype] <= 500)
                    {
                        if(Utils.NextFloat(Main.rand, chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                else
                {
                    chance /= 2 * (1 + (NPC.downedPlantBoss? 1 : 0) + (NPC.downedMoonlord? 1 : 0) + (AAWorld.downedEquinox? 1 : 0) + (AAWorld.downedShen? 1 : 0));
                    int digcheck = 500 + (NPC.downedPlantBoss? 200 : 0) + (NPC.downedMoonlord? 110 : 0) + (AAWorld.downedEquinox? 20 : 0);
                    bool flag = Config.LuckyOre[itemtype] <= digcheck;
                    if(flag || AAWorld.downedShen)
                    {
                        if(Utils.NextFloat(Main.rand, chance) < 1)
                        {
                            return itemtype;
                        }
                    }
                }
                ChanceBalance = 1 - 1/chance;
            }
            return 0;
        }

        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            ModTile tile = ModContent.GetModTile(type);
            if (tile != null && tile is IGlowmaskTile glowTile && glowTile.ShouldDrawGlow)
            {
                if (ModContent.RequestIfExists(tile.Texture + "_Glow", out Asset<Texture2D> tex))
                {
                    Tile t = Main.tile[i, j];
                    Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
                    if (Main.drawToScreen)
                        zero = Vector2.Zero;

                    Point coordSize = glowTile.GetCoordinateSize(i, j);
                    Main.spriteBatch.Draw(tex.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(t.TileFrameX, t.TileFrameY, coordSize.X, coordSize.Y), glowTile.GlowColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }

    public class AAGlobalWall : GlobalWall
    {
        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            if (TileProtectionSystem.UnbreakableWalls.Contains(new(i, j)))
                fail = true;
        }

        public override bool CanExplode(int i, int j, int type)
        {
            if (TileProtectionSystem.UnbreakableWalls.Contains(new(i, j)))
                return false;
            return base.CanExplode(i, j, type);
        }
    }

    public class TileProtectionSystem : ModSystem
    {
        public static readonly HashSet<Point> UnbreakableTiles = [];

        public static readonly HashSet<Point> UnbreakableWalls = [];

        public override void SaveWorldData(TagCompound tag)
        {
            tag["ProtectedTileList"] = UnbreakableTiles.ToList();
            tag["ProtectedWallList"] = UnbreakableWalls.ToList();
        }

        public override void LoadWorldData(TagCompound tag)
        {
            UnbreakableTiles.Clear();
            var list = tag.GetList<Point>("ProtectedTileList");
            foreach(Point p in list)
                UnbreakableTiles.Add(p);

            UnbreakableWalls.Clear();
            list = tag.GetList<Point>("ProtectedWallList");
            foreach (Point p in list)
                UnbreakableWalls.Add(p);
        }
    }

    public interface IGlowmaskTile
    {
        public bool ShouldDrawGlow => true;
        public Point GetCoordinateSize(int x, int y) => new(16, 16);
        public Color GlowColor => Color.White;
    }
}