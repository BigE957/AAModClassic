using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class GaleForce : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 200;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = 5;        
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.mana = 8;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = Mod.Find<ModProjectile>("HurricaneSpawn").Type;
            Item.shootSpeed = 9f;
            Item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
          // DisplayName.SetDefault("Gale Force");
          // Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "GaleOfWings", 1);
            recipe.AddIngredient(null, "StormSphere", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
