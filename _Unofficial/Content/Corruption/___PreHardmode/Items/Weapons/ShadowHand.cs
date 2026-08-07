using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Corruption.___PreHardmode.Items.Weapons
{
    public class ShadowHand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;

            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.mana = 5;

            Item.shoot = ModContent.ProjectileType<ShadowHand_Proj>();
            Item.shootSpeed = 0f;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item21;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.autoReuse = true;

            Item.knockBack = 4;

            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
        }   

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Venom Spray");
          // Tooltip.SetDefault("");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.RottenChunk, 6);
            recipe.AddTile(TileID.Bookcases);
            recipe.AddCondition(ConditionUtils.Unofficial);
            recipe.Register();
        }
    }
}
