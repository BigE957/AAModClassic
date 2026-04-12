using AAModClassic.___Content.Mire._PostMoonlord.NPCs.__BossYamata;
using AAModClassic.___Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic.Globals;
using AAModClassic.NPCs.Bosses.AH.Ashe;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic.NPCs.Bosses.Anubis;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Equinox;
using AAModClassic.NPCs.Bosses.Greed;
using AAModClassic.NPCs.Bosses.Rajah;
using AAModClassic.NPCs.Bosses.Shen;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI.Titles
{
    public class TitlesUI : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Titles modPlayer = Main.player[Main.myPlayer].GetModPlayer<Titles>();
            if (modPlayer.text)
            {
                var textLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var computerState = new LegacyGameInterfaceLayer("AAMod: UI",
                    delegate
                    {
                        BossTitle(modPlayer.BossID);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(textLayer, computerState);
            }
        }

        private void BossTitle(int BossID)
        {
            string BossName = "";
            string bossTitle = "";
            Color titleColor = Color.White;

            switch (BossID)
            {
                case 1:
                    BossName = BossTitle("AnubisLegendscribeName");
                    bossTitle = "";
                    titleColor = Color.Goldenrod;
                    break;
                case 2:
                    BossName = BossTitle("AthenaName");
                    bossTitle = "";
                    titleColor = Color.CornflowerBlue;
                    break;
                case 3:
                    BossName = BossTitle("GreedName");
                    bossTitle = "";
                    titleColor = Color.Gold;
                    break;
                case 4:
                    BossName = BossTitle("FAnubisName");
                    bossTitle = BossTitle("FAnubisTitle");
                    titleColor = Color.DarkGreen;
                    break;
                case 5:
                    BossName = BossTitle("OAthenaName");
                    bossTitle = BossTitle("OAthenaTitle");
                    titleColor = Color.Turquoise;
                    break;
                case 6:
                    BossName = BossTitle("WKGName");
                    bossTitle = BossTitle("WKGTitle");
                    titleColor = Color.Gold;
                    break;
                case 7:
                    BossName = BossTitle("AkumaName");
                    bossTitle = BossTitle("AkumaTitle");
                    titleColor = Color.OrangeRed;
                    break;
                case 8:
                    BossName = BossTitle("AkumaAName");
                    bossTitle = BossTitle("AkumaATitle");
                    titleColor = Color.DeepSkyBlue;
                    break;
                case 9:
                    BossName = BossTitle("YamataName");
                    bossTitle = BossTitle("YamataTitle");
                    titleColor = Color.Indigo;
                    break;
                case 10:
                    BossName = BossTitle("YamataAName");
                    bossTitle = BossTitle("YamataATitle");
                    titleColor = Color.MediumVioletRed;
                    break;
                case 11:
                    BossName = BossTitle("ZER0Name");
                    bossTitle = BossTitle("ZER0Title");
                    titleColor = Color.Red;
                    break;
                case 12:
                    BossName = BossTitle("ZER0PName");
                    bossTitle = BossTitle("ZER0PTitle");
                    titleColor = Color.Red;
                    break;
                case 13:
                    BossName = BossTitle("CRajahRabbitName");
                    bossTitle = BossTitle("CRajahRabbitTitle");
                    titleColor = Color.LightCyan;
                    break;
                case 14:
                    BossName = BossTitle("ShenName");
                    bossTitle = BossTitle("ShenTitle");
                    titleColor = Color.Magenta;
                    break;
                case 15:
                    BossName = BossTitle("ShenAName");
                    bossTitle = BossTitle("ShenATitle");
                    titleColor = Color.Magenta;
                    break;
                case 16:
                    BossName = BossTitle("AHName");
                    bossTitle = BossTitle("AHTitle");
                    titleColor = Color.Magenta;
                    break;
                case 17:
                    BossName = BossTitle("EquinoxName");
                    bossTitle = BossTitle("EquinoxTitle");
                    titleColor = Color.BlueViolet;
                    break;
                case 18:
                    BossName = BossTitle("RajahName");
                    bossTitle = "";
                    titleColor = Color.LightCyan;
                    break;
            }

            Titles modPlayer2 = Main.player[Main.myPlayer].GetModPlayer<Titles>();
            float alpha = modPlayer2.alphaText;
            float alpha2 = modPlayer2.alphaText2;

            if (BossID == 16)
            {
                Vector2 textSize2 = FontAssets.DeathText.Value.MeasureString(bossTitle) * .6f;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;

                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, bossTitle, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), titleColor * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);

                float alpha3 = modPlayer2.alphaText3;
                float alpha4 = modPlayer2.alphaText4;

                Vector2 ASize = FontAssets.DeathText.Value.MeasureString(BossTitle("AsheName"));
                Vector2 AndSize = FontAssets.DeathText.Value.MeasureString(BossTitle("AHANd"));
                Vector2 HSize = FontAssets.DeathText.Value.MeasureString(BossTitle("HarukaName"));
                Vector2 BlankTexSize = FontAssets.DeathText.Value.MeasureString(" ");
                float APositionLeft = Main.screenWidth / 2 - (ASize.X + BlankTexSize.X + AndSize.X + BlankTexSize.X + HSize.X) / 2;
                float AndPositionLeft = APositionLeft + ASize.X + BlankTexSize.X;
                float HPositionLeft = AndPositionLeft + AndSize.X + BlankTexSize.X;

                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, BossTitle("AsheName"), new Vector2(APositionLeft, Main.screenHeight / 2 - 300), Color.OrangeRed * ((255 - alpha) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, BossTitle("AHANd"), new Vector2(AndPositionLeft, Main.screenHeight / 2 - 300), Color.Magenta * ((255 - alpha3) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, BossTitle("HarukaName"), new Vector2(HPositionLeft, Main.screenHeight / 2 - 300), Color.Indigo * ((255 - alpha4) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                return;
            }
            if (BossID == 14)
            {
                var BossNameSplit = BossName.Split(' ');
                var BossTitleSplit = bossTitle.Split(' ');
                if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese))
                {
                    BossNameSplit = Regex.Split(BossName, "", RegexOptions.IgnoreCase); ;
                    BossTitleSplit = Regex.Split(bossTitle, "", RegexOptions.IgnoreCase); ;
                }
                Vector2 textSize = FontAssets.DeathText.Value.MeasureString("~ " + BossName + " ~");
                Vector2 textSize2 = FontAssets.DeathText.Value.MeasureString(bossTitle) * .6f; ;
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;
                int k = 0;
                foreach (string i in BossTitleSplit)
                {
                    if (i == "" || i == " ")
                    {
                        continue;
                    }
                    Vector2 SplitSizeTitle = FontAssets.DeathText.Value.MeasureString(i) * .6f; ;
                    Vector2 BlankTexSizeTitle = FontAssets.DeathText.Value.MeasureString(" ") * .6f; ;
                    Main.spriteBatch.DrawString(FontAssets.DeathText.Value, (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ? "" : " ") + i, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), (k % 2 == 0 ? Color.OrangeRed : Color.Indigo) * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);
                    text2PositionLeft += SplitSizeTitle.X + (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ? 0 : BlankTexSizeTitle.X);
                    k++;
                }
                Vector2 SplitSize = FontAssets.DeathText.Value.MeasureString("~");
                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, "~" + (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ? " " : ""), new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), Color.OrangeRed * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                textPositionLeft += SplitSize.X;
                k = 0;
                foreach (string i in BossNameSplit)
                {
                    if (i == "" || i == " ")
                    {
                        continue;
                    }
                    Vector2 SplitSizeName = FontAssets.DeathText.Value.MeasureString(i);
                    Vector2 BlankTexSizeName = FontAssets.DeathText.Value.MeasureString(" ");
                    Main.spriteBatch.DrawString(FontAssets.DeathText.Value, (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ? "" : " ") + i, new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), (k % 2 == 0 ? Color.OrangeRed : Color.Indigo) * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    textPositionLeft += SplitSizeName.X + (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese) ? 0 : BlankTexSizeName.X);
                    k++;
                }
                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, " ~", new Vector2(textPositionLeft, (Main.screenHeight / 2) - 300), Color.Indigo * ((255 - alpha2) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                return;
            }
            else
            {
                Vector2 textSize = FontAssets.DeathText.Value.MeasureString("~ " + BossName + " ~");
                Vector2 textSize2 = FontAssets.DeathText.Value.MeasureString(bossTitle) * .6f; ;
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;
                float text2PositionLeft = Main.screenWidth / 2 - textSize2.X / 2;

                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, bossTitle, new Vector2(text2PositionLeft, (Main.screenHeight / 2) - 350), titleColor * ((255 - alpha2) / 255f), 0f, Vector2.Zero, .6f, SpriteEffects.None, 0f);
                Main.spriteBatch.DrawString(FontAssets.DeathText.Value, "~ " + BossName + " ~", new Vector2(textPositionLeft, Main.screenHeight / 2 - 300), titleColor * ((255 - alpha) / 255f), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

        }

        public static void ShowTitle(NPC npc, TitleGlobalNPC.Titles ID)
        {
            if (AAConfigClient.Instance.AncientIntroText)
            {
                if(ID == TitleGlobalNPC.Titles.AsheHaruka)
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<SistersTitle>(), 0, 0, Main.myPlayer, 16, 0);
                else
                    Projectile.NewProjectile(npc.GetSource_FromThis(), npc.Center, Vector2.Zero, ModContent.ProjectileType<Title>(), 0, 0, Main.myPlayer, (int)ID, 0);
            }
        }

        public static string BossTitle(string Boss)
        {
            if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Chinese))
            {
                switch (Boss)
                {
                    case "AnubisLegendscribeName":
                        return "阿努比斯 史诗记述者";

                    case "AthenaName":
                        return "雅典娜";

                    case "GreedName":
                        return "金食饕餮";

                    case "FAnubisName":
                        return "阿努比斯";
                    case "FAnubisTitle":
                        return "逝落的断罪师";

                    case "OAthenaName":
                        return "奥林匹亚女武神雅典娜";
                    case "OAthenaTitle":
                        return "鸮姬女王";

                    case "WKGName":
                        return "金食饕餮";
                    case "WKGTitle":
                        return "鎏金万蟲王";

                    case "AkumaName":
                        return "邪鬼巨龙";
                    case "AkumaTitle":
                        return "凶煞恶魔";

                    case "AkumaAName":
                        return "狂煞魔豪鬼";
                    case "AkumaATitle":
                        return "激燃怒火的化身";

                    case "YamataName":
                        return "八岐大蛇";
                    case "YamataTitle":
                        return "惊惧梦魇";

                    case "YamataAName":
                        return "八俣远吕智";
                    case "YamataATitle":
                        return "渊暗恐惧的化身";

                    case "ZER0Name":
                        return "零";
                    case "ZER0Title":
                        return "末日机构";

                    case "ZER0PName":
                        return "零源协议";
                    case "ZER0PTitle":
                        return "深暗虚空的使徒";

                    case "CRajahRabbitName":
                        return "巨兔王公";
                    case "CRajahRabbitTitle":
                        return "无辜的保护者";

                    case "ShenName":
                        return "上神应龙";
                    case "ShenTitle":
                        return "冥昧末日的预言者";

                    case "ShenAName":
                        return "觉醒之上神应龙";
                    case "ShenATitle":
                        return "冥昭瞢暗的化身";

                    case "AHName":
                        return "艾希 和 遥香";
                    case "AHTitle":
                        return "混沌姐妹";
                    case "AsheName":
                        return "艾希";
                    case "AHANd":
                        return "和";
                    case "HarukaName":
                        return "遥香";

                    case "EquinoxName":
                        return "昼夜虫";
                    case "EquinoxTitle":
                        return "神虫";

                    case "RajahName":
                        return "巨兔王公";
                }
            }
            else if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Russian))
            {
                switch (Boss)
                {
                    case "AnubisLegendscribe":
                        return "Анубис Летописец Легенд";

                    case "AthenaName":
                        return "Афина";

                    case "GreedName":
                        return "Жадность";

                    case "FAnubisName":
                        return "Анубис";
                    case "FAnubisTitle":
                        return "Забытый Судья";

                    case "OAthenaName":
                        return "Олимпийская Афина";
                    case "OAthenaTitle":
                        return "Королева Серафимов";

                    case "WKGName":
                        return "Жадность";
                    case "WKGTitle":
                        return "Король Червей";

                    case "AkumaName":
                        return "Акума";
                    case "AkumaTitle":
                        return "Драконоподобный Демон";

                    case "AkumaAName":
                        return "Они Акума";
                    case "AkumaATitle":
                        return "Воплощение Опаляющей Ярости";

                    case "YamataName":
                        return "Ямата";
                    case "YamataTitle":
                        return "Ужасающий Кошмар";

                    case "YamataAName":
                        return "Ямата Но Орочи";
                    case "YamataATitle":
                        return "Воплощение Гнева Бездны";

                    case "ZER0Name":
                        return "ЗИР0";
                    case "ZER0Title":
                        return "Конструкт Судного дня";

                    case "ZER0PName":
                        return "ЗИР0 ПР0Т0К0Л";
                    case "ZER0PTitle":
                        return "ТЕМНЫЙ НУЛЬ";

                    case "CRajahRabbitName":
                        return "Кролик Раджа";
                    case "CRajahRabbitTitle":
                        return "Защитник Невинных";

                    case "ShenName":
                        return "Шен Дорагон";
                    case "ShenTitle":
                        return "Предвестник Раздора";

                    case "ShenAName":
                        return "Шен Дорагон Пробужденный";
                    case "ShenATitle":
                        return "Воплощение Несгибаемого Раздора";

                    case "AHName":
                        return "Аши и Харука";
                    case "AHTitle":
                        return "Сестры Раздора";
                    case "AsheName":
                        return "Аши";
                    case "AHANd":
                        return "и";
                    case "HarukaName":
                        return "Харука";

                    case "EquinoxName":
                        return "Равноденствия";
                    case "EquinoxTitle":
                        return "Черви";

                    case "RajahName":
                        return "Кролик Раджа";
                }
            }
            else
            {
                switch (Boss)
                {
                    case "AnubisLegendscribeName":
                        return "Anubis Legendscribe";

                    case "AthenaName":
                        return "Athena";

                    case "GreedName":
                        return "Greed";

                    case "FAnubisName":
                        return "Anubis";
                    case "FAnubisTitle":
                        return "Forsaken Judge";

                    case "OAthenaName":
                        return "Olympian Athena";
                    case "OAthenaTitle":
                        return "Seraph Queen";

                    case "WKGName":
                        return "Greed";
                    case "WKGTitle":
                        return "Worm King";

                    case "AkumaName":
                        return "Akuma";
                    case "AkumaTitle":
                        return "Draconian Demon";

                    case "AkumaAName":
                        return "Oni Akuma";
                    case "AkumaATitle":
                        return "Blazing Fury Incarnate";

                    case "YamataName":
                        return "Yamata";
                    case "YamataTitle":
                        return "Dread Nightmare";

                    case "YamataAName":
                        return "Yamata No Orochi";
                    case "YamataATitle":
                        return "Abyssal Wrath Incarnate";

                    case "ZER0Name":
                        return "ZER0";
                    case "ZER0Title":
                        return "Doomsday Construct";

                    case "ZER0PName":
                        return "ZER0 PR0T0C0L";
                    case "ZER0PTitle":
                        return "Dark Cipher";

                    case "CRajahRabbitName":
                        return "Rajah Rabbit";
                    case "CRajahRabbitTitle":
                        return "Champion of the Innocent";

                    case "ShenName":
                        return "Shen Doragon";
                    case "ShenTitle":
                        return "Discordian Doomsayer";

                    case "ShenAName":
                        return "Shen Doragon Awakened";
                    case "ShenATitle":
                        return "Unyielding Discord Incarnate";

                    case "AHName":
                        return "Ashe & Haruka";
                    case "AHTitle":
                        return "Sisters of Discord";
                    case "AsheName":
                        return "Ashe";
                    case "AHANd":
                        return "&";
                    case "HarukaName":
                        return "Haruka";

                    case "EquinoxName":
                        return "Equinox Worms";
                    case "EquinoxTitle":
                        return "The";

                    case "RajahName":
                        return "Rajah Rabbit";
                }
            }
            return "";
        }

    }

    public class TitleGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public enum Titles
        {
            Anubis = 1,
            Athena,
            Greed,
            AnubisF,
            AthenaA,
            WKGreed,
            Akuma,
            AkumaA,
            Yamata,
            YamataA,
            Zero,
            ZeroP,
            CRajah,
            Shen,
            ShenA,
            AsheHaruka,
            Equinox,
            Rajah
        }
        private static Dictionary<int, Titles> IDtoTitle = [];

        public bool ShowTitle = false;
        private bool oldShowTitle = false;

        public override void SetStaticDefaults()
        {
            IDtoTitle.Add(ModContent.NPCType<Anubis>(), Titles.Anubis);
            IDtoTitle.Add(ModContent.NPCType<Athena>(), Titles.Athena);
            IDtoTitle.Add(ModContent.NPCType<Greed>(), Titles.Greed);
            IDtoTitle.Add(ModContent.NPCType<ForsakenAnubis>(), Titles.AnubisF);
            IDtoTitle.Add(ModContent.NPCType<AthenaA>(), Titles.AthenaA);
            IDtoTitle.Add(ModContent.NPCType<GreedA>(), Titles.WKGreed);
            IDtoTitle.Add(ModContent.NPCType<Akuma>(), Titles.Akuma);
            IDtoTitle.Add(ModContent.NPCType<AkumaA>(), Titles.AkumaA);
            IDtoTitle.Add(ModContent.NPCType<YamataBody>(), Titles.Yamata);
            IDtoTitle.Add(ModContent.NPCType<YamataABody>(), Titles.YamataA);
            IDtoTitle.Add(ModContent.NPCType<Zero>(), Titles.Zero);
            IDtoTitle.Add(ModContent.NPCType<ZeroProtocol>(), Titles.ZeroP);
            IDtoTitle.Add(ModContent.NPCType<SupremeRajah>(), Titles.CRajah);
            IDtoTitle.Add(ModContent.NPCType<Shen>(), Titles.Shen);
            IDtoTitle.Add(ModContent.NPCType<ShenA>(), Titles.ShenA);
            IDtoTitle.Add(ModContent.NPCType<Ashe>(), Titles.AsheHaruka);
            IDtoTitle.Add(ModContent.NPCType<DaybringerHead>(), Titles.Equinox);
            IDtoTitle.Add(ModContent.NPCType<Rajah>(), Titles.Rajah);
        }

        public override bool PreAI(NPC npc)
        {
            if (oldShowTitle)
                return true;

            if (ShowTitle)
            {
                if (IDtoTitle.TryGetValue(npc.type, out var id))
                    TitlesUI.ShowTitle(npc, id);
            }

            oldShowTitle = ShowTitle;

            return true;
        }
    }

    public class Titles : ModPlayer
    {
        public bool text = false;
        public float alphaText = 255f;
        public float alphaText2 = 255f;
        public float alphaText3 = 255f;
        public float alphaText4 = 255f;
        public int BossID = 0;

        public override void ResetEffects()
        {
            text = false;
        }

        public override void PreUpdate()
        {
            if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<Title>()) && !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<SistersTitle>()))
            {
                alphaText = 255f;
                alphaText2 = 255f;
            }
            if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<SistersTitle>()))
            {
                alphaText3 = 255f;
                alphaText4 = 255f;
            }
        }
    }

    public class Title : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Titles modPlayer = player.GetModPlayer<Titles>();

            modPlayer.text = true;

            modPlayer.BossID = (int)Projectile.ai[0];

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;

            if (Projectile.timeLeft <= 45)
            {
                if (modPlayer.alphaText < 255f)
                {
                    modPlayer.alphaText += 10f;
                    modPlayer.alphaText2 += 10f;
                }
            }
            else
            {
                if (Projectile.timeLeft <= 180)
                {
                    modPlayer.alphaText -= 5f;
                }
                if (modPlayer.alphaText > 0f)
                {
                    modPlayer.alphaText2 -= 5f;
                }
            }
        }
    }

    public class SistersTitle : ModProjectile
    {
        public override string Texture => "AAModClassic/BlankTex";
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Titles modPlayer = player.GetModPlayer<Titles>();

            modPlayer.text = true;

            modPlayer.BossID = (int)Projectile.ai[0];

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;

            if (Projectile.timeLeft <= 45)
            {
                if (modPlayer.alphaText < 255f)
                {
                    modPlayer.alphaText += 10f;
                    modPlayer.alphaText2 += 10f;
                    modPlayer.alphaText3 += 10f;
                    modPlayer.alphaText4 += 10f;
                }
            }
            else
            {
                if (Projectile.timeLeft <= 240)
                {
                    modPlayer.alphaText -= 5f;
                }
                if (Projectile.timeLeft <= 200)
                {
                    modPlayer.alphaText3 -= 5f;
                }
                if (Projectile.timeLeft <= 160)
                {
                    modPlayer.alphaText4 -= 5f;
                }
                if (modPlayer.alphaText2 > 0f)
                {
                    modPlayer.alphaText2 -= 5f;
                }
            }
        }
    }
}
