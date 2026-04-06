using AAModClassic;
using AAModClassic.Projectiles.Djinn;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Djinn

{
    public class SultanScimitar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sultan's Scimitar");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 24;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 58;
			Item.height = 66;
			Item.useTime = 26;
            Item.useAnimation = 26;
            Item.shoot = ModContent.ProjectileType<DesertGust>();
            Item.shootSpeed = 5f;
	        Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
            Item.value = 50000;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Orange;
		}
	}
}
