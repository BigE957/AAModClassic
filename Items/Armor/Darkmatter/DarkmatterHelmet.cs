using AAModClassic.___Content.Stars._PostMoonlord.Items;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHelmet : BaseAAItem
	{
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Helmet");
            /* Tooltip.SetDefault(@"10% increased melee damage
Dark, yet still barely visible"); */

            Glowmask = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Glowmask.Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 300000;
			Item.defense = 34;
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

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Melee) += 0.10f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkmatterBreastplate>() && legs.type == ModContent.ItemType<DarkmatterGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{

            const float effectRange = 500;
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterHelmetBonus");
            if(!Main.dayTime && player.GetModPlayer<HelmetEffects>().ShieldCoolDown > 0) player.lifeRegen += 2;
            for(int p =0; p < Main.player.Length; p++)
            {
                if(Main.player[p].active && (Main.player[p].Center - player.Center).Length() < effectRange && player.team == Main.player[p].team && Main.player[p].GetModPlayer<HelmetEffects>().ShieldCoolDown <= 0)
                {
                    Main.player[p].GetModPlayer<HelmetEffects>().ShieldTime = 2;
                    Main.player[p].GetModPlayer<HelmetEffects>().badShield = false;
                }
            }
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 25);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    public class HelmetEffects : ModPlayer
    {
        public int ShieldTime = 0;
        public int ShieldCoolDown = 0;
        public float yetAnotherTrigCounter = 0;
        public bool badShield = false;
        
        public override void ResetEffects()
        {
            if(ShieldTime>0)
            {
                ShieldTime--;
            }
        }
        public override void PreUpdate()
        {
            yetAnotherTrigCounter += (float)Math.PI / 60;

            if (ShieldCoolDown > 0)
            {
                ShieldCoolDown--;
            }
            else
            {
                ShieldCoolDown = 0;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (ShieldTime>0)
            {
                if(badShield)
                {
                    modifiers.IncomingDamageMultiplier *= 1.4f;
                }
                else
                {
                    modifiers.IncomingDamageMultiplier *= 0.6f;
                    ShieldCoolDown = 1800;
                }
                
            }
        }
        public class drawShield : PlayerDrawLayer// = new PlayerLayer("AAMod", "drawShield", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawSet drawInfo)
        {
            public static Asset<Texture2D> DarkmatterShieldTex;
            public static Asset<Texture2D> RadiumShieldTex;

            public override void SetStaticDefaults()
            {
                DarkmatterShieldTex = ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Darkmatter/DarkmatterShield");
                RadiumShieldTex = ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/RadiumShield");
            }

            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ElectrifiedDebuffFront);

            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = AAMod.instance;
                Texture2D texture = DarkmatterShieldTex.Value;
                if(drawPlayer.GetModPlayer<HelmetEffects>().badShield)
                {
                    texture = RadiumShieldTex.Value;
                }
                if (drawPlayer.GetModPlayer<HelmetEffects>().ShieldTime>0)
                {
                    Vector2 Center = drawInfo.Position + new Vector2(drawPlayer.width / 2, 0) + Vector2.UnitY*-30 - Main.screenPosition;

                    DrawData data = new DrawData(texture, Center, null, Color.White, 0f, texture.Size() * .5f, 1f + (.1f * (float)Math.Sin(drawPlayer.GetModPlayer<HelmetEffects>().yetAnotherTrigCounter)), SpriteEffects.None, 0);
                    data.shader = drawInfo.cBody;
                    drawInfo.DrawDataCache.Add(data);
                }
            }
        }
    }   
}