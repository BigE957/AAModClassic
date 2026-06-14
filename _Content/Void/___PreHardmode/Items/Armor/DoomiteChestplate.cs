using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class DoomiteChestplate : BaseAAItem, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Doomite";
        public Color Color => AAColor.ZeroShield;

        public bool Condition(Player p) => p.GetModPlayer<AAPlayer>().doomite;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomite Plate");
            // Tooltip.SetDefault(@"+1 Minion slot");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
            Item.value = 9000;
		}

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 10);
            recipe.AddIngredient(ItemID.Coral, 8);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 16);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}