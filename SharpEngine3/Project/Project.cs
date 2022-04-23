namespace SE3.Project
{
    public class Project
    {
        public string name;
        public Version SE3Version;

        public Project(string name)
        {
            this.name = name;
            SE3Version = Constants.SE3_VERSION;
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Join("Projects", name)))
                Directory.CreateDirectory(Path.Join("Projects", name));
            Utils.JsonSave.SaveObj(this, Path.Join("Projects", name, "project.se3proj"));
        }

        public static Project Load(string name)
        {
            return Utils.JsonSave.LoadObj<Project>(Path.Join("Projects", name, "project.se3proj"));
        }
    }
}
