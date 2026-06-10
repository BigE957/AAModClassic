using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic._Content.Hell.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHelmetSummoner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Mask");
            // Tooltip.SetDefault(@"Increases summon damage by 9%");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = 9000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.09f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = FilePathUtils.SetBonusPath<TerraHelmetSummoner>();

            TerraHelmetSummonerPlayer modPlayer = player.GetModPlayer<TerraHelmetSummonerPlayer>();
            modPlayer.effect = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && player.FindBuffIndex(ModContent.BuffType<ChampionHelmetSummoner_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<TerraHelmetSummonerPlayer_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TerraHelmetSummonerPlayer_TerraCrystal>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<TerraHelmetSummonerPlayer_TerraCrystal>(), (int)player.GetDamage(DamageClass.Summon).ApplyTo(60), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}