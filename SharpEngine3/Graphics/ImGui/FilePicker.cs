using System.Drawing;
using System.Numerics;
using ImGuiNET;
using static ImGuiNET.ImGui;

namespace SE3.Graphics.ImGui
{
    public class FilePicker
    {
        static readonly Dictionary<object, FilePicker> _filePickers = new Dictionary<object, FilePicker>();

        public string rootFolder;
        public string currentFolder;
        public string selectedFile;
        public List<string> allowedExtensions;
        public bool onlyAllowFolders;

        public bool Draw(Vector2 size)
        {
            Text($"Current Folder: {currentFolder}");
            bool result = false;

            if(BeginChildFrame(1, size))
            {
                DirectoryInfo di = new DirectoryInfo(currentFolder);
                if(di.Exists)
                {
                    if(di.Parent != null)//&& currentFolder != rootFolder)
                    {
                        PushStyleColor(ImGuiCol.Text, Utils.SE3Utils.GetVector4FromColor(Color.Yellow));
                        if (Selectable("../", false, ImGuiSelectableFlags.DontClosePopups))
                            currentFolder = di.Parent.FullName;

                        PopStyleColor();
                    }

                    List<string> fileSystemEntries = GetFileSystemEntries(di.FullName);
                    foreach(string fse in fileSystemEntries)
                    {
                        if(Directory.Exists(fse))
                        {
                            string name = Path.GetFileName(fse);
                            PushStyleColor(ImGuiCol.Text, Utils.SE3Utils.GetVector4FromColor(Color.Yellow));
                            if (Selectable(name + "/", false, ImGuiSelectableFlags.DontClosePopups))
                                currentFolder = fse;
                            PopStyleColor();
                        }
                        else
                        {
                            string name = Path.GetFileName(fse);
                            bool isSelected = selectedFile == fse;
                            if (Selectable(name, isSelected, ImGuiSelectableFlags.DontClosePopups))
                                selectedFile = fse;

                            if(IsMouseDoubleClicked(0))
                            {
                                result = true;
                                CloseCurrentPopup();
                            }
                        }
                    }
                }
            }
            EndChildFrame();

            if(Button("Cancel"))
            {
                result = false;
                CloseCurrentPopup();
            }

            if(onlyAllowFolders)
            {
                SameLine();
                if(Button("Open"))
                {
                    result = true;
                    selectedFile = currentFolder;
                    CloseCurrentPopup();
                }
            }
            else if(selectedFile != null)
            {
                SameLine();
                if(Button("Open"))
                {
                    result = true;
                    CloseCurrentPopup();
                }
            }

            return result;
        }

        List<string> GetFileSystemEntries(string fullName)
        {
            List<string> files = new List<string>();
            List<string> dirs = new List<string>();

            foreach(string fse in Directory.GetFileSystemEntries(fullName, ""))
            {
                if(Directory.Exists(fse))
                    dirs.Add(fse);
                else if(!onlyAllowFolders)
                {
                    if(allowedExtensions != null)
                    {
                        string ext = Path.GetExtension(fse);
                        if(allowedExtensions.Contains(ext))
                            files.Add(ext);
                    }
                    else
                        files.Add(fse);
                }
            }

            List<string> ret = new List<string>(dirs);
            ret.AddRange(files);
            return ret;
        }
        
        bool TryGetFileInfo(string fileName, out FileInfo realFile)
        {
            try
            {
                realFile = new FileInfo(fileName);
                return true;
            }
            catch
            {
                realFile = null;
                return false;
            }
        }

        public static void RemoveFilePicker(object o) => _filePickers.Remove(o);

        public static FilePicker GetFolderPicker(object o, string startingPath) => GetFilePicker(0, startingPath, null, true);

        public static FilePicker GetFilePicker(object o, string startingPath, string searchFilter = null, bool onlyAllowFolders = false)
        {
            if(File.Exists(startingPath))
                startingPath = new FileInfo(startingPath).DirectoryName;
            else if(string.IsNullOrEmpty(startingPath) || !Directory.Exists(startingPath))
            {
                startingPath = Environment.CurrentDirectory;
                if (string.IsNullOrEmpty(startingPath))
                    startingPath = AppContext.BaseDirectory;
            }

            if(!_filePickers.TryGetValue(o, out FilePicker fp))
            {
                fp = new FilePicker();
                fp.rootFolder = startingPath;
                fp.currentFolder = startingPath;
                fp.onlyAllowFolders = onlyAllowFolders;

                if(searchFilter != null)
                {
                    if (fp.allowedExtensions != null)
                        fp.allowedExtensions.Clear();
                    else
                        fp.allowedExtensions = new List<string>();

                    fp.allowedExtensions.AddRange(searchFilter.Split('|', StringSplitOptions.RemoveEmptyEntries));
                }
                _filePickers.Add(o, fp);
            }
            return fp;
        }
    }
}
