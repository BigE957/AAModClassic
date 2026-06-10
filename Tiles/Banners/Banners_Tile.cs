
using AAModClassic._Content.Acropolis.__Hardmode.NPCs;
using AAModClassic._Content.Chaos.__Hardmode.NPCs;
using AAModClassic._Content.Desert.___PreHardmode.NPCs._Day;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs;
using AAModClassic._Content.Hoard.__Hardmode.NPCs.Scavenger;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs._Surface._Day;
using AAModClassic._Content.Inferno.___PreHardmode.NPCs.Wyrmling;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Surface._Day;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground._Desert;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground._Snow;
using AAModClassic._Content.Inferno.__Hardmode.NPCs._Underground.Wyrm;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs._Surface._Day;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.AncientLung;
using AAModClassic._Content.Madness.___PreHardmode.NPCs;
using AAModClassic._Content.Mire.___PreHardmode.NPCs;
using AAModClassic._Content.Mire.___PreHardmode.NPCs._Surface._Night;
using AAModClassic._Content.Mire.__Hardmode.NPCs;
using AAModClassic._Content.Mire.__Hardmode.NPCs._Surface._Night;
using AAModClassic._Content.Mire.__Hardmode.NPCs._Underground;
using AAModClassic._Content.Mire.__Hardmode.NPCs._Underground._Desert;
using AAModClassic._Content.Mire.__Hardmode.NPCs._Underground._Snow;
using AAModClassic._Content.Mire._PostMoonlord.NPCs;
using AAModClassic._Content.Mire._PostMoonlord.NPCs._Surface._Night;
using AAModClassic._Content.RedMushroom.___PreHardmode.NPCs;
using AAModClassic._Content.Sky.__Hardmode.NPCs;
using AAModClassic._Content.Snow.___PreHardmode.NPCs._Night._SnowSerpent;
using AAModClassic._Content.Stars._PostMoonlord.NPCs._Day;
using AAModClassic._Content.Stars._PostMoonlord.NPCs._Night;
using AAModClassic._Content.Terrarium.___PreHardmode.NPCs;
using AAModClassic._Content.Terrarium.___PreHardmode.NPCs.PurityWeaver;
using AAModClassic._Content.Terrarium.__Hardmode.NPCs;
using AAModClassic._Content.Terrarium.__Hardmode.NPCs.TerraWarlockSummons;
using AAModClassic._Content.Terrarium.__Hardmode.NPCs.TerraWarlockSummons.TerraWeaver;
using AAModClassic._Content.Void.___PreHardmode.NPCs;
using AAModClassic._Content.Void.__Hardmode.NPCs;
using AAModClassic._Content.Void._PostMoonlord.NPCs;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs._Surface._Night;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic.Tiles.Banners
{
    public class Banners_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinatePadding = 0;		
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);			
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
			DustType = -1;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);			
        }

        public static int[] GetBannerNPCTypes(int frameX)
        {
            int style = frameX / 16;
            int[] npcTypes = style switch
            {
                1 => [ModContent.NPCType<VoidScout>()],
                2 => [], //"FeralMonster", //Unused
                3 => [ModContent.NPCType<Djinn>()],
                4 => [ModContent.NPCType<BlazePhoenix>()],
                5 => [ModContent.NPCType<ChaoticDawn>()],
                6 => [ModContent.NPCType<PigronInferno>()],
                7 => [ModContent.NPCType<FlameBrute>()],
                8 => [], //"Flamespitter", //Unused
                9 => [ModContent.NPCType<InfernalSlime>()],
                10 => [ModContent.NPCType<Singemander>()],
                11 => [ModContent.NPCType<WyrmHead>(), ModContent.NPCType<WyrmBody1>(), ModContent.NPCType<WyrmBody2>(), ModContent.NPCType<WyrmBody3>(), ModContent.NPCType<WyrmBody4>()],
                12 => [ModContent.NPCType<WyrmlingHead>(), ModContent.NPCType<WyrmlingBody>(), ModContent.NPCType<WyrmlingTail1>(), ModContent.NPCType<WyrmlingTail2>()],
                13 => [], //"Wyvern", //Unused
                14 => [], //"Magmalgam", //Unused
                15 => [ModContent.NPCType<ChaoticTwilight>()],
                16 => [ModContent.NPCType<Kappa>()],
                17 => [ModContent.NPCType<PigronMire>()],
                18 => [ModContent.NPCType<MurkySlime>()],
                19 => [ModContent.NPCType<Mosster>()],
                20 => [ModContent.NPCType<Newt>()],
                21 => [ModContent.NPCType<AbyssClaw>()],
                22 => [ModContent.NPCType<BlazeClaw>()],
                23 => [ModContent.NPCType<ChaosDragon>()],
                24 => [ModContent.NPCType<CyberClaw>()], //TODO: Readd Item
                25 => [ModContent.NPCType<DragonClaw_NPC>()],
                26 => [ModContent.NPCType<ElderDragon>()],
                27 => [ModContent.NPCType<HydraClaw_NPC>()],
                28 => [ModContent.NPCType<MadnessBat>()],
                29 => [ModContent.NPCType<MadnessSlime>()],
                30 => [ModContent.NPCType<DimensionDiver>()], //TODO: Readd Item
                31 => [ModContent.NPCType<RiftShark>()], //TODO: Readd Item
                32 => [ModContent.NPCType<TrenchSquid>()], //TODO: Readd Item
                33 => [ModContent.NPCType<SnowSerpentHead>(), ModContent.NPCType<SnowSerpentBody>(), ModContent.NPCType<SnowSerpentTail>()],
                34 => [ModContent.NPCType<TerraKnight>()],
                35 => [ModContent.NPCType<TerraDeadshot>()],
                36 => [ModContent.NPCType<TerraWarlock>()],
                37 => [ModContent.NPCType<TerraWizard>()],
                38 => [ModContent.NPCType<Null>()],
                39 => [ModContent.NPCType<Searcher>()],
                40 => [ModContent.NPCType<Toxitoad>()],
                41 => [ModContent.NPCType<ShadowScout>()],
                42 => [ModContent.NPCType<PuritySquid>()],
                43 => [ModContent.NPCType<UnityProbe>()],
                44 => [ModContent.NPCType<UnityWatcher>()],
                45 => [ModContent.NPCType<PurityWeaverHead>(), ModContent.NPCType<PurityWeaverBody>(), ModContent.NPCType<PurityWeaverTail>()],
                46 => [ModContent.NPCType<PuritySphere>()],
                47 => [], //"TerraSerpent", //Unused
                48 => [ModContent.NPCType<PurityCrawler>()],
                49 => [ModContent.NPCType<TerraSquid>()],
                50 => [ModContent.NPCType<TerraCrawler>()],
                51 => [ModContent.NPCType<TerraSphere>()],
                52 => [ModContent.NPCType<TerraWeaverHead>(), ModContent.NPCType<TerraWeaverBody>(), ModContent.NPCType<TerraWeaverTail>()],
                53 => [ModContent.NPCType<Vortex>()],
                54 => [ModContent.NPCType<StoneSearcher>()],
                55 => [ModContent.NPCType<NightGuard>()],
                56 => [ModContent.NPCType<SunWatcher>()],
                57 => [ModContent.NPCType<AncientLungHead>(), ModContent.NPCType<AncientLungBody>(), ModContent.NPCType<AncientLungTail>()],
                58 => [ModContent.NPCType<Mushbug>()],
                59 => [ModContent.NPCType<MushroomJelly>()],
                60 => [ModContent.NPCType<MushroomCrab>()],
                61 => [ModContent.NPCType<MushroomZombie>(), ModContent.NPCType<MushroomZombie2>()],
                62 => [], //"InfernoSandShark", //Unused
                63 => [], //"MireSandShark", //Unused
                64 => [ModContent.NPCType<ShadowGhoul>()],
                65 => [ModContent.NPCType<Miresquito>()],
                66 => [ModContent.NPCType<FogAngler>()],
                67 => [ModContent.NPCType<Soulsucker>()],
                68 => [ModContent.NPCType<TerraSquire>()],
                69 => [ModContent.NPCType<Seraph>()],
                70 => [ModContent.NPCType<TinyToad>()],
                71 => [ModContent.NPCType<FungusFrog>()],
                72 => [ModContent.NPCType<InfernalGhoul>()],
                73 => [ModContent.NPCType<Skulker>()],
                74 => [ModContent.NPCType<ScavengerHead>(), ModContent.NPCType<ScavengerBody>(), ModContent.NPCType<ScavengerTail>()],
                _ => null,
            };

			return npcTypes;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            int[] npcTypes = GetBannerNPCTypes(frameX);
            if (npcTypes.Length > 0)
            {
                string dropName = ContentSamples.NpcsByNetId[npcTypes[0]].ModNPC.Name.Replace("Head", "");
                Item.NewItem(Item.GetSource_NaturalSpawn(), x * 16, y * 16, 16, 16, Mod.Find<ModItem>(dropName + "Banner").Type, 1, false, -1, false);
            }
        }

        public override void NearbyEffects(int x, int y, bool closer)
        {
            if (closer)
                return;

            int style = Main.tile[x, y].TileFrameX;
            int[] npcTypes = GetBannerNPCTypes(Main.tile[x, y].TileFrameX);
            if (npcTypes.Length == 0)
                return;

            int itemType = TileLoader.GetItemDropFromTypeAndStyle(Type, style / 18);
            if (ItemID.Sets.BannerStrength.IndexInRange(itemType) && ItemID.Sets.BannerStrength[itemType].Enabled)
            {
                Main.SceneMetrics.NPCBannerBuff[npcTypes[0]] = true;
                Main.SceneMetrics.hasBanner = true;
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
	}
}

