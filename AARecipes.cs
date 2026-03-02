using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod
{
    internal class AARecipes
    {
        private static Recipe GetNewRecipe()
        {
            return Recipe.Create();
        }

        public static void AddRecipes()
        {
            RemoveNightsEdgeRecipe();
            AddMusicBoxRecipes();
            AddPotionRecipes();
            AddMushroomPotionRecipes();
            AddModdedMushroomPotionRecipes();
            AddTransmuterRecipes();

            #region Materials
            Recipe recipe = GetNewRecipe(ItemID.HallowedBar, 1);
            recipe.AddIngredient(null, "HallowedOre", 4);
            recipe.AddTile(null, "HallowedForge");
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ShroomiteBar, 1);
            recipe.AddIngredient(null, "MushiumBar", 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 5);
            recipe.AddTile(TileID.Autohammer);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.Mushroom, 3);
            recipe.AddIngredient(null, "MushroomBlock");
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            #endregion

            #region Equipment
            recipe = GetNewRecipe(ItemID.TerraBlade, 1);
            recipe.AddIngredient(null, "TrueFleshrendClaymore", 1);
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

            recipe = GetNewRecipe(ItemID.Arkhalis);
            recipe.AddIngredient(ItemID.EnchantedSword);
            recipe.AddIngredient(ItemID.Muramasa);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.CobaltShield);
            recipe.AddIngredient(ItemID.CobaltBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ObsidianShield);
            recipe.AddIngredient(null, "PalladiumShield");
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
            recipe.AddIngredient(null, "DevilSilk", 5);
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
            recipe.AddIngredient(null, "RadiumBar", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentSolar);
            recipe.AddIngredient(null, "RadiumBar", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentStardust);
            recipe.AddIngredient(null, "DarkMatter", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.FragmentVortex);
            recipe.AddIngredient(null, "DarkMatter", 1);
            recipe.AddIngredient(ItemID.LunarOre, 3);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.LavaBucket);
            recipe.AddIngredient(ItemID.EmptyBucket, 1);
            recipe.AddTile(ModContent.TileType<Tiles.Furniture.Razewood.RazewoodSink>());
            recipe.Register();
            #endregion
        }

        private static void RemoveNightsEdgeRecipe()
        {
            RecipeFinder finder = new RecipeFinder();
            {
                finder.AddIngredient(ItemID.BloodButcherer, 1);
                finder.AddIngredient(ItemID.FieryGreatsword, 1);
                finder.AddIngredient(ItemID.BladeofGrass, 1);
                finder.AddIngredient(ItemID.Muramasa, 1);
                finder.AddTile(TileID.DemonAltar);
                finder.SetResult(ItemID.NightsEdge, 1);
                Recipe recipe2 = finder.FindExactRecipe();
                if (recipe2 != null)
                {
                    RecipeEditor editor = new RecipeEditor(recipe2);
                    editor.DeleteRecipe();
                }
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
            recipe.AddIngredient(null, "DevilSilk", 15);
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
            recipe.AddIngredient(null, "PlanteraPetal", 5);
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
            recipe.AddRecipeGroup("AAMod:DevBag");
            recipe.Register();
        }

        #region Potions
        private static void AddPotionRecipes()
        {
            Recipe recipe = GetNewRecipe(ItemID.RagePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "DragonClaw", 3);
            recipe.AddIngredient(null, "DragonScale", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.WrathPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "HydraClaw", 3);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.BattlePotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(null, "MirePod", 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.WaterWalkingPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(null, "MirePod", 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();

            recipe = GetNewRecipe(ItemID.ObsidianSkinPotion, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(null, "DragonScale", 2);
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
                recipe.AddIngredient(null, "Rainbow");
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.Register();
            }
        }

        private static void AddModdedMushroomPotionRecipes()
        {
            #region GRealm
            Mod GRealm = ModLoader.GetMod("Grealm");

            if (GRealm != null)
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
                    recipe.AddIngredient(null, "Rainbow");
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

        #region Transmuter
        private static void AddTransmuterRecipes()
        {
            #region Biomes
            TransmuteRecipe(ItemID.Ebonwood, ItemID.Shadewood);
            TransmuteRecipe(ItemID.EbonstoneBlock, ItemID.CrimstoneBlock);
            TransmuteRecipe(ItemID.DemoniteBar, ItemID.CrimtaneBar);
            TransmuteRecipe(ItemID.ShadowScale, ItemID.TissueSample);
            TransmuteRecipe(ItemID.VileMushroom, ItemID.ViciousMushroom);
            TransmuteRecipe(ItemID.CursedFlame, ItemID.Ichor);
            TransmuteRecipe(ItemID.CorruptionKey, ItemID.CrimsonKey);

            TransmuteRecipe(ItemID.SoulofNight, ItemID.SoulofLight);

            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("BroodScale").Type, (short)AAMod.instance.Find<ModItem>("HydraHide").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("Hotshroom").Type, (short)AAMod.instance.Find<ModItem>("Darkshroom").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("DragonFire").Type, (short)AAMod.instance.Find<ModItem>("HydraToxin").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("SoulOfSmite").Type, (short)AAMod.instance.Find<ModItem>("SoulOfSpite").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("InfernoKey").Type, (short)AAMod.instance.Find<ModItem>("MireKey").Type);
            #endregion

            #region Bars
            TransmuteRecipe(ItemID.CopperBar, ItemID.TinBar);
            TransmuteRecipe(ItemID.LeadBar, ItemID.IronBar);
            TransmuteRecipe(ItemID.SilverBar, ItemID.TungstenBar);
            TransmuteRecipe(ItemID.GoldBar, ItemID.PlatinumBar);
            TransmuteRecipe(ItemID.CobaltBar, ItemID.PalladiumBar);
            TransmuteRecipe(ItemID.MythrilBar, ItemID.OrichalcumBar);
            TransmuteRecipe(ItemID.AdamantiteBar, ItemID.TitaniumBar);

            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("AbyssiumBar").Type, (short)AAMod.instance.Find<ModItem>("IncineriteBar").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("DeepAbyssium").Type, (short)AAMod.instance.Find<ModItem>("RadiantIncinerite").Type);
            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("DaybreakIncinerite").Type, (short)AAMod.instance.Find<ModItem>("EventideAbyssium").Type);
            #endregion

            #region Ores
            TransmuteRecipe(ItemID.CopperOre, ItemID.TinOre);
            TransmuteRecipe(ItemID.LeadOre, ItemID.IronOre);
            TransmuteRecipe(ItemID.SilverOre, ItemID.TungstenOre);
            TransmuteRecipe(ItemID.GoldOre, ItemID.PlatinumOre);
            TransmuteRecipe(ItemID.DemoniteOre, ItemID.CrimtaneOre);
            TransmuteRecipe(ItemID.CobaltOre, ItemID.PalladiumOre);
            TransmuteRecipe(ItemID.MythrilOre, ItemID.OrichalcumOre);
            TransmuteRecipe(ItemID.TitaniumOre, ItemID.AdamantiteOre);

            TransmuteRecipe((short)AAMod.instance.Find<ModItem>("Abyssium").Type, (short)AAMod.instance.Find<ModItem>("Incinerite").Type);
            #endregion
        }

        private static void TransmuteRecipe(short item, short item2)
        { 
            Recipe recipe = GetNewRecipe(item2);
            recipe.AddIngredient(item, 2);
            recipe.AddTile(AAMod.instance, "Transmuter");
            recipe.Register();

            recipe = GetNewRecipe(item);
            recipe.AddIngredient(item2, 2);
            recipe.AddTile(AAMod.instance, "Transmuter");
            recipe.Register();
        }
        #endregion

        public static void AddRecipeGroups()
        {
            RecipeGroup group0 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.darkmatterhelmet"), new int[]
            {
                AAMod.instance.Find<ModItem>("DarkmatterVisor").Type,
                AAMod.instance.Find<ModItem>("DarkmatterHelm").Type,
                AAMod.instance.Find<ModItem>("DarkmatterHelmet").Type,
                AAMod.instance.Find<ModItem>("DarkmatterHeaddress").Type,
                AAMod.instance.Find<ModItem>("DarkmatterMask").Type
            });
            RecipeGroup.RegisterGroup("AAMod:DarkmatterHelmets", group0);

            RecipeGroup group1 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.radiumhelmet"), new int[]
            {
                AAMod.instance.Find<ModItem>("RadiumHat").Type,
                AAMod.instance.Find<ModItem>("RadiumHelm").Type,
                AAMod.instance.Find<ModItem>("RadiumHelmet").Type,
                AAMod.instance.Find<ModItem>("RadiumHeadgear").Type,
                AAMod.instance.Find<ModItem>("RadiumMask").Type
            });
            RecipeGroup.RegisterGroup("AAMod:RadiumHelmets", group1);

            RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.goldbar"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("AAMod:Gold", group2);
           
            RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.celestialcraftingstation"), new int[]
            {
                AAMod.instance.Find<ModItem>("RadiantArcanum").Type,
                AAMod.instance.Find<ModItem>("QuantumFusionAccelerator").Type,
            });
            RecipeGroup.RegisterGroup("AAMod:AstralStations", group3);

            RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ancientmaterial"), new int[]
            {
                AAMod.instance.Find<ModItem>("UnstableSingularity").Type,
                AAMod.instance.Find<ModItem>("CrucibleScale").Type,
                AAMod.instance.Find<ModItem>("DreadScale").Type
            });
            RecipeGroup.RegisterGroup("AAMod:AncientMaterials", group4);

            RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.superancientmaterial"), new int[]
            {
                AAMod.instance.Find<ModItem>("ChaosScale").Type
            });
            RecipeGroup.RegisterGroup("AAMod:SuperAncientMaterials", group5);
            
            RecipeGroup group6 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.worldevilmaterial"), new int[]
            {
                ItemID.Ichor,
                ItemID.CursedFlame
            });
            RecipeGroup.RegisterGroup("AnyIchor", group6);
            
            RecipeGroup group7 = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeforge"), validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AnyHardmodeForge", group7);

            RecipeGroup group8 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.chaosclaw"), new int[]
            {
                AAMod.instance.Find<ModItem>("DragonClaw").Type,
                AAMod.instance.Find<ModItem>("HydraClaw").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosClaw", group8);

            RecipeGroup group9 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ironbar"), new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAMod:Iron", group9);

            RecipeGroup group10 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.copperbar"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAMod:Copper", group10);

            RecipeGroup group11 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.silverbar"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAMod:Silver", group11);

            RecipeGroup group12 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.evilbar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAMod:EvilBar", group12);

            RecipeGroup group13 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.chaosbar"), new int[]
            {
                AAMod.instance.Find<ModItem>("IncineriteBar").Type,
                AAMod.instance.Find<ModItem>("AbyssiumBar").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBar", group13);

            RecipeGroup group14 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.EvilorChaosBar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                AAMod.instance.Find<ModItem>("IncineriteBar").Type,
                AAMod.instance.Find<ModItem>("AbyssiumBar").Type
            });
            RecipeGroup.RegisterGroup("AAMod:EvilorChaosBar", group14);

            RecipeGroup group15 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ancientcraftingstation"), new int[]
            {
                AAMod.instance.Find<ModItem>("BinaryReassembler").Type,
                AAMod.instance.Find<ModItem>("ChaosCrucible").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ACS", group15);

            RecipeGroup group16 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.evilsummonstaff"), new int[]
            {
                ModContent.ItemType<Items.Summoning.EaterStaff>(),
                ModContent.ItemType<Items.Summoning.CrimsonStaff>()
            });
            RecipeGroup.RegisterGroup("AAMod:EvilStaff", group16);

            RecipeGroup group17 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.altar"), new int[]
            {
                AAMod.instance.Find<ModItem>("MireAltar").Type,
                AAMod.instance.Find<ModItem>("CrimsonAltar").Type,
                AAMod.instance.Find<ModItem>("CorruptAltar").Type,
                AAMod.instance.Find<ModItem>("InfernoAltar").Type
            });
            RecipeGroup.RegisterGroup("AAMod:Altar", group17);

            RecipeGroup group18 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ChaosLeggings"), new int[]
            {
                AAMod.instance.Find<ModItem>("BlazingSuneate").Type,
                AAMod.instance.Find<ModItem>("AbyssalHakama").Type,
                AAMod.instance.Find<ModItem>("AtlanteanGreaves").Type,
                AAMod.instance.Find<ModItem>("DoomiteGreaves").Type,
                AAMod.instance.Find<ModItem>("RaiderLegs").Type,
                AAMod.instance.Find<ModItem>("DynaskullGreaves").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosBoots", group18);

            RecipeGroup group19 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.ChaosChestpiece"), new int[]
            {
                AAMod.instance.Find<ModItem>("BlazingDou").Type,
                AAMod.instance.Find<ModItem>("AbyssalGi").Type,
                AAMod.instance.Find<ModItem>("AtlanteanPlate").Type,
                AAMod.instance.Find<ModItem>("DoomiteBreastplate").Type,
                AAMod.instance.Find<ModItem>("RaiderChest").Type,
                AAMod.instance.Find<ModItem>("DynaskullRibguard").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ChaosPlates", group19);

            RecipeGroup group20 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeanvil"), new int[]
            {
                ItemID.MythrilAnvil, ItemID.OrichalcumAnvil
            });
            RecipeGroup.RegisterGroup("AAMod:HAnvil", group20);

            RecipeGroup group21 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAMod.Common.hardmodeforge"), new int[]
            {
                ItemID.AdamantiteForge, ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AAMod:HForge", group21);

            RecipeGroup group22 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.ShinyCharm"), new int[]
            {
                AAMod.instance.Find<ModItem>("ShinyCharm").Type,
                AAMod.instance.Find<ModItem>("ShinyCharmFish").Type
            });
            RecipeGroup.RegisterGroup("AAMod:ShinyCharm", group22);

            RecipeGroup group23 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAMod.Common.DevBag"), new int[]
            {
                AAMod.instance.Find<ModItem>("AlphaBag").Type,
                AAMod.instance.Find<ModItem>("InvokerBag").Type,
                AAMod.instance.Find<ModItem>("CCBox").Type,
                AAMod.instance.Find<ModItem>("BlazenBag").Type,
                AAMod.instance.Find<ModItem>("AvesBag").Type,
                AAMod.instance.Find<ModItem>("DellyBag").Type,
                AAMod.instance.Find<ModItem>("OldMagiciansHat").Type,
                AAMod.instance.Find<ModItem>("MagiciansHat").Type,
                AAMod.instance.Find<ModItem>("LizBag").Type,
                AAMod.instance.Find<ModItem>("FezLordsBag").Type,
                AAMod.instance.Find<ModItem>("MoonBag").Type,
                AAMod.instance.Find<ModItem>("GibsBag").Type,
                AAMod.instance.Find<ModItem>("GroviteSeaChest").Type,
                AAMod.instance.Find<ModItem>("PlutoBag").Type,
                AAMod.instance.Find<ModItem>("VoidBag").Type,
                AAMod.instance.Find<ModItem>("AnarchyBag").Type,
                AAMod.instance.Find<ModItem>("MaskBag").Type,
                AAMod.instance.Find<ModItem>("TopHat").Type,
                AAMod.instance.Find<ModItem>("BegBag").Type,
                AAMod.instance.Find<ModItem>("CharlieBag").Type,
                AAMod.instance.Find<ModItem>("MikBag").Type,
                AAMod.instance.Find<ModItem>("TailsToolbox").Type,
                AAMod.instance.Find<ModItem>("ShoxBag").Type,
                AAMod.instance.Find<ModItem>("ApawnEgg").Type
            });
            RecipeGroup.RegisterGroup("AAMod:DevBag", group23);

            RecipeGroup group24 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + "Terra Boots", new int[]
            {
                AAMod.instance.Find<ModItem>("NightsGreaves").Type,
                AAMod.instance.Find<ModItem>("FleshrendGreaves").Type,
                AAMod.instance.Find<ModItem>("TribalKilt").Type,
                AAMod.instance.Find<ModItem>("DeathlyGreaves").Type,
                AAMod.instance.Find<ModItem>("DemonBoots").Type
            });
            RecipeGroup.RegisterGroup("AAMod:TerraBoots", group24);

            RecipeGroup group25 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + "Terra Chestplate", new int[]
            {
                AAMod.instance.Find<ModItem>("NightsPlate").Type,
                AAMod.instance.Find<ModItem>("FleshrendPlate").Type,
                AAMod.instance.Find<ModItem>("TribalCloak").Type,
                AAMod.instance.Find<ModItem>("DeathlyRibguard").Type,
                AAMod.instance.Find<ModItem>("DemonGarb").Type
            });
            RecipeGroup.RegisterGroup("AAMod:TerraPlates", group25);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.Find<ModItem>("Razewood").Type);
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.Find<ModItem>("Bogwood").Type);
                RecipeGroup.recipeGroups[index].ValidItems.Add(AAMod.instance.Find<ModItem>("OroborosWood").Type);
            }
        }
    }
}
