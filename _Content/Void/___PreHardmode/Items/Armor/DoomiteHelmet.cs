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
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DoomiteHelmet : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Doomite";
        public Color Color => AAColor.ZeroShield;

        public bool Condition(Player p) => p.GetModPlayer<DoomiteHelmetSetPlayer>().effect;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Visor");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
            Item.value = 9000;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DoomiteChestplate>() && legs.type == ModContent.ItemType<DoomiteLeggings>();
        }

        public override void RegisterEquipStats()
        {
            AddEffect(new MaxMinionSlotEffect(1));

            AddSetEffect(new MaxMinionSlotEffect(1));
            AddSetEffect<DoomiteHelmetSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}