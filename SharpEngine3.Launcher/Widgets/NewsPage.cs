using ImGuiNET;
using System.Numerics;

namespace SE3Launcher.Widgets
{
    internal class NewsPage
    {
        public static void CreateImGuiWindow(Launcher launcher)
        {
            ImGui.SetNextWindowPos(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.30f, 0));
            ImGui.SetNextWindowSize(new Vector2(launcher.internalWindow.FramebufferSize.X * 0.70f, launcher.internalWindow.FramebufferSize.Y));

            ImGui.Begin("News Page", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus);
            ImGui.BeginChild("NewsPageRender");
            ImGui.Separator();

            foreach(Utils.Article article in Utils.NewsGetter.News.articles)
            {
                ImGui.Spacing();
                if (launcher.settings.countryCode == "fr-FR")
                {
                    ImGui.SetWindowFontScale(1.3f);
                    ImGui.TextWrapped(article.title_fr);
                    ImGui.Spacing();
                    ImGui.Spacing();
                    ImGui.SetWindowFontScale(1f);
                    foreach (string line in article.lines_fr)
                        ImGui.TextWrapped(line);
                    ImGui.Spacing();
                }
                else
                {
                    ImGui.SetWindowFontScale(1.3f);
                    ImGui.TextWrapped(article.title_en);
                    ImGui.Spacing();
                    ImGui.Spacing();
                    ImGui.SetWindowFontScale(1f);
                    foreach (string line in article.lines_en)
                        ImGui.TextWrapped(line);
                    ImGui.Spacing();
                }
                ImGui.Separator();
            }

            ImGui.EndChild();
            ImGui.End();
        }
    }
}
