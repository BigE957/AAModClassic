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
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Jungle.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
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
    public class AARecipes : ModSystem
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

        public override void AddRecipes()
        {
            AAMod.instance.Logger.Info("Handling AA Recipes...");

            RemoveNightsEdgeRecipe();
            AddMusicBoxRecipes();
            AddPotionRecipes();
            AddMushroomPotionRecipes();
            AddModdedMushroomPotionRecipes();
            RemoveZenithRecipe();

            #region Materials
            GetNewRecipe(ItemID.HallowedBar, 1)
            .AddIngredient(ModContent.ItemType<HallowedOre>(), 4)
            .AddTile(ModContent.TileType<HallowedForge_Tile>())
            .Register();

             GetNewRecipe(ItemID.ShroomiteBar, 1)
            .AddIngredient(ModContent.ItemType<MushiumBar>(), 1)
            .AddIngredient(ItemID.GlowingMushroom, 5)
            .AddTile(TileID.Autohammer)
            .Register();

             GetNewRecipe(ItemID.Mushroom, 3)
            .AddIngredient(ModContent.ItemType<MushroomBlock>())
            .AddTile(TileID.WorkBenches)
            .Register();
            #endregion

            #region Equipment
             GetNewRecipe(ItemID.Zenith, 1)
            .AddIngredient(ModContent.ItemType<TrueTerraBlade>())
            .AddIngredient(ModContent.ItemType<TheLolkat>())
            .AddIngredient(ModContent.ItemType<CosmicFury>())
            .AddIngredient(ItemID.InfluxWaver)
            .AddIngredient(ModContent.ItemType<Apocalypse>())
            .AddIngredient(ItemID.Seedler)
            .AddIngredient(ItemID.Starfury)
            .AddIngredient(ItemID.BeeKeeper)
            .AddIngredient(ItemID.Terragrim)
            .AddIngredient(ModContent.ItemType<PrismaticGreatsword>())
            .AddIngredient(ModContent.ItemType<BladeOfEvil>())
            .AddIngredient(ModContent.ItemType<Ikari>())
            .AddIngredient(ModContent.ItemType<InfinityBlade>()) //TODO: make this rift shredder in non-unofficial worlds... somehow
            .AddIngredient(ModContent.ItemType<Verdict>())
            .AddIngredient(ModContent.ItemType<SagittariusLeg>())
            .AddIngredient(ModContent.ItemType<RomulusTazesaber>())
            .AddIngredient(ModContent.ItemType<SubzeroSlasher>())
            .AddIngredient(ModContent.ItemType<Olympia>())
            .AddIngredient(ModContent.ItemType<Excalihare>())
            .AddIngredient(ModContent.ItemType<CarnalCrusher>())
            .AddIngredient(ModContent.ItemType<UltimaShortsword>())
            .AddIngredient(ModContent.ItemType<EXSoul>())
            .AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>())
            .Register();

             GetNewRecipe(ItemID.TerraBlade, 1)
            .AddIngredient(ModContent.ItemType<TrueFleshrendClaymore>(), 1)
            .AddIngredient(ItemID.TrueExcalibur, 1)
            .AddIngredient(ItemID.BrokenHeroSword, 1)
            .AddTile(TileID.MythrilAnvil)
            .Register();

             GetNewRecipe(ItemID.IceBlade)
            .AddIngredient(ItemID.IceBlock, 30)
            .AddIngredient(ItemID.Diamond, 1)
            .AddIngredient(ItemID.Sapphire, 1)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.Starfury)
            .AddIngredient(ItemID.PlatinumBroadsword)
            .AddIngredient(ItemID.FallenStar, 10)
            .AddIngredient(ItemID.ManaCrystal)
            .AddTile(TileID.Anvils)
            .Register();
            
             GetNewRecipe(ItemID.Starfury)
            .AddIngredient(ItemID.GoldBroadsword)
            .AddIngredient(ItemID.FallenStar, 10)
            .AddIngredient(ItemID.ManaCrystal)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.EnchantedSword)
            .AddIngredient(ItemID.PlatinumBroadsword)
            .AddIngredient(ItemID.ManaCrystal, 3)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.EnchantedSword)
            .AddIngredient(ItemID.GoldBroadsword)
            .AddIngredient(ItemID.ManaCrystal, 3)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.Terragrim)
            .AddIngredient(ItemID.EnchantedSword)
            .AddIngredient(ItemID.Muramasa)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.CobaltShield)
            .AddIngredient(ItemID.CobaltBar, 5)
            .AddTile(TileID.Anvils)
            .Register();

             GetNewRecipe(ItemID.ObsidianShield)
            .AddIngredient(ModContent.ItemType<PalladiumShield>())
            .AddIngredient(ItemID.ObsidianSkull)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

             GetNewRecipe(ItemID.GravityGlobe, 1)
            .AddIngredient(ItemID.SnowGlobe, 1)
            .AddIngredient(ItemID.SoulofFlight, 5)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ItemID.SoulofLight, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();

             GetNewRecipe(ItemID.MagicMirror)
            .AddIngredient(ItemID.Glass, 10)
            .AddIngredient(ItemID.RecallPotion, 10)
            .AddTile(TileID.GlassKiln)
            .Register();

             GetNewRecipe(ItemID.IceMirror)
            .AddIngredient(ItemID.IceBrick, 10)
            .AddIngredient(ItemID.RecallPotion, 10)
            .AddTile(TileID.IceMachine)
            .Register();
            #endregion

            #region Miscellaneous
             GetNewRecipe(ItemID.GuideVoodooDoll, 1)
            .AddIngredient(ModContent.ItemType<DevilSilk>(), 5)
            .AddIngredient(ItemID.Hay, 5)
            .AddTile(TileID.Loom)
            .Register();

             GetNewRecipe(ItemID.SnowGlobe, 1)
            .AddIngredient(ItemID.Glass, 10)
            .AddIngredient(ItemID.SnowBlock, 10)
            .AddRecipeGroup("Wood")
            .AddTile(TileID.GlassKiln)
            .Register();

             GetNewRecipe(ItemID.RodofDiscord)
            .AddIngredient(ItemID.SoulofLight, 60)
            .AddIngredient(ItemID.Pearlwood, 5)
            .AddIngredient(ItemID.CrystalShard, 30)
            .AddTile(TileID.MythrilAnvil)
            .Register();

             GetNewRecipe(ItemID.FragmentNebula)
            .AddIngredient(ModContent.ItemType<RadiumBar>(), 1)
            .AddIngredient(ItemID.LunarOre, 3)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

             GetNewRecipe(ItemID.FragmentSolar)
            .AddIngredient(ModContent.ItemType<RadiumBar>(), 1)
            .AddIngredient(ItemID.LunarOre, 3)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

             GetNewRecipe(ItemID.FragmentStardust)
            .AddIngredient(ModContent.ItemType<DarkmatterBar>(), 1)
            .AddIngredient(ItemID.LunarOre, 3)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

             GetNewRecipe(ItemID.FragmentVortex)
            .AddIngredient(ModContent.ItemType<DarkmatterBar>(), 1)
            .AddIngredient(ItemID.LunarOre, 3)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

             GetNewRecipe(ItemID.LavaBucket)
            .AddIngredient(ItemID.EmptyBucket, 1)
            .AddTile(ModContent.TileType<RazewoodSink_Tile>())
            .Register();
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
            GetNewRecipe(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Wood, 30)
            .AddIngredient(ItemID.IronBar, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Wood, 30)
            .AddIngredient(ItemID.LeadBar, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxOverworldDay, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GrassSeeds, 10)
            .AddIngredient(ItemID.DirtBlock, 10)
            .AddIngredient(ItemID.Wood, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxAltOverworldDay, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GrassSeeds, 10)
            .AddIngredient(ItemID.DirtBlock, 10)
            .AddIngredient(ItemID.Wood, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxNight, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Lens, 3)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxRain, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.BottledWater, 5)
            .AddIngredient(ItemID.UmbrellaHat, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxSnow, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.SnowBlock, 30)
            .AddIngredient(ItemID.BorealWood, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxIce, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.IceBlock, 30)
            .AddIngredient(ItemID.BorealWood, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxDesert, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.SandBlock, 40)
            .AddIngredient(ItemID.Cactus, 15)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxSandstorm, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.AncientBattleArmorMaterial, 1)
            .AddIngredient(ItemID.SharkFin, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxOcean, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Coral, 3)
            .AddIngredient(ItemID.Starfish, 3)
            .AddIngredient(ItemID.Seashell, 3)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxUnderground, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.DirtBlock, 50)
            .AddIngredient(ItemID.IronOre, 10)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxUnderground, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.DirtBlock, 50)
            .AddIngredient(ItemID.LeadOre, 10)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxAltUnderground, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.DirtBlock, 50)
            .AddIngredient(ItemID.LeadOre, 10)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxAltUnderground, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.DirtBlock, 50)
            .AddIngredient(ItemID.IronOre, 10)
            .AddIngredient(ItemID.StoneBlock, 50)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxSpace, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Feather, 20)
            .AddIngredient(ItemID.SunplateBlock, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxMushrooms, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GlowingMushroom, 20)
            .AddIngredient(ItemID.Mushroom, 10)
            .AddIngredient(ItemID.MushroomGrassSeeds, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxJungle, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.MudBlock, 20)
            .AddIngredient(ItemID.JungleGrassSeeds, 5)
            .AddIngredient(ItemID.RichMahogany, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxCorruption, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.RottenChunk, 10)
            .AddIngredient(ItemID.CorruptSeeds, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxUndergroundCorruption, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.EbonstoneBlock, 30)
            .AddIngredient(ItemID.RottenChunk, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxCrimson, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Vertebrae, 10)
            .AddIngredient(ItemID.CrimsonSeeds, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxUndergroundCrimson, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.CrimstoneBlock, 30)
            .AddIngredient(ItemID.Vertebrae, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxTheHallow, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.CrystalShard, 10)
            .AddIngredient(ItemID.HallowedSeeds, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxUndergroundHallow, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.PearlstoneBlock, 30)
            .AddIngredient(ItemID.UnicornHorn, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxHell, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.AshBlock, 20)
            .AddIngredient(ItemID.Hellstone, 15)
            .AddIngredient(ItemID.ObsidianBrick, 10)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxDungeon, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.BlueBrick, 20)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxDungeon, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GreenBrick, 20)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxDungeon, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.PinkBrick, 20)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxTemple, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.TempleKey, 1)
            .AddIngredient(ItemID.LihzahrdBrick, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss1, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.ShadowScale, 15)
            .AddIngredient(ItemID.DemoniteBar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss1, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.SoulofFright, 10)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss2, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GuideVoodooDoll, 1)
            .AddIngredient(ModContent.ItemType<DevilSilk>(), 15)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss2, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.SoulofSight, 10)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss2, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.TissueSample, 15)
            .AddIngredient(ItemID.CrimtaneBar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss3, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.SoulofMight, 10)
            .AddIngredient(ItemID.HallowedBar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss4, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.BeetleHusk, 8)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxBoss5, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.BeeWax, 20)
            .AddIngredient(ItemID.BottledHoney, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxPlantera, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.JungleSpores, 10)
            .AddIngredient(ModContent.ItemType<PlanteraPetal>(), 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxEerie, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Meteorite, 20)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxEerie, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.Shackle, 1)
            .AddIngredient(ItemID.MoneyTrough, 1)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxEclipse, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.LunarTabletFragment, 8)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxGoblins, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.GoblinBattleStandard, 1)
            .AddIngredient(ItemID.SpikyBall, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxPirates, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.PirateMap, 1)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxMartians, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.MartianConduitPlating, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxPumpkinMoon, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.PumpkinMoonMedallion)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxFrostMoon, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.NaughtyPresent, 1)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxTowers, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.FragmentNebula, 3)
            .AddIngredient(ItemID.FragmentSolar, 3)
            .AddIngredient(ItemID.FragmentVortex, 3)
            .AddIngredient(ItemID.FragmentStardust, 3)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxLunarBoss, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.LunarOre, 30)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ItemID.MusicBoxDD2, 1)
            .AddIngredient(ItemID.MusicBox, 1)
            .AddIngredient(ItemID.DefenderMedal, 15)
            .AddTile(TileID.Sawmill)
            .Register();

             GetNewRecipe(ModContent.ItemType<AncientCoin>(), 5)
            .AddRecipeGroup("AAModClassic:DevBag")
            .Register();
        }

        #region Potions
        private static void AddPotionRecipes()
        {
            GetNewRecipe(ItemID.RagePotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddIngredient(ModContent.ItemType<DragonClaw_Item>(), 3)
            .AddIngredient(ModContent.ItemType<DragonScale>(), 1)
            .AddTile(TileID.Bottles)
            .Register();

             GetNewRecipe(ItemID.WrathPotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddIngredient(ModContent.ItemType<HydraClaw_Item>(), 3)
            .AddIngredient(ModContent.ItemType<MirePod>(), 1)
            .AddTile(TileID.Bottles)
            .Register();

             GetNewRecipe(ItemID.BattlePotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddIngredient(ModContent.ItemType<DragonScale>(), 1)
            .AddTile(TileID.Bottles)
            .Register();

             GetNewRecipe(ItemID.BattlePotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Deathweed, 1)
            .AddIngredient(ModContent.ItemType<MirePod>(), 1)
            .AddTile(TileID.Bottles)
            .Register();

             GetNewRecipe(ItemID.WaterWalkingPotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Waterleaf, 1)
            .AddIngredient(ModContent.ItemType<MirePod>(), 2)
            .AddTile(TileID.Bottles)
            .Register();

             GetNewRecipe(ItemID.ObsidianSkinPotion, 1)
            .AddIngredient(ItemID.BottledWater, 1)
            .AddIngredient(ItemID.Waterleaf, 1)
            .AddIngredient(ModContent.ItemType<DragonScale>(), 2)
            .AddTile(TileID.Bottles)
            .Register();
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

            foreach (Tuple<short, string[], int> potion in potions)
            {
                Recipe recipe = GetNewRecipe(potion.Item1, potion.Item3);
                foreach (var mushroom in potion.Item2)
                {
                    recipe.AddIngredient(null, mushroom);
                }
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddTile(TileID.Bottles);
                recipe.Register();

                // Rainbow s
                 GetNewRecipe(potion.Item1)
                .AddIngredient(ModContent.ItemType<RainbowMushroom>())
                .AddIngredient(ItemID.BottledWater)
                .AddTile(TileID.Bottles)
                .Register();
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

                foreach (Tuple<string, string[], int> potion in GRealmPotions)
                {
                    Recipe recipe = GetNewRecipe(GRealm, potion.Item1);
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

                    // Rainbow s
                    Recipe recipe2 = GetNewRecipe(GRealm, potion.Item1);
                    recipe2.AddIngredient(ModContent.ItemType<RainbowMushroom>());
                    if (potion.Item1 == "BloodbathPotion" || potion.Item1 == "ChitinPotion")
                    {
                        recipe2.AddIngredient(ItemID.BottledWater);
                    }
                    else
                    {
                        recipe2.AddIngredient(GRealm, "CosmicContainer");
                    }
                    recipe2.AddTile(TileID.Bottles);
                    recipe2.Register();
                }
            }
            #endregion
        }
        #endregion

        public override void AddRecipeGroups()
        {
            RecipeGroup Group = new RecipeGroup(() => "nothing", new int[]
            {
                ItemID.Snail
            });

            #region Ore
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Copper"), new int[]
            {
                ItemID.CopperOre,
                ItemID.TinOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:CopperOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Iron"), new int[]
            {
                ItemID.IronOre,
                ItemID.LeadOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:IronOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Silver"), new int[]
            {
                ItemID.SilverOre,
                ItemID.TungstenOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:SilverOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Gold"), new int[]
            {
                ItemID.GoldOre,
                ItemID.PlatinumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:GoldOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Evil"), new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Chaos"), new int[]
            {
                ModContent.ItemType<IncineriteOre>(),
                ModContent.ItemType<AbyssiumOre>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.EvilOrChaos"), new int[]
            {
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
                ModContent.ItemType<IncineriteOre>(),
                ModContent.ItemType<AbyssiumOre>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Cobalt"), new int[]
            {
                ItemID.CobaltOre,
                ItemID.PalladiumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:CobaltOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Mythril"), new int[]
            {
                ItemID.MythrilOre,
                ItemID.OrichalcumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:MythrilOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Adamantite"), new int[]
            {
                ItemID.AdamantiteOre,
                ItemID.TitaniumOre
            });
            RecipeGroup.RegisterGroup("AAModClassic:AdamantiteOre", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Ore.Hallowed"), new int[]
            {
                ModContent.ItemType<HallowedOre>(),
                ModContent.ItemType<FulguriteShard>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HallowedOre", Group);
            #endregion
            #region Bars
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Copper"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:CopperBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Iron"), new int[]
            {
                ItemID.IronBar,
                ItemID.LeadBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:IronBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Silver"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:SilverBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Gold"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:GoldBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Evil"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Chaos"), new int[]
            {
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.EvilOrChaos"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ModContent.ItemType<IncineriteBar>(),
                ModContent.ItemType<AbyssiumBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Cobalt"), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:CobaltBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Mythril"), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:MythrilBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Adamantite"), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("AAModClassic:AdamantiteBar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Bars.Hallowed"), new int[]
            {
                ItemID.HallowedBar,
                ModContent.ItemType<FulguriteBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HallowedBar", Group);
            #endregion
            #region Materials
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.ShinyCharm"), new int[]
            {
                ModContent.ItemType<ShinyCharm>(),
                ModContent.ItemType<ShinyCharmFish>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ShinyCharm", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.ChaosClaw"), new int[]
            {
                ModContent.ItemType<DragonClaw_Item>(),
                ModContent.ItemType<HydraClaw_Item>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosClaw", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Evil"), new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Chaos"), new int[]
            {
                ModContent.ItemType<ScorchedScale>(),
                ModContent.ItemType<HydraHide>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.EvilOrChaos"), new int[]
            {
                ItemID.ShadowScale,
                ItemID.TissueSample,
                ModContent.ItemType<ScorchedScale>(),
                ModContent.ItemType<HydraHide>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilOrChaosMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeEvil"), new int[]
            {
                ItemID.CursedFlame,
                ItemID.Ichor
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeEvilMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeChaos"), new int[]
            {
                ModContent.ItemType<DragonFire>(),
                ModContent.ItemType<Bogtoxin>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeChaosMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.HardmodeEvilOrChaos"), new int[]
            {
                ItemID.CursedFlame,
                ItemID.Ichor,
                ModContent.ItemType<DragonFire>(),
                ModContent.ItemType<Bogtoxin>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeEvilOrChaosMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.EarlyAncient"), new int[]
            {
                ModContent.ItemType<StormSphere>(),
                ModContent.ItemType<CovetiteBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EarlyAncientMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.LateAncient"), new int[]
            {
                ModContent.ItemType<UnstableSingularity>(),
                ModContent.ItemType<CrucibleScale>(),
                ModContent.ItemType<DreadScale>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:LateAncientMaterial", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Materials.Superancient"), new int[]
            {
                ModContent.ItemType<ChaosScale>(),
                ModContent.ItemType<Infinitium>(),
                ModContent.ItemType<RealityBar>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:SuperancientMaterial", Group);
            #endregion
            #region Crafting Stations
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.Altar"), new int[]
            {
                ModContent.ItemType<AbyssAltarSafe>(),
                ModContent.ItemType<CrimsonAltar>(),
                ModContent.ItemType<CorruptAltar>(),
                ModContent.ItemType<DragonAltarSafe>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:Altar", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.HardmodeAnvil"), new int[]
            {
                ItemID.MythrilAnvil,
                ItemID.OrichalcumAnvil
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeAnvil", Group);
            Group = new RecipeGroup(getName: () => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.HardmodeForge"), validItems: new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });
            RecipeGroup.RegisterGroup("AAModClassic:HardmodeForge", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.CelestialCraftingStation"), new int[]
            {
                ModContent.ItemType<RadiantArcanum>(),
                ModContent.ItemType<QuantumFusionAccelerator>(),
            });
            RecipeGroup.RegisterGroup("AAModClassic:CelestialCraftingStation", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.CraftingStations.AncientCraftingStation"), new int[]
            {
                ModContent.ItemType<BinaryReassembler>(),
                ModContent.ItemType<ChaosCrucible>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:AncientCraftingStation", Group);
            #endregion
            #region Weapons
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " +  Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Weapons.EvilStaff"), new int[]
            {
                ModContent.ItemType<DemoniteStaff>(),
                ModContent.ItemType<CrimeraStaff>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:EvilStaff", Group);
            #endregion
            #region Armor
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.TerraChestplate"), new int[]
            {
                ModContent.ItemType<NightsChestplate>(),
                ModContent.ItemType<FleshrendChestplate>(),
                ModContent.ItemType<TribalChestplate>(),
                ModContent.ItemType<DeathlyChestplate>(),
                ModContent.ItemType<DemonChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraChestplate", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.TerraLeggings"), new int[]
            {
                ModContent.ItemType<FleshrendLeggings>(),
                ModContent.ItemType<NightsLeggings>(),
                ModContent.ItemType<TribalLeggings>(),
                ModContent.ItemType<DeathlyLeggings>(),
                ModContent.ItemType<DemonLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:TerraLeggings", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.ChaosChestplate"), new int[]
            {
                ModContent.ItemType<BlazingChestplate>(),
                ModContent.ItemType<AbyssalChestplate>(),
                ModContent.ItemType<AtlanteanChestplate>(),
                ModContent.ItemType<DoomiteChestplate>(),
                ModContent.ItemType<RaiderChestplate>(),
                ModContent.ItemType<DynaskullChestplate>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosChestplate", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.ChaosLeggings"), new int[]
            {
                ModContent.ItemType<BlazingLeggings>(),
                ModContent.ItemType<AbyssalLeggings>(),
                ModContent.ItemType<AtlanteanLeggings>(),
                ModContent.ItemType<DoomiteLeggings>(),
                ModContent.ItemType<RaiderLeggings>(),
                ModContent.ItemType<DynaskullLeggings>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:ChaosLeggings", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.RadiumHelmet"), new int[]
            {
                ModContent.ItemType<RadiumHelmetSummoner>(),
                //ModContent.ItemType<RadiumHelm>(),
                ModContent.ItemType<RadiumHelmetMelee>(),
                ModContent.ItemType<RadiumHelmetRanged>(),
                ModContent.ItemType<RadiumHelmetMage>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:RadiumHelmet", Group);
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Armor.DarkmatterHelmet"), new int[]
            {
                ModContent.ItemType<DarkmatterHelmetRanged>(),
                //ModContent.ItemType<DarkmatterHelm>(),
                ModContent.ItemType<DarkmatterHelmetMelee>(),
                ModContent.ItemType<DarkmatterHelmetSummoner>(),
                ModContent.ItemType<DarkmatterHelmetMage>()
            });
            RecipeGroup.RegisterGroup("AAModClassic:DarkmatterHelmet", Group);
            #endregion
            #region Misc
            Group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue("Mods.AAModClassic.Common.RecipeGroups.Misc.DevBag"), new int[]
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
            RecipeGroup.RegisterGroup("AAModClassic:DevBag", Group);
            #endregion
            #region Vanilla Sets
            if (RecipeGroup.recipeGroupIDs.ContainsKey("Wood"))
            {
                int index = RecipeGroup.recipeGroupIDs["Wood"];
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Razewood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<Bogwood>());
                RecipeGroup.recipeGroups[index].ValidItems.Add(ModContent.ItemType<OuroborosWood>());
            }
            #endregion
        }
    }
}
