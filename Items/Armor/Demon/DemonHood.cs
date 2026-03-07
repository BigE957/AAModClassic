using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;


namespace AAModClassic.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Head)]
    public class DemonHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Cowl");
            // Tooltip.SetDefault(@"9% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 9000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("DemonGarb").Type && legs.type == Mod.Find<ModItem>("DemonBoots").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DemonHoodBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.impSet = true;
            modPlayer.demonBonus = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("DemonBuff").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("DemonBuff").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("ImpMinion").Type] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("ImpMinion").Type, 20, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.Bone, 5);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.ShadowScale, 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "ImpHood", 1);
                recipe.AddIngredient(ItemID.Bone, 5);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.TissueSample, 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}