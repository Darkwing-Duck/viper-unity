using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityViper.Editor
{
    public class ModuleCreationPopup : EditorWindow
    {
        [MenuItem("Assets/Create/Viper Module", false, 0)]
        private static void GenerateModule()
        {
            ModuleCreationPopup popup = GetWindow<ModuleCreationPopup>(true, "Create Viper Module");
            popup.ShowUtility();
        }

        private string _moduleName = "NewModule";
        
        protected virtual void OnGUI()
        {
            _moduleName = EditorGUILayout.TextField("Module Name:", _moduleName);

            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create"))
            {
                _moduleName = Regex.Replace(_moduleName, @"\s+", "");
                
                ViperModuleGenerator generator = new ViperModuleGenerator(_moduleName);
                generator.Generate();
                Close();
            }
        }
    }
    
    public class ViperModuleGenerator
    {
        public string ModuleName { get; set; }

        public ViperModuleGenerator(string moduleName)
        {
            ModuleName = moduleName;
        }

        public void Generate()
        {
            string MODULE_NAMESPACE = "UnityViper.Modules";
            string targetPath = Path.Combine(Application.dataPath, "Modules");

            Object selectedObject = Selection.activeObject;
            if (selectedObject != null)
            {
                targetPath = AssetDatabase.GetAssetPath(selectedObject.GetInstanceID());
            }

            string templatePath = Path.Combine(Application.dataPath, "Scripts/UnityViper/Editor/Template");
            string[] templateFiles = Directory.GetFiles(templatePath, "*.template", SearchOption.AllDirectories);

            foreach (string filePath in templateFiles)
            {
                string fileContent = File.ReadAllText(filePath);
                string fileDirectory = filePath.Replace($"{templatePath}/", "");
                fileDirectory = Path.GetDirectoryName(fileDirectory);
                
                string fileName = Path.GetFileNameWithoutExtension(filePath)
                    .Replace("{{MODULE_NAME}}", ModuleName);

                string result = fileContent
                    .Replace("{{MODULE_NAMESPACE}}", MODULE_NAMESPACE)
                    .Replace("{{MODULE_NAME}}", ModuleName);

                string targetDirectory = Path.Combine(targetPath, ModuleName, fileDirectory);

                if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);

                string targetFilePath = Path.Combine(targetDirectory, $"{fileName}.cs");
                File.WriteAllText(targetFilePath, result);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}