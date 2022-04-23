using ImGuiNET;
using System.Numerics;

namespace SE3Editor.Widgets
{
    internal class AssetsBrowser
    {
        public static void CreateImGuiWindow(Editor editor)
        {
            ImGui.SetNextWindowPos(new Vector2(0, editor.internalWindow.FramebufferSize.Y - editor.internalWindow.FramebufferSize.Y * 0.35f + 20));
            ImGui.SetNextWindowSize(new Vector2(editor.internalWindow.FramebufferSize.X * 0.75f, editor.internalWindow.FramebufferSize.Y * 0.35f - 20));

            ImGui.Begin(Resources.strings.AssetsBrowser, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);
            ImGui.BeginChild("AssetsBrowserRender");
            ImGui.EndChild();
            ImGui.End();
        }
    }
}
