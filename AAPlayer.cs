using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Items.Armor.Champion;
using AAModClassic.Items.Armor.Terra.Projectiles;
using AAModClassic.Items.Dev.RuneBook;
using AAModClassic.Items.FishingItem;
using AAModClassic.Items.Usable;
using AAModClassic.Items.Vanity.Aves;
using AAModClassic.Items.Vanity.Delly;
using AAModClassic.Items.Vanity.Hallam;
using AAModClassic.NPCs.Bosses.Akuma;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;
using AAModClassic.NPCs.Bosses.Anubis.Forsaken;
using AAModClassic.NPCs.Bosses.Athena;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Shen;
using AAModClassic.NPCs.Bosses.Toad;
using AAModClassic.NPCs.Bosses.Yamata;
using AAModClassic.NPCs.Bosses.Yamata.Awakened;
using AAModClassic.NPCs.Bosses.Zero;
using AAModClassic.NPCs.Bosses.Zero.Protocol;
using AAModClassic.Projectiles.AH;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace AAModClassic
{
    public partial class AAPlayer : ModPlayer
    {
        #region Variables

        #region Minions
        public bool FireSpirit = false;
        public bool ImpServant = false;
        public bool ImpSlave = false;
        public bool MoonBee = false;
        public bool Searcher = false;
        public bool enderMinion = false;
        public bool enderMinionEX = false;
        public bool LungMinion = false;
        public bool DragonMinion = false;
        public bool BabyPhoenix = false;
        public bool GripMinion = false;
        public bool ProbeMinion = false;
        public bool SkullMinion = false;
        public bool EaterMinion = false;
        public bool CrimeraMinion = false;
        public bool CrowMinion = false;
        public bool DemonMinion = false;
        public bool DevilMinion = false;
        public bool DoomiteProbe = false;
        public bool DoomiteProbeC = false;
        public bool TerraMinion = false;
        public bool HallowedPrism = false;
        public bool TrueHallowedPrism = false;
        public bool SnakeMinion = false;
        public bool dustDevil = false;
        public bool KrakenMinion = false;
        public bool Fishnado = false;
        public bool MadnessElemental = false;
        public bool FlameSoul = false;
        public bool Orbiters = false;
        public bool Protocol = false;
        public bool ScoutMinion = false;
        public bool SagOrbiter = false;
        public bool Rabbitcopter = false;
        public bool RabbitcopterR = false;
        public bool Sock = false;
        public bool Socc = false;
        public bool Squirrel = false;
        public bool DapperSquirrel = false;
        public bool CyberClaw = false;
        public bool ChaosClaw = false;
        public bool MiniZero = false;
        public bool TerraSummon = false;
        public bool DragonSpirit = false;
        public bool Seraph = false;
        public bool Athena = false;
        public bool Baron = false;
        public bool Xiao = false;
        public bool ChaosConstruct = false;
        public bool CCBook = false;
        public bool CCBookEX = false;
        public bool WeakCCRune = false;
        public bool CCRune = false;
        #endregion

        #region Biome bools.
        public bool ZoneMire = false;
        public bool ZoneInferno = false;
        public bool ZoneVoid = false;
        public bool ZoneMush = false;
        public bool ZoneStorm = false;
        public bool ZoneRisingSunPagoda = false;
        public bool ZoneRisingMoonLake = false;
        public bool ZoneShip = false;
        public bool VoidUnit = false;
        public bool SunAltar = false;
        public bool MoonAltar = false;
        public bool AkumaAltar = false;
        public bool YamataAltar = false;
        public bool Terrarium = false;
        public bool ZoneStars = false;
        public bool ZoneHoard = false;
        public bool ZoneAcropolis = false;
        public bool AshCurse;
        public int VoidGrav = 0;
        public static int Ashes = 0;
        public int CthulhuCountdown = 10800;
        public bool Leave = false;

        public bool RadiumStars = false;
        public bool Darkmatter = false;
        #endregion

        #region Armor bools.
        public bool AncientGoldBody = false;
        public bool AncientGoldLeg = false;
        public bool AncientGoldSet = false;
        public bool StripeManFish = false;
        public bool StripeManOre = false;
        public bool StripeManSpawn = false;
        public bool StripeManSet = false;
        public bool MoonSet;
        public bool goblinSlayer;
        public bool IsGoblin;
        public bool leatherSet;
        public bool mushiumSet;
        public bool kindledSet;
        public bool depthSet;
        public bool impSet;
        public bool DynaskullSet;
        public bool fleshrendSet;
        public bool nightsSet;
        public bool deathlySet;
        public bool tribalSet;
        public bool demonSet;
        public bool demonBonus;
        public bool terraSet;
        public bool chaosSet;
        public bool darkmatterSetMe;
        public bool darkmatterSetRa;
        public bool darkmatterSetMa;
        public bool darkmatterSetSu;
        public bool darkmatterSetTh;
        public bool radiumMe;
        public bool radiumRa;
        public bool radiumMa;
        public bool radiumSu;
        public bool DarkmatterSet;
        public bool dracoSet;
        public bool dreadSet;
        public bool zeroSet1;
        public bool zeroSet;
        public bool valkyrieSet;
        public bool infinitySet;
        public bool Alpha;
        public bool Palladium;
        public bool fulgurite;
        public bool ringActive = false;
        public bool doomite;
        public bool Radium;
        public bool perfectChaos;
        public bool perfectChaosMe;
        public bool perfectChaosRa;
        public bool perfectChaosMa;
        public bool perfectChaosSu;
        public bool Assassin;
        public bool AbyssalStealth;
        public bool Witch;

        public bool ChaosMe = false;
        public bool ChaosRa = false;
        public bool ChaosMe1 = false;
        public bool ChaosRa2 = false;
        public bool ChaosMa = false;
        public bool ChaosSu = false;

        public bool Olympian = false;
        public bool StoneSoldier = false;

        public bool ChampionMe = false;
        public bool ChampionRa = false;
        public bool ChampionMa = false;
        public int CarrotBuff = 0;
        public bool ChampionSu = false;

        public bool TerraMe = false;
        public bool TerraRa = false;
        public bool TerraSu = false;
        public int CrystalMode = 0;
        public bool TerraMa = false;
        public int RoseCooldown = 0;

        public bool onoPrevious;
        public bool ono;
        public bool onoHideVanity;
        public bool onoForceVanity;

        public bool AsheFlame;
        public float AsheFlameScale = 0f;
        public int AsheCooldown = 0;
        #endregion

        #region Accessory bools
        public bool artifactJudgement;
		public int artifactJudgementCharge = 0;
		public bool artifactGuilt;
		public int artifactGuiltCharge = 0;
        public bool clawsOfChaos;
        public bool HydraPendant;
        public bool demonGauntlet;
        public bool BrokenCode;
        public int AbilityCD = 180;
        public bool AshRemover;
        public bool FogRemover;
        public bool Baolei;
        public bool Naitokurosu;
        public bool Duality;
        public bool DragonShell;
        public bool ammo20percentdown = false;
        public int AADash;
        public int AADashTime;
        public int dashDelayAA;
        public bool RStar;
        public bool DVoid;
        public int[] AADoubleTapKeyTimer = new int[4];
        public int[] AAHoldDownKeyTimer = new int[4];
        public bool DiscordShredder;
        public bool lantern = false;
        public bool HeartP = false;
        public bool HeartS = false;
        public bool HeartA = false;
        public bool DragonsGuard = false;
        public bool ShadowBand = false;
        public bool RajahCape = false;
        public bool olympianWings = false;
        public bool BlackLotusEmblem = false;

        public bool SagShield = false;
        public bool ShieldUp = false;
        public int SagCooldown = 0;

        public bool GreedCharm;
        public bool GreedTalisman;
        public bool OldOneCharm = false;
        public bool SpellBookofRagnarok;
        public bool CursedEyeofSoulBinder;
        #endregion

        #region debuffs
        public bool CursedHellfire = false;
        public bool infinityOverload = false;
        public bool discordInferno = false;
        public bool dragonFire = false;
        public bool hydraToxin = false;
        public bool terraBlaze = false;
        public bool Snagged = false;
        public bool Snagged1 = false;
        public bool YamataCount = false;
        public bool YamataACount = false;
        public bool Clueless = false;
        public bool Yanked = false;
        public bool InfinityScorch = false;
        public bool LockedOn = false;
        public bool shroomed = false;
        public bool riftbent = false;
        public bool DestinedToDie = false;
        public int TeleportTimer = 0;
        public bool YamataGravity = false;
        public bool YamataAGravity = false;
        public bool Hunted = false;
        public bool IB = false;
        public bool Spear = false;
        public bool AkumaPain = false;
        public bool FFlames = false;
        #endregion

        #region buffs

        public bool Ronin = false;
        public bool Glitched = false;
        public bool Greed1 = false;
        public bool Greed2 = false;
        public float GreedyDamage = 0;

        public bool luckycalm = false;
        public bool luckythorns = false;
        public bool StripeCrasyLucky = false;
        public bool CrasyLucky = false;
        #endregion

        #region pets
        public bool Broodmini = false;
        public bool Raidmini = false;
        public bool MiniProbe = false;
        public bool Sharkron = false;
        public bool RoyalKitten = false;
        public bool Mudkip = false;
        public bool MudkipS = false;
        public bool BoomBoi = false;
        public bool DragonSoul = false;
        public bool Glowmoss = false;
        public bool Cerberus = false;
        public bool K9 = false;
        public bool Lunamini = false;
        public bool ZeroBab = false;
        #endregion

        //NPCcount
        public static int yamata = -1;

        #region Colors
        public static Color IncineriteColor = new Color((int)(242 * 0.7f), (int)(107 * 0.7f), 0);
        public static Color ZeroColor = new Color((int)(233 * 0.7f), (int)(53 * 0.7f), (int)(53 * 0.7f));
        public static Color groviteColor = new Color(138, (int)(39 * 0.7f), (int)(196 * 0.7f));
        public static bool[] groviteGlow = new bool[255];

        public static int ZeroKills = 0;

        public int ManaLantern = 0;
        #endregion

        #region Misc
        public bool Compass = false;
        public Vector2 RiftPos = new Vector2(0, 0);
        public int PrismCooldown = 0;
        public bool WorldgenReminder = false;
        public bool NewAAReminder = false;
        public bool DemonSun = false;
        public bool AnubisBook = false;
        public bool GivenAnuSummon = false;
        public bool GivenWormIdol = false;

        public float spellbookDamage = 1f;
        public float MaxMovespeedboost = 0;
        public bool bossactive = false;
        public bool nohitplayer = true;
        #endregion

        #endregion

        #region Save/Load
        public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
        {
            var saved = new List<string>();
            if (AnubisBook) saved.Add("Book");
            if (GivenAnuSummon) saved.Add("Stick");
            if (GivenWormIdol) saved.Add("Idol");
        }

        public override void LoadData(TagCompound tag)
        {
            var downed = tag.GetList<string>("saved");
            AnubisBook = downed.Contains("Book");
            GivenAnuSummon = downed.Contains("Stick");
            GivenWormIdol = downed.Contains("Idol");
        }

        #endregion

        #region Reset Effects

        public override void ResetEffects()
        {
            ResetMinionEffect();
            ResetArmorEffect();
            ResetAccessoryEffect();
            ResetBuffEffect();
            ResetDebuffEffect();
            ResetPetsEffect();

            spellbookDamage = 1f;
            MaxMovespeedboost = 0;
            bossactive = false;

            //EnemyChecks
            IsGoblin = false;
            ResetMiscEffect();
        }

        private void ResetMiscEffect()
        {
            Compass = false;
            DemonSun = false;
            onoPrevious = ono;
            ono = onoHideVanity = onoForceVanity = false;
        }

        private void ResetMinionEffect()
        {
            FireSpirit = false;
            ImpServant = false;
            ImpSlave = false;
            MoonBee = false;
            Searcher = false;
            enderMinion = false;
            enderMinionEX = false;
            BabyPhoenix = false;
            LungMinion = false;
            DragonMinion = false;
            GripMinion = false;
            ProbeMinion = false;
            SkullMinion = false;
            EaterMinion = false;
            CrimeraMinion = false;
            CrowMinion = false;
            DemonMinion = false;
            DevilMinion = false;
            DoomiteProbe = false;
            DoomiteProbeC = false;
            HallowedPrism = false;
            TrueHallowedPrism = false;
            TerraMinion = false;
            SnakeMinion = false;
            dustDevil = false;
            KrakenMinion = false;
            Fishnado = false;
            MadnessElemental = false;
            FlameSoul = false;
            Orbiters = false;
            Protocol = false;
            ScoutMinion = false;
            SagOrbiter = false;
            Rabbitcopter = false;
            RabbitcopterR = false;
            Sock = false;
            Socc = false;
            Squirrel = false;
            DapperSquirrel = false;
            CyberClaw = false;
            ChaosClaw = false;
            MiniZero = false;
            TerraSummon = false;
            DragonSpirit = false;
            Seraph = false;
            Athena = false;
            Baron = false;
            Xiao = false;
            ChaosConstruct = false;
            CCBook = false;
            CCBookEX = false;
            WeakCCRune = false;
            CCRune = false;
        }

        private void ResetArmorEffect()
        {
			artifactJudgement = false;
			artifactGuilt = false;
            MoonSet = false;
            valkyrieSet = false;
            kindledSet = false;
            depthSet = false;
            demonSet = false;
            demonBonus = false;
            fleshrendSet = false;
            goblinSlayer = false;
            tribalSet = false;
            impSet = false;
            terraSet = false;
            chaosSet = false;
            DynaskullSet = false;
            zeroSet = false;
            dracoSet = false;
            dreadSet = false;
            darkmatterSetMe = false;
            darkmatterSetRa = false;
            darkmatterSetMa = false;
            darkmatterSetSu = false;
            darkmatterSetTh = false;
            infinitySet = false;
            Alpha = false;
            Palladium = false;
            fulgurite = false;
            doomite = false;
            DarkmatterSet = false;
            perfectChaos = false;
            Assassin = false;
            AbyssalStealth = false;
            AsheFlame = false;
            Witch = false;
            ChaosMe = false;
            ChaosMe1 = false;
            ChaosRa = false;
            ChaosRa2 = false;
            ChaosMa = false;
            ChaosSu = false;
            Olympian = false;
            AncientGoldBody = false;
            AncientGoldLeg = false;
            AncientGoldSet = false;
            StripeManFish = false;
            StripeManOre = false;
            StripeManSpawn = false;
            StripeManSet = false;
            ChampionMe = false;
            ChampionRa = false;
            ChampionMa = false;
            ChampionSu = false;
            StoneSoldier = false;
            TerraMe = false;
            TerraRa = false;
            TerraSu = false;
            TerraMa = false;
        }

        private void ResetAccessoryEffect()
        {
            AshRemover = false;
            FogRemover = false;
            clawsOfChaos = false;
            HydraPendant = false;
            demonGauntlet = false;
            BrokenCode = false;
            Baolei = false;
            Duality = false;
            Naitokurosu = false;
            ammo20percentdown = false;
            AshCurse = !Main.dayTime && !AAWorld.downedAkuma;
            AADash = 0;
            DiscordShredder = false;
            RStar = false;
            DVoid = false;
            lantern = false;
            HeartP = false;
            HeartS = false;
            HeartA = false;
            BlackLotusEmblem = false;
            SagShield = false;
            ShieldUp = false;
            DragonsGuard = false;
            ShadowBand = false;
            RajahCape = false;
            GreedCharm = false;
            GreedTalisman = false;
            Greed1 = false;
            Greed2 = false;
            olympianWings = false;
            OldOneCharm = false;
            SpellBookofRagnarok = false;
            CursedEyeofSoulBinder = false;
        }

        private void ResetBuffEffect()
        {
            Ronin = false;
            luckycalm = false;
            luckythorns = false;
            CrasyLucky = false;
        }

        private void ResetDebuffEffect()
        {
            CursedHellfire = false;
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Clueless = false;
            Yanked = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            IB = false;
            Spear = false;
            AkumaPain = false;
            Greed1 = false;
            Greed2 = false;
            FFlames = false;
        }

        private void ResetPetsEffect()
        {
            Broodmini = false;
            Raidmini = false;
            MiniProbe = false;
            Sharkron = false;
            RoyalKitten = false;
            Mudkip = false;
            MudkipS = false;
            BoomBoi = false;
            DragonSoul = false;
            Glowmoss = false;
            Cerberus = false;
            K9 = false;
            Lunamini = false;
            ZeroBab = false;
        }

        public override void Initialize()
        {
            AbilityCD = 0;
            ManaLantern = 0;
            ZoneInferno = false;
            ZoneMire = false;
            ZoneMush = false;
            ZoneStorm = false;
            ZoneVoid = false;
            ZoneRisingMoonLake = false;
            ZoneRisingSunPagoda = false;
            ZoneShip = false;
            ZoneStars = false;
            ZoneHoard = false;
            ZoneAcropolis = false;
            WorldgenReminder = false;
            NewAAReminder = false;
        }

        #endregion

        #region Biomes

        public bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            return ZoneMire == modOther.ZoneMire &&
                ZoneInferno == modOther.ZoneInferno &&
                ZoneVoid == modOther.ZoneVoid &&
                ZoneMush == modOther.ZoneMush &&
                Terrarium == modOther.Terrarium &&
                ZoneStorm == modOther.ZoneStorm &&
                ZoneShip == modOther.ZoneShip &&
                ZoneStars == modOther.ZoneStars &&
                ZoneHoard == modOther.ZoneHoard &&
                ZoneAcropolis == modOther.ZoneAcropolis;
        }

        public void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            modOther.ZoneInferno = ZoneInferno;
            modOther.ZoneMire = ZoneMire;
            modOther.ZoneVoid = ZoneVoid;
            modOther.ZoneMush = ZoneMush;
            modOther.Terrarium = Terrarium;
            modOther.ZoneStorm = ZoneStorm;
            modOther.ZoneRisingMoonLake = ZoneRisingMoonLake;
            modOther.ZoneRisingSunPagoda = ZoneRisingSunPagoda;
            modOther.ZoneShip = ZoneShip;
            modOther.ZoneStars = ZoneStars;
            modOther.ZoneHoard = ZoneHoard;
            modOther.ZoneAcropolis = ZoneAcropolis;
        }

        public void SendCustomBiomes(BinaryWriter bb)
        {
            BitsByte zoneByte = 0;
            zoneByte[0] = ZoneInferno;
            zoneByte[1] = ZoneMire;
            zoneByte[2] = ZoneVoid;
            zoneByte[3] = ZoneMush;
            zoneByte[4] = Terrarium;
            zoneByte[5] = ZoneStorm;
            zoneByte[6] = ZoneRisingSunPagoda;
            zoneByte[7] = ZoneRisingMoonLake;
            bb.Write(zoneByte);

            BitsByte zoneByte2 = 0;
            zoneByte2[0] = ZoneShip;
            zoneByte2[1] = ZoneStars;
            zoneByte2[2] = ZoneHoard;
            zoneByte2[3] = ZoneAcropolis;
            bb.Write(zoneByte2);
        }

        public void ReceiveCustomBiomes(BinaryReader bb)
        {
            BitsByte zoneByte = bb.ReadByte();
            ZoneInferno = zoneByte[0];
            ZoneMire = zoneByte[1];
            ZoneVoid = zoneByte[2];
            ZoneMush = zoneByte[3];
            Terrarium = zoneByte[4];
            ZoneStorm = zoneByte[5];
            ZoneRisingSunPagoda = zoneByte[6];
            ZoneRisingMoonLake = zoneByte[7];

            BitsByte zoneByte2 = bb.ReadByte();
            ZoneShip = zoneByte2[0];
            ZoneStars = zoneByte2[1];
            ZoneHoard = zoneByte2[2];
            ZoneAcropolis = zoneByte2[3];
        }

        #endregion

        #region Hit Effects

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers) 
		{
			if (npc.HasBuff(ModContent.BuffType<ForsakenWeak>()))
			{
                modifiers.FinalDamage.Flat -= modifiers.FinalDamage.Flat / 5;
			}

            if(luckythorns)
            {
                if (Player.whoAmI == Main.myPlayer && !Player.immune && !npc.dontTakeDamage)
                {
                    int RDamage = (int)(Player.GetDamage(DamageClass.Generic).ApplyTo(npc.damage) * 0.433f);
                    int direc = -1;
                    if (npc.position.X + npc.width / 2 < Player.position.X + Player.width / 2)
                    {
                        direc = 1;
                    }
                    Player.ApplyDamageToNPC(npc, RDamage, 10f, -direc, false);
                }
            }
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Palladium)
            {
                Player.AddBuff(BuffID.RapidHealing, 300);
            }

            if (StoneSoldier)
            {
                if (target.life <= 0 && Main.rand.Next(80) == 0)
                {
                    Projectile.NewProjectile(target.GetSource_GiftOrReward(), target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0, Main.myPlayer);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (TerraRa && proj.CountsAsClass(DamageClass.Ranged) && Main.rand.Next(3) == 0)
            {
                float screenX;
                float screenY;
                if (Main.rand.Next(2) == 0)
                {
                    screenX = Main.screenPosition.X;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenX += Main.screenWidth;
                    }
                    screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                }
                else
                {
                    screenY = Main.screenPosition.Y;
                    if (Main.rand.Next(2) == 0)
                    {
                        screenY += Main.screenHeight;
                    }
                    screenX = Main.screenPosition.X;
                    screenX += Main.rand.Next(Main.screenWidth);
                }
                Vector2 vector = new Vector2(screenX, screenY);
                float velocityX = target.Center.X - vector.X;
                float velocityY = target.Center.Y - vector.Y;
                velocityX += Main.rand.Next(-50, 51) * 0.1f;
                velocityY += Main.rand.Next(-50, 51) * 0.1f;
                float num6 = 24 / (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
                velocityX *= num6;
                velocityY *= num6;
                Projectile p = Projectile.NewProjectileDirect(Player.GetSource_OnHit(target), new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<Items.Armor.Terra.Projectiles.TerraBullet>(), damageDone / 3, 0f, Player.whoAmI);
                p.tileCollide = false;
            }

            if (Palladium)
            {
                Player.AddBuff(BuffID.RapidHealing, 300);
            }

            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
                if (target.life <= 0 && Main.rand.Next(80) == 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Vector2.Zero, ProjectileID.CoinPortal, 0, 0, Main.myPlayer);
                }
            }

            if (target.HasBuff(ModContent.BuffType<Forsaken>()) && proj.type == ModContent.ProjectileType<EnchancedMummyArrow>())
            {
				float num1 = 9f;
				Vector2 vector2 = new Vector2(Player.position.X + Player.width * 0.5f, Player.position.Y + Player.height * 0.5f);
				float f1 = target.Center.X - vector2.X;
				float f2 = target.Center.Y - vector2.Y;
				float num4 = (float)Math.Sqrt(f1 * (double)f1 + f2 * (double)f2);
				float num5;
				if (float.IsNaN(f1) && float.IsNaN(f2) || f1 == 0.0 && f2 == 0.0)
				{
					f1 = Player.direction;
					f2 = 0.0f;
					num5 = num1;
				}
				else
					num5 = num1 / num4;
				float SpeedX = f1 * num5;
				float SpeedY = f2 * num5;
                Vector2 velocity = new Vector2(SpeedX, SpeedY);

                float numberProjectiles = 3;
				float rotation = MathHelper.ToRadians(3);
				vector2 += Vector2.Normalize(velocity) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
					Projectile.NewProjectile(target.GetSource_OnHurt(proj), vector2.X, vector2.Y, perturbedSpeed.X*2, perturbedSpeed.Y*2, ModContent.ProjectileType<ForsakenArrow>(), damageDone / 2, proj.knockBack, Player.whoAmI);
				}
				target.buffImmune[ModContent.BuffType<Forsaken>()] = true;
			}
        }

		public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
		{
			if (artifactJudgement)
			{
				artifactJudgementCharge += hurtInfo.Damage;
			}
			if (artifactGuilt)
			{
				artifactGuiltCharge += hurtInfo.Damage;
			}
		}

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (TerraMe)
            {
                Projectile.NewProjectile(Player.GetSource_OnHurt(hurtInfo.DamageSource), Player.Center, Vector2.Zero, ModContent.ProjectileType<TerraSphere>(), 30, 4, Main.myPlayer, 0, npc.whoAmI);
            }

            if (DragonsGuard || ChaosMe)
            {
                npc.AddBuff(BuffID.OnFire, 120);
            }

            if (artifactJudgement)
            {
                artifactJudgementCharge += hurtInfo.Damage;
            }
            if (artifactGuilt)
            {
                artifactGuiltCharge += hurtInfo.Damage;
            }

            if (fleshrendSet && Main.rand.Next(2) == 0)
            {
                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 position = new Vector2(Player.Center.X - 40, Player.Center.Y - 40);
                        Dust.NewDust(position, 80, 80, DustID.RainCloud, 0f, 0f, 124, new Color(255, 50, 0), 1f);
                    }

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        float dist = npc.Distance(Player.Center);

                        if (target.active && !target.dontTakeDamage && !target.friendly && target.immune[Player.whoAmI] == 0 && dist < 100f)
                        {
                            Player.ApplyDamageToNPC(target, 30, 0, 0, false); // target , damage, knockback, direction, crit
                        }
                    }
                }
            }

            if (ChaosMe)
            {
                npc.AddBuff(ModContent.BuffType<DragonFire>(), 180);
                npc.AddBuff(ModContent.BuffType<HydraToxin>(), 180);
            }

            if (BrokenCode)
            {
                Player.AddBuff(BuffID.Panic, 180);
                Player.immuneTime = Player.longInvince ? 180 : 120;
            }

            if (npc.type == NPCID.GoblinArcher
                || npc.type == NPCID.GoblinPeon
                || npc.type == NPCID.GoblinScout
                || npc.type == NPCID.GoblinSorcerer
                || npc.type == NPCID.GoblinSummoner
                || npc.type == NPCID.GoblinThief
                || npc.type == NPCID.GoblinWarrior
                || npc.type == NPCID.DD2GoblinBomberT1
                || npc.type == NPCID.DD2GoblinBomberT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.DD2GoblinT1
                || npc.type == NPCID.DD2GoblinT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.BoundGoblin
                || npc.type == NPCID.GoblinTinkerer)
            {
                Player.endurance += .8f;
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {
            if (goblinSlayer)
            {
                if (target.type == NPCID.GoblinArcher
                    || target.type == NPCID.GoblinPeon
                    || target.type == NPCID.GoblinScout
                    || target.type == NPCID.GoblinSorcerer
                    || target.type == NPCID.GoblinSummoner
                    || target.type == NPCID.GoblinThief
                    || target.type == NPCID.GoblinWarrior
                    || target.type == NPCID.DD2GoblinBomberT1
                    || target.type == NPCID.DD2GoblinBomberT2
                    || target.type == NPCID.DD2GoblinBomberT3
                    || target.type == NPCID.DD2GoblinT1
                    || target.type == NPCID.DD2GoblinT2
                    || target.type == NPCID.DD2GoblinBomberT3
                    || target.type == NPCID.BoundGoblin
                    || target.type == NPCID.GoblinTinkerer)
                {
                    modifiers.FinalDamage.Flat *= 5;
                    IsGoblin = true;
                }
            }

            if (perfectChaosMe)
            {
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (valkyrieSet)
            {
                target.AddBuff(BuffID.Frostburn, 180);
                target.AddBuff(BuffID.Chilled, 180);
            }

            if (Baolei)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }

            if (Naitokurosu)
            {
                int buff = Main.dayTime ? BuffID.Venom : ModContent.BuffType<AAModClassic.Buffs.Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (Duality)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : ModContent.BuffType<AAModClassic.Buffs.Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (darkmatterSetMe)
            {
                target.AddBuff(ModContent.BuffType<Electrified>(), 500);
            }

            if (kindledSet)
            {
                Player.magmaStone = true;
            }

            if (clawsOfChaos)
            {
                Player.ApplyDamageToNPC(target, 5, 0, 0, false);
            }

            if (DiscordShredder)
            {
                Player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (demonGauntlet)
            {
                int buff = WorldGen.crimson ? BuffID.Ichor : BuffID.CursedInferno;
                target.AddBuff(buff, 180);
            }

            if (HeartP && Player.statLife > (Player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<DragonFire>(), 600);
            }
            else if (HeartP && Player.statLife < (Player.statLifeMax / 3))
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (HeartS && Player.statLife > (Player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<HydraToxin>(), 600);
            }
            else if (HeartS && Player.statLife < (Player.statLifeMax / 3))
            {
                target.AddBuff(ModContent.BuffType<AAModClassic.Buffs.Moonraze>(), 600);
            }

            if (dracoSet)
            {
                target.AddBuff(BuffID.Daybreak, 600);
            }

            if (Alpha && !target.boss)
            {
                target.AddBuff(BuffID.Wet, 600);
            }

            if (Player.HasBuff(ModContent.BuffType<DragonfireFlaskBuff>()))
            {
                target.AddBuff(ModContent.BuffType<DragonFire>(), 900);
            }

            if (Player.HasBuff(ModContent.BuffType<HydratoxinFlaskBuff>()))
            {
                target.AddBuff(ModContent.BuffType<Hydratoxin>(), 900);
            }
            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
            }

            if (ChampionMa)
            {
                if (Main.rand.Next(30) == 0)
                {
                    int i = Item.NewItem(target.GetSource_OnHurt(Player), target.Hitbox, ModContent.ItemType<CarrotBooster>(), 1, false, 0, true);
                    Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                }
            }
        }


        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            if (proj.CountsAsClass(DamageClass.Melee))
            {
                if (perfectChaosMe)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (dracoSet)
                {
                    target.AddBuff(BuffID.Daybreak, 600);
                }

                if (valkyrieSet)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                    target.AddBuff(BuffID.Chilled, 180);
                }

                if (darkmatterSetMe)
                {
                    target.AddBuff(ModContent.BuffType<Electrified>(), 500);
                }

                if (ChaosMe || ChaosMe1)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(Mod.Find<ModBuff>(buffName).Type, 180);
                }

                if (demonGauntlet)
                {
                    int buff = WorldGen.crimson ? BuffID.Ichor : BuffID.CursedInferno;
                    target.AddBuff(buff, 180);
                }

                if (Player.HasBuff(ModContent.BuffType<DragonfireFlaskBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<DragonFire>(), 900);
                }

                if (Player.HasBuff(ModContent.BuffType<HydratoxinFlaskBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<Hydratoxin>(), 900);
                }
            }

            if (proj.CountsAsClass(DamageClass.Ranged))
            {
                if (perfectChaosRa)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (dreadSet)
                {
                    target.AddBuff(ModContent.BuffType<AAModClassic.Buffs.Moonraze>(), 600);
                }

                if (DynaskullSet && Main.rand.Next(4) == 0)
                {
                    target.AddBuff(BuffID.Confused, 180);
                }

                if (depthSet)
                {
                    target.AddBuff(BuffID.Poisoned, 180);
                }

                if (darkmatterSetRa)
                {
                    target.AddBuff(ModContent.BuffType<Electrified>(), 500);
                }

                if (ChaosRa || ChaosRa2)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(Mod.Find<ModBuff>(buffName).Type, 180);
                }
            }

            if (proj.CountsAsClass(DamageClass.Magic))
            {
                if (MoonSet)
                {
                    target.AddBuff(ModContent.BuffType<AAModClassic.Buffs.Moonraze>(), 300);
                }

                if (zeroSet)
                {
                    target.AddBuff(ModContent.BuffType<BrokenArmor>(), 1000);
                }

                if (perfectChaosMa)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (darkmatterSetMa)
                {
                    target.AddBuff(ModContent.BuffType<Electrified>(), 500);
                }

                if (ChaosMa)
                {
                    string buffName = Main.rand.Next(2) == 0 ? "DragonFire" : "HydraToxin";
                    target.AddBuff(Mod.Find<ModBuff>(buffName).Type, 180);
                }

                if (BlackLotusEmblem)
                {
                    target.AddBuff(ModContent.BuffType<Moonraze>(), 180);
                }
            }

            if (proj.minion)
            {
                if (zeroSet1)
                {
                    target.AddBuff(ModContent.BuffType<BrokenArmor>(), 1000);
                }

                if (perfectChaosSu)
                {
                    target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
                }

                if (impSet)
                {
                    target.AddBuff(BuffID.OnFire, 180);
                }

                if (darkmatterSetSu)
                {
                    target.AddBuff(ModContent.BuffType<Electrified>(), 500);
                }
            }

            if (proj.CountsAsClass(DamageClass.Throwing))
            {
                if (darkmatterSetTh)
                {
                    target.AddBuff(ModContent.BuffType<Electrified>(), 500);
                }

                if (Alpha && Main.rand.Next(2) == 0 && !target.boss)
                {
                    target.AddBuff(BuffID.Wet, 500);
                }
            }

            if (ChampionMa)
            {
                if (Main.rand.Next(30) == 0)
                {
                    int i = Item.NewItem(target.GetSource_OnHurt(Player), target.Hitbox, ModContent.ItemType<CarrotBooster>(), 1, false, 0, true);
                    Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                }
            }

            if (Baolei && (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.Magic)))
            {
                int buff = Main.dayTime ? BuffID.Daybreak : BuffID.OnFire;
                target.AddBuff(buff, 1000);
            }

            if (Naitokurosu && (proj.CountsAsClass(DamageClass.Ranged) || proj.minion))
            {
                int buff = Main.dayTime ? BuffID.Venom : ModContent.BuffType<AAModClassic.Buffs.Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (Duality)
            {
                int buff = Main.dayTime ? BuffID.Daybreak : ModContent.BuffType<AAModClassic.Buffs.Moonraze>();
                target.AddBuff(buff, 1000);
            }

            if (clawsOfChaos)
            {
                Player.ApplyDamageToNPC(target, 5, 0, 0, false);
            }

            if (DiscordShredder)
            {
                Player.ApplyDamageToNPC(target, 30, 0, 0, false);
                target.AddBuff(ModContent.BuffType<DiscordInferno>(), 300);
            }

            if (StoneSoldier)
            {
                target.AddBuff(BuffID.Midas, 600);
            }
        }

        #endregion

        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            int liquidType = 0;
            if (attempt.inLava)
                liquidType = 1;
            if (attempt.inHoney)
                liquidType = 2;

            if (Main.rand.Next(100) < (10 + (Player.cratePotion ? 10 : 0)))
            {
                if (liquidType == 0 && Player.ZoneSnow)
                {
                    itemDrop = ModContent.ItemType<IceCrate>();
                }

                if (liquidType == 0 && Player.ZoneDesert)
                {
                    itemDrop = ModContent.ItemType<DesertCrate>();
                }

                if ((liquidType == 0 || liquidType == 1) && Player.GetModPlayer<AAPlayer>().ZoneInferno)
                {
                    itemDrop = ModContent.ItemType<InfernoCrate>();
                }

                if (liquidType == 0 && Player.GetModPlayer<AAPlayer>().ZoneMire)
                {
                    itemDrop = ModContent.ItemType<MireCrate>();
                }

                if (liquidType == 0 && Player.GetModPlayer<AAPlayer>().ZoneVoid)
                {
                    itemDrop = ModContent.ItemType<VoidCrate>();
                }

                if (liquidType == 0 && Player.GetModPlayer<AAPlayer>().ZoneHoard)
                {
                    itemDrop = ItemID.GoldenCrate;
                }

                if (liquidType == 1 && attempt.CanFishInLava && Player.ZoneUnderworldHeight)
                {
                    itemDrop = ModContent.ItemType<HellCrate>();
                }
            }

            if (attempt.questFish == ModContent.ItemType<TriHeadedKoi>() && Player.GetModPlayer<AAPlayer>().ZoneMire && Main.rand.NextBool())
            {
                itemDrop = ModContent.ItemType<TriHeadedKoi>();
            }

            if (attempt.questFish == ModContent.ItemType<Fishmother>() && Player.GetModPlayer<AAPlayer>().ZoneInferno && Main.rand.NextBool())
            {
                itemDrop = ModContent.ItemType<Fishmother>();
            }

            if (attempt.questFish == ModContent.ItemType<GlitchFish>() && Player.GetModPlayer<AAPlayer>().ZoneVoid && Main.rand.NextBool())
            {
                itemDrop = ModContent.ItemType<GlitchFish>();
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if(Main.rand.Next(50) == 0 && Main.hardMode)
                {
                    itemDrop = ModContent.ItemType<ScorchShark>();
                }
                else if(Main.rand.Next(49) == 0)
                {
                    itemDrop = ModContent.ItemType<SharpeningLavaFish>();
                }
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneMire && Main.hardMode)
            {
                if(Main.rand.Next(50) == 0 && Main.hardMode)
                {
                    itemDrop = ModContent.ItemType<SwimmingHydra>();
                }
                else if(Main.rand.Next(49) == 0)
                {
                    itemDrop = ModContent.ItemType<ToxinMonkfish>();
                }
            }

            if ((Main.rand.Next(4096) == 0 && liquidType == 0 && Player.fishingSkill >= 100)|| (Main.rand.Next(2048) == 0 && Player.accFishingLine && Player.accTackleBox))
            {
                itemDrop = ModContent.ItemType<ShinyCharmFish>();
            }
        }

        public int[] Charges = null;
        public int[] Spheres = null;

        public float ShieldScale = 0;
        public float RingRotation = 0;

        public float TimeScale = 0;

        public override void PostUpdate()
        {
            if (!bossactive)
            {
                nohitplayer = true;
            }
            if (Ronin)
            {
                Player.immune = true;
                Player.immuneTime = 60;
            }
            if (olympianWings && Player.dash < 1)
            {
                if (Player.velocity.Y != 0)
                {
                    Player.dash = 2;
                }
                else
                {
                    Player.dash = 0;
                }
            }
			if (artifactJudgementCharge >= 250)
			{
				Player.AddBuff(ModContent.BuffType<EyeOfJudgement>(), 900);
				artifactJudgementCharge = 0;
			}
			if (artifactGuiltCharge >= 250)
			{
				Player.AddBuff(ModContent.BuffType<EyeOfForsaken>(), 900);
				artifactGuiltCharge = 0;
			}
            if (!Greed1 && !Greed2)
            {
                GreedyDamage = 0;
            }
            DarkmatterSet = darkmatterSetMe || darkmatterSetRa || darkmatterSetMa || darkmatterSetSu || darkmatterSetTh;

            if (NPC.AnyNPCs(ModContent.NPCType<AkumaTransition>()))
            {
                int n = BaseAI.GetNPC(Player.Center, ModContent.NPCType<AkumaTransition>(), -1);
                NPC akuma = Main.npc[n];

                if (akuma.ai[0] >= 660)
                {
                    Player.AddBuff(ModContent.BuffType<BlazingPain>(), 2);
                }
            }
            else if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                Player.AddBuff(ModContent.BuffType<BlazingPain>(), 2);
            }

            if (BasePlayer.HasAccessory(Player, ModContent.ItemType<Items.Vanity.HappySunSticker>(), true, true))
            {
                TextureAssets.Sun = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/DemonSun");
                TextureAssets.Sun3 = ModContent.Request<Texture2D>("AAModClassic/Backgrounds/DemonSunEclipse");
            }
            else
            {
                TextureAssets.Sun = ModContent.Request<Texture2D>("Terraria/Images/Sun");;
                TextureAssets.Sun3 = ModContent.Request<Texture2D>("Terraria/Images/Sun3");
            }

            if (Player.ZoneSandstorm && (ZoneInferno || ZoneMire))
            {
                EmitDust();
            }

            #region SagShieldDrawMethod

            if (SagCooldown > 0)
            {
                SagCooldown--;
            }
            else
            {
                SagCooldown = 0;
            }

            if (ShieldUp)
            {
                RingRotation += .05f;
                ShieldScale += .02f;
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
            }
            else
            {
                ShieldScale -= .02f;
                if (ShieldScale <= 0f)
                {
                    ShieldScale = 0f;
                }
            }

            if (ShieldScale > 0f || TimeScale > 0f)
            {
                RingRotation += .05f;
            }

            if (ShieldScale > 0)
            {
                RingRotation += .05f;
            }

            #endregion

            #region AsheFlameDrawMethod

            if (AsheCooldown > 0)
            {
                AsheCooldown--;
            }
            else
            {
                AsheCooldown = 0;
            }

            if (AsheFlame)
            {
                RingRotation += .05f;
                AsheFlameScale += .02f;
                if (AsheFlameScale >= 1f)
                {
                    AsheFlameScale = 1f;
                }
            }
            else
            {
                AsheFlameScale -= .02f;
                if (AsheFlameScale <= 0f)
                {
                    AsheFlameScale = 0f;
                }
            }

            if (AsheFlameScale > 0f)
            {
                RingRotation += .05f;
            }

            #endregion

            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Equinox.DaybringerHead>()) || NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Equinox.NightcrawlerHead>()))
            {
                TimeScale = 0;
            }

            if (Orbiters)
            {
                Spheres = BaseAI.GetProjectiles(Player.Center, ModContent.ProjectileType<FireOrbiter>(), Main.myPlayer, 48);

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<FireOrbiter>()] > 0)
                {
                    Player.GetDamage(DamageClass.Summon) += AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<FireOrbiter>()) * .1f;

                    if (Main.netMode != NetmodeID.Server && Main.LocalPlayer.miscCounter % 3 == 0)
                    {
                        for (int m = 0; m < Spheres.Length; m++)
                        {
                            Projectile projectile = Main.projectile[Spheres[m]];

                            if (projectile != null && projectile.active)
                            {
                                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<AkumaDustLight>());

                                Main.dust[dustID].position += Player.position - Player.oldPosition;
                                Main.dust[dustID].velocity = (Player.Center - projectile.Center) * 0.05f;
                                Main.dust[dustID].alpha = 100;
                                Main.dust[dustID].noGravity = true;
                            }
                        }
                    }
                }
            }

            if (AAWorld.ModContentGenerated || ZoneInferno || ZoneMire || ZoneVoid || Terrarium || ZoneMush)
            {
                AAWorld.ModContentGenerated = true;
                WorldgenReminder = true;
            }

            if (Main.netMode != NetmodeID.Server)
            {
                List<int> yappers = [0, 1, 2, 3, 4, 5, 6, 7, 8];
                if (!NewAAReminder && !ModContent.GetInstance<AAConfigClient>().DisableNewAAReminderMessage && !ModLoader.TryGetMod("AAMod", out _))
                {
                    int yapper = yappers[Main.rand.Next(yappers.Count)];
                    switch (yapper)
                    {
                        case 0:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo1"), new Color(180, 41, 32));
                            break;
                        case 1:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo2"), new Color(45, 46, 70));
                            break;
                        case 2:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo3"), new Color(255, 0, 0));
                            break;
                        case 3:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo4"), new Color(102, 20, 48));
                            break;
                        case 4:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo5"), new Color(72, 78, 117));
                            break;
                        case 5:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo6"), new Color(128, 0, 0));
                            break;
                        case 6:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo7"), new Color(216, 110, 40));
                            break;
                        case 7:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo8"), new Color(43, 46, 61));
                            break;
                        case 8:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.NewAAInfo9"), Color.CadetBlue);
                            break;

                    }
                    yappers.Remove(yapper);
                }

                NewAAReminder = true;

                if (!WorldgenReminder)
                {
                    int yapper = yappers[Main.rand.Next(yappers.Count)];
                    switch (yapper)
                    {
                        case 0:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo1"), new Color(180, 41, 32));
                            break;
                        case 1:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo2"), new Color(45, 46, 70));
                            break;
                        case 2:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo3"), new Color(255, 0, 0));
                            break;
                        case 3:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo4"), new Color(102, 20, 48));
                            break;
                        case 4:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo5"), new Color(72, 78, 117));
                            break;
                        case 5:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo6"), new Color(128, 0, 0));
                            break;
                        case 6:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo7"), new Color(216, 110, 40));
                            break;
                        case 7:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo8"), new Color(43, 46, 61));
                            break;
                        case 8:
                            Main.NewText(Language.GetTextValue("Mods.AAModClassic.Common.WorldgenReminderInfo9"), Color.CadetBlue);
                            break;
                    }

                    WorldgenReminder = true;
                }
            }

            if (RStar)
            {
                Lighting.AddLight((int)(Player.position.X + Player.width / 2) / 16, (int)(Player.position.Y + Player.height / 2) / 16, 1f, 0.95f, 0.8f);
            }

            if (kindledSet || lantern)
            {
                Lighting.AddLight((int)(Player.position.X + Player.width / 2) / 16, (int)(Player.position.Y + Player.height / 2) / 16, AAColor.Lantern.R / 255, AAColor.Lantern.G / 255 * 0.95f, AAColor.Lantern.B / 255 * 0.8f);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<Yamata>()))
            {
                Player.AddBuff(ModContent.BuffType<YamataGravity>(), 10, true);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                Player.AddBuff(ModContent.BuffType<YamataAGravity>(), 10, true);
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneMire || Player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.dayTime && !AAWorld.downedYamata)
                {
                    if (!Player.GetModPlayer<AAPlayer>().FogRemover)
                    {
                        Player.AddBuff(ModContent.BuffType<Clueless>(), 5);
                    }
                }
            }

            if (Terrarium)
            {
                Player.AddBuff(ModContent.BuffType<Terrarium>(), 2);
                Player.AddBuff(BuffID.DryadsWard, 2);
            }

            if (NPC.AnyNPCs(ModContent.NPCType<ZeroProtocol>()))
            {
                if (!Filters.Scene["MoonLordShake"].IsActive())
                {
                    Filters.Scene.Activate("MoonLordShake", Player.position, new object[0]);
                }

                Filters.Scene["MoonLordShake"].GetShader().UseIntensity(1f);
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneInferno || Player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AshCurse)
                {
                    AshRain(Player);
                }
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake || Player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (AAWorld.downedAllAncients && !AAWorld.downedShen)
                {
                    EmberRain(Player);
                }
            }

            if(Player.GetModPlayer<AAPlayer>().Assassin)
            {
                float RandomX = 50f;
                float RandomY = 25f;
                bool flag = Player.itemAnimation > 0;
                if (flag && Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Melee) && Main.rand.Next(200) == 0 && Player.whoAmI == Main.myPlayer)
                {
                    Vector2 SpeedVector = Main.MouseWorld - Player.RotatedRelativePoint(Player.MountedCenter, true);
                    SpeedVector.Normalize();
                    if (SpeedVector.HasNaNs())
                    {
                        SpeedVector = Vector2.UnitX * Player.direction;
                    }
                    SpeedVector *= 15f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(Player.Center.X + Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[1] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    Spwanposition[2] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY, RandomY));
                    int i = 0;
                    while (i < 3)
                    {
                        if(Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(Player.GetSource_ItemUse(Player.inventory[Player.selectedItem]), Spwanposition[i].X, Spwanposition[i].Y, SpeedVector.X, SpeedVector.Y, ModContent.ProjectileType<AssassinDagger>(), (int)(Player.inventory[Player.selectedItem].damage * 1.3), 2f, Player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(SpeedVector.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, ModContent.DustType<AcidDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                        i++;
                    }
                }
            }

            if (BlackLotusEmblem && Player.inventory[Player.selectedItem].mana > 0 && Player.statMana < (int)(Player.inventory[Player.selectedItem].mana * Player.manaCost))
            {
                BlackLotusQuickMana();
            }

            if (Player.controlQuickHeal)
            {
                SpecialQuickHeal();
            }

            if (StripeManSet)
            {
                if(AAMod.ArmorAbilityKey.JustPressed)
                {
                    StripeCrasyLucky = !StripeCrasyLucky;
                }
            }

            if (StripeCrasyLucky || CrasyLucky)
            {
                if(StripeCrasyLucky) StripeCrasyLucky = true;
                Main.rand = new AAFakeRand();
                if(Main.raining)
                {
                    Main.rainTime = 300;
                    Main.maxRaining = .7f;
                }
            }
            else
            {
                StripeCrasyLucky = false;
                Main.rand = new UnifiedRandom();
            }

            if (ZoneVoid)
            {
                Player.gravity = Player.defaultGravity + .1f;
            }

            if (CCBook || CCBookEX)
            {
                float slotscanuse = Player.maxMinions - Player.slotsMinions;
                if (slotscanuse > 1)
                {
                    bool RuneControl = Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>()] > 1;
                    bool RuneControlEX = Player.ownedProjectileCounts[ModContent.ProjectileType<TerraRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<ChaosRune>()] > 1 || Player.ownedProjectileCounts[ModContent.ProjectileType<VoidRune>()] > 1;
                    if (RuneControl || RuneControlEX)
                    {
                        Player.ClearBuff(ModContent.BuffType<CCRune>());
                    }
                    if (Player.FindBuffIndex(ModContent.BuffType<CCRune>()) == -1)
                    {
                        Player.AddBuff(ModContent.BuffType<CCRune>(), 3600, true);
                    }
                    if (CCBook)
                    {
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>()] < 1 && slotscanuse > 1f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.BunnyRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(1), 0, Player.whoAmI, 0f, 0f);
                        }
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>()] < 1 && slotscanuse > 2f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.DiscordRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(50), 4f, Player.whoAmI, 0f, 0f);
                        }
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>()] < 1 && slotscanuse > 3f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<Items.Dev.RuneBook.EnergyRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(100), 2f, Player.whoAmI, 0f, 0f);
                        }
                    }
                    if (CCBookEX)
                    {
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<TerraRune>()] < 1 && slotscanuse > 1f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<TerraRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(1), 0, Player.whoAmI, 0f, 0f);
                        }
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<ChaosRune>()] < 1 && slotscanuse > 2f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<ChaosRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(400), 4f, Player.whoAmI, 0f, 0f);
                        }
                        if (Player.ownedProjectileCounts[ModContent.ProjectileType<VoidRune>()] < 1 && slotscanuse > 3f)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, -1f, ModContent.ProjectileType<VoidRune>(), (int)(Player.GetDamage(DamageClass.Summon)).ApplyTo(800), 2f, Player.whoAmI, 0f, 0f);
                        }
                    }
                }
            }

            if (ChampionMe && AAMod.ArmorAbilityKey.JustPressed && !Player.HasBuff(ModContent.BuffType<Items.Armor.Champion.RageCool>()))
            {
                int BuffLength = 240;
                if (Player.statLife < (int)(Player.statLifeMax2 * .75f))
                {
                    BuffLength = 360;
                }
                if (Player.statLife < (int)(Player.statLifeMax2 * .5f))
                {
                    BuffLength = 480;
                }
                if (Player.statLife < (int)(Player.statLifeMax2 * .25f))
                {
                    BuffLength = 600;
                }
                Player.AddBuff(ModContent.BuffType<RageBuff>(), BuffLength);
                int RageCooldown = BuffLength * 4;
                Player.AddBuff(ModContent.BuffType<Items.Armor.Champion.RageCool>(), RageCooldown);
            }

            if (Player.HasBuff(ModContent.BuffType<RageBuff>()))
            {
                Player.armorEffectDrawShadowLokis = true;
            }

            if (ChampionRa && AAMod.ArmorAbilityKey.JustPressed && !Player.HasBuff(ModContent.BuffType<DroneCool>()) && 
                !AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<RajahDrone>()))
            {
                Vector2 vector2;
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                Projectile.NewProjectile(Player.GetSource_FromThis(), vector2.X, vector2.Y, 0, 0, ModContent.ProjectileType<RajahDrone>(), (int)(Player.GetDamage(DamageClass.Ranged)).ApplyTo(100), 2, Main.myPlayer, 0f, 0f);
            }

            if (TerraSu)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    CrystalMode++;
                    if (CrystalMode > 2)
                    {
                        CrystalMode = 0;
                    }
                }
                if (CrystalMode == 2)
                {
                    Player.lifeRegen += 12;
                    Player.statDefense.FinalMultiplier *= 1.2f;
                    Player.GetDamage(DamageClass.Generic) /= 2;
                }
            }

            if (RoseCooldown > 0)
            {
                RoseCooldown--;
            }

            if (TerraMa && RoseCooldown <= 0)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    RoseCooldown = 600;
                    float playerY = Player.position.Y + Player.height;

                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X - 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraRoseA>(), (int)(Player.GetDamage(DamageClass.Magic)).ApplyTo(50), 4, Main.myPlayer);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X + 64, playerY), new Vector2(0, -10), ModContent.ProjectileType<TerraRoseA>(), (int)(Player.GetDamage(DamageClass.Magic)).ApplyTo(50), 4, Main.myPlayer);
                }
            }
        }

        public void CarrotLevelup()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 22; i++)
                {
                    if (Player.buffType[i] == ModContent.BuffType<CBoost1>() || 
                        Player.buffType[i] == ModContent.BuffType<CBoost2>() ||
                        Player.buffType[i] == ModContent.BuffType<CBoost3>())
                    {
                        Player.DelBuff(i);
                    }
                }
                CarrotBuff = (int)MathHelper.Clamp(CarrotBuff + 1, 0f, 3f);
                Player.AddBuff(Mod.Find<ModBuff>("CBoost" + CarrotBuff).Type, 480, true);
                return;
            }
        }

        public void SpecialQuickHeal()
        {
            if (Player.noItems)
			{
				return;
			}
            Item item = new Item();
            for (int i = 0; i < 58; i++)
			{
                item = Player.inventory[i];
				if (item.type == ModContent.ItemType<RoninPotion>() && ItemLoader.CanUseItem(item, Player))
				{
                    break;
                }
            }
			if (item == null)
			{
				return;
			}
			if (Player.potionDelay > 0 || (Player.statLife == Player.statLifeMax2 && item.type != ModContent.ItemType<RoninPotion>()))
			{
				return;
			}
			SoundEngine.PlaySound(item.UseSound, Player.position);
			if (item.potion)
			{
				if (item.type == ItemID.RestorationPotion)
				{
					Player.potionDelay = Player.restorationDelayTime;
					Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
				}
				else
				{
					Player.potionDelay = Player.potionDelayTime;
					Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
				}
			}
			ItemLoader.UseItem(item, Player);
			Player.statLife += item.healLife;
			Player.statMana += item.healMana;
			if (Player.statLife > Player.statLifeMax2)
			{
				Player.statLife = Player.statLifeMax2;
			}
			if (Player.statMana > Player.statManaMax2)
			{
				Player.statMana = Player.statManaMax2;
			}
			if (item.healLife > 0 && Main.myPlayer == Player.whoAmI)
			{
				Player.HealEffect(item.healLife, true);
			}
			if (item.healMana > 0)
			{
				Player.AddBuff(BuffID.ManaSickness, Player.manaSickTime, true);
				if (Main.myPlayer == Player.whoAmI)
				{
					Player.ManaEffect(item.healMana);
				}
			}
			if (ItemLoader.ConsumeItem(item, Player))
			{
				item.stack--;
			}
			if (item.stack <= 0)
			{
				item.TurnToAir();
			}
			Recipe.FindRecipes();
        }

        public void BlackLotusQuickMana()
		{
			if (Player.noItems)
			{
				return;
			}
			if (Player.statMana == Player.statManaMax2)
			{
				return;
			}
			for (int i = 0; i < 58; i++)
			{
				if (Player.inventory[i].stack > 0 && Player.inventory[i].type > ItemID.None && Player.inventory[i].healMana > 0 && (Player.potionDelay == 0 || !Player.inventory[i].potion) && ItemLoader.CanUseItem(Player.inventory[i], Player))
				{
					SoundEngine.PlaySound(Player.inventory[i].UseSound, Player.position);
					if (Player.inventory[i].potion)
					{
						if (Player.inventory[i].type == ItemID.RestorationPotion)
						{
							Player.potionDelay = Player.restorationDelayTime;
							Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
						}
						else
						{
							Player.potionDelay = Player.potionDelayTime;
							Player.AddBuff(BuffID.PotionSickness, Player.potionDelay, true);
						}
					}
					ItemLoader.UseItem(Player.inventory[i], Player);
					Player.statLife += Player.inventory[i].healLife;
					Player.statMana += Player.inventory[i].healMana;
					if (Player.statLife > Player.statLifeMax2)
					{
						Player.statLife = Player.statLifeMax2;
					}
					if (Player.statMana > Player.statManaMax2)
					{
						Player.statMana = Player.statManaMax2;
					}
					if (Player.inventory[i].healLife > 0 && Main.myPlayer == Player.whoAmI)
					{
						Player.HealEffect(Player.inventory[i].healLife, true);
					}
					if (Player.inventory[i].healMana > 0)
					{
						Player.AddBuff(BuffID.ManaSickness, 60, true);
						if (Main.myPlayer == Player.whoAmI)
						{
							Player.ManaEffect(Player.inventory[i].healMana);
						}
					}
					if (ItemLoader.ConsumeItem(Player.inventory[i], Player))
					{
						Player.inventory[i].stack--;
					}
					if (Player.inventory[i].stack <= 0)
					{
						Player.inventory[i].TurnToAir();
					}
					Recipe.FindRecipes();
					return;
				}
			}
		}

        public override void PostUpdateBuffs()
        {
            if (Player.mount.Active || Player.mount.Cart)
            {
                Player.dashDelay = 60;
                AADash = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            if (Player.mount.Active || Player.mount.Cart)
            {
                Player.dashDelay = 60;
                AADash = 0;
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            float movespeedmax = 1f + MaxMovespeedboost;

            Player.maxRunSpeed *= movespeedmax;
            
            if (Player.pulley && AADash > 0)
            {
                AADashMovement();
            }
            else if (Player.grappling[0] == -1 && !Player.tongued)
            {
                AAHorizontalMovement();
                if (AADash > 0)
                {
                    AADashMovement();
                }
            }
        }
        
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if(item.CountsAsClass(DamageClass.Ranged) && Assassin)
            {
                velocity *= 1.3f;
                if(Main.rand.Next(10) == 0 && Player.whoAmI == Main.myPlayer)
                {
                    float RandomX = 50f;
                    float RandomY = 25f;
                    Vector2[] Spwanposition = new Vector2[3];
                    Spwanposition[0] = new Vector2(Player.Center.X + Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    Spwanposition[1] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    Spwanposition[2] = new Vector2(Player.Center.X - Player.direction * Main.rand.NextFloat(25f, RandomX), Player.Center.Y - Main.rand.NextFloat(-RandomY,RandomY));
                    for (int i = 0; i < 3; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Spwanposition[i].X, Spwanposition[i].Y, velocity.X, velocity.Y, ModContent.ProjectileType<AssassinArrow>(), (int)(item.damage * 1.3), 2f, Player.whoAmI, 0f, 1f);
                        float round = 16f;
                        int k = 0;
                        while (k < round)
                        {
                            Vector2 vector12 = Vector2.UnitX * 0f;
                            vector12 += -Vector2.UnitY.RotatedBy(k * (6.28318548f / round), default) * new Vector2(1f, 4f);
                            vector12 = vector12.RotatedBy(velocity.ToRotation(), default);
                            int Dusti = Dust.NewDust(Spwanposition[i], 0, 0, ModContent.DustType<AcidDust>(), 0f, 0f, 0, default, 1f);
                            Main.dust[Dusti].scale = 1.5f;
                            Main.dust[Dusti].noGravity = true;
                            Main.dust[Dusti].position = Spwanposition[i] + vector12;
                            Main.dust[Dusti].velocity = vector12.SafeNormalize(Vector2.UnitY) * 1f;
                            k++;
                        }
                    }
                }
            }
			return true;
		}

        public void AAHorizontalMovement()
        {
            float runSpeed = (Player.accRunSpeed + Player.maxRunSpeed) / 2f;
            if (Player.controlLeft && Player.velocity.X > -Player.accRunSpeed && Player.dashDelay >= 0)
            {
                if (Player.velocity.X < -runSpeed && Player.velocity.Y == 0f && !Player.mount.Active)
                {
                    if (AADash == 1 && Main.rand.Next(50) == 0)
                    {
                        int dust = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y), Player.width + 8, 4, ModContent.DustType<Feather>(), -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }
            }
            else if (Player.controlRight && Player.velocity.X < Player.accRunSpeed && Player.dashDelay >= 0)
            {
                if (Player.velocity.X > runSpeed && Player.velocity.Y == 0f && !Player.mount.Active)
                {
                    if (AADash == 1 && Main.rand.Next(50) == 0)
                    {
                        int dust = Dust.NewDust(new Vector2(Player.position.X - 4f, Player.position.Y), Player.width + 8, 4, ModContent.DustType<Feather>(), -Player.velocity.X * 0.5f, Player.velocity.Y * 0.5f, 50, default, 1.5f);
                        Main.dust[dust].velocity.X = Main.dust[dust].velocity.X * 0.2f;
                        Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y * 0.2f;
                        Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }
            }
        }

        public void AADashMovement()
        {
            if (Player.dashDelay > 0)
            {
                return;
            }
            if (Player.dashDelay < 0)
            {
                float num7 = 12f;
                float num8 = 0.985f;
                float num9 = Math.Max(Player.accRunSpeed, Player.maxRunSpeed);
                float num10 = 0.94f;
                int num11 = 20;
                if (AADash == 1)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int num12;
                        if (Player.velocity.Y == 0f)
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + Player.height - 4f), Player.width, 8, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                        }
                        else
                        {
                            num12 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y + Player.height / 2 - 8f), Player.width, 16, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                        }
                        Main.dust[num12].velocity *= 0.1f;
                        Main.dust[num12].scale *= 1f + Main.rand.Next(20) * 0.01f;
                        Main.dust[num12].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                    }
                }
                if (AADash > 0)
                {
                    Player.vortexStealthActive = false;
                    if (Player.velocity.X > num7 || Player.velocity.X < -num7)
                    {
                        Player.velocity.X = Player.velocity.X * num8;
                        return;
                    }
                    if (Player.velocity.X > num9 || Player.velocity.X < -num9)
                    {
                        Player.velocity.X = Player.velocity.X * num10;
                        return;
                    }
                    Player.dashDelay = num11;
                    if (Player.velocity.X < 0f)
                    {
                        Player.velocity.X = -num9;
                        return;
                    }
                    if (Player.velocity.X > 0f)
                    {
                        Player.velocity.X = num9;
                        return;
                    }
                }
            }
            else if (AADash > 0 && !Player.mount.Active)
            {
                if (AADash == 1)
                {
                    int direction = 0;
                    bool DashAttempt = false;
                    if (AADashTime > 0)
                    {
                        AADashTime--;
                    }
                    if (AADashTime < 0)
                    {
                        AADashTime++;
                    }
                    if (Player.controlRight && Player.releaseRight && Player.velocity.Y != 0)
                    {
                        if (AADashTime > 0)
                        {
                            direction = 1;
                            DashAttempt = true;
                            AADashTime = 0;
                        }
                        else
                        {
                            AADashTime = 15;
                        }
                    }
                    else if (Player.controlLeft && Player.releaseLeft && Player.velocity.Y != 0)
                    {
                        if (AADashTime < 0)
                        {
                            direction = -1;
                            DashAttempt = true;
                            AADashTime = 0;
                        }
                        else
                        {
                            AADashTime = -15;
                        }
                    }
                    if (DashAttempt)
                    {
                        Player.velocity.X = 14.5f * direction;
                        Point point = (Player.Center + new Vector2(direction * Player.width / 2 + 2, Player.gravDir * -Player.height / 2f + Player.gravDir * 2f)).ToTileCoordinates();
                        Point point2 = (Player.Center + new Vector2(direction * Player.width / 2 + 2, 0f)).ToTileCoordinates();
                        if (WorldGen.SolidOrSlopedTile(point.X, point.Y) || WorldGen.SolidOrSlopedTile(point2.X, point2.Y))
                        {
                            Player.velocity.X = Player.velocity.X / 2f;
                        }
                        Player.dashDelay = -1;
                        for (int num17 = 0; num17 < 2; num17++)
                        {
                            int num18 = Dust.NewDust(new Vector2(Player.position.X, Player.position.Y), Player.width, Player.height, ModContent.DustType<Feather>(), 0f, 0f, 100, default, 1);
                            Dust expr_CDB_cp_0 = Main.dust[num18];
                            expr_CDB_cp_0.position.X += Main.rand.Next(-5, 6);
                            Dust expr_D02_cp_0 = Main.dust[num18];
                            expr_D02_cp_0.position.Y += Main.rand.Next(-5, 6);
                            Main.dust[num18].velocity *= 0.2f;
                            Main.dust[num18].scale *= .1f + Main.rand.Next(20) * 0.01f;
                            Main.dust[num18].shader = GameShaders.Armor.GetSecondaryShader(Player.cWings, Player);
                        }
                        return;
                    }
                }
            }
        }

        #region Dust Effects

        public static void EmitDust()
        {
            if (Main.gamePaused)
            {
                return;
            }

            int sandTiles = Main.SceneMetrics.SandTileCount;
            Player player = Main.LocalPlayer;
            bool flag = Sandstorm.Happening && player.ZoneSandstorm && (Main.bgStyle == 2 || Main.bgStyle == 5) && Main.bgDelay < 50;
            Sandstorm.HandleEffectAndSky(flag && Main.UseStormEffects);

            if (sandTiles < 100 || player.position.Y > Main.worldSurface * 16.0 || player.ZoneBeach)
            {
                return;
            }

            if (!flag)
            {
                return;
            }

            int maxValue = 1;
            if (Main.rand.Next(maxValue) != 0)
            {
                return;
            }

            int num = Math.Sign(Main.WindForVisuals);
            float num2 = Math.Abs(Main.WindForVisuals);
            if (num2 < 0.01f)
            {
                return;
            }

            float num3 = num * MathHelper.Lerp(0.9f, 1f, num2);
            float num4 = 2000f / sandTiles;
            float num5 = 3f / num4;
            num5 = MathHelper.Clamp(num5, 0.77f, 1f);
            int num6 = (int)num4;
            float num7 = Main.screenWidth / (float)Main.maxScreenW;
            int num8 = (int)(1000f * num7);
            float num9 = 20f * Sandstorm.Severity;
            float num10 = num8 * (Main.gfxQuality * 0.5f + 0.5f) + num8 * 0.1f - Dust.SandStormCount;
            if (num10 <= 0f)
            {
                return;
            }

            float num11 = Main.screenWidth + 1000f;
            float num12 = Main.screenHeight;
            Vector2 value = Main.screenPosition + player.velocity;

            WeightedRandom<Color> weightedRandom = new WeightedRandom<Color>();
            //TODO: Figure out wtf this was doing
            weightedRandom.Add(new Color(200, 160, 20, 180), 1);// Main.screenTileCounts[53] + Main.screenTileCounts[396] + Main.screenTileCounts[397]);
            weightedRandom.Add(new Color(103, 98, 122, 180), 1);// Main.screenTileCounts[112] + Main.screenTileCounts[400] + Main.screenTileCounts[398]);
            weightedRandom.Add(new Color(135, 43, 34, 180), 1);// Main.screenTileCounts[234] + Main.screenTileCounts[401] + Main.screenTileCounts[399]);
            weightedRandom.Add(new Color(213, 196, 197, 180), 1);// Main.screenTileCounts[116] + Main.screenTileCounts[403] + Main.screenTileCounts[402]);

            float num13 = MathHelper.Lerp(0.2f, 0.35f, Sandstorm.Severity);
            float num14 = MathHelper.Lerp(0.5f, 0.7f, Sandstorm.Severity);
            int num15 = 0;

            while (num15 < num9)
            {
                if (Main.rand.Next(num6 / 4) == 0)
                {
                    Vector2 vector = new Vector2(Main.rand.NextFloat() * num11 - 500f, Main.rand.NextFloat() * -50f);

                    if (Main.rand.Next(3) == 0 && num == 1)
                    {
                        vector.X = Main.rand.Next(500) - 500;
                    }
                    else if (Main.rand.Next(3) == 0 && num == -1)
                    {
                        vector.X = Main.rand.Next(500) + Main.screenWidth;
                    }

                    if (vector.X < 0f || vector.X > Main.screenWidth)
                    {
                        vector.Y += Main.rand.NextFloat() * num12 * 0.9f;
                    }

                    vector += value;

                    int num16 = (int)vector.X / 16;
                    int num17 = (int)vector.Y / 16;

                    if (Main.tile[num16, num17] != null && Main.tile[num16, num17].WallType == WallID.None)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(vector, 10, 10, DustID.Sandstorm, 0f, 0f, 0)];
                            dust.velocity.Y = 2f + Main.rand.NextFloat() * 0.2f;

                            Dust expr_460_cp_0 = dust;
                            expr_460_cp_0.velocity.Y *= dust.scale;

                            Dust expr_47A_cp_0 = dust;
                            expr_47A_cp_0.velocity.Y *= 0.35f;

                            dust.velocity.X = num3 * 5f + Main.rand.NextFloat() * 1f;

                            Dust expr_4B7_cp_0 = dust;
                            expr_4B7_cp_0.velocity.X += num3 * num14 * 20f;

                            dust.fadeIn += num14 * 0.2f;
                            dust.velocity *= 1f + num13 * 0.5f;
                            dust.color = weightedRandom;
                            dust.velocity *= 1f + num13;
                            dust.velocity *= num5;
                            dust.scale = 0.9f;

                            num10 -= 1f;
                            if (num10 <= 0f)
                            {
                                break;
                            }
                        }

                        if (num10 <= 0f)
                        {
                            return;
                        }
                    }
                }

                num15++;
            }
        }

        public static void AshRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }

            if ((player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda) && player.GetModPlayer<AAPlayer>().AshCurse)
            {
                if (!player.GetModPlayer<AAPlayer>().AshRemover || !(player.ZoneSkyHeight || player.ZoneOverworldHeight))
                {
                    player.AddBuff(ModContent.BuffType<BurningAsh>(), 5);
                }

                if (AAWorld.infernoTiles > 0 && Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
                {
                    int maxValue = 800 / AAWorld.infernoTiles;
                    float num = Main.screenWidth / (float)Main.maxScreenW;
                    int num2 = (int)(500f * num);
                    num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                    float num3 = 1f + 50f * Main.cloudAlpha;
                    int num4 = 0;

                    while (num4 < num3)
                    {
                        try
                        {
                            if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                            {
                                break;
                            }

                            if (Main.rand.Next(maxValue) == 0)
                            {
                                int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                                int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                                if (Main.LocalPlayer.velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.LocalPlayer.velocity.Y;
                                }

                                if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) - 500;
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) + Main.screenWidth;
                                }

                                if (num5 < 0 || num5 > Main.screenWidth)
                                {
                                    num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                                }

                                num5 += (int)Main.screenPosition.X;

                                int num7 = num5 / 16;
                                int num8 = num6 / 16;

                                if (Main.tile[num7, num8] != null && Main.tile[num7, num8].WallType == WallID.None)
                                {
                                    int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Dusts.AshRain>(), 0f, 0f, 0);
                                    Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                                    Dust expr_292_cp_0 = Main.dust[dust];
                                    expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                                    if (!player.GetModPlayer<AAPlayer>().AshCurse)
                                    {
                                        Main.dust[dust].velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                        Dust expr_2EC_cp_0 = Main.dust[dust];
                                        expr_2EC_cp_0.velocity.X += Main.WindForVisuals * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                                        Dust expr_370_cp_0 = Main.dust[dust];
                                        expr_370_cp_0.velocity.Y *= 0.5f;
                                    }

                                    Dust expr_38E_cp_0 = Main.dust[dust];
                                    expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                                    Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                        catch
                        {
                        }

                        num4++;
                    }
                }
            }
        }

        public static void EmberRain(Player player)
        {
            if (Main.gamePaused)
            {
                return;
            }

            if ((player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda || player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake) && AAWorld.downedAllAncients && !AAWorld.downedShen)
            {
                if (Main.LocalPlayer.position.Y < Main.worldSurface * 16.0)
                {
                    int maxValue = 8;
                    float num = Main.screenWidth / (float)Main.maxScreenW;
                    int num2 = (int)(500f * num);
                    num2 = (int)(num2 * (1f + 2f * Main.cloudAlpha));
                    float num3 = 1f + 50f * Main.cloudAlpha;
                    int num4 = 0;

                    while (num4 < num3)
                    {
                        try
                        {
                            if (Ashes >= num2 * (Main.gfxQuality / 2f + 0.5f) + num2 * 0.1f)
                            {
                                break;
                            }

                            if (Main.rand.Next(maxValue) == 0)
                            {
                                int num5 = Main.rand.Next(Main.screenWidth + 1000) - 500;
                                int num6 = (int)Main.screenPosition.Y - Main.rand.Next(50);

                                if (Main.LocalPlayer.velocity.Y > 0f)
                                {
                                    num6 -= (int)Main.LocalPlayer.velocity.Y;
                                }

                                if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) - 500;
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    num5 = Main.rand.Next(500) + Main.screenWidth;
                                }

                                if (num5 < 0 || num5 > Main.screenWidth)
                                {
                                    num6 += Main.rand.Next((int)(Main.screenHeight * 0.8)) + (int)(Main.screenHeight * 0.1);
                                }

                                num5 += (int)Main.screenPosition.X;

                                int num7 = num5 / 16;
                                int num8 = num6 / 16;

                                if (Main.tile[num7, num8] != null && Main.tile[num7, num8].WallType == WallID.None)
                                {
                                    int dust = Dust.NewDust(new Vector2(num5, num6), 10, 10, ModContent.DustType<Discord>(), 0f, 0f, 0);
                                    Main.dust[dust].velocity.Y = 3f + Main.rand.Next(30) * 0.1f;

                                    Dust expr_292_cp_0 = Main.dust[dust];
                                    expr_292_cp_0.velocity.Y *= Main.dust[dust].scale;

                                    if (!player.GetModPlayer<AAPlayer>().AshCurse)
                                    {
                                        Main.dust[dust].velocity.X = Main.rand.Next(-10, 10) * 0.1f;

                                        Dust expr_2EC_cp_0 = Main.dust[dust];
                                        expr_2EC_cp_0.velocity.X += Main.WindForVisuals * Main.cloudAlpha * 10f;
                                    }
                                    else
                                    {
                                        Main.dust[dust].velocity.X = (Main.cloudAlpha + 0.5f) * 25f + Main.rand.NextFloat() * 0.2f - 0.1f;

                                        Dust expr_370_cp_0 = Main.dust[dust];
                                        expr_370_cp_0.velocity.Y *= 0.5f;
                                    }

                                    Dust expr_38E_cp_0 = Main.dust[dust];
                                    expr_38E_cp_0.velocity.Y *= 1f + 0.3f * Main.cloudAlpha;

                                    Main.dust[dust].scale += Main.cloudAlpha * 0.2f;
                                    Main.dust[dust].velocity *= 1f + Main.cloudAlpha * 0.5f;
                                }
                            }
                        }
                        catch
                        {
                        }

                        num4++;
                    }
                }
            }
        }

        #endregion

        #region Dev Armor

        public void DropDevArmor(int dropType)
        {
            //0 = Pre-HM
            //1 = HM
            //2 = Post-Plant
            //3 = PML
            //4 = PA
            string addonEX = dropType == 4 ? "EX" : ""; //only include EX if it's a dropType 3 (ie from ancients)

            bool spawnedDevItems = false; //this prevents it from not dropping anything if the chance lands on something it cannot drop yet (for prehm/hm) as by this point it's past the 10% chance and thus should drop.
            while (!spawnedDevItems)
            {
                int choice = Main.rand.Next(40);

                switch (choice)
                {
                    case 0:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MagiciansHat>());

                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("Prismeow" + addonEX).Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 1:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Alphakip.AlphaBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("AmphibianLongsword" + addonEX).Type);
                        }

                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<AlphakipTerratool>());
                        }

                        spawnedDevItems = true;
                        break;

                    case 2:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Beg.BegBag>());

                        if (dropType >= 1)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<MonochromeApple>());
                        }

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("PoniumStaff" + addonEX).Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 3:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Maskano.MaskBag>());

                        spawnedDevItems = true;
                        break;

                    case 4:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Charlie.CharlieBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SoulSiphon>());
                        }
                        break;

                    case 5:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TailsHead>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TailsBody>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<TailsLegs>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>(dropType == 4 ? "FreedomStar" : "MobianBuster").Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 6:
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<SkrallStaff>());
                            spawnedDevItems = true;
                        }

                        break;

                    case 7:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<DellyBag>());

                        break;

                    case 8:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<FezLordsBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>(dropType == 4 ? "Chronos" : "TimeTeller").Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 9:
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("TitanAxe" + addonEX).Type);
                            spawnedDevItems = true;
                        }

                        break;

                    case 10:
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EnderStaff" + addonEX).Type);
                            spawnedDevItems = true;
                        }

                        break;

                    case 12:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<AvesBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("DuckstepGun" + addonEX).Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 13:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Tied.OldMagiciansHat>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>(dropType == 4 ? "GentlemansLongblade" : "GentlemansRapier").Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 14:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Moon.MoonBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("Etheral" + addonEX).Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 15:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Grox.GroviteSeaChest>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>(dropType == 4 ? "SoccStaff" : "SockStaff").Type);
                        }

                        if (dropType >= 4)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GroviteTerratool>());
                        }

                        spawnedDevItems = true;
                        break;

                    case 16:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.CC.CCBox>());

                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CCRuneBookPage>());
                        }

                        spawnedDevItems = true;
                        break;

                    case 17:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Gibs.GibsBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Main.rand.Next(2) == 0 ? ModContent.ItemType<Skullshot>() : ModContent.ItemType<GibsFemur>());
                        }

                        spawnedDevItems = true;
                        break;

                    case 18:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<ApawnEgg>());
                        spawnedDevItems = true;
                        break;

                    case 19:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CursedHood>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CursedRobe>());
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<CursedPants>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("CursedSickle" + addonEX).Type);
                        }

                        spawnedDevItems = true;
                        break;

                    case 20:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Mikpin.MikBag>());

                        spawnedDevItems = true;
                        break;

                    case 21:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Fargo.TopHat>());

                        if (dropType >= 3)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MagicAcorn" + addonEX).Type);
                            }
                            else
                            {
                                Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Placeholder>());
                            }
                        }

                        spawnedDevItems = true;
                        break;

                    case 22:

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Blazen.BlazenBag>());

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ThunderLord" + addonEX).Type);
                        }
                        spawnedDevItems = true;
                        break;

                    case 23:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ItemID.ReaperHood);
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ItemID.ReaperRobe);

                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("GrimReaperScythe" + addonEX).Type);
                        }
                        spawnedDevItems = true;
                        break;

                    case 24:
                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<UmbralReaper>());
                        }
                        spawnedDevItems = true;
                        break;

                    case 25:
                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("FuryForger" + addonEX).Type);
                        }
                        spawnedDevItems = true;
                        break;

                    case 26:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Cerberus.InvokerBag>());
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<InvokerStaff>());
                        }
                        spawnedDevItems = true;
                        break;

                    case 27:
                        if (dropType >= 2)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<GameRaider>());
                        }
                        spawnedDevItems = true;
                        break;
                    case 28:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Pluto.PlutoBag>());
                        break;
                    case 29:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.VoidEye.VoidBag>());
                        break;
                    case 30:
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Anarchy.AnarchyBag>());
                        break;
                    case 31:
                        if (dropType >= 3)
                        {
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("UmbreonSP" + addonEX).Type);
                        }
                        break;
                    case 32:
                            Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), ModContent.ItemType<Items.Vanity.Shox.ShoxBag>());
                        break;

                    default:
                        spawnedDevItems = false;
                        break;
                }
            }
        }

        public void PHMDevArmor()
        {
            DropDevArmor(0);
        }

        public void HMDevArmor()
        {
            DropDevArmor(1);
        }

        public void PPDevArmor()
        {
            DropDevArmor(2);
        }

        public void PMLDevArmor()
        {
            DropDevArmor(3);
        }

        public void SADevArmor()
        {
            DropDevArmor(4);
        }

        #endregion

        public override void PreUpdate()
        {
            groviteGlow[Player.whoAmI] = false;

            if (Player.GetModPlayer<AAPlayer>().ZoneVoid || Player.GetModPlayer<AAPlayer>().ZoneInferno || Player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
            {
                if (Main.raining)
                {
                    Main.rainTime = 0;
                    Main.raining = false;
                    Main.maxRaining = 0f;
                }
            }

            if (Player.GetModPlayer<AAPlayer>().ZoneMire || Player.GetModPlayer<AAPlayer>().ZoneRisingMoonLake)
            {
                if (Main.raining)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        Main.rainTime++;
                    }
                }
            }
        }

        public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
        {
            if (demonGauntlet)
            {
                if (item.CountsAsClass(DamageClass.Melee))
                {
                    knockback += 2f;
                }
            }

            if (IsGoblin)
            {
                knockback += 5f;
            }
        }
 
        public override float UseTimeMultiplier(Item item)
        {
            float multiplier = 1f;

            if (item.damage > 0)
            {
                if (HydraPendant)
                {
                    multiplier *= 1.15f;
                }

                while (item.useTime / multiplier < 1)
                {
                    multiplier -= .1f;
                }

                while (item.useAnimation / multiplier < 2)
                {
                    multiplier -= .1f;
                }
            }

            return multiplier;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AAMod.Rift.JustPressed)
            {
                RiftPos = Player.position;
                for (int m = 0; m < 58; m++)
                {
                    if (Player.inventory[m].type == ModContent.ItemType<RiftMirror>())
                    {
                        SoundEngine.PlaySound(SoundID.Item6, Player.position);
                        Player.Spawn(PlayerSpawnContext.RecallFromItem);
                    }
                }
            }

            if (AAMod.RiftReturn.JustPressed && RiftPos != new Vector2(0, 0))
            {
                for (int m = 0; m < 58; m++)
                {
                    if (Player.inventory[m].type == ModContent.ItemType<RiftMirror>())
                    {
                        SoundEngine.PlaySound(SoundID.Item6, Player.position);
                        LeaveDust(Player);
                        Player.velocity = Vector2.Zero;
                        Player.position = RiftPos;
                    }
                }
            }

            if (SagShield)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed && SagCooldown == 0)
                {
                    Player.AddBuff(ModContent.BuffType<SagShield>(), 300);
                    SagCooldown = 5400;
                }
            }

            if (Witch)
            {
                if (AAMod.ArmorAbilityKey.JustPressed && AsheCooldown == 0)
                {
                    SoundEngine.PlaySound(SoundID.Zombie104, Player.position);
                    if(Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Magic) || Player.inventory[Player.selectedItem].CountsAsClass(DamageClass.Summon))
                    {
                        for(int i = 0; i < 8; i++)
                        {
                            Vector2 shoot = new Vector2((float)Math.Sin(i * 0.25f * 3.1415926f), (float)Math.Cos(i * 0.25f * 3.1415926f));
                            shoot *= 8f;
                            int id = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, shoot.X, shoot.Y, ModContent.ProjectileType<AsheFire>(), Player.inventory[Player.selectedItem].damage, 5, Main.myPlayer, 0f, 1f);
                            Main.projectile[id].DamageType = DamageClass.Magic;
                            Main.projectile[id].hostile = false;
                            Main.projectile[id].friendly = true;
                        }
                    }
                    Player.AddBuff(ModContent.BuffType<AsheFlame>(), 900);
                    AsheCooldown = 5400;
                }
            }

            if (OldOneCharm)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed && DD2Event.Ongoing && DD2Event.TimeLeftBetweenWaves > 0)
                {
                    DD2Event.TimeLeftBetweenWaves = 60;
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        AANet.SendNetMessage(AANet.DD2EventTime, (byte)DD2Event.TimeLeftBetweenWaves);
                    }
                }
            }

            if (ChaosRa2)
            {
                if (AAMod.ArmorAbilityKey.JustPressed && AbilityCD == 0)
                {
                    AbilityCD = 180;

                    int damage = 70;
                    float knockback = 1;

                    Vector2 vector2 = Player.RotatedRelativePoint(Player.MountedCenter, true);
                    float speedX = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float speedY = Main.mouseY + Main.screenPosition.Y - vector2.Y;

                    if (Player.gravDir == -1f)
                    {
                        speedY = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
                    }

                    if ((float.IsNaN(speedX) && float.IsNaN(speedY)) || (speedX == 0f && speedY == 0f))
                    {
                        speedX = Player.direction;
                        speedY = 0f;
                    }

                    Vector2 velocity = new Vector2(speedX, speedY);

                    vector2.X = Main.mouseX + Main.screenPosition.X;
                    vector2.Y = Main.mouseY + Main.screenPosition.Y;

                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, velocity.X, velocity.Y, ModContent.ProjectileType<DragonShot>(), damage, knockback, Main.myPlayer, 0f, 0f);
                }
            }

            if (AbilityCD != 0)
            {
                AbilityCD--;
            }
        }

        private static void LeaveDust(Player player)
        {
            for (int index = 0; index < 70; ++index)
                Main.dust[Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 150, Color.Cyan, 1.2f)].velocity *= 0.5f;
            Main.TeleportEffect(player.getRect(), 1);
            Main.TeleportEffect(player.getRect(), 3);
        }

        public bool ShinyCheck()
        {
            for (int i = 0; i < 58; i++)
            {
                Item item = Player.inventory[i];
                if (item.type == ModContent.ItemType<ShinyCharm>())
                {
                    if (Main.rand.Next(2048) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            if (Main.rand.Next(4096) == 0)
                return true;

            return false;
        }
        public int IZHoldTimer = 180;
        public bool InfZ = false;
        public int GetIZHealth = 2500000;
        public int EscapeLine = 180;
        public int RiftTimer;
        public int RiftDamage = 10;

        public int AARegenCount = 0;

        public override void UpdateLifeRegen()
        {
            if (SagShield)
            {
                if (Player.lifeRegen < 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen += 2;
            }

            if (TerraMe)
            {
                AARegenCount++;
                while (AARegenCount >= 100)
                {
                    AARegenCount -= 100;
                    if (Player.statLife < Player.statLifeMax2)
                    {
                        Player.statLife += 2;
                        for (int i = 0; i < 10; i++)
                        {
                            int num6 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Terra, 0f, 0f, 175, default, 1.75f);
                            Main.dust[num6].noGravity = true;
                            Main.dust[num6].velocity *= 0.75f;
                            int num7 = Main.rand.Next(-40, 41);
                            int num8 = Main.rand.Next(-40, 41);
                            Dust expr_7EE_cp_0 = Main.dust[num6];
                            expr_7EE_cp_0.position.X = expr_7EE_cp_0.position.X + num7;
                            Dust expr_80A_cp_0 = Main.dust[num6];
                            expr_80A_cp_0.position.Y = expr_80A_cp_0.position.Y + num8;
                            Main.dust[num6].velocity.X = -num7 * 0.075f;
                            Main.dust[num6].velocity.Y = -num8 * 0.075f;
                        }
                    }
                }
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Spear)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 2;
            }

            if (infinityOverload)
            {
                Player.lifeRegen -= 60;
            }

            if (YamataGravity || YamataAGravity)
            {
                if (Player.mount.CanFly())
                {
                    Player.mount.Dismount(Player);
                }

                Player.wingTimeMax /= 2;
                if (Player.wingTime > Player.wingTimeMax)
                    Player.wingTimeMax = Player.wingTimeMax;

                if (YamataAGravity)
                {
                    Player.moveSpeed *= .58f;
                }
            }

            if (FFlames)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 40 * (Player.statLife / Player.statLifeMax2);
            }


            if (CursedHellfire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 30;
            }

            if (shroomed && Player.velocity.Y == 0)
            {
                Player.velocity.X *= .8f;
            }

            if (Hunted)
            {
                if (Player.rocketTimeMax > 30)
                {
                    Player.wingTimeMax = 30;
                }

                if (Player.accRunSpeed > 3f)
                {
                    Player.accRunSpeed = 3f;
                }

                Player.wingTimeMax /= 2;
                if (Player.wingTimeMax <= 0)
                {
                    Player.wingTimeMax = 0;
                }
            }

            if (terraBlaze)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 16;
            }

            if (dragonFire)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 8;
                
            }

            if (riftbent)
            {
                RiftTimer++;
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;

                if (RiftTimer >= 120)
                {
                    RiftDamage += 10;
                    RiftTimer = 0;
                }

                if (RiftDamage >= 80)
                {
                    RiftDamage = 80;
                }

                Player.lifeRegen -= RiftDamage;
            }
            else
            {
                RiftDamage = 10;
                RiftTimer = 0;
            }

            if (hydraToxin)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegen -= Math.Abs((int)Player.velocity.X);
            }


            if (discordInferno)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;
                Player.lifeRegen -= Math.Abs((int)Player.velocity.X) + 4;
                Player.GetDamage(DamageClass.Generic) *= 0.8f;
            }

            if (AkumaPain)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }

                Player.lifeRegenTime = 0;

                if ((Player.onFire || Player.frostBurn || Player.onFire2 || dragonFire || discordInferno) && Player.lifeRegen < 0)
                {
                    Player.lifeRegen *= 2;
                }
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
		{
            if (bossactive)
            {
                nohitplayer = false;
            }
            if (ShieldUp)
			{
                modifiers.Cancel();
                return;
			}
            if (Ronin)
            {
                modifiers.Cancel();
                return;
            }
            if (AncientGoldSet)
            {
                long num = 0;
                for (int i = 0; i < 54; i++)
                {
                    if (Player.inventory[i].type == ItemID.CopperCoin)
                    {
                        num += Player.inventory[i].stack;
                    }
                    if (Player.inventory[i].type == ItemID.SilverCoin)
                    {
                        num += Player.inventory[i].stack * 100;
                    }
                    if (Player.inventory[i].type == ItemID.GoldCoin)
                    {
                        num += Player.inventory[i].stack * 10000;
                    }
                    if (Player.inventory[i].type == ItemID.PlatinumCoin)
                    {
                        num += Player.inventory[i].stack * 1000000;
                    }
                }
                if(num >= modifiers.FinalDamage.Flat * 10000)
                {
                    for (int i = 0; i < 54; i++)
                    {
                        if (Player.inventory[i].type == ItemID.CopperCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.SilverCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.GoldCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                        if (Player.inventory[i].type == ItemID.PlatinumCoin)
                        {
                            Player.inventory[i].stack = 0;
                            Player.inventory[i].TurnToAir();
                        }
                    }
                    modifiers.Cancel();
                    return;
                }
            }
        }

        public override void UpdateDead()
        {
            infinityOverload = false;
            discordInferno = false;
            dragonFire = false;
            hydraToxin = false;
            terraBlaze = false;
            Yanked = false;
            InfinityScorch = false;
            LockedOn = false;
            shroomed = false;
            riftbent = false;
            DestinedToDie = false;
            YamataGravity = false;
            YamataAGravity = false;
            Hunted = false;
            Spear = false;
            MaxMovespeedboost = 0;
            spellbookDamage = 1f;
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (demonGauntlet)
            {
                if (Main.rand.NextFloat() < 1f)
                {
                    int ThisDust = 170;
                    if (!WorldGen.crimson)
                    {
                        ThisDust = 75;
                    }

                    Dust dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ThisDust, 0f, 0f, 46)];
                    dust.noGravity = true;
                }
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (FFlames)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<ForsakenDust>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1.5f);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                    r *= 0.1f;
                    g *= 0.7f;
                    b *= 0.1f;
                }
            }

            if (infinityOverload)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadB>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.3f;
                b *= 0.7f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadR>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.7f;
                g *= 0.2f;
                b *= 0.2f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadG>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.1f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadY>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.5f;
                g *= 0.5f;
                b *= 0.1f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadP>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.6f;
                g *= 0.1f;
                b *= 0.6f;

                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<InfinityOverloadO>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.8f;
                g *= 0.5f;
                b *= 0.1f;

                fullBright = true;
            }

            if (terraBlaze)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Terra, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                r *= 0.1f;
                g *= 0.7f;
                b *= 0.2f;

                fullBright = true;
            }

            if (CursedHellfire)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.CursedTorch, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);

                    Main.dust[dust].scale = 3f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;

                    //Main.playerDrawDust.Add(dust);
                }

                fullBright = true;
            }

            if (discordInferno)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Discord>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }
            }

            if (shroomed)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Dusts.ShroomDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 0f, 0.45f);
            }

            if (riftbent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width, Player.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), 0f, -2.5f, 0);

                    Main.dust[dust].alpha = 100;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale += Main.rand.NextFloat();
                }

                Lighting.AddLight((int)(Player.Center.X / 16f), (int)(Player.Center.Y / 16f), 0f, 0f, 0.45f);
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (ammo20percentdown && Main.rand.Next(5) == 0)
            {
                return false;
            }

            return base.CanConsumeAmmo(weapon, ammo);
        }

        #region Highest Damage check

        public static bool MeleeHighest(Player player)
        {
            return player.GetDamage(DamageClass.Melee).Flat > player.GetDamage(DamageClass.Ranged).Flat &&
                player.GetDamage(DamageClass.Melee).Flat > player.GetDamage(DamageClass.Magic).Flat &&
                player.GetDamage(DamageClass.Melee).Flat > player.GetDamage(DamageClass.Summon).Flat &&
                player.GetDamage(DamageClass.Melee).Flat > player.GetDamage(DamageClass.Throwing).Flat;
        }

        public static bool RangedHighest(Player player)
        {
            return player.GetDamage(DamageClass.Ranged).Flat > player.GetDamage(DamageClass.Melee).Flat &&
                player.GetDamage(DamageClass.Ranged).Flat > player.GetDamage(DamageClass.Magic).Flat &&
                player.GetDamage(DamageClass.Ranged).Flat > player.GetDamage(DamageClass.Summon).Flat &&
                player.GetDamage(DamageClass.Ranged).Flat > player.GetDamage(DamageClass.Throwing).Flat;
        }

        public static bool MagicHighest(Player player)
        {
            return player.GetDamage(DamageClass.Magic).Flat > player.GetDamage(DamageClass.Ranged).Flat &&
                player.GetDamage(DamageClass.Magic).Flat > player.GetDamage(DamageClass.Melee).Flat &&
                player.GetDamage(DamageClass.Magic).Flat > player.GetDamage(DamageClass.Summon).Flat &&
                player.GetDamage(DamageClass.Magic).Flat > player.GetDamage(DamageClass.Throwing).Flat;
        }

        public static bool SummonHighest(Player player)
        {
            return player.GetDamage(DamageClass.Summon).Flat > player.GetDamage(DamageClass.Ranged).Flat &&
                player.GetDamage(DamageClass.Summon).Flat > player.GetDamage(DamageClass.Magic).Flat &&
                player.GetDamage(DamageClass.Summon).Flat > player.GetDamage(DamageClass.Melee).Flat &&
                player.GetDamage(DamageClass.Summon).Flat > player.GetDamage(DamageClass.Throwing).Flat;
        }

        public static bool ThrownHighest(Player player)
        {
            return player.GetDamage(DamageClass.Throwing).Flat > player.GetDamage(DamageClass.Ranged).Flat &&
                player.GetDamage(DamageClass.Throwing).Flat > player.GetDamage(DamageClass.Magic).Flat &&
                player.GetDamage(DamageClass.Throwing).Flat > player.GetDamage(DamageClass.Summon).Flat &&
                player.GetDamage(DamageClass.Throwing).Flat > player.GetDamage(DamageClass.Melee).Flat;
        }

        #endregion


        public override void FrameEffects()
        {
            if (onoForceVanity && !onoHideVanity)
            {
                Player.legs = EquipLoader.GetEquipSlot(Mod, "onoLeg", EquipType.Legs);
                Player.body = EquipLoader.GetEquipSlot(Mod, "onoBody", EquipType.Body);
                Player.head = EquipLoader.GetEquipSlot(Mod, "onoHead", EquipType.Head);
            }
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 10; n < 18 + Player.extraAccessorySlots; n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<StripeManShirt>())
                {
                    Player.accWatch = 3;
                    Player.accDepthMeter = 1;
                    Player.accCompass = 1;
                    Player.accFishFinder = true;
                    Player.accWeatherRadio = true;
                    Player.accCalendar = true;
                    Player.accThirdEye = true;
                    Player.accJarOfSouls = true;
                    Player.accCritterGuide = true;
                    Player.accStopwatch = true;
                    Player.accOreFinder = true;
                    Player.accDreamCatcher = true;
                }
                if (item.type == ModContent.ItemType<Items.Vanity.Ohno.Ono>())
                {
                    onoHideVanity = false;
                    onoForceVanity = true;
                }
                if (item.type == ModContent.ItemType<Equinox>())
                {
                    Player.hideWolf = false;
                    Player.forceWerewolf = true;
                    if ((Player.wet && !Player.lavaWet && (!Player.mount.Active || Player.mount.Type != MountID.Slime)) || !Player.forceWerewolf)
                    {
                        Player.hideMerman = false;
                        Player.forceMerman = true;
                    }
                }
            }
        }
    }

    public class MimicSummon : ModPlayer
    {
        int LastChest = 0;

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
                {
                    int x2 = Main.chest[LastChest].x;
                    int y2 = Main.chest[LastChest].y;
                    ChestItemSummonCheck(x2, y2, Mod);
                }
                LastChest = Player.chest;
            }
        }

        public override void UpdateAutopause()
        {
            LastChest = Player.chest;
        }

        public static void ChestItemSummonCheck(int x, int y, Mod mod)
        {
            if (!Main.hardMode || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
            {
                return;
            }

            ushort tileType = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].TileType;
            int tileStyle = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].TileFrameX / 36;

            if (!TileID.Sets.BasicChest[tileType] || tileStyle == 5 || tileStyle == 6)
            {
                return;
            }

            bool hasInfernoKey = false;
            bool hasItems = false;

            for (int i = 0; i < 40; i++)
            {
                if (Main.chest[chestIndex].item[i] == null || Main.chest[chestIndex].item[i].type <= ItemID.None)
                {
                    continue;
                }

                if (hasItems || Main.chest[chestIndex].item[i].stack != 1)
                {
                    return;
                }

                hasItems = true;

                if (Main.chest[chestIndex].item[i].type == ModContent.ItemType<KeyOfSmite>())
                {
                    hasInfernoKey = true;
                }
                else if (Main.chest[chestIndex].item[i].type != ModContent.ItemType<KeyOfSpite>())
                {
                    return;
                }
            }

            if (!hasItems)
            {
                return;
            }

            for (int j = x; j <= x + 1; j++)
            {
                for (int k = y; k <= y + 1; k++)
                {
                    if (TileID.Sets.BasicChest[Main.tile[j, k].TileType])
                    {
                        Main.tile[j, k].ClearTile(); //previously tried setting .HasTile to false directly. Unsure if this is correct substitution
                    }
                }
            }

            for (int l = 0; l < 40; l++)
            {
                Main.chest[chestIndex].item[l] = new Item();
            }

            Chest.DestroyChest(x, y);
            NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y, 0f, chestIndex);
            NetMessage.SendTileSquare(-1, x, y, 3);

            int npcToSpawn = ModContent.NPCType<MireMimic>();
            if (hasInfernoKey)
            {
                npcToSpawn = ModContent.NPCType<InfernoMimic>();
            }

            int npcIndex = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), x * 16 + 16, y * 16 + 32, npcToSpawn);
            Main.npc[npcIndex].whoAmI = npcIndex;
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
            Main.npc[npcIndex].BigMimicSpawnSmoke();
        }
    }

    public class AADrawLayers
    {
        public class glAfterWep : PlayerDrawLayer
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);
            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if(drawInfo.shadow != 0)
                {
                    return;
                }

                Player drawPlayer = drawInfo.drawPlayer;
                Item heldItem = drawPlayer.inventory[drawPlayer.selectedItem];
                BaseAAItem baseAAItem = null;

                if (heldItem.ModItem != null && heldItem.ModItem is BaseAAItem)
                {
                    baseAAItem = (BaseAAItem)heldItem.ModItem;
                }

                if (baseAAItem != null && baseAAItem.glowmaskTexture != null && baseAAItem.glowmaskDrawType != BaseAAItem.GLOWMASKTYPE_NONE)
                {
                    Vector2? offsetNull = baseAAItem.HoldoutOffset();
                    Vector2 offset = (offsetNull != null) ? (Vector2)offsetNull : Vector2.Zero;

                    if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_SWORD)
                    {
                        BaseDrawing.DrawHeldSword(drawInfo, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, null, 1, ModContent.Request<Texture2D>("AAModClassic/" + baseAAItem.glowmaskTexture).Value);
                    }
                    else if (baseAAItem.glowmaskDrawType == BaseAAItem.GLOWMASKTYPE_GUN)
                    {
                        BaseDrawing.DrawHeldGun(drawInfo, 0, drawPlayer, baseAAItem.glowmaskDrawColor, 0f, (int)offset.X, (int)offset.Y, false, false, 0f, 0f, null, 1, ModContent.Request<Texture2D>("AAModClassic/" + baseAAItem.glowmaskTexture).Value);
                    }
                }
            }
        }

        public class glAfterHead : PlayerDrawLayer
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;
                AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

                Vector2 position = drawInfo.Position;
                int dyeHead = drawInfo.cHead;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DracoHelm>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DracoHelm_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomsdayHelmet>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomsdayHelmet_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterVisor>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterVisor_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterHelm>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterHelm_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterHelmet>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterHelmet_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterHeaddress>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterHeaddress_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (!Main.dayTime && modPlayer.DarkmatterSet && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Nightcrawler, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumHat>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumHat_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumHelm>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumHelm_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumHelmet>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumHelmet_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumHeadgear>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumHeadgear_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (Main.dayTime && modPlayer.Radium && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumMask_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<GripMaskRed>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/GripMaskRed_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DaybringerMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DaybringerMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<NightcrawlerMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/NightcrawlerMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                //else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<RetrieverMask>()))
                //{
                //    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/RetrieverMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                //}
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<ZeroMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ZeroMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                //else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<TiedMask>()))
                //{
                //    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/TiedMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.FlashGlow, drawInfo.shadow), drawPlayer.bodyFrame);
                //}
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<ShroomHat>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ShroomHat_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DJDuckHead>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DJDuckHead_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomiteVisor>()) && modPlayer.doomite)
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomiteVisor_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosKabuto>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosKabutoBlue_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosKabuto_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosMask>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosMaskBlue_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosHood>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosHoodBlue_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosHood_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosVisor>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosVisorBlue_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosVisor_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<BlazenHelmet>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/BlazenHelmet_Head").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<GibsSkull>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/GibsSkull_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<CursedHood>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/CursedHood_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<HoodlumHood>()) && drawPlayer.statLife < (drawPlayer.statLifeMax2 / 2))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/HoodlumHood_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<AthenaAMask>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/AthenaAMask_Head_Glow").Value, dyeHead, drawPlayer, position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Flash, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }
        
        public class glAfterShield : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterShield", PlayerDrawLayer.ShieldAcc, delegate (PlayerDrawSet drawInfo)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<TaiyangBaolei>()))
                {
                    string texturePath = Main.dayTime ? "Glowmasks/TaiyangBaoleiA_Shield_Glow" : "Glowmasks/TaiyangBaolei_Shield_Glow";
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/" + texturePath).Value, drawInfo.cShield, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        public class glAfterFace : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterFace", PlayerDrawLayer.FaceAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.FaceAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                //This is fucked up
                /*
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<SoulStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/SoulStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<SpaceStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/SpaceStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<RealityStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/RealityStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<TimeStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/TimeStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PowerStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PowerStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<MindStone>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawPlayer, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/MindStone_Face_Glow").Value, drawInfo.cFace, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.headFrame);
                }
                */
            }
        }

        public class glAfterNeck : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterNeck", PlayerDrawLayer.NeckAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.NeckAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<Naitokurosu>()))
                {
                    string texturePath = Main.dayTime ? "Glowmasks/Naitokurosu_Neck_Glow" : "Glowmasks/NaitokurosuA_Neck_Glow";
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/" + texturePath).Value, drawInfo.cShield, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                } 
            }
        }

        public class glAfterHandOn : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterHandOn", PlayerDrawLayer.HandOnAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.NeckAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DemonGauntlet>()))
                {
                    Texture2D Glow = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DemonGauntlet_HandsOn_Glow").Value;
                    Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                    BaseDrawing.DrawPlayerTexture(drawInfo, Glow, drawInfo.cHandOn, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, drawInfo.shadow), drawPlayer.bodyFrame);
                } 
            }
        }

        public class glAfterHandOff : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterHandOff", PlayerDrawLayer.HandOffAcc, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {  
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DemonGauntlet>()))
                {
                    Texture2D Glow = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DemonGauntlet_HandsOff_Glow").Value;
                    Color GlowColor = WorldGen.crimson ? AAColor.Ichor : AAColor.CursedInferno;

                    BaseDrawing.DrawPlayerTexture(drawInfo, Glow, drawInfo.cHandOff, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(GlowColor, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        public class glAfterBody : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterBody", PlayerDrawLayer.Body, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Torso);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;
                AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DracoPlate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DracoPlate_Body_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomsdayChestplate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomsdayChestplate_Body_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterBreastplate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterBreastplate_Body_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumPlatemail>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumPlatemail_Body").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<ShroomShirt>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ShroomShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DJDuckShirt>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DJDuckShirt_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomiteBreastplate>()) && modPlayer.doomite)
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomiteBreastplate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosPlate>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosPlateBlue_" + (drawPlayer.Male ? "Body" : "FemaleBody")).Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosPlate_" + (drawPlayer.Male ? "Body" : "Female") + "_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<BlazenPlate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/BlazenPlate_" + (drawPlayer.Male ? "Body" : "Female")).Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<CursedRobe>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/CursedRobe_Body_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        public class glAfterArm : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterArm", PlayerDrawLayer.Arms, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;
                AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DracoPlate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DracoPlate_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomsdayChestplate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomsdayChestplate_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterBreastplate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterBreastplate_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumPlatemail>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumPlatemail_Arms").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<ShroomShirt>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ShroomShirt_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomiteBreastplate>()) && modPlayer.doomite)
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomiteBreastplate_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<PerfectChaosPlate>()))
                {
                    if (drawPlayer.direction == 1)
                    {
                        BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/PerfectChaos/PerfectChaosPlateBlue_Arms").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 0, 0f, 0f, drawPlayer.GetImmuneAlphaPure(BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), drawInfo.shadow), drawPlayer.bodyFrame);
                    }
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/PerfectChaosPlate_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Shen3, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<BlazenPlate>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/BlazenPlate_Arms").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<CursedRobe>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/CursedRobe_Arms_Glow").Value, drawInfo.cBody, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.bodyFrame);
                }
            }
        }

        public class glAfterLegs : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterLegs", PlayerDrawLayer.Legs, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Leggings);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;
                AAPlayer modPlayer = drawPlayer.GetModPlayer<AAPlayer>();

                if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DracoLeggings>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DracoLeggings_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 2, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomsdayLeggings>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomsdayLeggings_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (modPlayer.Darkmatter && !Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<DarkmatterGreaves>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DarkmatterGreaves_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (modPlayer.Radium && Main.dayTime && HasAndCanDraw(drawPlayer, ModContent.ItemType<RadiumCuisses>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumCuisses_Legs").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<ShroomPants>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/ShroomPants_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.Glow, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<DoomiteGreaves>()) && modPlayer.doomite)
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/DoomiteGreaves_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.ZeroShield, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<BlazenBoots>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/BlazenBoots_Legs").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.legFrame);
                }
                else if (HasAndCanDraw(drawPlayer, ModContent.ItemType<CursedPants>()))
                {
                    BaseDrawing.DrawPlayerTexture(drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/CursedPants_Legs_Glow").Value, drawInfo.cLegs, drawPlayer, drawInfo.Position, 1, 0f, 0f, drawPlayer.GetImmuneAlphaPure(AAColor.COLOR_WHITEFADE1, drawInfo.shadow), drawPlayer.legFrame);
                }
                /*else if (HasAndCanDraw(drawPlayer, mod.ItemType("ShoxCurse")))
                {
                    if (!drawPlayer.invis && !drawPlayer.mount.Active)
                    {
                        Color color14 = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)(edi.position.X + drawPlayer.width * 0.5) / 16, (int)(edi.position.Y + drawPlayer.height * 0.75) / 16, Color.White), 0f);
                        Texture2D texture2 = ModContent.Request<Texture2D>("AAModClassic/Items/Vanity/Shox/ShoxCurse_Pants");
                        bool flag10 = drawPlayer.legFrame.Y == 0;
                        int num65 = drawPlayer.miscCounter / 3 % 8;
                        if (flag10)
                        {
                            num65 = drawPlayer.miscCounter / 4 % 8;
                        }
                        Rectangle rectangle3 = new Rectangle(18 * flag10.ToInt(), num65 * 26, 16, 24);
                        float num66 = 12f - Main.OffsetsPlayerHeadgear[drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height].Y;
                        Vector2 arg_6147_0 = edi.position + drawPlayer.Size * new Vector2(0.5f, 0.5f + 0.5f * drawPlayer.gravDir);
                        int arg_6135_0 = drawPlayer.direction;
                        Vector2 vector7 = arg_6147_0 + new Vector2(0, -num66 * drawPlayer.gravDir) - Main.screenPosition + drawPlayer.legPosition;
                        vector7 = vector7.Floor();
                        DrawData value = new DrawData(texture2, vector7, new Rectangle?(rectangle3), color14, drawPlayer.legRotation, rectangle3.Size() * new Vector2(0.5f, 0.5f - drawPlayer.gravDir / 2), 1f, SpriteEffects.None, 0);
                        value.shader = edi.legArmorShader;
                        drawInfo.Add(value);
                    }
                }*/

            }
        }

        #region Grovite Layers
        public class glGroviteHead : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteHead", PlayerDrawLayer.Head, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !AAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<AngryPirateHood>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/AngryPirateHood_Head_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cHead, drawPlayer, drawPlayer.bodyFrame, drawPlayer.headRotation, drawPlayer.headPosition, drawInfo.headVect);
                }
            }
        }

        public class glGroviteBody : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteBody", PlayerDrawLayer.Body, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !AAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<AngryPirateCofferplate>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/GroviteCofferplateBodyGlow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cBody, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, drawInfo.bodyVect);
                }
            }
        }

        public class glGroviteLegs : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteLegs", PlayerDrawLayer.Legs, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !AAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && (!drawPlayer.mount.Active || drawPlayer.mount.Type != MountID.Minecart) && HasAndCanDraw(drawPlayer, ModContent.ItemType<AngryPirateBoots>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/AngryPirateBoots_Legs_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cLegs, drawPlayer, drawPlayer.legFrame, drawPlayer.legRotation, drawPlayer.legPosition, drawInfo.legVect);
                }
            }
        }

        public class glGroviteArm : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteArm", PlayerDrawLayer.Arms, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !AAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawInfo.shadow == 0 && HasAndCanDraw(drawPlayer, ModContent.ItemType<AngryPirateCofferplate>()))
                {
                    Texture2D tex = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/AngryPirateCofferplate_Arms_Glow").Value;
                    DrawFlickerTexture(0, drawInfo, drawInfo, tex, drawInfo.cBody, drawPlayer, drawPlayer.bodyFrame, drawPlayer.bodyRotation, drawPlayer.bodyPosition, drawInfo.bodyVect);
                }
            }
        }

        public class glGroviteWings : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glGroviteWings", PlayerDrawLayer.Wings, delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.OffhandAcc);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                if (drawInfo.drawPlayer.merman || drawInfo.drawPlayer.wereWolf || !AAPlayer.groviteGlow[drawInfo.drawPlayer.whoAmI])
                    return;

                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                int accSlot = 0;
                bool social = false;

                if (drawInfo.shadow == 0 && !drawPlayer.mount.Active && HasAndCanDraw(drawPlayer, ModContent.ItemType<AngryPirateSails>(), ref social, ref accSlot))
                {
                    int dye = BaseDrawing.GetDye(drawPlayer, accSlot, social, true);
                    if (dye == -1)
                    {
                        dye = 0;
                    }

                    DrawFlickerTexture(1, drawInfo, drawInfo, ModContent.Request<Texture2D>("AAModClassic/Glowmasks/AngryPirateSails_Wings/Glow").Value, dye, drawPlayer);
                }
            }
        }
        #endregion

        public class glAfterAll : PlayerDrawLayer// = new PlayerDrawLayer("AAMod", "glAfterAll", delegate (PlayerDrawSet edi)
        {
            public override Position GetDefaultPosition() => PlayerDrawLayers.AfterLastVanillaLayer;

            protected override void Draw(ref PlayerDrawSet drawInfo)
            { 
                Mod mod = AAMod.instance;
                Player drawPlayer = drawInfo.drawPlayer;

                if (drawPlayer.mount.Active)
                {
                    return;
                }

                if (drawPlayer.GetModPlayer<AAPlayer>().ShieldScale > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>("AAModClassic/Textures/SagittariusShield").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.ZeroShield, true);

                    Texture2D Ring = ModContent.Request<Texture2D>("AAModClassic/Textures/SagittariusRing").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, Ring, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);

                    Texture2D RingGlow = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/SagittariusFreeRing_Glow").Value;
                    BaseDrawing.DrawTexture(Main.spriteBatch, RingGlow, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().ShieldScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, RingGlow.Width, RingGlow.Height), ColorUtils.COLOR_GLOWPULSE, true);
                }

                if (drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>("AAModClassic/NPCs/Bosses/AH/Ashe/AsheShield").Value;
                    int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, red, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), BaseDrawing.GetLightColor(new Vector2(drawPlayer.position.X, drawPlayer.position.Y)), true);
                }

                int cbuff = drawPlayer.GetModPlayer<AAPlayer>().CarrotBuff;

                if (cbuff > 0)
                {
                    Texture2D Shield = ModContent.Request<Texture2D>("AAModClassic/Textures/CBoost1").Value;
                    if (drawPlayer.HasBuff(ModContent.BuffType<CBoost2>()))
                    {
                        Shield = ModContent.Request<Texture2D>("AAModClassic/Textures/CBoost2").Value;
                    }
                    if (drawPlayer.HasBuff(ModContent.BuffType<CBoost3>()))
                    {
                        Shield = ModContent.Request<Texture2D>("AAModClassic/Textures/CBoost3").Value;
                    }
                    BaseDrawing.DrawTexture(Main.spriteBatch, Shield, 0, drawPlayer.position, drawPlayer.width, drawPlayer.height, drawPlayer.GetModPlayer<AAPlayer>().AsheFlameScale, drawPlayer.GetModPlayer<AAPlayer>().RingRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), Main.DiscoColor, true);
                }
            }
        }

        public static bool HasAndCanDraw(Player player, int type)
        {
            int dum = 0;
            bool dummy = false;

            return HasAndCanDraw(player, type, ref dummy, ref dum);
        }

        public static bool HasAndCanDraw(Player player, int type, ref bool social, ref int slot)
        {
            if (player.wereWolf || player.merman)
            {
                return false;
            }

            ModItem mitem = ItemLoader.GetItem(type);
            if (mitem != null)
            {
                Item item = mitem.Item;
                if (item.headSlot > 0)
                {
                    return BasePlayer.HasHelmet(player, type) && BaseDrawing.ShouldDrawHelmet(player, type);
                }
                else if (item.bodySlot > 0)
                {
                    return BasePlayer.HasChestplate(player, type) && BaseDrawing.ShouldDrawChestplate(player, type);
                }
                else if (item.legSlot > 0)
                {
                    return BasePlayer.HasLeggings(player, type) && BaseDrawing.ShouldDrawLeggings(player, type);
                }
                else if (item.accessory)
                {
                    return BasePlayer.HasAccessory(player, type, true, true, ref social, ref slot) && BaseDrawing.ShouldDrawAccessory(player, type);
                }
            }

            return false;
        }

        public static void DrawFlickerTexture(int drawType, object sb, PlayerDrawSet edi, Texture2D tex, int shader, Player drawPlayer, Rectangle frame = default, float rotation = 0, Vector2 drawPos = default, Vector2 framePos = default)
        {
            if (drawPlayer == null || !drawPlayer.active || drawPlayer.dead)
            {
                return;
            }

            for (int j = 0; j < 7; j++)
            {
                Color color = new Color(110 - j * 10, 110 - j * 10, 110 - j * 10, 110 - j * 10);
                Vector2 vector = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)) * 0.4f;

                if (drawType == 2)
                {
                    BaseDrawing.DrawPlayerTexture(sb, tex, shader, drawPlayer, edi.Position, 1, -6f + vector.X, (drawPlayer.wings > 0 ? 0f : BaseDrawing.GetYOffset(drawPlayer)) + vector.Y, color, frame);
                }
                else
                {
                    bool wings = drawType == 1;
                    if (wings)
                    {
                        rotation = drawPlayer.bodyRotation;
                        frame = new Rectangle(0, TextureAssets.Wings[drawPlayer.wings].Height() / 4 * drawPlayer.wingFrame, TextureAssets.Wings[drawPlayer.wings].Width(), TextureAssets.Wings[drawPlayer.wings].Height() / 4);
                        framePos = new Vector2(TextureAssets.Wings[drawPlayer.wings].Width() / 2, TextureAssets.Wings[drawPlayer.wings].Height() / 8);
                    }

                    Vector2 pos;
                    int x;
                    int y;

                    if (wings)
                    {
                        x = (int)(edi.Position.X - Main.screenPosition.X + drawPlayer.width / 2 - 9 * drawPlayer.direction);
                        y = (int)(edi.Position.Y - Main.screenPosition.Y + drawPlayer.height / 2 + 2f * drawPlayer.gravDir);
                        pos = new Vector2(x, y);
                    }
                    else
                    {
                        x = (int)(edi.Position.X - Main.screenPosition.X - frame.Width / 2 + drawPlayer.width / 2);
                        y = (int)(edi.Position.Y - Main.screenPosition.Y + drawPlayer.height - frame.Height + 4f);
                        pos = new Vector2(x, y);
                    }

                    if (sb is List<DrawData>)
                    {
                        DrawData dd = new DrawData(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.playerEffect, 0)
                        {
                            shader = shader
                        };
                        ((List<DrawData>)sb).Add(dd);
                    }
                    else if (sb is SpriteBatch)
                    {
                        ((SpriteBatch)sb).Draw(tex, pos + drawPos + (wings ? default : framePos) + vector, new Rectangle?(frame), color, rotation, framePos, 1f, edi.playerEffect, 0);
                    }
                }
            }
        }
    }
}