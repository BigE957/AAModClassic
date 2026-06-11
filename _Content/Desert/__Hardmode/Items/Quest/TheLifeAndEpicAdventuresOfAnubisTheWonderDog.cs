using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Desert.__Hardmode.Items.Quest
{
    public class TheLifeAndEpicAdventuresOfAnubisTheWonderDog : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            /* DisplayName.SetDefault(@"The Life and Epic Adventures 
of Anubis the Wonder Dog!"); */

            /* Tooltip.SetDefault(@"A very...interesting take on the life and misadventures of
Anubis the Legendscribe. Maybe if you give this to him he'll
give you something for it."); */
        }


        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.width = 28;
            Item.height = 30;
            Item.rare = ItemRarityID.Quest;
        }
    }
}