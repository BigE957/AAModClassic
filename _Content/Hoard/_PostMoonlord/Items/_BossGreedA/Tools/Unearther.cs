using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Tools;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.Tools
{
    public class Unearther : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unearther");
            // Tooltip.SetDefault("Mines ores even faster");
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 44;
            Item.height = 44;
            Item.useAnimation = 10;
            Item.useTime = 5;
            Item.pick = 230;
            Item.tileBoost += 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
            if (Main.tileOreFinderPriority[tile.TileType] > 0 && PickCheck(tile, Item.pick))
            {
                player.PickTile(Player.tileTargetX, Player.tileTargetY, 5000);
            }
            return true;
        }
        public static bool PickCheck(Tile tile, int pickPower)
        {
            ModTile tile2 = TileLoader.GetTile(tile.TileType);
            if (tile.TileType == TileID.Chlorophyte && pickPower < 200)
            {
                return false;
            }
            else if ((tile.TileType == TileID.Ebonstone || tile.TileType == TileID.Crimstone) && pickPower < 65)
            {
                return false;
            }
            else if (tile.TileType == TileID.Pearlstone && pickPower < 65)
            {
                return false;
            }
            else if (tile.TileType == TileID.Meteorite && pickPower < 50)
            {
                return false;
            }
            else if (tile.TileType == TileID.DesertFossil && pickPower < 65)
            {
                return false;
            }
            else if ((tile.TileType == TileID.Demonite || tile.TileType == TileID.Crimtane) && pickPower < 55)
            {
                return false;
            }
            else if (tile.TileType == TileID.Obsidian && pickPower < 65)
            {
                return false;
            }
            else if (tile.TileType == TileID.Hellstone && pickPower < 65)
            {
                return false;
            }
            else if ((tile.TileType == TileID.LihzahrdBrick || tile.TileType == TileID.LihzahrdAltar) && pickPower < 210)
            {
                return false;
            }
            else if (tile.TileType == TileID.Cobalt && pickPower < 100)
            {
                return false;
            }
            else if (tile.TileType == TileID.Mythril && pickPower < 110)
            {
                return false;
            }
            else if (tile.TileType == TileID.Adamantite && pickPower < 150)
            {
                return false;
            }
            else if (tile.TileType == TileID.Palladium && pickPower < 100)
            {
                return false;
            }
            else if (tile.TileType == TileID.Orichalcum && pickPower < 110)
            {
                return false;
            }
            else if (tile.TileType == TileID.Titanium && pickPower < 150)
            {
                return false;
            }
            else if (tile2 != null && pickPower < tile2.MinPick)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<MINEer>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteBar>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
