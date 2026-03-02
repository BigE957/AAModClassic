using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;


namespace AAModClassic.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Head)]
    public class RazewoodHelmet : BaseAAItem
    {
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
            return body.type == Mod.Find<ModItem>("RazewoodChestplate").Type && legs.type == Mod.Find<ModItem>("RazewoodBoots").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.RazewoodHelmet");
            player.statDefense += 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Razewood", 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}