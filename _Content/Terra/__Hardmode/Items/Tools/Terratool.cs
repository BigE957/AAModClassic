using AAModClassic._Content.Corruption.__Hardmode.Items.Tools;
using AAModClassic._Content.Crimson.__Hardmode.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Tools
{
    public class Terratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 5;
            Item.useAnimation = 20;
            Item.tileBoost += 3;
            Item.knockBack = 3;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 60;
            Item.pick = 215;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terratool");
            /* Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                AAMod.instance.TerratoolTState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.pick = UI.TerratoolTUI.Pick;
                Item.axe = UI.TerratoolTUI.Axe;
                Item.hammer = UI.TerratoolTUI.Hammer;
                Item.damage = 60;
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<TrueNightaxe>());
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {

                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<TrueScalpel>());
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        }
    }
}
