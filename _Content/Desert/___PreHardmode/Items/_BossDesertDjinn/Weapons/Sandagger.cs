using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert.___PreHardmode.Items._BossDesertDjinn.Weapons
{
    public class Sandagger : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sandagger");
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
		{

            Item.damage = 15;            
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 14;
			Item.useTime = 8;
            Item.maxStack = Item.CommonMaxStack;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.value = 8;
			Item.rare = ItemRarityID.Orange;
			Item.shootSpeed = 9f;
			Item.shoot = ModContent.ProjectileType<Sandagger_Proj>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.consumable = true;
		}
    }
}
