
/*namespace AAModClassic.Items.Materials
{
    public class VoidEnergy : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void Energy");
			Tooltip.SetDefault("A singularity of dark, unstable energy");
			// ticksperframe, frameCount
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		// TODO -- Velocity Y smaller, post NewItem?
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			Item.maxStack = 9999;
			item.value = 1000;
			item.rare = 2;
            item.alpha = 50;
		}

        public override void PostUpdate()
		{
			Lighting.AddLight(item.Center, AAColor.ZeroShield.ToVector3() * 0.55f * Main.essScale);
		}
	}
}*/