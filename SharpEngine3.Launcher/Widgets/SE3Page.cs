using ImGuiNET;
using System.Diagnostics;
using System.Numerics;

namespace SE3Launcher.Widgets
{
    internal class SE3Page
    {
        public static int versionChoose = 0;

        public static void CreateImGuiWindow(Launcher launcher)
        {
            ImGui.SetNextWindowPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.30f, 0));
            ImGui.SetNextWindowSize(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f, launcher.internalWindow.FramebufferSize.Y));

            ImGui.Begin("SE3 Page", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus);

            ImGui.Separator();
            ImGui.Combo("Version", ref versionChoose, Utils.SE3Getter.se3Versions.versions.Select(t => t.version).ToArray(), Utils.SE3Getter.se3Versions.versions.Count);
            if (ImGui.Button("Download"))
                launcher.se3Manager.AddVersion(Utils.SE3Getter.se3Versions.versions[versionChoose].version);
            ImGui.Separator();

            for (int i = 0; i < launcher.se3Manager.versions.Count; i++)
            {
                string version = launcher.se3Manager.versions[i];
                ImGui.SetWindowFontScale(1.3f);
                ImGui.Text(version);
                ImGui.SetWindowFontScale(1f);
                ImGui.Spacing();
                var temp = ImGui.GetCursorPos();
                if(ImGui.Button($"Delete##{i}"))
                    launcher.se3Manager.RemoveVersion(version);
                ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f - ImGui.CalcTextSize(Resources.strings.ProjectsPage_OpenExplorer).X - 20, temp.Y));
                if (ImGui.Button($"Open Explorer##{i}"))
                {
                    if (Directory.Exists(Path.Join("SE3Versions", version)))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            Arguments = Path.Join("SE3Versions", version),
                            FileName = "explorer.exe"
                        };

                        Process.Start(startInfo);
                    }
                }
                ImGui.Separator();
            }

            ImGui.End();
        }
    }
}
