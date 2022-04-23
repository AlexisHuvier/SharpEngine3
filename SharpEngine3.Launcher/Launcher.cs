using SE3.Graphics;
using ImGuiNET;
using System.Numerics;

namespace SE3Launcher
{
    internal class Launcher: Window
    {
        public Utils.Settings settings;
        public Managers.ProjectManager projectManager;
        public Managers.SE3Manager se3Manager;

        public static bool showAbout;
        public static bool showEditorParameters;
        public static int currentPage = 0;

        private static int country;
        private static string[] countries = new string[] { "en-US", "fr-FR" };

        public Launcher() : base("SharpEngine 3 - Launcher", 900, 600)
        {
            if (File.Exists("settings.json"))
                settings = Utils.Settings.Load();
            else
                settings = new Utils.Settings();

            projectManager = new Managers.ProjectManager();
            se3Manager = new Managers.SE3Manager();
            country = new List<string>(countries).IndexOf(settings.countryCode);
            if (country == -1)
                country = 0;
            SE3.Utils.Localization.SetCountry(countries[country]);
        }

        public override void CreateImGuiWindow()
        {
            base.CreateImGuiWindow();

            Widgets.SideMenu.CreateImGuiWindow(this);

            if (currentPage == 0)
                Widgets.NewsPage.CreateImGuiWindow(this);
            else if (currentPage == 1)
                Widgets.ProjectsPage.CreateImGuiWindow(this);
            else
                Widgets.SE3Page.CreateImGuiWindow(this);

            if (showAbout)
                CreateAboutWindow();
            if (showEditorParameters)
                CreateLauncherParametersWindow();
        }

        private void CreateLauncherParametersWindow()
        {
            ImGui.SetNextWindowPos(new Vector2(0, 20), ImGuiCond.Once);
            ImGui.Begin(Resources.strings.LauncherParameters, ref showEditorParameters);
            ImGui.Combo(Resources.strings.LauncherParameters_Language, ref country, countries, 2);
            if (ImGui.Button(Resources.strings.LauncherParameters_Validate))
            {
                settings.countryCode = countries[country];
                settings.Save();

                SE3.Utils.Localization.SetCountry(countries[country]);
            }
            ImGui.End();
        }

        private void CreateAboutWindow()
        {
            ImGui.SetNextWindowPos(new Vector2(0, 20), ImGuiCond.Once);
            ImGui.Begin(Resources.strings.About, ref showAbout);

            ImGui.Text(Resources.strings.About_Credits);
            ImGui.Separator();
            ImGui.Text(String.Format(Resources.strings.About_SE3Version, SE3.Constants.SE3_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_SE3LauncherVersion, Constants.SE3Launcher_VERSION));
            ImGui.Separator();
            ImGui.Text(String.Format(Resources.strings.About_GLVersion, SE3.Graphics.OpenGL.Constants.GL_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_GLSLVersion, SE3.Graphics.OpenGL.Constants.GLSL_VERSION));
            ImGui.Text(String.Format(Resources.strings.About_RendererName, SE3.Graphics.OpenGL.Constants.RENDER_NAME));
            ImGui.Text(String.Format(Resources.strings.About_VendorName, SE3.Graphics.OpenGL.Constants.VENDOR_NAME));

            ImGui.End();
        }
    }
}
