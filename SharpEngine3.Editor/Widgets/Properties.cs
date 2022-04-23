using ImGuiNET;
using System.Numerics;

namespace SE3Editor.Widgets
{
    internal class Properties
    {
        public static void CreateImGuiWindow(Editor editor)
        {
            ImGui.SetNextWindowPos(new Vector2(editor.internalWindow.FramebufferSize.X - editor.internalWindow.FramebufferSize.X * 0.25f, 20));
            ImGui.SetNextWindowSize(new Vector2(editor.internalWindow.FramebufferSize.X * 0.25f, editor.internalWindow.FramebufferSize.Y));

            ImGui.Begin(Resources.strings.Properties, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);
            ImGui.BeginChild("PropertiesRender");
            ImGui.EndChild();
            ImGui.End();
        }
    }
}
