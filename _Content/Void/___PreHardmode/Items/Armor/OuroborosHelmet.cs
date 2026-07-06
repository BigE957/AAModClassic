using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OuroborosHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Ouroboros";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ouroboros Wood Helmet");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = 1000;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 4;
        }
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OuroborosChestplate>() && legs.type == ModContent.ItemType<OuroborosLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            AddSetEffect(new DefenseEffect(3));
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<OuroborosWood>(), 20);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}