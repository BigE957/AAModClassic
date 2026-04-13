using AAModClassic.___Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Usable;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items._BossHydra
{
    public class HydraChow : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydra Chow");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            /* Tooltip.SetDefault(@"Just holding this makes you gag
Summons the Hydra
Can only be used at night"); */
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 500;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<MirePod>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Moonpowder>(), 30);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<HydraBody>(), true, 0, 0, Language.GetTextValue("Mods.AAModClassic.Common.Hydra"), false);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.HydraChowTimeFalse"), Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);
                return false;
            }
            if (player.ZoneAnyMire())
			{
				if (NPC.AnyNPCs(ModContent.NPCType<HydraBody>()))
				{
					if(player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.HydraChowFalse1"), Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);
					return false;
				}
                return true;
			}
			if(player.whoAmI == Main.myPlayer) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.HydraChowFalse2"), Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);			
			return false;
		}	
	}
}