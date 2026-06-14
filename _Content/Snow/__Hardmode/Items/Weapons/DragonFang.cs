using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic._Content.Snow.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Snow.Projectiles;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Snow.__Hardmode.Items.Weapons
{
    public class DragonFang : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Fang");
            // Tooltip.SetDefault("Right click to slash at your foes with the grace of a Valkyrie");
        }

        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.width = 48;
            Item.height = 46;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.knockBack = 3;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<DragonFang_ValkyrieSlash>();
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.channel = false;
                Item.useAnimation = 15;
                Item.useTime = 15;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = true;
                Item.channel = false;
                Item.shoot = ModContent.ProjectileType<IceShrapnel>();
                Item.shootSpeed = 10;
            }
            else
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.channel = true;
                Item.useAnimation = 25;
                Item.useTime = 5;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.autoReuse = false;
                Item.channel = true;
                Item.shoot = ModContent.ProjectileType<DragonFang_ValkyrieSlash>();
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<IceLongsword>());
            recipe.AddIngredient(ItemID.Arkhalis);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
