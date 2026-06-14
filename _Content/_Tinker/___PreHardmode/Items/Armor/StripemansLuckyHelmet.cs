using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;


namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class StripemansLuckyHelmet : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.StripemansLucky";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stripeman's Lucky Hat");
			/* Tooltip.SetDefault(@"Provides light when worn
Get the effect of Architect Gizmo Pack
When digging stones, you may get ore from them
You can put any sand into the Extractinator"); */

		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.defense = 1;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().StripeManOre = true;
			
			player.autoPaint = true;
			player.pickSpeed -= 1f;
			player.tileSpeed += 3f;
			player.wallSpeed += 3f;
			if (player.whoAmI == Main.myPlayer)
			{
				Player.tileRangeX += 6;
				Player.tileRangeY += 4;
			}

			Vector2 vector = new Vector2(player.width / 2 + 8 * player.direction, 2f);
			if (player.fullRotation != 0f)
			{
				vector = vector.RotatedBy(player.fullRotation, player.fullRotationOrigin);
			}
			int i = (int)(player.position.X + vector.X) / 16;
			int j = (int)(player.position.Y + vector.Y) / 16;
			Lighting.AddLight(i, j, 0.92f, 0.8f, 0.65f);
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<StripemansLuckyChestplate>() && legs.type == ModContent.ItemType<StripemansLuckyLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			string active = "";
			if (player.GetModPlayer<AAPlayer>().StripeCrasyLucky)
			{
				active = Language.GetTextValue("Mods.AAModClassic.Common.StripeManSetBonusactive");
			}
			else
			{
				active = Language.GetTextValue("Mods.AAModClassic.Common.StripeManSetBonusunactive");
			}
			
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.StripeManSetBonus1") + active  + "\n" + Language.GetTextValue("Mods.AAModClassic.Common.StripeManSetBonus2");

			if(player.GetModPlayer<AAPlayer>().StripeManFish && player.GetModPlayer<AAPlayer>().StripeManSpawn)
				player.GetModPlayer<AAPlayer>().StripeManSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MiningHelmet, 1);
			recipe.AddIngredient(ItemID.MiningShirt, 1);
			recipe.AddIngredient(ItemID.MiningPants, 1);
			recipe.AddIngredient(ItemID.BonePickaxe, 1);
			recipe.AddIngredient(ItemID.ArchitectGizmoPack, 1);
			recipe.AddIngredient(ModContent.ItemType<LuckyCracker>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.Register();
		}
	}
}