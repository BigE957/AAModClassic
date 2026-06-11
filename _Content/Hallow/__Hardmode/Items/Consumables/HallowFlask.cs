using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Consumables
{
    public class HallowFlask : BaseAAItem
	{
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = ProjectileID.HallowSpray;
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
            // DisplayName.SetDefault("Crystal Flask");
            // Tooltip.SetDefault(@"Spreads the Hallow");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<HallowFlask_Proj>();
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = ProjectileID.HallowSpray;
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }
    }
}
