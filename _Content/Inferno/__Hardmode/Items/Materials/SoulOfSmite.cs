using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Materials
{
    public class SoulOfSmite : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soul of Smite");
			// Tooltip.SetDefault("The essence of Inferno creatures");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		// TODO -- Velocity Y smaller, post NewItem?
		public override void SetDefaults()
		{
			Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofNight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 1000;
            Item.rare = refItem.rare;
        }

        public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.OrangeRed.ToVector3() * 0.55f * Main.essScale);
		}
	}
}