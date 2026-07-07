using AAModClassic._CrossMod.Overhaul;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class DarkmatterSlasher_Slash : ModProjectile
    {
        public static Asset<Texture2D> specialSlash;
        public static int specialProjFrames = 7;
        readonly int chargeSlashDirection = 1;


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
            if (Main.netMode == NetmodeID.Server) 
                return;
            //TODO: What the fuck are you doing
            Player projOwner = Main.player[Projectile.owner];
            Projectile.position.X = projOwner.Center.X - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - Projectile.height / 2;
            specialSlash = ModContent.Request<Texture2D>(Texture + "2");
            Projectile.direction = projOwner.direction;
            Projectile.spriteDirection = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
        }
        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 66;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override bool? CanCutTiles() { return true; }
        public int FrameCheck
        {
            get { return (int)Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }
        public int SlashLogic
        {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Saber.AINormalSlash(Projectile, SlashLogic)) { }
            else
            {
                // Charged attack
                Saber.AISetChargeSlashVariables(player, chargeSlashDirection);
                Saber.NormalSlash(Projectile, player);
            }
            //Projectile.damage = 0;
            Projectile.ai[0] += 1f; // Framerate
            Projectile.position += player.velocity;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            int weaponItemID = ModContent.ItemType<DarkmatterSlasher>();
            Color lighting = Lighting.GetColor((int)(player.MountedCenter.X / 16), (int)(player.MountedCenter.Y / 16));
            return Saber.PreDrawSlashAndWeapon(Main.spriteBatch, Projectile, weaponItemID, lighting,
                SlashLogic == 0f ? specialSlash.Value : null,
                SlashLogic == 0f ? new Color(1f, 255f, 181f, 1f) : lighting,
                specialProjFrames,
                SlashLogic == 0f ? chargeSlashDirection : SlashLogic);
        }

    }
}
