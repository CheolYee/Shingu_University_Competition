using System.IO;
using UnityEngine;

namespace _00._Work.Teams.PMC._01._Codes
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(this);
            }
        }
        
        private static string StageClearFilePath => Application.persistentDataPath + "/StageClear.json";


        private static SaveStageData LoadSaveStageData()
        {
            if (!File.Exists(StageClearFilePath))
            {
                SaveStageData saveStageData = new SaveStageData();
                saveStageData.clearStageIds.Add("3");
                saveStageData.clearTutorialIds.Add("16");
                SaveStageData(saveStageData);
                return saveStageData;
            }

            string json = File.ReadAllText(StageClearFilePath);
            return JsonUtility.FromJson<SaveStageData>(json);
        }

        private static void SaveStageData(SaveStageData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(StageClearFilePath, json);
        }
        
        public static void SaveStageId(string saveStageData)
        {
            var data = LoadSaveStageData();
            if (!data.clearStageIds.Contains(saveStageData))
            {
                data.clearStageIds.Add(saveStageData);
                SaveStageData(data);
            }
        }
        
        public static void SaveTutorialStageId(string saveStageData)
        {
            var data = LoadSaveStageData();
            if (!data.clearTutorialIds.Contains(saveStageData))
            {
                data.clearTutorialIds.Add(saveStageData);
                SaveStageData(data);
            }
        }

        public static bool IsStageCleared(string stageId, bool isTutorial = false)
        {
            if (isTutorial) return LoadSaveStageData().clearTutorialIds.Contains(stageId);
            return LoadSaveStageData().clearStageIds.Contains(stageId);
        }

    }
}
