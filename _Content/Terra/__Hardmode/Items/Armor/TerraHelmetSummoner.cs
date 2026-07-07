using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic._Content.Hell.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHelmetSummoner : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Terra";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Mask");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 9000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.09f;

            AddSetEffect<TerraHelmetSummonerSetEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}