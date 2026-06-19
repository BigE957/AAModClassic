using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic._Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class ChaosSlayerChestplate : BaseAAItem, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.ChaosSlayer";
        public Color Color => AAColor.Shen3;

        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, Texture + "_Body_Alt", EquipType.Body, item: this, name: $"{Name}_Body_Alt");
            AAPlayer.ModifyDrawInfoEvent += ModifyDrawInfo;
        }

        private void ModifyDrawInfo(Player player)
        {
            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Body_Alt", EquipType.Body);

            if (player.body == blue && player.direction == -1)
                player.body = red;
            else if (player.body == red && player.direction == 1)
                player.body = blue;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Slayer Plate");
            /* Tooltip.SetDefault(@"4% increased damage resistance
+75 Max Life
The power of discordian rage radiates from this armor"); */

            int red = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            int blue = EquipLoader.GetEquipSlot(Mod, Name + "_Body_Alt", EquipType.Body);

            ArmorIDs.Body.Sets.HidesTopSkin[red] = true;
            ArmorIDs.Body.Sets.HidesTopSkin[blue] = true;
        }

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 60;
        }

        public override void UpdateEquip(Player player)
		{
            player.endurance += .04f;
            player.GetAttackSpeed(DamageClass.Melee) += .15f;
            player.statLifeMax2 += 75;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadMoonChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 10);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}