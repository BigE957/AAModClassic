using AAModClassic.Items.Armor.GlowingMushium;
using AAModClassic.Items.Armor.Mushium;
using AAModClassic.Items.Blocks;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Boss.Grips;
using AAModClassic.Items.Boss.Hydra;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Boss.Toad;
using AAModClassic.Items.BossSummons;
using AAModClassic.Items.Materials;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Throwing;
using AAModClassic.Items.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ModContent.ItemType<Incinerite>(),
            ModContent.ItemType<IncineriteBar>(),
            ModContent.ItemType<MirePod>(),
            ModContent.ItemType<DragonScale>(),
            ModContent.ItemType<AAModClassic.Items.Materials.DragonClaw>(),
            ModContent.ItemType<AAModClassic.Items.Materials.HydraClaw>(),
            ModContent.ItemType<Hotshroom>(),
            ModContent.ItemType<Darkshroom>(),
            #endregion

            #region Boss Summons
            ModContent.ItemType<IntimidatingMushroom>(),
            ModContent.ItemType<ConfusingMushroom>(),

            ModContent.ItemType<CuriousClaw>(),
            ModContent.ItemType<InterestingClaw>(),

            ModContent.ItemType<Toadstool>(),
            #endregion

            #region Boss Drops
            ModContent.ItemType<Mushium>(),
            ModContent.ItemType<MushiumBar>(),
            ModContent.ItemType<MushiumHat>(),
            ModContent.ItemType<MushiumShirt>(),
            ModContent.ItemType<MushiumPants>(),
            ModContent.ItemType<MushMace>(),
            ModContent.ItemType<Musharang>(),
            ModContent.ItemType<Mushbow>(),
            ModContent.ItemType<Mushpick>(),
            ModContent.ItemType<Mushmallet>(),

            ModContent.ItemType<GlowingMushium>(),
            ModContent.ItemType<GlowingMushiumBar>(),
            ModContent.ItemType<ShroomHat>(),
            ModContent.ItemType<ShroomShirt>(),
            ModContent.ItemType<ShroomPants>(),
            ModContent.ItemType<GlowMushpick>(),
            ModContent.ItemType<GlowMushmallet>(),

            ModContent.ItemType<ClawBaton>(),
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

                #region Materials
                OldToNewItems.Add(ModContent.ItemType<Incinerite>(), NewAA.Find<ModItem>("IncineriteOre").Type);
                //OldToNewItems.Add(ModContent.ItemType<Abyssium>(), NewAA.Find<ModItem>("AbyssiumOre").Type);
                OldToNewItems.Add(ModContent.ItemType<IncineriteBar>(), NewAA.Find<ModItem>("IncineriteBar").Type);
                //OldToNewItems.Add(ModContent.ItemType<AbyssiumBar>(), NewAA.Find<ModItem>("AbyssiumBar").Type);

                OldToNewItems.Add(ModContent.ItemType<MirePod>(), NewAA.Find<ModItem>("BeastScales").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonScale>(), NewAA.Find<ModItem>("DragonScale").Type);

                OldToNewItems.Add(ModContent.ItemType<AAModClassic.Items.Materials.DragonClaw>(), NewAA.Find<ModItem>("ChaosPowder").Type);
                OldToNewItems.Add(ModContent.ItemType<AAModClassic.Items.Materials.HydraClaw>(), NewAA.Find<ModItem>("ChaosPowder").Type);

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
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ToadLeg>()] = NewAA.Find<ModItem>("TruffleLeg").Type;
            ItemID.Sets.ShimmerTransformToItem[NewAA.Find<ModItem>("TruffleLeg").Type] = ModContent.ItemType<HeartyTruffle>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<HeartyTruffle>()] = ModContent.ItemType<MagicTruffle>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<MagicTruffle>()] = ModContent.ItemType<ToadLeg>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<ClawOfChaos>()] = NewAA.Find<ModItem>("TwinClawPendant").Type;
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

                    AAMod.instance.Logger.Info($"Recipe Hit: Item {oldID} has been replaced by Item {newID}");
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
