using AAModClassic._Content.Bunny.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class HoppingHoodlumChestplate : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.HoppingHoodlum";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hopping Hoodlum Shirt");
            /* Tooltip.SetDefault(@"'Hopping Mad'"); */
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 28;
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new WingTimeMaxEffect(180));
            damageMap.GetAttackSpeed(DamageClass.Melee) += .1f;
            AddEffect(new MaxMinionSlotEffect(1));
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new AggroEffect(200));
            else
                AddEffect(new AggroEffect(2));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RajahPelt>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}