
using Terraria;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.Items.DevTools
{
    public class ChaosConverter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			// DisplayName.SetDefault("[DEV] Chaos Converter");
            BaseUtility.AddTooltips(Item, new string[] { "Converts a strand of Mire or Inferno down below you." });					
		}			
		
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Red;
            Item.value = 0;
			Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 45;
            Item.useTime = 45;		
        }

        public bool flag = false;

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (flag)
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionType.INFERNO);
            }
            else
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionType.MIRE);
            }
            flag = false;
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            flag = true;
            return true;
        }
    }
}