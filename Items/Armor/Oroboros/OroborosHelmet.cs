using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Head)]
    public class OroborosHelmet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oroboros Wood Helmet");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = 1000;
            Item.rare = 3;
            Item.defense = 4;
        }
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("OroborosChestplate").Type && legs.type == Mod.Find<ModItem>("OroborosBoots").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.OroborosHelmetBonus");
            player.statDefense += 3;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "OroborosWood", 20);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }
    }
}