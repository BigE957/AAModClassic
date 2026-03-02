using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Mask");
            // Tooltip.SetDefault(@"9% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 9000;
            Item.rare = 7;
            Item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("TerraPlate").Type && legs.type == Mod.Find<ModItem>("TerraGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraMaskBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.TerraSu = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("TerraCrystal").Type] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("TerraCrystal").Type, (int)(60 * player.GetDamage(DamageClass.Summon)), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DemonHood", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}