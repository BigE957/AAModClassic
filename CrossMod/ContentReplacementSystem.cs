using AAModClassic.Items.Blocks;
using AAModClassic.Items.Boss.Broodmother;
using AAModClassic.Items.Boss.Hydra;
using AAModClassic.Items.Boss.MushroomMonarch;
using AAModClassic.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.CrossMod
{
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

        internal static HashSet<int> ReplacedItems =>
        [
            ModContent.ItemType<Incinerite>(),
            ModContent.ItemType<IncineriteBar>(),
            ModContent.ItemType<Abyssium>(),
            ModContent.ItemType<AbyssiumBar>(),
        ];

        public override void OnModLoad()
        {
            if(ModLoader.TryGetMod("AAMod", out var newAA))
            {
                NewAA = newAA;

                NewAA.TryFind<ModBiome>("InfernoSurfaceBiome", out NewInfernoSurface);
                NewAA.TryFind<ModBiome>("InfernoUndergroundBiome", out NewInfernoUnderground);

                NewAA.TryFind<ModBiome>("MireSurfaceBiome", out NewMireSurface);
                NewAA.TryFind<ModBiome>("MireUndergroundBiome", out NewMireUnderground);

                #region Materials
                OldToNewItems.Add(ModContent.ItemType<Incinerite>(), NewAA.Find<ModItem>("IncineriteOre").Type);
                OldToNewItems.Add(ModContent.ItemType<Abyssium>(), NewAA.Find<ModItem>("AbyssiumOre").Type);
                OldToNewItems.Add(ModContent.ItemType<IncineriteBar>(), NewAA.Find<ModItem>("IncineriteBar").Type);
                OldToNewItems.Add(ModContent.ItemType<AbyssiumBar>(), NewAA.Find<ModItem>("AbyssiumBar").Type);

                OldToNewItems.Add(ModContent.ItemType<MirePod>(), NewAA.Find<ModItem>("BeastScales").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonScale>(), NewAA.Find<ModItem>("DragonScale").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonClaw>(), NewAA.Find<ModItem>("ChaosPowder").Type);
                OldToNewItems.Add(ModContent.ItemType<HydraClaw>(), NewAA.Find<ModItem>("ChaosPowder").Type);

                OldToNewItems.Add(ModContent.ItemType<BroodScale>(), NewAA.Find<ModItem>("ScorchedScale").Type);
                OldToNewItems.Add(ModContent.ItemType<HydraHide>(), NewAA.Find<ModItem>("LurkerHide").Type);

                OldToNewItems.Add(ModContent.ItemType<Hotshroom>(), NewAA.Find<ModItem>("InfernoShroom").Type);
                OldToNewItems.Add(ModContent.ItemType<Darkshroom>(), NewAA.Find<ModItem>("MireShroom").Type);

                OldToNewItems.Add(ModContent.ItemType<MushiumBar>(), NewAA.Find<ModItem>("BlightShroom").Type);
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
                    if (ReplacedItems.Contains(recipe.createItem.type))
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

                    recipe.AddIngredient(newItem, stack);
                    shouldAdd = true;
                }
            }

            return shouldAdd;
        }
    }
}
