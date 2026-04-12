using AAModClassic.___Content.Inferno._PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class Incineration : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.useTime = 25;
            Item.CloneDefaults(ItemID.CrimsonYoyo);

            Item.damage = 19;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.shoot = ModContent.ProjectileType<Projectiles.Incineration>();
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Incineration");
            // Tooltip.SetDefault("Spinning Singe");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}