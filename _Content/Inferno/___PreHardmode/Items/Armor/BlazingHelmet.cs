using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class BlazingHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Blazing";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blazing Kabuto");
			/* Tooltip.SetDefault(@"'Forged in the flames of the blazing sun'"); */
        }

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<BlazingChestplate>() && legs.type == ModContent.ItemType<BlazingLeggings>();
        }

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += 0.03f;
            AddEffect(new EnduranceEffect(0.01f));
            
            AddSetEffect(new AggroEffect(4));
            AddSetEffect<MagmaStoneEffect>();
            AddSetEffect(new EmitLightFromPlayerEffect(AAColor.Lantern.R / 255, AAColor.Lantern.G / 255 * 0.95f, AAColor.Lantern.B / 255 * 0.8f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KindledHelmet>());
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}