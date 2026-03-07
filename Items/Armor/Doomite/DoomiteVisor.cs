using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Head)]
    public class DoomiteVisor : BaseAAItem
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
            return body.type == Mod.Find<ModItem>("DoomiteBreastplate").Type && legs.type == Mod.Find<ModItem>("DoomiteGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DoomiteVisorBonus");
            player.maxMinions += 1;
            player.GetModPlayer<AAPlayer>().doomite = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("Searcher").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("Searcher").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Searcher").Type] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Searcher").Type, 30, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DoomiteUHelm");
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ItemID.FossilOre, 5);
            recipe.AddIngredient(null, "BroodScale", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
	}
}