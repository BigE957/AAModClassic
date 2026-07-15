using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Decoration
{
	public class DiscordianTorch : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Discordian Torch");
			ItemID.Sets.Torches[Type] = true;
            ItemID.Sets.SingleUseInGamepad[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.ShimmerTorch;
			Item.ResearchUnlockCount = 100;
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
			Item.createTile = ModContent.TileType<DiscordianTorch_Tile>();
			Item.flame = true;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }

        

        public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<Discord_Dust>());
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, AAColor.Shen.R / 255, AAColor.Shen.G / 255, AAColor.Shen.B / 255);
		}

		public override void PostUpdate()
		{
			if (!Item.wet)
			{
				Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), AAColor.Shen.R / 255, AAColor.Shen.G / 255, AAColor.Shen.B / 255);
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
			recipe.AddIngredient(ItemID.Torch, 50);
			recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>());
			recipe.Register();
		}
	}
}