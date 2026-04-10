using AAModClassic.___Content.Mire._Hardmode.Items.Materials;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Tiles.Decoration
{
	public class BogtoxinTorch : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bogtoxin Torch");
			ItemID.Sets.Torches[Type] = true;
			//ItemID.Sets.WaterTorches[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 12;
			Item.maxStack = 99;
			Item.holdStyle = 1;
			Item.noWet = true;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<BogtoxinTorch_Tile>();
			Item.flame = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 1, 0);
		}

		public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<Dusts.HydratoxinDust>());
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, AAColor.BogToxin.R / 255, AAColor.BogToxin.G / 255, AAColor.BogToxin.B / 255);
		}

		public override void PostUpdate()
		{
			if (!Item.wet)
			{
				Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), AAColor.BogToxin.R / 255, AAColor.BogToxin.G / 255, AAColor.BogToxin.B / 255);
			}
		}

		//???
		//public override void AutoLightSelect(ref bool dryTorch, ref bool wetTorch, ref bool glowstick)/* tModPorter Note: _Unreleased. Use , , and ItemID.Sets.Glowsticks[Type] in SetStaticDefaults */
		//{
		//	dryTorch = false;
		//}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(33);
			recipe.AddIngredient(ItemID.Torch, 33);
			recipe.AddIngredient(ModContent.ItemType<Bogtoxin>());
			recipe.Register();
		}
	}
}