using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.Attributes;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class ChaosSlayerLeggings : BaseAAItem, ILocalizedModType, ICustomEquipGlow
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
            /* Tooltip.SetDefault(@"45% increased movement speed
2% increased damage resistance
The power of discordian rage radiates from this armor"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 35;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += .02f;
            player.moveSpeed += .45f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonLeggings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 4);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 4);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}