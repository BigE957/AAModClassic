using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Crafters;

namespace AAModClassic.Items.Armor.Darkmatter
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
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.value = 300000;
            Item.defense = 26;
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
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.ammoCost80 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DarkmatterBreastplate>() && legs.type == ModContent.ItemType<DarkmatterGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterVisorBonus");
            player.GetModPlayer<VisorEffects>().setBonus = true;
            player.GetModPlayer<VisorEffects>().sunPortal = false;
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
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, ModContent.ProjectileType<SunSphere>(), (int)(Player.GetDamage(DamageClass.Ranged).ApplyTo(Player.HeldItem.damage) * .5f), 2f, Player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, ModContent.ProjectileType<DarkmatterSphere>(), (int)(Player.GetDamage(DamageClass.Ranged).ApplyTo(Player.HeldItem.damage) * .3f), 2f, Player.whoAmI);
                    }
                    
                }
                shot = true;
            }
            else
            {
                shot = false;
            }
        }
        public class PortalDrawLayer : PlayerDrawLayer// = new PlayerLayer("AAMod", "Portal", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawSet drawInfo)
        {
            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ElectrifiedDebuffFront);
            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = AAMod.instance;
                Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Darkmatter/DarkPortal").Value;
                if(drawPlayer.GetModPlayer<VisorEffects>().sunPortal)
                {
                    texture = ModContent.Request<Texture2D>("AAModClassic/Items/Armor/Radium/SunPortal").Value;
                }
                if (drawPlayer.GetModPlayer<VisorEffects>().setBonus)
                {
                    Vector2 Center = drawInfo.Position + new Vector2(drawPlayer.width / 2, drawPlayer.height / 2) + drawPlayer.GetModPlayer<VisorEffects>().portalOffset - Main.screenPosition;

                    DrawData data = new DrawData(texture, Center, texture.Frame(1, drawPlayer.GetModPlayer<VisorEffects>().portalFrameCount, 0, drawPlayer.GetModPlayer<VisorEffects>().portalFrame), Color.White, 0f, new Vector2(texture.Size().X, texture.Size().Y / 4) * .5f, 1f, drawInfo.playerEffect, 0)
                    {
                        shader = drawInfo.cBody
                    };
                    drawInfo.DrawDataCache.Add(data);
                }
            }
        }
    }   
}