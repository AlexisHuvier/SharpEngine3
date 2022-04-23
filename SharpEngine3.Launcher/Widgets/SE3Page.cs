using ImGuiNET;
using System.Numerics;

namespace SE3Launcher.Widgets
{
    internal class SE3Page
    {
        public static void CreateImGuiWindow(Launcher launcher)
        {
            ImGui.SetNextWindowPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.30f, 0));
            ImGui.SetNextWindowSize(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f, launcher.internalWindow.FramebufferSize.Y));

            ImGui.Begin("SE3 Page", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus);
            ImGui.BeginChild("SE3PageRender");
            ImGui.Text("SE3");
            ImGui.EndChild();
            ImGui.End();
        }
    }
}
