using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic._Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class ChaosSlayerLeggings : EquipAbstract, ILocalizedModType, ICustomEquipGlow
	{
        public new string LocalizationCategory => "Items.Armor.ChaosSlayer";
        public Color Color => AAColor.Shen3;

        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Legs_Alt", EquipType.Legs, item: this, name: $"{Name}_Legs_Alt");
            AAPlayer.ModifyDrawInfoEvent += ModifyDrawInfo;
        }

        private void ModifyDrawInfo(Player player)
        {
            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Legs);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Legs_Alt", EquipType.Legs);

            if (player.legs == blue && player.direction == -1)
                player.legs = red;
            else if (player.legs == red && player.direction == 1)
                player.legs = blue;
        }

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Slayer Greaves");
            /* Tooltip.SetDefault(@"'The power of discordian rage radiates from this armor'"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 35;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect(new EnduranceEffect(0.02f));
            AddEffect(new MovementSpeedEffect(0.45f));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 4);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}