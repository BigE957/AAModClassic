using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.SunkenShip.___PreHardmode.Items
{
    public class ShatteredMirror : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shattered Mirror");
            /* Tooltip.SetDefault(@"A long abandoned keepsake in dire need of repairs."); */
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicMirror);
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.consumable = false;
        }
    }
}
