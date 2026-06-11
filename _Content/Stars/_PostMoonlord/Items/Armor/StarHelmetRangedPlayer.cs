using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetRangedPlayer : ModPlayer
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
            if (Player.itemTime > 1 && Player.HeldItem.CountsAsClass(DamageClass.Ranged))
            {

                if (!shot && setBonus)
                {
                    if (sunPortal)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, ModContent.ProjectileType<StarHelmetRangedPlayer_SunSphere>(), (int)(Player.GetDamage(DamageClass.Ranged).ApplyTo(Player.HeldItem.damage) * .5f), 2f, Player.whoAmI);
                    }
                    else
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + portalOffset, (Main.MouseWorld - (Player.Center + portalOffset)).SafeNormalize(-Vector2.UnitY) * Player.HeldItem.shootSpeed, ModContent.ProjectileType<StarHelmetRangedPlayer_DarkmatterSphere>(), (int)(Player.GetDamage(DamageClass.Ranged).ApplyTo(Player.HeldItem.damage) * .3f), 2f, Player.whoAmI);
                    }

                }
                shot = true;
            }
            else
            {
                shot = false;
            }
        }
        public class StarHelmetRangedPlayer_PortalDrawLayer : PlayerDrawLayer// = new PlayerLayer("AAMod", "Portal", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawSet drawInfo)
        {
            public static Asset<Texture2D> DarkPortalTex;
            public static Asset<Texture2D> SunPortalTex;

            public override void SetStaticDefaults()
            {
                DarkPortalTex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<StarHelmetRangedPlayer>() + "_DarkPortal");
                SunPortalTex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<StarHelmetRangedPlayer>() + "_SunPortal");
            }

            public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ElectrifiedDebuffFront);
            protected override void Draw(ref PlayerDrawSet drawInfo)
            {
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = AAMod.instance;
                Texture2D texture = DarkPortalTex.Value;
                if (drawPlayer.GetModPlayer<StarHelmetRangedPlayer>().sunPortal)
                {
                    texture = SunPortalTex.Value;
                }
                if (drawPlayer.GetModPlayer<StarHelmetRangedPlayer>().setBonus)
                {
                    Vector2 Center = drawInfo.Position + new Vector2(drawPlayer.width / 2, drawPlayer.height / 2) + drawPlayer.GetModPlayer<StarHelmetRangedPlayer>().portalOffset - Main.screenPosition;

                    DrawData data = new DrawData(texture, Center, texture.Frame(1, drawPlayer.GetModPlayer<StarHelmetRangedPlayer>().portalFrameCount, 0, drawPlayer.GetModPlayer<StarHelmetRangedPlayer>().portalFrame), Color.White, 0f, new Vector2(texture.Size().X, texture.Size().Y / 4) * .5f, 1f, drawInfo.playerEffect, 0)
                    {
                        shader = drawInfo.cBody
                    };
                    drawInfo.DrawDataCache.Add(data);
                }
            }
        }
    }
}