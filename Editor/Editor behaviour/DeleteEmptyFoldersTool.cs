using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    public static class DeleteEmptyFoldersTool
    {
        [MenuItem("Tools/Cleanup/Delete Empty Folders")]
        public static void DeleteEmptyDirectories()
        {
            DirectoryInfo assetsDirectory = new DirectoryInfo(Application.dataPath);
            List<DirectoryInfo> emptyDirectories = new List<DirectoryInfo>();

            FindEmptyDirectoriesRecursive(assetsDirectory, emptyDirectories);

            DeleteDirectories(emptyDirectories);

            LogEmptyDirectories(emptyDirectories);
        }

        static void FindEmptyDirectoriesRecursive(DirectoryInfo directory, List<DirectoryInfo> emptyDirectories)
        {
            DirectoryInfo[] children = directory.GetDirectories();

            foreach (DirectoryInfo child in children)
            {
                if (DirectoryIsEmpty(child))
                {
                    emptyDirectories.Add(child);
                }
                else
                {
                    FindEmptyDirectoriesRecursive(child, emptyDirectories);
                }
            }

            bool allChildrenAreEmpty = children.All(child => emptyDirectories.Contains(child));

            if (allChildrenAreEmpty)
            {
                if (OnlyFilesAreChildDirectoryMetaFiles(directory))
                {
                    ReplaceChildrenWithParent(directory, emptyDirectories, children);
                }
            }
        }

        static void ReplaceChildrenWithParent(DirectoryInfo parent, List<DirectoryInfo> emptyDirectories, DirectoryInfo[] children)
        {
            foreach (DirectoryInfo child in children)
            {
                emptyDirectories.Remove(child);
            }

            emptyDirectories.Add(parent);
        }

        static bool OnlyFilesAreChildDirectoryMetaFiles(DirectoryInfo directory)
        {
            DirectoryInfo[] children = directory.GetDirectories();

            return directory.GetFiles().Length == children.Length &&
                                children.All(child => File.Exists(GetMetaFilePath(child)));
        }

        static bool DirectoryIsEmpty(DirectoryInfo directory)
        {
            return (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0);
        }

        static string GetMetaFilePath(DirectoryInfo directory)
        {
            return directory.FullName + ".meta";
        }


        static void DeleteDirectories(IReadOnlyCollection<DirectoryInfo> directories)
        {
            foreach (DirectoryInfo directory in directories)
            {
                Directory.Delete(directory.FullName, true);
                File.Delete(GetMetaFilePath(directory));
            }

            AssetDatabase.Refresh();
        }

        static void LogEmptyDirectories(List<DirectoryInfo> emptyDirectories)
        {
            StringBuilder sb = new StringBuilder();
            string assetsFolderPath = Path.GetDirectoryName(Application.dataPath);

            sb.Append($"Deleted <b>{emptyDirectories.Count}</b> empty directories: ");

            if (emptyDirectories.Count > 0)
            {
                for (int i = 0; i < emptyDirectories.Count; i++)
                {
                    string relativePath = emptyDirectories[i].RelativeTo(assetsFolderPath);

                    sb.Append($"<b>{relativePath}</b>");
                    if (i < emptyDirectories.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }

            Debug.Log(sb.ToString());
        }
    }
}
