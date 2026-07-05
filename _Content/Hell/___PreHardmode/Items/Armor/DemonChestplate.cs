using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonChestplate : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Demon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Garb");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = 9000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.09f;
            AddEffect(new MaxMinionSlotEffect(2));
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<ImpChestplate>(), 1);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddRecipeGroup("AAModClassic:EvilMaterial", 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}