using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SerpentSpike : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Serpent Spike");
        }

        public override void SetDefaults()
        {
            Item.damage = 30; 
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 132;
            Item.height = 132;
            Item.scale = 1.1f;
            Item.useTime = 25; 
            Item.useAnimation = 25;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 5, 0, 0); 
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<SerpentSpike_Proj>();  
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
