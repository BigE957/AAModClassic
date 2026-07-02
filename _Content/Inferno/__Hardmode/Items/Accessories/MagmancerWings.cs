using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Accessories
{

    [AutoloadEquip(EquipType.Wings)]
    public class MagmancerWings : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Magmancer Wings");

            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(120, 7, 1.5f);
        }

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 42;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
			Item.accessory = true;
            Item.alpha = 100;
		}

        public override void RegisterEquipStats()
        {
            AddEffect(new WingTimeMaxEffect(120));
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 1.7f;
			constantAscend = 0.135f;
		}

        public override bool WingUpdate(Player player, bool inUse)
        {
            int WingTicks;
            if (inUse)
            {
                WingTicks = 6;
            }
            else
            {
                WingTicks = 8;
            }

            if (player.velocity.Y != 0)
            {
                player.wingFrameCounter++;
                if (player.wingFrameCounter > WingTicks)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame >= 3)
                    {
                        player.wingFrame = 0;
                    }
                }
            }
            else
            {
                player.wingFrame = 4;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LavaBucket, 5);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSmite>(), 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
