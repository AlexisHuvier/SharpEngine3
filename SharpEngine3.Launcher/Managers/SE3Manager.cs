namespace SE3Launcher.Managers
{
    internal class SE3Manager
    {
        public List<string> versions = new List<string>();

        public SE3Manager()
        {
            versions = new List<string>();
            if (!Directory.Exists("SE3Versions"))
                Directory.CreateDirectory("SE3Versions");

            foreach (string version in Directory.EnumerateFileSystemEntries("SE3Versions"))
            {
                if (Directory.Exists(version))
                    versions.Add(version.Replace("SE3Versions\\", ""));
            }
        }

        public void AddVersion(string version)
        {
            _ = Utils.SE3Getter.DownloadVersion(version);
            versions.Add(version);
        }

        public void RemoveVersion(string version)
        {
            versions.Remove(version);
            Directory.Delete(Path.Join("SE3Versions", version), true);
        }
    }
}
