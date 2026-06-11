using AAModClassic._Content.Inferno.__Hardmode.Items.Tools;
using AAModClassic._Content.Mire.__Hardmode.Items.Tools;
using AAModClassic.UI.Tools;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Tools
{
    public class ChaosTerratool : BaseAAItem
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
            Item.pick = 205;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Terratool");
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
                AAMod.instance.TerratoolCState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.pick = TerratoolCUI.Pick;
                Item.axe = TerratoolCUI.Axe;
                Item.hammer = TerratoolCUI.Hammer;
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
                recipe.AddIngredient(ModContent.ItemType<PerfectStonebreaker>());
                recipe.AddIngredient(ModContent.ItemType<PerfectShadowDrill>());
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
        }
    }
}
