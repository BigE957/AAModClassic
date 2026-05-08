using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content._Misc.__Hardmode.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Armor;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Armor;
using AAModClassic._Content.Crimson.__Hardmode.Items.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items.Armor;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Armor;
using AAModClassic._Content.Evil.__Hardmode.Items.Weapons;
using AAModClassic._Content.Hell.___PreHardmode.Items.Armor;
using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture;
using AAModClassic._Content.Inferno.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Jungle.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Armor;
using AAModClassic._Content.Parthenan.__Hardmode.Items.Weapons;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.Items.Armor;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Underground.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Armor;
using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons;
using AAModClassic.Items.Armor.Darkmatter;
using AAModClassic.Items.Armor.Radium;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Boss.Akuma;
using AAModClassic.Items.Boss.Shen;
using AAModClassic.Items.Boss.Zero;
using AAModClassic.Items.Summoning;
using AAModClassic.Items.Vanity.Alphakip;
using AAModClassic.Items.Vanity.Anarchy;
using AAModClassic.Items.Vanity.Apawn;
using AAModClassic.Items.Vanity.Aves;
using AAModClassic.Items.Vanity.Beg;
using AAModClassic.Items.Vanity.Blazen;
using AAModClassic.Items.Vanity.CC;
using AAModClassic.Items.Vanity.Cerberus;
using AAModClassic.Items.Vanity.Charlie;
using AAModClassic.Items.Vanity.Dallin;
using AAModClassic.Items.Vanity.Delly;
using AAModClassic.Items.Vanity.Fargo;
using AAModClassic.Items.Vanity.Gibs;
using AAModClassic.Items.Vanity.Grox;
using AAModClassic.Items.Vanity.Hallam;
using AAModClassic.Items.Vanity.Maskano;
using AAModClassic.Items.Vanity.Mikpin;
using AAModClassic.Items.Vanity.Moon;
using AAModClassic.Items.Vanity.Pluto;
using AAModClassic.Items.Vanity.Shox;
using AAModClassic.Items.Vanity.Tails;
using AAModClassic.Items.Vanity.Tied;
using AAModClassic.Items.Vanity.VoidEye;
using AAModClassic.Tiles.Crafters;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic
{
    internal class AARecipes
    {
        private static Recipe GetNewRecipe(int type, int amt = 1)
        {
            return Recipe.Create(type, amt);
        }

        private static Recipe GetNewRecipe(Mod mod, string name, int amt = 1)
        {
            if (mod == null)
                mod = AAMod.instance;
            return Recipe.Create(mod.Find<ModItem>(name).Type, amt);
        }

        public static void AddRecipes()
        {
            RemoveNightsEdgeRecipe();
            AddMusicBoxRecipes();
            AddPotionRecipes();
            AddMushroomPotionRecipes();
            AddModdedMushroomPotionRecipes();
            RemoveZenithRecipe();

            #region Materials
            Recipe recipe = GetNewRecipe(ItemID.HallowedBar, 1);
            recipe.AddIngredient(ModContent.ItemType<HallowedOre>(), 4);
            recipe.AddTile(ModContent.TileType<HallowedForge_Tile>());
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ShroomiteBar, 1);
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.Mushroom, 3);
            recipe.AddIngredient(ModContent.ItemType<MushroomBlock>());
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            #endregion

            #region Equipment
            recipe = GetNewRecipe(ItemID.Zenith, 1);
            recipe.AddIngredient(ModContent.ItemType<TrueTerraBlade>());
            recipe.AddIngredient(ModContent.ItemType<TheLolkat>());
            recipe.AddIngredient(ModContent.ItemType<CosmicFury>());
            recipe.AddIngredient(ItemID.InfluxWaver);
            recipe.AddIngredient(ModContent.ItemType<Apocalypse>());
            recipe.AddIngredient(ItemID.Seedler);
            recipe.AddIngredient(ItemID.Starfury);
            recipe.AddIngredient(ItemID.BeeKeeper);
            recipe.AddIngredient(ItemID.Terragrim);
            recipe.AddIngredient(ModContent.ItemType<PrismaticGreatsword>());
            recipe.AddIngredient(ModContent.ItemType<BladeOfEvil>());
            recipe.AddIngredient(ModContent.ItemType<ChaosSlayerEX>());
            recipe.AddIngredient(ModContent.ItemType<InfinityBlade>()); //TODO: make this rift shredder in non-unofficial worlds... somehow
            recipe.AddIngredient(ModContent.ItemType<Verdict>());
            recipe.AddIngredient(ModContent.ItemType<SagittariusLeg>());
            recipe.AddIngredient(ModContent.ItemType<RomulusTazesaber>());
            recipe.AddIngredient(ModContent.ItemType<SubzeroSlasher>());
            recipe.AddIngredient(ModContent.ItemType<Olympia>());
            recipe.AddIngredient(ModContent.ItemType<Excalihare>());
            recipe.AddIngredient(ModContent.ItemType<CarnalCrusher>());
            recipe.AddIngredient(ModContent.ItemType<UltimaShortsword>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();

            recipe = GetNewRecipe(ItemID.TerraBlade, 1);
            recipe.AddIngredient(ModContent.ItemType<TrueFleshrendClaymore>(), 1);
            recipe.AddIngredient(ItemID.TrueExcalibur, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.IceBlade);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.Starfury);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            recipe = GetNewRecipe(ItemID.Starfury);
            recipe.AddIngredient(ItemID.GoldBroadsword);
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.PlatinumBroadsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.GoldBroadsword);
            recipe.AddIngredient(ItemID.ManaCrystal, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.Terragrim);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.CobaltShield);
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ObsidianShield);
            recipe.AddIngredient(ModContent.ItemType<PalladiumShield>());
            recipe.AddIngredient(ItemID.ObsidianSkull);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.GravityGlobe, 1);
            recipe.AddIngredient(ItemID.SnowGlobe, 1);
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.RecallPotion, 10);
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.IceMirror);
            recipe.AddIngredient(ItemID.IceBrick, 10);
            recipe.AddIngredient(ItemID.RecallPotion, 10);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
            #endregion

            #region Miscellaneous
            recipe = GetNewRecipe(ItemID.GuideVoodooDoll, 1);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
            recipe.AddIngredient(ItemID.Hay, 5);
            recipe.AddTile(TileID.Loom);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.SnowGlobe, 1);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.SnowBlock, 10);
            recipe.AddRecipeGroup("Wood");
            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.RodofDiscord);
            recipe.AddIngredient(ItemID.SoulofLight, 60);
            recipe.AddIngredient(ItemID.Pearlwood, 5);
            recipe.AddIngredient(ItemID.CrystalShard, 30);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentNebula);
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentSolar);
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentStardust);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentVortex);
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.EmptyBucket, 1);
            recipe.AddTile(ModContent.TileType<RazewoodSink_Tile>());
            recipe.Register();
            #endregion
        }

        private static void RemoveZenithRecipe()
        {
            foreach(Recipe recipe in Main.recipe)
            {
                if (!recipe.HasResult(ItemID.Zenith))
                    continue;

                recipe.DisableRecipe();
            }
        }

        private static void RemoveNightsEdgeRecipe()
        {
            foreach(var v in Main.recipe)
            {
                if (v.HasIngredient(ItemID.BloodButcherer) && v.HasIngredient(ItemID.BladeofGrass) && v.HasIngredient(ItemID.Muramasa) && v.HasIngredient(ItemID.FieryGreatsword) && v.HasTile(TileID.DemonAltar) && v.HasResult(ItemID.NightsEdge))
                    v.DisableRecipe();

            }
        }

        private static void AddMusicBoxRecipes()
        {
            // Music Box
            Recipe recipe = GetNewRecipe(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxOverworldDay, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GrassSeeds, 10);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxAltOverworldDay, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GrassSeeds, 10);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxNight, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Lens, 3);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxRain, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddIngredient(ItemID.UmbrellaHat, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxSnow, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SnowBlock, 30);
            recipe.AddIngredient(ItemID.BorealWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxIce, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.BorealWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxDesert, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SandBlock, 40);
            recipe.AddIngredient(ItemID.Cactus, 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxSandstorm, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
            recipe.AddIngredient(ItemID.SharkFin, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxOcean, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Coral, 3);
            recipe.AddIngredient(ItemID.Starfish, 3);
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxUnderground, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.IronOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxUnderground, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.LeadOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxAltUnderground, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.LeadOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxAltUnderground, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 50);
            recipe.AddIngredient(ItemID.IronOre, 10);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxSpace, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Feather, 20);
            recipe.AddIngredient(ItemID.SunplateBlock, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxMushrooms, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 20);
            recipe.AddIngredient(ItemID.Mushroom, 10);
            recipe.AddIngredient(ItemID.MushroomGrassSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxJungle, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.MudBlock, 20);
            recipe.AddIngredient(ItemID.JungleGrassSeeds, 5);
            recipe.AddIngredient(ItemID.RichMahogany, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxCorruption, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.RottenChunk, 10);
            recipe.AddIngredient(ItemID.CorruptSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxUndergroundCorruption, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.EbonstoneBlock, 30);
            recipe.AddIngredient(ItemID.RottenChunk, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxCrimson, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddIngredient(ItemID.CrimsonSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxUndergroundCrimson, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.CrimstoneBlock, 30);
            recipe.AddIngredient(ItemID.Vertebrae, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxTheHallow, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddIngredient(ItemID.HallowedSeeds, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxUndergroundHallow, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PearlstoneBlock, 30);
            recipe.AddIngredient(ItemID.UnicornHorn, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxHell, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.AshBlock, 20);
            recipe.AddIngredient(ItemID.Hellstone, 15);
            recipe.AddIngredient(ItemID.ObsidianBrick, 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxDungeon, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BlueBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxDungeon, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GreenBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxDungeon, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PinkBrick, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxTemple, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.TempleKey, 1);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss1, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.ShadowScale, 15);
            recipe.AddIngredient(ItemID.DemoniteBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss1, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss2, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GuideVoodooDoll, 1);
            recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss2, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss2, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.TissueSample, 15);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss3, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss4, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BeetleHusk, 8);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxBoss5, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.BeeWax, 20);
            recipe.AddIngredient(ItemID.BottledHoney, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxPlantera, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ModContent.ItemType<PlanteraPetal>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxEerie, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Meteorite, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxEerie, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Shackle, 1);
            recipe.AddIngredient(ItemID.MoneyTrough, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxEclipse, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.LunarTabletFragment, 8);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxGoblins, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.GoblinBattleStandard, 1);
            recipe.AddIngredient(ItemID.SpikyBall, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxPirates, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PirateMap, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxMartians, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxPumpkinMoon, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.PumpkinMoonMedallion, 30);
            recipe.AddIngredient(ItemID.SpookyWood, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxFrostMoon, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.NaughtyPresent, 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxTowers, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.FragmentNebula, 3);
            recipe.AddIngredient(ItemID.FragmentSolar, 3);
            recipe.AddIngredient(ItemID.FragmentVortex, 3);
            recipe.AddIngredient(ItemID.FragmentStardust, 3);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxLunarBoss, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.LunarOre, 30);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.MusicBoxDD2, 1);
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.DefenderMedal, 15);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();

            recipe = GetNewRecipe(null, "AncientCoin", 5);
            recipe.AddRecipeGroup("AAModClassic:DevBag");
            recipe.Register();
        }

        #region Potions
        private static void AddPotionRecipes()
        {
            Recipe recipe = GetNewRecipe(ItemID.RagePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ModContent.ItemType<DragonClaw_Item>(), 3);
            recipe.AddIngredient(ModContent.ItemType<DragonScale>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.WrathPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ModContent.ItemType<HydraClaw_Item>(), 3);
            recipe.AddIngredient(ModContent.ItemType<MirePod>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ModContent.ItemType<MirePod>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ModContent.ItemType<MirePod>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.WaterWalkingPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(ModContent.ItemType<MirePod>(), 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ObsidianSkinPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(ModContent.ItemType<DragonScale>(), 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }

        private static void AddMushroomPotionRecipes()
        {
            // Potion created, required mushrooms, amount of potions created
            List<Tuple<short, string[], int>> potions = new List<Tuple<short, string[], int>>()
            {
                // Blue
                Tuple.Create(ItemID.CalmingPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.FeatherfallPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.FlipperPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.GillsPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.InvisibilityPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.RecallPotion, new string[] { "Blue" }, 4),
                Tuple.Create(ItemID.WaterWalkingPotion, new string[] { "Blue" }, 2),
                Tuple.Create(ItemID.WormholePotion, new string[] { "Blue" }, 2),

                // Brown
                Tuple.Create(ItemID.BuilderPotion, new string[] { "Brown"}, 2),
                Tuple.Create(ItemID.CratePotion, new string[] { "Brown" }, 2),

                // Green
                Tuple.Create(ItemID.FishingPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.NightOwlPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SonarPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SummoningPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.SwiftnessPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.ThornsPotion, new string[] { "Green" }, 2),
                Tuple.Create(ItemID.TitanPotion, new string[] { "Green" }, 2),

                // Gray
                Tuple.Create(ItemID.AmmoReservationPotion, new string[] { "Gray" } , 2),
                Tuple.Create(ItemID.EndurancePotion, new string[] { "Gray" }, 2),
                Tuple.Create(ItemID.MiningPotion, new string[] { "Gray" }, 2),

                // Orange
                Tuple.Create(ItemID.ArcheryPotion, new string[] { "Orange" }, 2),
                Tuple.Create(ItemID.HunterPotion, new string[] { "Orange" }, 2),
                Tuple.Create(ItemID.TrapsightPotion, new string[] { "Orange" }, 2),

                // Pink
                Tuple.Create(ItemID.HeartreachPotion, new string[] { "Pink" }, 2),
                Tuple.Create(ItemID.ManaRegenerationPotion, new string[] { "Pink" }, 2),
                Tuple.Create(ItemID.RegenerationPotion, new string[] { "Pink" }, 2),

                // Purple
                Tuple.Create(ItemID.BattlePotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.GravitationPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.MagicPowerPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.ObsidianSkinPotion, new string[] { "Purple" }, 2),
                Tuple.Create(ItemID.TeleportationPotion, new string[] { "Purple" }, 2),

                // Red
                Tuple.Create(ItemID.InfernoPotion, new string[] { "Red" }, 1),
                Tuple.Create(ItemID.LifeforcePotion, new string[] { "Red" }, 1),
                Tuple.Create(ItemID.RagePotion, new string[] { "Red" }, 2),
                Tuple.Create(ItemID.WrathPotion, new string[] { "Red" }, 2),

                // Yellow
                Tuple.Create(ItemID.IronskinPotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.ShinePotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.SpelunkerPotion, new string[] { "Yellow" }, 2),
                Tuple.Create(ItemID.WarmthPotion, new string[] { "Yellow" }, 2),
                
                // Multiple
                Tuple.Create(ItemID.GenderChangePotion, new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Gray", "Brown", "Pink" }, 2)
            };
            Recipe recipe;

            foreach (Tuple<short, string[], int> potion in potions)
            {
                recipe = GetNewRecipe(potion.Item1, potion.Item3);
                foreach (var mushroom in potion.Item2)
                {
                    recipe.AddIngredient(null, mushroom);
                }
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.Register();

                // Rainbow recipes
                recipe = GetNewRecipe(potion.Item1);
                recipe.AddIngredient(ModContent.ItemType<RainbowMushroom>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.Register();
            }
        }

        private static void AddModdedMushroomPotionRecipes()
        {
            #region GRealm
            if (ModLoader.TryGetMod("Grealm", out var GRealm))
            {
                // Potion created, mushrooms required, amount of mushrooms required
                List<Tuple<string, string[], int>> GRealmPotions = new List<Tuple<string, string[], int>>()
                {
                    Tuple.Create("ChitinPotion", new string[] { "Brown" }, 1),
                    Tuple.Create("CosmicChitinPotion", new string[] { "Brown" }, 2),
                    Tuple.Create("CosmicEndurancePotion", new string[] { "Gray" }, 2),
                    Tuple.Create("CosmicSummoningPotion", new string[] { "Green" }, 2),
                    Tuple.Create("CosmicArcheryPotion", new string[] { "Orange" }, 2),
                    Tuple.Create("CosmicRegenerationPotion", new string[] { "Pink" }, 2),
                    Tuple.Create("CosmicMagicPowerPotion", new string[] { "Purple" }, 2),
                    Tuple.Create("BloodbathPotion", new string[] { "Red" }, 1),
                    Tuple.Create("CosmicRagePotion", new string[] { "Red" }, 2),
                    Tuple.Create("CosmicWrathPotion", new string[] { "Red" }, 2)
                };
                Recipe recipe;

                foreach (Tuple<string, string[], int> potion in GRealmPotions)
                {
                    recipe = GetNewRecipe(GRealm, potion.Item1);
                    foreach (var mushroom in potion.Item2)
                    {
                        recipe.AddIngredient(null, mushroom, potion.Item3);
                    }
                    if (potion.Item1 == "BloodbathPotion" || potion.Item1 == "ChitinPotion")
                    {
                        recipe.AddIngredient(ItemID.BottledWater);
                    }
                    else
                    {
                        recipe.AddIngredient(GRealm, "CosmicContainer");
                    }
                    recipe.AddTile(TileID.Bottles);
                    recipe.Register();

                    // Rainbow recipes
                    recipe = GetNewRecipe(GRealm, potion.Item1);
                    recipe.AddIngredient(ModContent.ItemType<RainbowMushroom>());
                    if (potion.Item1 == "BloodbathPotion" || potion.Item1 == "ChitinPotion")
                    {
                        recipe.AddIngredient(ItemID.BottledWater);
                    }
                    else
                    {
                        recipe.AddIngredient(GRealm, "CosmicContainer");
                    }
                    recipe.AddTile(TileID.Bottles);
                    recipe.Register();
                }
            }
            #endregion
        }
        #endregion

        public static void AddRecipeGroups()
        {
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.darkmatterhelmet"), new int[]
            {
                ModContent.ItemType<DarkmatterVisor>(),
                //ModContent.ItemType<DarkmatterHelm>(),
                ModContent.ItemType<DarkmatterHelmet>(),
                ModContent.ItemType<DarkmatterHeaddress>(),
                ModContent.ItemType<DarkmatterMask>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.radiumhelmet"), new int[]
            {
                ModContent.ItemType<RadiumHat>(),
                //ModContent.ItemType<RadiumHelm>(),
                ModContent.ItemType<RadiumHelmet>(),
                ModContent.ItemType<RadiumHeadgear>(),
                ModContent.ItemType<RadiumMask>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.goldbar"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:Gold", group2);
           
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.celestialcraftingstation"), new int[]
            {
                ModContent.ItemType<RadiantArcanum>(),
                ModContent.ItemType<QuantumFusionAccelerator>(),
            });
            RecipeGroup.RegisterGroup("AAModClassic:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.ancientmaterial"), new int[]
            {
                ModContent.ItemType<UnstableSingularity>(),
                ModContent.ItemType<CrucibleScale>(),
                ModContent.ItemType<DreadScale>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:AncientMaterials", group4);

            //TODO: Add SoC material
            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.superancientmaterial"), new int[]
            {
                ModContent.ItemType<ChaosScale>(),
                ModContent.ItemType<Infinitium>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:SuperAncientMaterials", group5);
            
            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.worldevilmaterial"), new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);
            
            RecipeGroup group7 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.hardmodeforge"), validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.chaosclaw"), new int[]
            {
                ModContent.ItemType<DragonClaw_Item>(),
                ModContent.ItemType<HydraClaw_Item>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosClaw", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.ironbar"), new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:Iron", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.copperbar"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:Copper", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.silverbar"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:Silver", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.evilbar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.chaosbar"), new int[]
            {
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosBar", group13);

            RecipeGroup group14 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.EvilorChaosBar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilorChaosBar", group14);

            RecipeGroup group15 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.ancientcraftingstation"), new int[]
            {
                ModContent.ItemType<BinaryReassembler>(),
                ModContent.ItemType<ChaosCrucible>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ACS", group15);

            RecipeGroup group16 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.evilsummonstaff"), new int[]
            {
                ModContent.ItemType<EaterStaff>(),
                ModContent.ItemType<CrimsonStaff>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilStaff", group16);

            RecipeGroup group17 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.altar"), new int[]
            {
                ModContent.ItemType<AbyssAltarSafe>(),
                ModContent.ItemType<CrimsonAltar>(),
                ModContent.ItemType<CorruptAltar>(),
                ModContent.ItemType<DragonAltarSafe>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:Altar", group17);

            RecipeGroup group18 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.ChaosLeggings"), new int[]
            {
                ModContent.ItemType<BlazingLeggings>(),
                ModContent.ItemType<AbyssalLeggings>(),
                ModContent.ItemType<AtlanteanLeggings>(),
                ModContent.ItemType<DoomiteLeggings>(),
                ModContent.ItemType<RaiderLeggings>(),
                ModContent.ItemType<DynaskullLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosBoots", group18);

            RecipeGroup group19 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.ChaosChestpiece"), new int[]
            {
                ModContent.ItemType<BlazingChestplate>(),
                ModContent.ItemType<AbyssalChestplate>(),
                ModContent.ItemType<AtlanteanChestplate>(),
                ModContent.ItemType<DoomiteChestplate>(),
                ModContent.ItemType<RaiderChestplate>(),
                ModContent.ItemType<DynaskullChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosPlates", group19);

            RecipeGroup group20 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.hardmodeanvil"), new int[]
            {
                ItemID.MythrilAnvil, ItemID.OrichalcumAnvil
            });
            RecipeGroup.RegisterGroup("AAModClassic:HAnvil", group20);

            RecipeGroup group21 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.hardmodeforge"), new int[]
            {
                ItemID.AdamantiteForge, ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AAModClassic:HForge", group21);

            RecipeGroup group22 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.ShinyCharm"), new int[]
            {
                ModContent.ItemType<ShinyCharm>(),
                ModContent.ItemType<ShinyCharmFish>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ShinyCharm", group22);

            RecipeGroup group23 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.DevBag"), new int[]
            {
                ModContent.ItemType<AlphaBag>(),
                ModContent.ItemType<InvokerBag>(),
                ModContent.ItemType<CCBox>(),
                ModContent.ItemType<BlazenBag>(),
                ModContent.ItemType<AvesBag>(),
                ModContent.ItemType<DellyBag>(),
                ModContent.ItemType<OldMagiciansHat>(),
                ModContent.ItemType<MagiciansHat>(),
                ModContent.ItemType<FezLordsBag>(),
                ModContent.ItemType<MoonBag>(),
                ModContent.ItemType<GibsBag>(),
                ModContent.ItemType<GroviteSeaChest>(),
                ModContent.ItemType<PlutoBag>(),
                ModContent.ItemType<VoidBag>(),
                ModContent.ItemType<AnarchyBag>(),
                ModContent.ItemType<MaskBag>(),
                ModContent.ItemType<TopHat>(),
                ModContent.ItemType<BegBag>(),
                ModContent.ItemType<CharlieBag>(),
                ModContent.ItemType<MikBag>(),
                ModContent.ItemType<TailsToolbox>(),
                ModContent.ItemType<ShoxBag>(),
                ModContent.ItemType<ApawnEgg>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:DevBag", group23);

            RecipeGroup group24 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + "Terra Boots", new int[]
            {
                ModContent.ItemType<FleshrendLeggings>(),
                ModContent.ItemType<NightsLeggings>(),
                ModContent.ItemType<TribalLeggings>(),
                ModContent.ItemType<DeathlyLeggings>(),
                ModContent.ItemType<DemonLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraBoots", group24);

            RecipeGroup group25 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + "Terra Chestplate", new int[]
            {
                ModContent.ItemType<NightsChestplate>(),
                ModContent.ItemType<FleshrendChestplate>(),
                ModContent.ItemType<TribalChestplate>(),
                ModContent.ItemType<DeathlyChestplate>(),
                ModContent.ItemType<DemonChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraPlates", group25);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<OroborosWood>());
            }
        }
    }
}
