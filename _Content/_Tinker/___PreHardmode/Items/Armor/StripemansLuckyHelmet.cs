using AAModClassic._Content._Misc.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content._Tinker.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class StripemansLuckyHelmet : EquipAbstract, ILocalizedModType
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

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<StripemansLuckyChestplate>() && legs.type == ModContent.ItemType<StripemansLuckyLeggings>();
		}

        public override void RegisterEquipEffects()
        {
			AddEffect<ArchitectGizmoPackEffect>();
            AddEffect(new EmitLightFromPlayerEffect(0.92f, 0.8f, 0.65f));
			AddEffect<StripemansLuckyHelmetEffect>();

			AddSetEffect<StripemansLuckyHelmetSetEffect>();
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