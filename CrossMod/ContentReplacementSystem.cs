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
        private static Mod newAA = null;

        public static bool NewAAPresent => newAA != null;

        public static bool NeedToReplaceContent => NewAAPresent && AAConfigClient.Instance.EnableContentReplacement;

        internal static readonly Dictionary<int, int> OldToNewItems = [];

        internal static HashSet<int> ReplacedItems =>
        [
            ModContent.ItemType<Incinerite>(),
            ModContent.ItemType<IncineriteBar>(),
            ModContent.ItemType<Abyssium>(),
            ModContent.ItemType<AbyssiumBar>(),
        ];

        public override void OnModLoad()
        {
            if(ModLoader.TryGetMod("AAMod", out newAA))
            {
                #region Materials
                OldToNewItems.Add(ModContent.ItemType<Incinerite>(), newAA.Find<ModItem>("IncineriteOre").Type);
                OldToNewItems.Add(ModContent.ItemType<Abyssium>(), newAA.Find<ModItem>("AbyssiumOre").Type);
                OldToNewItems.Add(ModContent.ItemType<IncineriteBar>(), newAA.Find<ModItem>("IncineriteBar").Type);
                OldToNewItems.Add(ModContent.ItemType<AbyssiumBar>(), newAA.Find<ModItem>("AbyssiumBar").Type);

                OldToNewItems.Add(ModContent.ItemType<MirePod>(), newAA.Find<ModItem>("BeastScales").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonScale>(), newAA.Find<ModItem>("DragonScale").Type);

                OldToNewItems.Add(ModContent.ItemType<DragonClaw>(), newAA.Find<ModItem>("ChaosPowder").Type);
                OldToNewItems.Add(ModContent.ItemType<HydraClaw>(), newAA.Find<ModItem>("ChaosPowder").Type);

                OldToNewItems.Add(ModContent.ItemType<BroodScale>(), newAA.Find<ModItem>("ScorchedScale").Type);
                OldToNewItems.Add(ModContent.ItemType<HydraHide>(), newAA.Find<ModItem>("LurkerHide").Type);

                OldToNewItems.Add(ModContent.ItemType<Hotshroom>(), newAA.Find<ModItem>("InfernoShroom").Type);
                OldToNewItems.Add(ModContent.ItemType<Darkshroom>(), newAA.Find<ModItem>("MireShroom").Type);

                OldToNewItems.Add(ModContent.ItemType<MushiumBar>(), newAA.Find<ModItem>("BlightShroom").Type);
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
