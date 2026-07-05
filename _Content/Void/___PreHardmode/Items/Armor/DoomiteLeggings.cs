using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    [AutoloadEquipGlow(EquipType.Legs)]
    public class DoomiteLeggings : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Doomite";
        public Color Color => AAColor.ZeroShield;

        public bool Condition(Player p) => p.GetModPlayer<DoomiteHelmetSetPlayer>().effect;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Greaves");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
            Item.value = 9000;
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new MaxMinionSlotEffect(1));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 6);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 12);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}