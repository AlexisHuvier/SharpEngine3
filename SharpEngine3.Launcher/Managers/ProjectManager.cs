using SE3.Project;
using SE3.Utils;

namespace SE3Launcher.Managers
{

    internal class ProjectManager
    {
        internal List<Project> projects;

        public ProjectManager()
        {
            projects = new List<Project>();
            if (!Directory.Exists("Projects"))
                Directory.CreateDirectory("Projects");

            foreach(string project in Directory.EnumerateFileSystemEntries("Projects"))
            {
                if(File.Exists(Path.Join(project, "project.se3proj")))
                    projects.Add(Project.Load(project.Replace("Projects\\", "")));
            }
        }

        public void AddProject(string name)
        {
            Project project = new Project(name);
            project.Save();
            projects.Add(project);
        }

        public void RemoveProject(Project project)
        {
            projects.Remove(project);
            Directory.Delete(Path.Join("Projects", project.name), true);
        }
    }
}
