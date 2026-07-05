using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Hell.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ImpChestplate : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Imp";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Imp Garb");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = 7000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.07f;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 7);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
        }
    }
}