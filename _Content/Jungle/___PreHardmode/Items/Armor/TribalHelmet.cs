using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Jungle.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TribalHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Tribal";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tribal Hat");
            /* Tooltip.SetDefault(@"8% Increased magic critical chance
Increases maximum mana by 20"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TribalChestplate>() && legs.type == ModContent.ItemType<TribalLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetCritChance(DamageClass.Magic) += 8;
            AddEffect(new MaxManaEffect(20));

            AddSetEffect(new ManaCostMultiplierEffect(0.7f));
            AddSetEffect<ManaFlowerEffect>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddRecipeGroup("AAModClassic:EvilMaterial", 8);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}