using AAModClassic._Content._Dev.__Hardmode.Items.Pets;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content._Dev.__Hardmode.Items.Consumables
{
    public class PlanterrorBag : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Vanity";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plant Terror's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Plant Terror!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
		{
            NPC.NewNPC(Item.GetSource_Loot(), (int)player.Center.X, (int)player.Center.Y, NPCID.Plantera);
        }
    }
}