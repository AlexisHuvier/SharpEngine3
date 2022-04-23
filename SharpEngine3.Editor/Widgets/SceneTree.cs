using ImGuiNET;
using System.Numerics;

namespace SE3Editor.Widgets
{
    internal class SceneTree
    {
        public static void CreateImGuiWindow(Editor editor)
        {
            ImGui.SetNextWindowPos(new Vector2(0, 20));
            ImGui.SetNextWindowSize(new Vector2(editor.internalWindow.FramebufferSize.X * 0.25f, editor.internalWindow.FramebufferSize.Y * 0.65f));

            ImGui.Begin(Resources.strings.SceneTree, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);
            ImGui.BeginChild("SceneTreeRender");
            ImGui.EndChild();
            ImGui.End();
        }
    }
}
