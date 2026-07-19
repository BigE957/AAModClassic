using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DeathlyHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Deathly";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Skull");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DeathlyChestplate>() && legs.type == ModContent.ItemType<DeathlyLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Ranged) += 0.09f;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddSetEffect(new AggroEffect(-500));
            else
                AddSetEffect(new AggroEffect(-5));
            AddSetEffect<AmmoCost80Effect>();
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroHelmet, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddRecipeGroup("AAModClassic:EvilMaterial", 5);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}