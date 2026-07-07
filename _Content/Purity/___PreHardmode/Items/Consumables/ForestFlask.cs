using AAModClassic._Content.Purity.__Hardmode.Items.Consumables;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Purity.___PreHardmode.Items.Consumables
{
    public class ForestFlask : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Consumables";
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = ModContent.ProjectileType<DeepGreenSolution_Proj>();
            Item.shootSpeed = 1f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Forest Flask");
            // Tooltip.SetDefault(@"Converts Jungle to Forest");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                Item.shoot = ModContent.ProjectileType<ForestFlask_Proj>();
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<DeepGreenSolution_Proj>();
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }
    }
}