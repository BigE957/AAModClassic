using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Weapons
{
    public class Mushmace : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushmace");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.knockBack = 4f;
            Item.damage = 19 / 2;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Mushmace_Holdout>();
            Item.shootSpeed = 9;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 5);
            recipe.AddIngredient(ModContent.ItemType<MushiumBar>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}