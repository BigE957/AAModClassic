using Microsoft.Xna.Framework;
using Terraria;

using System;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Globals
{
	public static class AAColor
    {
        public static Color Rarity12 => new Color(239, 0, 243);

        public static Color Rarity13 => new Color(0, 125, 243);

        public static Color Rarity14 => new Color(255, 22, 0);

        public static Color Rarity15 => new Color(0, 178, 107);

        public static Color Lantern => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(224, 213, 94), new Color(130, 104, 41), new Color(224, 213, 94));

        public static Color Mushroom => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.LightCoral, Color.Coral, Color.LightCoral);

        public static Color Broodmother => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Orange, Color.DarkOrange, Color.Orange);

        public static Color Hydra => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkSlateBlue, Color.Indigo, Color.DarkSlateBlue);

        public static Color Glow => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.White, Color.Transparent, Color.Transparent, Color.White);

        public static Color Flash => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Transparent, Color.White, Color.Transparent, Color.White, Color.Transparent, Color.Transparent, Color.Transparent);

        public static Color Akuma => new Color(146, 37, 30);

        public static Color Yamata => new Color(53, 57, 126);

        public static Color AkumaA => Color.DeepSkyBlue;

        public static Color YamataA => new Color(146, 30, 68);

        public static Color Hero => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.ForestGreen, Color.Brown, Color.Brown, Color.ForestGreen);

        public static Color Zero => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Red, Color.DarkRed, Color.Red);

        public static Color Shen => Color.Magenta;

        public static Color Shen2 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.OrangeRed, Color.Indigo, Color.Indigo, Color.OrangeRed);

        public static Color Shen3 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Shen, AkumaA, Shen, YamataA, Shen, AkumaA, Shen);

        public static Color IZ => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkGray, Color.Gray, Color.DarkGray);

        public static Color Cthulhu => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Cyan, Color.DarkCyan, Color.Cyan);

        public static Color Cthulhu2 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(10, 22, 28), Color.White, new Color(10, 22, 28));

        public static Color Snow => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(0, 140, 128), new Color(0, 117, 128), new Color(0, 140, 128));

        public static Color Desert => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(227, 159, 0), new Color(176, 88, 0), new Color(227, 159, 0));

        public static Color Ocean => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(112, 255, 243), new Color(27, 178, 191), new Color(112, 255, 243));

        public static Color Hell => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(179, 75, 0), new Color(128, 43, 0), new Color(179, 75, 0));

        public static Color Inferno => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(214, 36, 0), new Color(153, 13, 0), new Color(214, 36, 0));

        public static Color Mire => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(57, 0, 156), new Color(26, 0, 128), new Color(57, 0, 156));

        public static Color Dungeon => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(232, 36, 102), new Color(33, 31, 77), new Color(232, 36, 102));

        public static Color Corruption => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(117, 0, 140), new Color(75, 0, 102), new Color(117, 0, 140));

        public static Color Crimson => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(140, 0, 2), new Color(102, 0, 2), new Color(140, 0, 2));

        public static Color Jungle => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(124, 140, 2), new Color(56, 102, 0), new Color(124, 140, 2));

        public static Color Sky => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(0, 138, 166), new Color(0, 99, 148), new Color(0, 138, 166));

        public static Color Cavern => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(122, 47, 24), new Color(77, 22, 15), new Color(122, 47, 24));

        public static Color Hallow => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkCyan, Color.Magenta, Color.DarkCyan);

        public static Color TerraGlow => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkGreen, Color.Lime, Color.DarkGreen);

        public static Color Daybringer => new Color(47, 31, 108);

        public static Color Nightcrawler => new Color(42, 7, 74);

        public static Color Storm => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.DarkViolet, Color.Violet, Color.DarkViolet, Color.Violet, Color.DarkViolet);

        public static Color Djinn => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.SandyBrown, Color.Sienna, Color.SandyBrown);

        public static Color Serpent => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.CornflowerBlue, Color.CadetBlue, Color.CornflowerBlue);

        public static Color TODE => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DodgerBlue, Color.CornflowerBlue, Color.DodgerBlue);

        public static Color Jevil => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Violet, Color.ForestGreen, Color.ForestGreen, Color.Violet);

        public static Color CthulhuItemRarity => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.SeaGreen, Color.DarkSeaGreen, Color.SeaGreen);

        public static Color ZeroShield => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Red, Color.DarkRed, Color.DarkRed, Color.Red);

        public static Color CursedInferno => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(55, 200, 26), new Color(179, 252, 0), new Color(218, 255, 0), new Color(179, 252, 0), new Color(55, 200, 26));

        public static Color Ichor => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(253, 152, 0), new Color(252, 202, 80), new Color(255, 251, 166), new Color(252, 202, 80), new Color(253, 152, 0));

        public static Color BogToxin => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(32, 51, 91), new Color(42, 120, 165), new Color(32, 51, 91));

        public static Color DragonFire => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, new Color(125, 10, 50), new Color(212, 45, 40), new Color(125, 10, 50));

        public static Color Uranium => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, BaseDrawing.GetLightColor(Main.LocalPlayer.position), BaseDrawing.GetLightColor(Main.LocalPlayer.position), Color.Green, Color.Green, BaseDrawing.GetLightColor(Main.LocalPlayer.position));
        
        public static Color FlashGlow => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Transparent, Color.White, Color.White, Color.Transparent);

        public static Color Oblivion
        {
            get
            {
                float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
                float Pie = 1f * (float)Math.Sin(Eggroll);
                Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
                return color1;
            }
        }

        public static Color Storm2
        {
            get
            {
                float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
                float Pie = 1f * (float)Math.Sin(Eggroll);
                Color color1 = Color.Lerp(new Color(86, 50, 125), Color.Black, Pie);
                return color1;
            }
        }

        public static Color COLOR_WHITEFADE1
        {
            get
            {
                Color c = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.White, Color.White * 0.7f, Color.White);
                c.A = 255;
                return c;
            }
        }

        public static int DiscoR1 = 0;

        public static int DiscoB1 = 255;

        public static int DiscoG1 = 0;

        public static int DiscoStyle1;


        public static Color Rainbow1 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Red, Color.Green, Color.Blue);

        public static Color Rainbow2 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Green, Color.Blue, Color.Red);

        public static Color Rainbow3 => BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Blue, Color.Red, Color.Green);
    }
}