using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterVisor : BaseAAItem
    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Darkmatter Visor");
            /* Tooltip.SetDefault(@"15% increased Ranged damage
20% decreased ammo consumption 
Dark, yet still barely visible"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.value = 300000;
            Item.defense = 26;
            Item.rare = 9;
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
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.ammoCost80 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("DarkmatterBreastplate").Type && legs.type == Mod.Find<ModItem>("DarkmatterGreaves").Type;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DarkmatterVisorBonus");
            player.GetModPlayer<VisorEffects>().setBonus = true;
            player.GetModPlayer<VisorEffects>().sunPortal = false;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

    }
    public class VisorEffects : ModPlayer
    {
        public bool setBonus = false;
        public Vector2 portalOffset = new Vector2(0, -50);
        public int portalFrame = 0;
        public int portalFrameCount = 4;
        public bool sunPortal = false;
        int timer;
        bool shot = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
        
        public override void PreUpdate()
        {
            
            timer++;
            if (timer % 10 == 0)
            {
                portalFrame++;
                if (portalFrame >= portalFrameCount)
                {
                    portalFrame = 0;
                }
            }
            if(Player.itemTime>1 && Player.HeldItem.CountsAsClass(DamageClass.Ranged))
            {
                
                if (!shot && setBonus)
                {
                    if(sunPortal)
                    {
                        Projectile.NewProjectile(Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, Mod.Find<ModProjectile>("SunSphere").Type, (int)(Player.HeldItem.damage * Player.GetDamage(DamageClass.Ranged) * .5f), 2f, Player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectile(Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, Mod.Find<ModProjectile>("DarkmatterSphere").Type, (int)(Player.HeldItem.damage * Player.GetDamage(DamageClass.Ranged) * .3f), 2f, Player.whoAmI);
                    }
                    
                }
                shot = true;
            }
            else
            {
                shot = false;
            }
        }
        public static readonly PlayerLayer Portal = new PlayerLayer("AAMod", "Portal", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawSet drawInfo)
        {

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = AAMod.instance;
            Texture2D texture = mod.GetTexture("Items/Armor/Darkmatter/DarkPortal");
            if(drawPlayer.GetModPlayer<VisorEffects>().sunPortal)
            {
                texture = mod.GetTexture("Items/Armor/Radium/SunPortal");
            }
            if (drawPlayer.GetModPlayer<VisorEffects>().setBonus)
            {
                Vector2 Center = drawInfo.Position + new Vector2(drawPlayer.width / 2, drawPlayer.height / 2) + drawPlayer.GetModPlayer<VisorEffects>().portalOffset - Main.screenPosition;

                DrawData data = new DrawData(texture, Center, texture.Frame(1, drawPlayer.GetModPlayer<VisorEffects>().portalFrameCount, 0, drawPlayer.GetModPlayer<VisorEffects>().portalFrame), Color.White, 0f, new Vector2(texture.Size().X, texture.Size().Y / 4) * .5f, 1f, drawInfo.playerEffect, 0)
                {
                    shader = drawInfo.cBody
                };
                Main.playerDrawData.Add(data);
            }
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {


            int frontLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsFront"));
            if (frontLayer != -1)
            {
                Portal.visible = true;
                layers.Insert(frontLayer + 1, Portal);
            }
        }
    }
    
}