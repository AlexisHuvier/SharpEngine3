using ImGuiNET;
using System.Numerics;
using System.Diagnostics;
using SE3.Project;

namespace SE3Launcher.Widgets
{
    internal class ProjectsPage
    {
        private static string nameProject = "";

        public static void CreateImGuiWindow(Launcher launcher)
        {
            ImGui.SetNextWindowPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.30f, 0));
            ImGui.SetNextWindowSize(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f, launcher.internalWindow.FramebufferSize.Y));

            ImGui.Begin("Projects Page", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus);

            ImGui.Separator();
            ImGui.InputText(Resources.strings.ProjectsPage_NameProject, ref nameProject, 50);
            if (ImGui.Button(Resources.strings.ProjectsPage_CreateProject))
            {
                if (!launcher.projectManager.projects.Select(project => project.name == nameProject).Any())
                {
                    launcher.projectManager.AddProject(nameProject);
                    nameProject = "";
                }
            }
            ImGui.Separator();

            for(int i = 0; i < launcher.projectManager.projects.Count; i++) 
            {
                Project project = launcher.projectManager.projects[i];
                ImGui.SetWindowFontScale(1.3f);
                ImGui.Text(project.name);
                ImGui.SetWindowFontScale(1f);
                ImGui.Spacing();
                ImGui.Spacing();
                ImGui.Text(string.Format(Resources.strings.ProjectsPage_SE3Version, project.SE3Version));
                ImGui.Spacing();
                var temp = ImGui.GetCursorPos();
                ImGui.Button($"{Resources.strings.ProjectsPage_Launch}##{i}");
                ImGui.SetCursorPos(temp + new Vector2(70, 0));
                if (ImGui.Button($"{Resources.strings.ProjectsPage_Delete}##{i}"))
                    launcher.projectManager.RemoveProject(project);
                ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f - ImGui.CalcTextSize(Resources.strings.ProjectsPage_OpenExplorer).X - 20, temp.Y));
                if(ImGui.Button($"{Resources.strings.ProjectsPage_OpenExplorer}##{i}"))
                {
                    if(Directory.Exists(Path.Join("Projects", project.name))) {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            Arguments = Path.Join("Projects", project.name),
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
