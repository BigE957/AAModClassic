using AAModClassic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Blocks
{
	public class DiscordianTorch : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Discordian Torch");
			ItemID.Sets.Torches[Type] = true;

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
			Item.createTile = Mod.Find<ModTile>("DiscordianTorch").Type;
			Item.flame = true;
            AARarity = 14;
            Item.value = Item.sellPrice(0, 0, 50, 0);
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void HoldItem(Player player)
		{
			if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, Mod.Find<ModDust>("DiscordDust").Type);
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
			recipe.AddIngredient(null, "Discordium");
			recipe.Register();
		}
	}
}