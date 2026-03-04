using Microsoft.Xna.Framework;

namespace AAModClassic.NPCs.Bosses.Greed
{
    class GreedrawColors
    {
        public static Color Copper => new Color(150, 67, 22); public static Color CopperGlow => new Color(255, 229, 183);

        public static Color Tin => new Color(87, 92, 80); public static Color TinGlow => new Color(187, 165, 124);

        public static Color Iron => new Color(87, 60, 60); public static Color IronGlow => new Color(189, 159, 139);

        public static Color Lead => new Color(47, 62, 87); public static Color LeadGlow => new Color(104, 140, 150);

        public static Color Silver => new Color(61, 72, 73); public static Color SilverGlow => new Color(122, 140, 144);

        public static Color Tungsten => new Color(39, 70, 40); public static Color TungstenGlow => new Color(154, 190, 155);

        public static Color Gold => new Color(148, 126, 24); public static Color GoldGlow => new Color(255, 249, 183);

        public static Color Platinum => new Color(72, 73, 114); public static Color PlatinumGlow => new Color(190, 222, 222);

        public static Color Demonite => new Color(68, 69, 114); public static Color DemoniteGlow => new Color(120, 117, 179);

        public static Color Crimtane => new Color(76, 76, 76); public static Color CrimtaneGlow => new Color(216, 59, 63);

        public static Color Incinerite => new Color(62, 58, 48); public static Color IncineriteGlow => new Color(204, 108, 42);

        public static Color Abyssium => new Color(52, 36, 88); public static Color AbyssiumGlow => new Color(18, 103, 92);

        public static Color Yttrium => new Color(128, 73, 97); public static Color YttriumGlow => new Color(228, 155, 102);

        public static Color Hellstone => new Color(102, 34, 34); public static Color HellstoneGlow => new Color(238, 102, 70);

        public static Color Cobalt => new Color(4, 48, 111); public static Color CobaltGlow => new Color(143, 210, 253);

        public static Color Palladium => new Color(160, 36, 11); public static Color PalladiumGlow => new Color(245, 95, 55);

        public static Color Mythril => new Color(22, 119, 125); public static Color MythrilGlow => new Color(212, 255, 190);

        public static Color Oricalcum => new Color(151, 0, 127); public static Color OricalcumGlow => new Color(248, 113, 227);

        public static Color Adamantite => new Color(128, 26, 52); public static Color AdamantiteGlow => new Color(221, 85, 152);

        public static Color Titanium => new Color(91, 90, 119); public static Color TitaniumGlow => new Color(190, 187, 220);

        public static Color Uranium => new Color(28, 39, 67); public static Color UraniumGlow => new Color(92, 157, 103);

        public static Color Chlorophyte => new Color(36, 137, 0); public static Color ChlorophyteGlow => new Color(234, 254, 126);

        public static Color Technecium => new Color(68, 81, 112); public static Color TechneciumGlow => new Color(96, 225, 225);

        public Color MainColor(int type)
        {
            switch (type)
            {
                case 0: return Copper;
                case 1: return Tin;
                case 2: return Iron;
                case 3: return Lead;
                case 4: return Silver;
                case 5: return Tungsten;
                case 6: return Gold;
                case 7: return Platinum;
                case 8: return Demonite;
                case 9: return Crimtane;
                case 10: return Incinerite;
                case 11: return Abyssium;
                case 12: return Yttrium;
                case 13: return Hellstone;
                case 14: return Cobalt;
                case 15: return Palladium;
                case 16: return Mythril;
                case 17: return Oricalcum;
                case 18: return Adamantite;
                case 19: return Titanium;
                case 20: return Uranium;
                case 21: return Chlorophyte;
                default: return Technecium;
            }
        }
        public Color GlowColor(int type)
        {
            switch (type)
            {
                case 0: return CopperGlow;
                case 1: return TinGlow;
                case 2: return IronGlow;
                case 3: return LeadGlow;
                case 4: return SilverGlow;
                case 5: return TungstenGlow;
                case 6: return GoldGlow;
                case 7: return PlatinumGlow;
                case 8: return DemoniteGlow;
                case 9: return CrimtaneGlow;
                case 10: return IncineriteGlow;
                case 11: return AbyssiumGlow;
                case 12: return YttriumGlow;
                case 13: return HellstoneGlow;
                case 14: return CobaltGlow;
                case 15: return PalladiumGlow;
                case 16: return MythrilGlow;
                case 17: return OricalcumGlow;
                case 18: return AdamantiteGlow;
                case 19: return TitaniumGlow;
                case 20: return UraniumGlow;
                case 21: return ChlorophyteGlow;
                default: return TechneciumGlow;
            }
        }
    }
}
