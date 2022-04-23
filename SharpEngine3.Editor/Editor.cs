using SE3.Graphics;
using ImGuiNET;
using System.Numerics;

namespace SE3Editor
{
    internal class Editor : Window
    {
        public Utils.Settings settings;

        private static bool showAbout;
        private static bool showEditorParameters;

        private static int country;
        private static string[] countries = new string[] { "en-US", "fr-FR" };

        public Editor() : base("SharpEngine 3 - Editor", 1280, 800)
        {
            if (File.Exists("settings.json"))
                settings = Utils.Settings.Load();
            else
                settings = new Utils.Settings();

            country = new List<string>(countries).IndexOf(settings.countryCode);
            if(country == -1)
                country = 0;
            SE3.Utils.Localization.SetCountry(countries[country]);
        }

        public override void OnLoad()
        {
            base.OnLoad();
            Widgets.GameViewport.OnLoad();
        }

        public override void OnRender(double delta)
        {
            base.OnRender(delta);

            Widgets.GameViewport.OnRender(this);
        }

        public override void CreateImGuiWindow()
        {
            base.CreateImGuiWindow();

            CreateMainMenu();
            Widgets.GameViewport.CreateImGuiWindow(this);
            Widgets.SceneTree.CreateImGuiWindow(this);
            Widgets.Properties.CreateImGuiWindow(this);
            Widgets.AssetsBrowser.CreateImGuiWindow(this);

            if (showAbout)
                CreateAboutWindow();
            if (showEditorParameters)
                CreateEditorParametersWindow();
        }

        private void CreateMainMenu()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu(Resources.strings.MainMenu_File))
                {
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu(Resources.strings.MainMenu_Project))
                {
                    ImGui.EndMenu();
                }
                if(ImGui.BeginMenu(Resources.strings.MainMenu_Parameters))
                {
                    ImGui.MenuItem(Resources.strings.MainMenu_Parameters_EditorParameters, null, ref showEditorParameters);
                    ImGui.MenuItem(Resources.strings.MainMenu_Parameters_About, null, ref showAbout);
                    ImGui.EndMenu();
                }
                ImGui.EndMainMenuBar();
            }
        }

        private void CreateEditorParametersWindow()
        {
            ImGui.SetNextWindowPos(new Vector2(0, 0), ImGuiCond.Once);
            ImGui.Begin(Resources.strings.EditorParameters, ref showEditorParameters);
            ImGui.Combo(Resources.strings.EditorParameters_Language, ref country, countries, 2);
            if (ImGui.Button(Resources.strings.EditorParameters_Validate))
            {
                settings.countryCode = countries[country];
                settings.Save();

                SE3.Utils.Localization.SetCountry(countries[country]);
            }
            ImGui.End();
        }

        private void CreateAboutWindow()
        {
            ImGui.SetNextWindowPos(new Vector2(0, 0), ImGuiCond.Once);
            ImGui.Begin(Resources.strings.About, ref showAbout);

            ImGui.Text(Resources.strings.About_Credits);
            ImGui.Separator();
            ImGui.Text(String.Format(Resources.strings.About_SE3Version, SE3.Constants.SE3_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_SE3EditorVersion, Constants.SE3Editor_VERSION));
            ImGui.Separator();
            ImGui.Text(String.Format(Resources.strings.About_GLVersion, SE3.Graphics.OpenGL.Constants.GL_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_GLSLVersion, SE3.Graphics.OpenGL.Constants.GLSL_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_RendererName, SE3.Graphics.OpenGL.Constants.RENDER_NAME));
            ImGui.Text(String.Format(Resources.strings.About_VendorName, SE3.Graphics.OpenGL.Constants.VENDOR_NAME));

            ImGui.End();
        }

    }
}
