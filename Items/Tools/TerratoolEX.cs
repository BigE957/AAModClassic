using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TerratoolEX : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 60;
            Item.useStyle = 1;
            Item.useTime = 4;
            Item.useAnimation = 16;
            Item.tileBoost += 25;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(1, 50, 0, 0);
            Item.rare = 11;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 120;
            Item.pick = 320;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terraformer");
            /* Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
Terratool EX"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                Item.autoReuse = false;
                Item.noUseGraphic = true;
                AAMod.instance.TerratoolEXState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.autoReuse = true;
                Item.noUseGraphic = false;
                Item.pick = UI.TerratoolEXUI.Pick;
                Item.axe = UI.TerratoolEXUI.Axe;
                Item.hammer = UI.TerratoolEXUI.Hammer;
                Item.damage = 120;
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Terratool");
            recipe.AddIngredient(Mod, "EXSoul");
            recipe.AddTile(Mod, "ACS");
            recipe.Register();
        }
    }
}
