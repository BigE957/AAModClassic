using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Weapons
{
    public class ChaosChain : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Chain");
            // Tooltip.SetDefault(@"Throws a volitile sphere of chaotic energy");
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.knockBack = 15f;
            Item.width = 20;
            Item.height = 20;
            Item.damage = 45;
            Item.shoot = ModContent.ProjectileType<ChaosChain_Holdout>();
            Item.shootSpeed = 14f;
            Item.UseSound = SoundID.Item10;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<Ryusei>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}