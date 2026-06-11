using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.Tools;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Tools
{
    public class DreadTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 60;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 4;
            Item.useAnimation = 20;
            Item.tileBoost += 20;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 100;
            Item.pick = 300;
        }


        

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Terratool");
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
                AAMod.instance.TerratoolYState.ToggleUI(AAMod.instance.TerratoolInterface);
                Item.pick = 0;
                Item.axe = 0;
                Item.hammer = 0;
                Item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                Item.pick = TerratoolYUI.Pick;
                Item.axe = TerratoolYUI.Axe;
                Item.hammer = TerratoolYUI.Hammer;
                Item.damage = 100;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}