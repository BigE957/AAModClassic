using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Weapons;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis.Projectiles;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Weapons
{
    public class GaleForce : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gale Force");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.damage = 200;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.mana = 8;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = ModContent.ProjectileType<AthenaGale>();
            Item.shootSpeed = 9f;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GaleOfWings>(), 1);
            recipe.AddIngredient(ModContent.ItemType<StormSphere>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
