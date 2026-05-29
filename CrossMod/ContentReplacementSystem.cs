using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.BossStandard;
using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.Accessories;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Accessories;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Armor;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Tools;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.BossStandard;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Armor;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tools;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Weapons;
using AAModClassic.Items.Blocks;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod
{
    //Based on Calamity: Fables' "ParasiteCore"
    public class ContentReplacementSystem : ModSystem
    {
        public static Mod NewAA { get; private set; } = null;

        public static bool NewAAPresent => NewAA != null;

        public static bool NeedToReplaceContent => NewAAPresent && AAConfigClient.Instance.EnableContentReplacement;

        internal static readonly Dictionary<int, int> OldToNewItems = [];

        private static ModBiome NewInfernoSurface = null;
        private static ModBiome NewInfernoUnderground = null;

        public static bool InNewInferno(Player p) => ContentReplacementSystem.NewAAPresent && (p.InModBiome(ContentReplacementSystem.NewInfernoSurface) || p.InModBiome(ContentReplacementSystem.NewInfernoUnderground));

        private static ModBiome NewMireSurface = null;
        private static ModBiome NewMireUnderground = null;

        public static bool InNewMire(Player p) => ContentReplacementSystem.NewAAPresent && (p.InModBiome(ContentReplacementSystem.NewMireSurface) || p.InModBiome(ContentReplacementSystem.NewMireUnderground));

        internal static HashSet<int> RemovedItems =>
        [
            #region Materials
            ModContent.ItemType<IncineriteOre>(),
            ModContent.ItemType<IncineriteBar>(),
            ModContent.ItemType<MirePod>(),
            ModContent.ItemType<DragonScale>(),
            ModContent.ItemType<_Content.Inferno.___PreHardmode.Items.Materials.DragonClaw_Item>(),
            ModContent.ItemType<_Content.Mire.___PreHardmode.Items.Materials.HydraClaw_Item>(),
            ModContent.ItemType<Hotshroom>(),
            ModContent.ItemType<Darkshroom>(),
            #endregion

            #region Boss Summons
            ModContent.ItemType<IntimidatingLookingMushroom>(),
            ModContent.ItemType<ConfusingLookingMushroom>(),

            ModContent.ItemType<CuriousLookingClaw>(),
            ModContent.ItemType<InterestingLookingClaw>(),

            ModContent.ItemType<Toadstool>(),
            #endregion

            #region Boss Drops
            ModContent.ItemType<Mushium>(),
            ModContent.ItemType<MushiumBar>(),
            ModContent.ItemType<Mushmace>(),
            ModContent.ItemType<Musharang>(),
            ModContent.ItemType<MushroomBow>(),
            ModContent.ItemType<Mushpick>(),
            ModContent.ItemType<Mushmallet>(),

            ModContent.ItemType<GlowingMushium>(),
            ModContent.ItemType<GlowingMushiumBar>(),
            ModContent.ItemType<GlowingMushpick>(),
            ModContent.ItemType<GlowingMushmallet>(),

            ModContent.ItemType<ClawBaton>(),
            #endregion

            #region Armor
            ModContent.ItemType<RazewoodHelmet>(),
            ModContent.ItemType<RazewoodChestplate>(),
            ModContent.ItemType<RazewoodLeggings>(),

            ModContent.ItemType<BogwoodHelmet>(),
            ModContent.ItemType<BogwoodChestplate>(),
            ModContent.ItemType<BogwoodLeggings>(),

            ModContent.ItemType<MushiumHelmet>(),
            ModContent.ItemType<MushiumChestplate>(),
            ModContent.ItemType<MushiumLeggings>(),

            ModContent.ItemType<GlowingMushiumHelmet>(),
            ModContent.ItemType<GlowingMushiumChestplate>(),
            ModContent.ItemType<GlowingMushiumLeggings>(),
            #endregion

            #region Useless Items
            ModContent.ItemType<AshProofVest3>(),
            ModContent.ItemType<Lantern>(),
            ModContent.ItemType<FurnitureDynamo>(),
            #endregion
        ];

        public override void Load()
        {
            if (ModLoader.TryGetMod("AAMod", out var newAA))
                NewAA = newAA;
        }

        public override void OnModLoad()
        {
            if(NewAA != null)
            {
                NewAA.TryFind<ModBiome>("InfernoSurfaceBiome", out NewInfernoSurface);
                NewAA.TryFind<ModBiome>("InfernoUndergroundBiome", out NewInfernoUnderground);

                NewAA.TryFind<ModBiome>("MireSurfaceBiome", out NewMireSurface);
                NewAA.TryFind<ModBiome>("MireUndergroundBiome", out NewMireUnderground);

                #region Old To New

                #region Blocks
                //Biome tiles
                OldToNewItems.Add(ModContent.ItemType<Bogwood>(), NewAA.Find<ModItem>("Bogwood").Type);
                OldToNewItems.Add(ModContent.ItemType<Depthstone>(), NewAA.Find<ModItem>("Depthstone").Type);
                OldToNewItems.Add(ModContent.ItemType<Darkmud>(), NewAA.Find<ModItem>("Darkmud").Type);
                OldToNewItems.Add(ModContent.ItemType<Depthsand>(), NewAA.Find<ModItem>("Depthsand").Type);
                //OldToNewItems.Add(ModContent.ItemType<Depthsandstone>(), NewAA.Find<ModItem>("Depthsandstone").Type);
                OldToNewItems.Add(ModContent.ItemType<DepthsandHardened>(), NewAA.Find<ModItem>("HardenedDepthsand").Type);

                OldToNewItems.Add(ModContent.ItemType<Razewood>(), NewAA.Find<ModItem>("Razewood").Type);
                OldToNewItems.Add(ModContent.ItemType<Torchstone>(), NewAA.Find<ModItem>("Torchstone").Type);
                OldToNewItems.Add(ModContent.ItemType<TorchAsh>(), NewAA.Find<ModItem>("TorchAsh").Type);
                OldToNewItems.Add(ModContent.ItemType<Torchice>(), NewAA.Find<ModItem>("Torchice").Type);
                OldToNewItems.Add(ModContent.ItemType<Torchsand>(), NewAA.Find<ModItem>("Torchsand").Type);
                OldToNewItems.Add(ModContent.ItemType<Torchsandstone>(), NewAA.Find<ModItem>("Torchsandstone").Type);
                OldToNewItems.Add(ModContent.ItemType<TorchsandHardened>(), NewAA.Find<ModItem>("HardenedTorchsand").Type);

                //Crafters
                OldToNewItems.Add(ModContent.ItemType<FurnitureDynamo>(), NewAA.Find<ModItem>("FurnitureDynamo").Type);

                //Trophies (Maybe used inr ecipes? I don't know)
                OldToNewItems.Add(ModContent.ItemType<MireGripTrophy>(), NewAA.Find<ModItem>("MireGripTrophy").Type);
                OldToNewItems.Add(ModContent.ItemType<InfernoGripTrophy>(), NewAA.Find<ModItem>("InfernoGripTrophy").Type);
                OldToNewItems.Add(ModContent.ItemType<MushroomMonarchTrophy>(), NewAA.Find<ModItem>("MonarchTrophy").Type);
                OldToNewItems.Add(ModContent.ItemType<FeudalFungusTrophy>(), NewAA.Find<ModItem>("MonarchTrophy").Type);
                OldToNewItems.Add(ModContent.ItemType<TruffleToadTrophy>(), NewAA.Find<ModItem>("ToadTrophy").Type);
                #endregion

                #region Materials
                OldToNewItems.Add(ModContent.ItemType<IncineriteOre>(), NewAA.Find<ModItem>("IncineriteOre").Type);
                //OldToNewItems.Add(ModContent.ItemType<Abyssium>(), NewAA.Find<ModItem>("AbyssiumOre").Type);
                OldToNewItems.Add(ModContent.ItemType<IncineriteBar>(), NewAA.Find<ModItem>("IncineriteBar").Type);
                //OldToNewItems.Add(ModContent.ItemType<AbyssiumBar>(), NewAA.Find<ModItem>("AbyssiumBar").Type);

                OldToNewItems.Add(ModContent.ItemType<MirePod>(), NewAA.Find<ModItem>("BeastScales").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonScale>(), NewAA.Find<ModItem>("DragonScale").Type);

                OldToNewItems.Add(ModContent.ItemType<_Content.Inferno.___PreHardmode.Items.Materials.DragonClaw_Item>(), NewAA.Find<ModItem>("ChaosPowder").Type);
                OldToNewItems.Add(ModContent.ItemType<_Content.Mire.___PreHardmode.Items.Materials.HydraClaw_Item>(), NewAA.Find<ModItem>("ChaosPowder").Type);

                //OldToNewItems.Add(ModContent.ItemType<BroodScale>(), NewAA.Find<ModItem>("ScorchedScale").Type);
                //OldToNewItems.Add(ModContent.ItemType<HydraHide>(), NewAA.Find<ModItem>("LurkerHide").Type);

                OldToNewItems.Add(ModContent.ItemType<Hotshroom>(), NewAA.Find<ModItem>("InfernoShroom").Type);
                OldToNewItems.Add(ModContent.ItemType<Darkshroom>(), NewAA.Find<ModItem>("MireShroom").Type);

                OldToNewItems.Add(ModContent.ItemType<MushiumBar>(), NewAA.Find<ModItem>("BlightShroom").Type);
                OldToNewItems.Add(ModContent.ItemType<GlowingMushiumBar>(), NewAA.Find<ModItem>("Biomass").Type);
                #endregion

                OldToNewItems.Add(ModContent.ItemType<ClawBaton>(), NewAA.Find<ModItem>("HelpingHands").Type);

                #endregion
            }
        }

        public override void PostAddRecipes()
        {
            if (!NewAAPresent)
                return;


            List<Recipe> newRecipes = [];

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (AAConfigClient.Instance.EnableContentReplacement)
                {
                    if (RemovedItems.Contains(recipe.createItem.type))
                        recipe.DisableRecipe();
                    else
                        recipe.Modernize();
                }
                else
                {
                    Recipe modernVer = recipe.Clone();
                    if (modernVer.Modernize())
                        newRecipes.Add(modernVer);
                }
            }

            foreach (Recipe recipe in newRecipes)
                recipe.Register();

            if (!NeedToReplaceContent)
                return;

            #region Shimmer Transmutes
            //Boss Items
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<TruffleLegs>()] = NewAA.Find<ModItem>("TruffleLeg").Type;
            ItemID.Sets.ShimmerTransformToItem[NewAA.Find<ModItem>("TruffleLeg").Type] = ModContent.ItemType<HeartyTruffle>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<HeartyTruffle>()] = ModContent.ItemType<GlowingTruffle>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<GlowingTruffle>()] = ModContent.ItemType<TruffleLegs>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ClawOfChaos>()] = NewAA.Find<ModItem>("TwinClawPendant").Type;

            //Tiles
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<IncineriteOre>()] = NewAA.Find<ModItem>("IncineriteOre").Type;
            ItemID.Sets.ShimmerTransformToItem[NewAA.Find<ModItem>("IncineriteOre").Type] = ModContent.ItemType<IncineriteOre>();
            #endregion
        }


    }

    public class ReplacementGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }

    internal static class ReplacementUtils
    {
        internal static bool Modernize(this Recipe recipe)
        {
            bool shouldAdd = false;

            for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
            {
                int oldID = recipe.requiredItem[i].type;
                int stack = recipe.requiredItem[i].stack;

                if (ContentReplacementSystem.OldToNewItems.TryGetValue(oldID, out int newID))
                {
                    recipe.RemoveIngredient(recipe.requiredItem[i]);

                    ModItem newItem = ItemLoader.GetItem(newID);
                    if (newItem.Item.maxStack < stack)
                        stack = newItem.Item.maxStack;

                    if (recipe.HasIngredient(newID))
                    {
                        for (int j = 0; j < recipe.requiredItem.Count; j++)
                        {
                            if (recipe.requiredItem[j].type == newID)
                                recipe.requiredItem[j].stack += stack;
                            if (recipe.requiredItem[j].stack > recipe.requiredItem[j].maxStack)
                                recipe.requiredItem[j].stack = recipe.requiredItem[j].maxStack;
                        }
                    }
                    else
                        recipe.AddIngredient(newItem, stack);
                    shouldAdd = true;

                    ModContent.GetInstance<AAMod>().Logger.Info($"Recipe Hit: Item {oldID} has been replaced by Item {newID}");
                }
            }

            return shouldAdd;
        }

        public static void Modernize(this ILoot loot)
        {
            var rules = loot.Get();
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                if (!rules[i].Modernize())
                    rules[i] = new DropNothing();
            }
        }

        private static bool Modernize(this IItemDropRule rule)
        {
            if (rule is CommonDrop drop)
            {
                if (ContentReplacementSystem.OldToNewItems.TryGetValue(drop.itemId, out int newID))
                    drop.itemId = newID;
                else if (ContentReplacementSystem.RemovedItems.Contains(drop.itemId))
                    return false;
                return true;
            }
            else if (rule is ItemDropWithConditionRule conditionalDrop)
            {
                if (ContentReplacementSystem.OldToNewItems.TryGetValue(conditionalDrop.itemId, out int newID))
                    conditionalDrop.itemId = newID;
                else if (ContentReplacementSystem.RemovedItems.Contains(conditionalDrop.itemId))
                    return false;
                return true;
            }
            else if (rule is DropBasedOnExpertMode expertDrop)
            {
                if (!expertDrop.ruleForNormalMode.Modernize())
                    expertDrop.ruleForNormalMode = new DropNothing();
                if (!expertDrop.ruleForExpertMode.Modernize())
                    expertDrop.ruleForExpertMode = new DropNothing();

                return true;
            }
            else if (rule is DropBasedOnMasterMode masterDrop)
            {
                if (!masterDrop.ruleForDefault.Modernize())
                    masterDrop.ruleForDefault = new DropNothing();
                if (!masterDrop.ruleForMasterMode.Modernize())
                    masterDrop.ruleForMasterMode = new DropNothing();

                return true;
            }
            else if (rule is LeadingConditionRule lcr)
            {
                for (int i = lcr.ChainedRules.Count - 1; i >= 0; i--)
                {
                    if (!lcr.ChainedRules[i].RuleToChain.Modernize())
                        lcr.ChainedRules.RemoveAt(i);
                }
            }
            return true;
        }
    }
}
