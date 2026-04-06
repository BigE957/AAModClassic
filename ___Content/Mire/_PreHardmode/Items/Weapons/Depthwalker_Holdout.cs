using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Depthwalker : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
 
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 14;//Set the projectile hitbox width
            Projectile.height = 14; //Set the projectile hitbox height            
            Projectile.aiStyle = ProjAIStyleID.Yoyo; // aiStyle 99 is used for all yoyos, and is Extremely suggested, as yoyo are extremely difficult without them
            Projectile.friendly = true;  //Tells the game whether it is friendly to players/friendly npcs or not
            Projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed. -1 = never
            Projectile.DamageType = DamageClass.Melee; //Tells the game whether it is a melee projectile or not        
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player.
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 5f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player.
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 150f;
            // YoyosTopSpeed is top speed of the yoyo projectile.
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12f;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Depthwalker");
    }
        
     }
}
