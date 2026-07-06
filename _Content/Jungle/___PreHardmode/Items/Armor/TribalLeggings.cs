using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Jungle.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class TribalLeggings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Tribal";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tribal Kilt");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Magic) += 8;
            AddEffect(new MaxManaEffect(20));
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JunglePants, 1);
                recipe.AddRecipeGroup("AAModClassic:EvilMaterial", 6);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}