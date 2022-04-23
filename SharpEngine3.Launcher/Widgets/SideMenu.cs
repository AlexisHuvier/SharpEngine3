using ImGuiNET;
using System.Numerics;

namespace SE3Launcher.Widgets
{
    internal class SideMenu
    {
        public static void CreateImGuiWindow(Launcher launcher)
        {
            ImGui.SetNextWindowPos(new Vector2(0, 0));
            ImGui.SetNextWindowSize(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.30f, launcher.internalWindow.FramebufferSize.Y));

            ImGui.Begin("SideMenu", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus);
            ImGui.BeginChild("SideMenuRender");

            ImGui.SetCursorPos(new Vector2(0, 0));
            if(ImGui.Button("P", new Vector2(20)))
                Launcher.showEditorParameters = true;
            ImGui.SetCursorPos(new Vector2(25, 0));
            if (ImGui.Button("A", new Vector2(20)))
                Launcher.showAbout = true;
            ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.15f - 100, launcher.internalWindow.FramebufferSize.Y * 0.5f - 130));
            if (ImGui.Button(Resources.strings.SideMenu_News, new Vector2(200, 30)))
                Launcher.currentPage = 0;
            ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.15f - 100, launcher.internalWindow.FramebufferSize.Y * 0.5f - 60));
            if (ImGui.Button(Resources.strings.SideMenu_Projects, new Vector2(200, 30)))
                Launcher.currentPage = 1;
            ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.15f - 100, launcher.internalWindow.FramebufferSize.Y * 0.5f + 10));
            if (ImGui.Button(Resources.strings.SideMenu_SE3, new Vector2(200, 30)))
                Launcher.currentPage = 2;
            ImGui.SetCursorPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.15f - 100, launcher.internalWindow.FramebufferSize.Y * 0.5f + 80));
            if (ImGui.Button(Resources.strings.SideMenu_Exit, new Vector2(200, 30)))
                launcher.Close();

            ImGui.EndChild();
            ImGui.End();
        }
    }
}
