using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BogwoodHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Bogwood";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bogwood Helmet");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = 1000;
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BogwoodChestplate>() && legs.type == ModContent.ItemType<BogwoodLeggings>();
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new DefenseEffect(1));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Bogwood>(), 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}