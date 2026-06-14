using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Tiles.Decoration
{
	public class DaybreakTorch : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybreak Torch");
			ItemID.Sets.Torches[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 12;
			Item.maxStack = Item.CommonMaxStack;
			Item.holdStyle = 1;
			Item.noWet = true;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<DaybreakTorch_Tile>();
			Item.flame = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }

        

        public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<Dusts.AkumaADust>());
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, AAColor.AkumaA.R / 255, AAColor.AkumaA.G / 255, AAColor.AkumaA.B / 255);
		}

		public override void PostUpdate()
		{
			if (!Item.wet)
			{
				Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), AAColor.AkumaA.R / 255, AAColor.AkumaA.G / 255, AAColor.AkumaA.B / 255);
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
			recipe.AddIngredient(ItemID.Torch, 50);
			recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>());
			recipe.Register();
		}
	}
}