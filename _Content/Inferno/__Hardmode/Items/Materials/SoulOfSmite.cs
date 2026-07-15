using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Materials
{
    public class SoulOfSmite : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Materials";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soul of Smite");
			// Tooltip.SetDefault("The essence of Inferno creatures");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[Item.type] = true;
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;

            Item.ResearchUnlockCount = 25;
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