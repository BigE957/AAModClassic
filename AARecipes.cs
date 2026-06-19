using AAModClassic._Content._Dev.___PreHardmode.Items.Currency;
using AAModClassic._Content._Dev.___PreHardmode.Items.Materials;
using AAModClassic._Content._Dev.__Hardmode.Items.Armor.Vanity;
using AAModClassic._Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._EX._PostMoonlord.Items.Weapons;
using AAModClassic._Content._Misc.__Hardmode.Items.Materials.Mushrooms;
using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Armor;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Corruption.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Armor;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Crimson.__Hardmode.Items.Weapons;
using AAModClassic._Content.Desert.___PreHardmode.Items.Armor;
using AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.Weapons;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Armor;
using AAModClassic._Content.Evil.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Evil.__Hardmode.Items.Weapons;
using AAModClassic._Content.Hallow.__Hardmode.Items.Materials;
using AAModClassic._Content.Hallow.__Hardmode.Items.Tiles.Functional;
using AAModClassic._Content.Hell.___PreHardmode.Items.Armor;
using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.__Hardmode.Items.Consumables;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Jungle.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Mire.__Hardmode.Items.Consumables;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Armor;
using AAModClassic._Content.Parthenan.__Hardmode.Items.Weapons;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.World.Tiles;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons;
using AAModClassic._Content.Snow.___PreHardmode.Items.Armor;
using AAModClassic._Content.Stars._PostMoonlord.Items.Armor;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Underground.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.Weapons;
using AAModClassic._Content.Void.___PreHardmode.Items.Armor;
using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic._Unofficial.Content._Dev.__Hardmode.Items.Consumables;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.Weapons;
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
            recipe.AddIngredient(ModContent.ItemType<Ikari>());
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
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
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
            recipe.AddIngredient(ItemID.PumpkinMoonMedallion);
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

            recipe = GetNewRecipe(ModContent.ItemType<AncientCoin>(), 5);
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
            string red = "RedAlchemicalMushroom";
            string orange = "OrangeAlchemicalMushroom";
            string yellow = "YellowAlchemicalMushroom";
            string green = "GreenAlchemicalMushroom";
            string blue = "BlueAlchemicalMushroom";
            string purple = "PurpleAlchemicalMushroom";
            string pink = "PinkAlchemicalMushroom";
            string brown = "BrownAlchemicalMushroom";
            string gray = "GrayAlchemicalMushroom";

            // Potion created, required mushrooms, amount of potions created
            List<Tuple<short, string[], int>> potions = new List<Tuple<short, string[], int>>()
            {
                Tuple.Create(ItemID.InfernoPotion, new string[] { red }, 1),
                Tuple.Create(ItemID.LifeforcePotion, new string[] { red }, 1),
                Tuple.Create(ItemID.RagePotion, new string[] { red }, 2),
                Tuple.Create(ItemID.WrathPotion, new string[] { red }, 2),

                Tuple.Create(ItemID.ArcheryPotion, new string[] { orange }, 2),
                Tuple.Create(ItemID.HunterPotion, new string[] { orange }, 2),
                Tuple.Create(ItemID.TrapsightPotion, new string[] { orange }, 2),

                Tuple.Create(ItemID.IronskinPotion, new string[] { yellow }, 2),
                Tuple.Create(ItemID.ShinePotion, new string[] { yellow }, 2),
                Tuple.Create(ItemID.SpelunkerPotion, new string[] { yellow }, 2),
                Tuple.Create(ItemID.WarmthPotion, new string[] { yellow }, 2),

                Tuple.Create(ItemID.FishingPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.NightOwlPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.SonarPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.SummoningPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.SwiftnessPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.ThornsPotion, new string[] { green }, 2),
                Tuple.Create(ItemID.TitanPotion, new string[] { green }, 2),

                Tuple.Create(ItemID.CalmingPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.FeatherfallPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.FlipperPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.GillsPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.InvisibilityPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.RecallPotion, new string[] { blue }, 4),
                Tuple.Create(ItemID.WaterWalkingPotion, new string[] { blue }, 2),
                Tuple.Create(ItemID.WormholePotion, new string[] { blue }, 2),

                Tuple.Create(ItemID.BattlePotion, new string[] { purple }, 2),
                Tuple.Create(ItemID.GravitationPotion, new string[] { purple }, 2),
                Tuple.Create(ItemID.MagicPowerPotion, new string[] { purple }, 2),
                Tuple.Create(ItemID.ObsidianSkinPotion, new string[] { purple }, 2),
                Tuple.Create(ItemID.TeleportationPotion, new string[] { purple }, 2),

                Tuple.Create(ItemID.HeartreachPotion, new string[] { pink }, 2),
                Tuple.Create(ItemID.ManaRegenerationPotion, new string[] { pink }, 2),
                Tuple.Create(ItemID.RegenerationPotion, new string[] { pink }, 2),

                Tuple.Create(ItemID.BuilderPotion, new string[] { brown }, 2),
                Tuple.Create(ItemID.CratePotion, new string[] { brown }, 2),

                Tuple.Create(ItemID.AmmoReservationPotion, new string[] { gray } , 2),
                Tuple.Create(ItemID.EndurancePotion, new string[] { gray }, 2),
                Tuple.Create(ItemID.MiningPotion, new string[] { gray }, 2),

                Tuple.Create(ItemID.GenderChangePotion, new string[] { red, orange, yellow, green, blue, purple, pink, brown, gray }, 2)
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
            RecipeGroup recipeGroup = new RecipeGroup(() => "nothing", new int[]
            {
                ItemID.Snail
            });

            #region Ore
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Copper"), new int[]
            {
                ItemID.CopperOre,
                ItemID.TinOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:CopperOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Iron"), new int[]
            {
                ItemID.IronOre,
                ItemID.LeadOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:IronOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Silver"), new int[]
            {
                ItemID.SilverOre,
                ItemID.TungstenOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:SilverOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Gold"), new int[]
            {
                ItemID.GoldOre,
                ItemID.PlatinumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:GoldOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Evil"), new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Chaos"), new int[]
            {
                ModContent.ItemType<IncineriteOre>(),
                ModContent.ItemType<AbyssiumOre>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.EvilOrChaos"), new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
                ModContent.ItemType<IncineriteOre>(),
                ModContent.ItemType<AbyssiumOre>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Cobalt"), new int[]
            {
                ItemID.CobaltOre,
                ItemID.PalladiumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:CobaltOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Mythril"), new int[]
            {
                ItemID.MythrilOre,
                ItemID.OrichalcumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:MythrilOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Adamantite"), new int[]
            {
                ItemID.AdamantiteOre,
                ItemID.TitaniumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:AdamantiteOre", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Hallowed"), new int[]
            {
                ModContent.ItemType<HallowedOre>(),
                ModContent.ItemType<FulguriteShard>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HallowedOre", recipeGroup);
            #endregion
            #region Bars
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Copper"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:CopperBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Iron"), new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:IronBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Silver"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:SilverBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Gold"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:GoldBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Evil"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Chaos"), new int[]
            {
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.EvilOrChaos"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Cobalt"), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:CobaltBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Mythril"), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:MythrilBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Adamantite"), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:AdamantiteBar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Hallowed"), new int[]
            {
                ItemID.HallowedBar,
                ModContent.ItemType<FulguriteBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HallowedBar", recipeGroup);
            #endregion
            #region Materials
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.ShinyCharm"), new int[]
            {
                ModContent.ItemType<ShinyCharm>(),
                ModContent.ItemType<ShinyCharmFish>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ShinyCharm", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.ChaosClaw"), new int[]
            {
                ModContent.ItemType<DragonClaw_Item>(),
                ModContent.ItemType<HydraClaw_Item>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosClaw", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Evil"), new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Chaos"), new int[]
            {
                ModContent.ItemType<ScorchedScale>(),
                ModContent.ItemType<HydraHide>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.EvilOrChaos"), new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample,
                ModContent.ItemType<ScorchedScale>(),
                ModContent.ItemType<HydraHide>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeEvil"), new int[]
            {
                ItemID.CursedFlame,
                ItemID.Ichor
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeEvilMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeChaos"), new int[]
            {
                ModContent.ItemType<DragonFire>(),
                ModContent.ItemType<Bogtoxin>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeChaosMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeEvilOrChaos"), new int[]
            {
                ItemID.CursedFlame,
                ItemID.Ichor,
                ModContent.ItemType<DragonFire>(),
                ModContent.ItemType<Bogtoxin>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeEvilOrChaosMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.EarlyAncient"), new int[]
            {
                ModContent.ItemType<StormSphere>(),
                ModContent.ItemType<CovetiteBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EarlyAncientMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.LateAncient"), new int[]
            {
                ModContent.ItemType<UnstableSingularity>(),
                ModContent.ItemType<CrucibleScale>(),
                ModContent.ItemType<DreadScale>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:LateAncientMaterial", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Superancient"), new int[]
            {
                ModContent.ItemType<ChaosScale>(),
                ModContent.ItemType<Infinitium>(),
                ModContent.ItemType<RealityBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:SuperancientMaterial", recipeGroup);
            #endregion
            #region Crafting Stations
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.Altar"), new int[]
            {
                ModContent.ItemType<AbyssAltarSafe>(),
                ModContent.ItemType<CrimsonAltar>(),
                ModContent.ItemType<CorruptAltar>(),
                ModContent.ItemType<DragonAltarSafe>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:Altar", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.HardmodeAnvil"), new int[]
            {
                ItemID.MythrilAnvil,
                ItemID.OrichalcumAnvil
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeAnvil", recipeGroup);
            recipeGroup = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.HardmodeForge"), validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeForge", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.CelestialCraftingStation"), new int[]
            {
                ModContent.ItemType<RadiantArcanum>(),
                ModContent.ItemType<QuantumFusionAccelerator>(),
            });
            RecipeGroup.RegisterGroup("AAModClassic:CelestialCraftingStation", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.AncientCraftingStation"), new int[]
            {
                ModContent.ItemType<BinaryReassembler>(),
                ModContent.ItemType<ChaosCrucible>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:AncientCraftingStation", recipeGroup);
            #endregion
            #region Weapons
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Weapons.EvilStaff"), new int[]
            {
                ModContent.ItemType<DemoniteStaff>(),
                ModContent.ItemType<CrimeraStaff>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilStaff", recipeGroup);
            #endregion
            #region Armor
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.TerraChestplate"), new int[]
            {
                ModContent.ItemType<NightsChestplate>(),
                ModContent.ItemType<FleshrendChestplate>(),
                ModContent.ItemType<TribalChestplate>(),
                ModContent.ItemType<DeathlyChestplate>(),
                ModContent.ItemType<DemonChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraChestplate", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.TerraBppts"), new int[]
            {
                ModContent.ItemType<FleshrendLeggings>(),
                ModContent.ItemType<NightsLeggings>(),
                ModContent.ItemType<TribalLeggings>(),
                ModContent.ItemType<DeathlyLeggings>(),
                ModContent.ItemType<DemonLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraLeggings", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.ChaosChestplate"), new int[]
            {
                ModContent.ItemType<BlazingChestplate>(),
                ModContent.ItemType<AbyssalChestplate>(),
                ModContent.ItemType<AtlanteanChestplate>(),
                ModContent.ItemType<DoomiteChestplate>(),
                ModContent.ItemType<RaiderChestplate>(),
                ModContent.ItemType<DynaskullChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosChestplate", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.ChaosLeggings"), new int[]
            {
                ModContent.ItemType<BlazingLeggings>(),
                ModContent.ItemType<AbyssalLeggings>(),
                ModContent.ItemType<AtlanteanLeggings>(),
                ModContent.ItemType<DoomiteLeggings>(),
                ModContent.ItemType<RaiderLeggings>(),
                ModContent.ItemType<DynaskullLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosLeggings", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.RadiumHelmet"), new int[]
            {
                ModContent.ItemType<RadiumHelmetSummoner>(),
                //ModContent.ItemType<RadiumHelm>(),
                ModContent.ItemType<RadiumHelmetMelee>(),
                ModContent.ItemType<RadiumHelmetRanged>(),
                ModContent.ItemType<RadiumHelmetMage>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:RadiumHelmet", recipeGroup);
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.DarkmatterHelmet"), new int[]
            {
                ModContent.ItemType<DarkmatterHelmetRanged>(),
                //ModContent.ItemType<DarkmatterHelm>(),
                ModContent.ItemType<DarkmatterHelmetMelee>(),
                ModContent.ItemType<DarkmatterHelmetSummoner>(),
                ModContent.ItemType<DarkmatterHelmetMage>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:DarkmatterHelmet", recipeGroup);
            #endregion
            #region Misc
            recipeGroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Misc.DevBag"), new int[]
            {
                ModContent.ItemType<BigEBag>(),
                ModContent.ItemType<CerberusBag>(),
                ModContent.ItemType<CCBag>(),
                ModContent.ItemType<BlazenBag>(),
                ModContent.ItemType<AvesBag>(),
                ModContent.ItemType<DellyBag>(),
                ModContent.ItemType<TiedBag>(),
                ModContent.ItemType<HallamBag>(),
                ModContent.ItemType<DallinBag>(),
                ModContent.ItemType<MoonBag>(),
                ModContent.ItemType<GibsBag>(),
                ModContent.ItemType<GroxBag>(),
                ModContent.ItemType<PlutoBag>(),
                ModContent.ItemType<VoidEyeBag>(),
                ModContent.ItemType<AnarchyBag>(),
                ModContent.ItemType<MaskanoBag>(),
                ModContent.ItemType<FargoBag>(),
                ModContent.ItemType<BegBag>(),
                ModContent.ItemType<CharlieBag>(),
                ModContent.ItemType<MikpinBag>(),
                ModContent.ItemType<TailsBag>(),
                ModContent.ItemType<ShoxBag>(),
                ModContent.ItemType<ApawnBag>(),
                ModContent.ItemType<PlanterrorBag>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:DevBag", recipeGroup);
            #endregion
            #region Vanilla Sets
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<OroborosWood>());
            }
            #endregion
        }
    }
}
