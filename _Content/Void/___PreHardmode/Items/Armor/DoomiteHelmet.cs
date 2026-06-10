using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DoomiteHelmet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Visor");
            // Tooltip.SetDefault(@"+1 Minion slot");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
            Item.value = 9000;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DoomiteChestplate>() && legs.type == ModContent.ItemType<DoomiteLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DoomiteVisorBonus");
            player.maxMinions += 1;
            player.GetModPlayer<AAPlayer>().doomite = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<Searcher_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<Searcher_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DoomiteHelmet_Searcher>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<DoomiteHelmet_Searcher>(), 30, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkDoomiteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ScorchedScale>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}