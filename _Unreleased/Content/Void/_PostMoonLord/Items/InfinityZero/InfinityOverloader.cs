using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Unreleased.Content.Void.Buffs;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Boss.Zero;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero
{
    public class InfinityOverloader : ModItem
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinity Beacon");
            // Tooltip.SetDefault(@"Calls the Infinity Slayer");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 500;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 15);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 20);
            //TODOSIEGE doesnt exist 
            //recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<OroborosWood>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DoomsdayTesseract>(), 1);
            //recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/_Unreleased/Glowmasks/" + GetType().Name + "_Glow").Value;
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                color1,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, color1, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
            Main.NewText("...Target Identified.", new Color(158, 3, 32));
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player2 = Main.player[i];
                if (player2 != null && player2.active && !player2.dead)
                {
                    player2.AddBuff(ModContent.BuffType<LockedOn_Buff>(), 60);
                }
            }
            SpawnBoss(player, "InfinityZeroSpawn1", "Infinity Zero");
			SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (NPC.AnyNPCs(ModContent.NPCType<InfinityCore>()) || NPC.AnyNPCs(ModContent.NPCType<InfinityZeroSpawn1>()))
            {
                return false;
            }
            return true;
		}

		public void SpawnBoss(Player player, string name, string displayName)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int bossType = Mod.Find<ModNPC>(name).Type;
				if(NPC.AnyNPCs(bossType)){ return; } //don't spawn if there's already a boss!
				int npcID = NPC.NewNPC(Item.GetSource_FromThis(), (int)player.Center.X, (int)player.Center.Y, bossType, 0, 0f);
				Main.npc[npcID].Center = player.Center;
				Main.npc[npcID].netUpdate2 = true;
			}
		}	

		public override void UseStyle(Player player, Rectangle heldItemFrame) { BaseUseStyle.SetStyleBoss(player, Item, true, true); }
		public override void UseItemFrame(Player player) { BaseUseStyle.SetFrameBoss(player, Item); }		
	}
}