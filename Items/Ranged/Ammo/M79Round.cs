using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class M79Round : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 25;
			Item.width = 8;
			Item.height = 16;
			Item.maxStack = 999;
			Item.value = Item.sellPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Orange;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("M79P").Type;
			Item.ammo = Item.type;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("M79 Round");
		}
    }
}
