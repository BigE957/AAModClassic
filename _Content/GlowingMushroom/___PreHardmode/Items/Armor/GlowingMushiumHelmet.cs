using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class GlowingMushiumHelmet : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.GlowingMushium";
        public Color Color => AAColor.Glow;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowing Mushium Hat");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 16;
			Item.value = 90;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<GlowingMushiumChestplate>() && legs.type == ModContent.ItemType<GlowingMushiumLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            AddEffect(new ManaRegenEffect(2));

			AddSetEffect(new BuffImmunityEffect(BuffID.ManaSickness));
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlowingMushiumBar>(), 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
