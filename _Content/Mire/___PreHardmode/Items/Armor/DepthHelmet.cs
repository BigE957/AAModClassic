using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DepthHelmet : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Depth";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Depth Fukumen");
            /* Tooltip.SetDefault(@"'Weightless as shadow itself'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 7500;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DepthChestplate>() && legs.type == ModContent.ItemType<DepthLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Ranged) += .08f;
            AddEffect(new MovementSpeedEffect(0.25f));

            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Ranged, (BuffID.Poisoned, 180)));
            AddSetEffect(new AggroEffect(-3));
            AddSetEffect<AmmoCost80Effect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}