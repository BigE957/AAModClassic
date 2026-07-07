using AAModClassic._Content.Madness.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Madness.___PreHardmode.Items.Weapons
{
    public class MadnessBow : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Bow");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.width = 12;
            Item.height = 28;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.useAmmo = AmmoID.Arrow;
            Item.UseSound = SoundID.Item5;
            Item.damage = 11;
            Item.shootSpeed = 5f;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Green;
            Item.noMelee = true;
            Item.value = 3000;
            Item.DamageType = DamageClass.Ranged;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MadnessFragment>(), 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}