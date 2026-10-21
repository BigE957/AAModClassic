using AAModClassic._Content.Chaos._PostMoonlord.Items.Dyes;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Dyes;
using AAModClassic._Content.Mire._PostMoonlord.Items.Dyes;
using AAModClassic._Content.Void._PostMoonlord.Items.Dyes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Effects
{
    public class ShaderSystem : ModSystem
    {
        private const string ShaderPrefix = "AAModClassic:";
        public override void PostSetupContent()
        {
            if (Main.dedServ)
                return;

            RegisterScreenShader("Shockwave", "Shockwave", EffectPriority.VeryHigh);

            RegisterScreenShader("RadialBlur", "RadialBlurPass");

            RegisterMiscShader("Mask", "Mask");

            RegisterMiscShader("Masking", "MaskingPass");

            RegisterMiscShader("MaskEdge", "EdgePass");

            BindDyeShaders();
        }

        private static Asset<Effect> LoadShader(string fileName)
        {
            string shaderPath = "Effects/";
            return AAMod.instance.Assets.Request<Effect>($"{shaderPath}{fileName}", AssetRequestMode.ImmediateLoad);
        }


        private static void RegisterScreenShader(string shaderName, string passName, EffectPriority priority = EffectPriority.High)
        {
            var effect = LoadShader(shaderName);
            ScreenShaderData shaderdata = new(effect, passName);
            Filters.Scene[ShaderPrefix + shaderName] = new Filter(shaderdata, priority);
            Filters.Scene[ShaderPrefix + shaderName].Load();
        }

        private static void RegisterMiscShader(string shaderName, string passName)
        {
            var effect = LoadShader(shaderName);
            MiscShaderData passParamRegistration = new(effect, passName);
            GameShaders.Misc[ShaderPrefix + shaderName] = passParamRegistration;
        }

        private static void BindDyeShaders()
        {
            GameShaders.Armor.BindShader(ModContent.ItemType<BlazingDye>(),
                NewShaderFrom(ItemID.LivingFlameDye)
                .UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f)
                .UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<AbyssalDye>(),
                NewShaderFrom(ItemID.LivingFlameDye)
                .UseColor(146f / 255f, 30f / 255f, 68f / 255f)
                .UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<DoomsdayDye>(),
                NewShaderFrom(ItemID.VortexDye)
                .UseImage("Images/Misc/noise")
                .UseColor(0f, 0f, 0f)
                .UseSecondaryColor(1f, 0f, 0f)
                .UseSaturation(1f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<DiscordianDye>(),
                NewShaderFrom(ItemID.LivingFlameDye)
                .UseColor(0.66f, 0f, 1f)
                .UseSecondaryColor(0.66f, 0f, 1f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<DiscordianInfernoDye>(),
                NewShaderFrom(ItemID.HadesDye)
                .UseColor(0.88f, 0f, 1f)
                .UseSecondaryColor(0.66f, 0f, 1f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<AbyssalWrathDye>(),
                NewShaderFrom(ItemID.HadesDye)
                .UseColor(146f / 255f, 30f / 255f, 68f / 255f)
                .UseSecondaryColor(105f / 255f, 20f / 255f, 50f / 255f)
            );

            GameShaders.Armor.BindShader(ModContent.ItemType<BlazingFuryDye>(),
                NewShaderFrom(ItemID.HadesDye)
                .UseColor(Color.SkyBlue.R / 255f, Color.SkyBlue.G / 255f, Color.SkyBlue.B / 255f)
                .UseSecondaryColor(Color.DeepSkyBlue.R / 255f, Color.DeepSkyBlue.G / 255f, Color.DeepSkyBlue.B / 255f)
            );
        }

        private static readonly FieldInfo ShaderRefField = typeof(ShaderData).GetField("_shader", BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo AssetField = typeof(ShaderData).GetField("_asset", BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo PassNameField = typeof(ShaderData).GetField("_passName", BindingFlags.NonPublic | BindingFlags.Instance);

        private static ArmorShaderData NewShaderFrom(int vanillaDyeId)
        {
            ArmorShaderData baseShader = GameShaders.Armor.GetShaderFromItemId(vanillaDyeId);

            var newShader = (ArmorShaderData)RuntimeHelpers.GetUninitializedObject(typeof(ArmorShaderData));

            ShaderRefField.SetValue(newShader, ShaderRefField.GetValue(baseShader));
            AssetField.SetValue(newShader, AssetField.GetValue(baseShader));
            PassNameField.SetValue(newShader, PassNameField.GetValue(baseShader));

            return newShader;
        }
    }
}
