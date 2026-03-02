using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class ArchwitchStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Archwitch's Galactic Scepter");
            /* Tooltip.SetDefault(@"The staff of the Dragon Queen
Left-click to spin the scepter, firing off stars at nearby enemies
Right click to fire explosive magic bolts"); */
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.DamageType = DamageClass.Magic;
            Item.width = 102;
            Item.height = 100;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 40, 0, 0);
            Item.rare = ItemRarityID.Purple;                  
            Item.shoot = Mod.Find<ModProjectile>("ArchwitchStaff").Type;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.expert = Item.expertOnly = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.staff[Item.type] = true;
                Item.shoot = Mod.Find<ModProjectile>("ArchwitchStorm").Type;
                Item.shootSpeed = 12;
                Item.noUseGraphic = false;
                Item.channel = false;
                Item.autoReuse = true;
                Item.useTime = 10;
                Item.useAnimation = 30;
                Item.UseSound = new LegacySoundStyle(2, 105, Terraria.Audio.SoundType.Sound);
            }
            else
            {
                Item.staff[Item.type] = false;
                Item.shoot = Mod.Find<ModProjectile>("ArchwitchStaff").Type;
                Item.shootSpeed = 0f;
                Item.noUseGraphic = true;
                Item.channel = true;
                Item.autoReuse = false;
                Item.useTime = 6;
                Item.useAnimation = 6;
                Item.UseSound = SoundID.Item1;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ArchwitchWand");
            recipe.AddIngredient(null, "EXSoul");
            recipe.Register(); 
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "CatsEyeRifleEX");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}