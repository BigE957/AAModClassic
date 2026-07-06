using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class RazewoodHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Razewood";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Razewood Helmet");
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
            return body.type == ModContent.ItemType<RazewoodChestplate>() && legs.type == ModContent.ItemType<RazewoodLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            AddSetEffect(new DefenseEffect(1));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Razewood>(), 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}