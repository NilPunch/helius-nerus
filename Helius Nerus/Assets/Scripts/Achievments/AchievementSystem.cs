using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem
{
    private class AchievementsSaving
    {
        public List<bool> Achievements = new List<bool>();

        public void UpdateInfo(List<Achievment> achievements)
        {
            Achievements.Clear();
            foreach (Achievment achievment in achievements)
            {
                Achievements.Add(achievment.WasTriggered);
            }
        }
    }

    private List<Achievment> _achievments = new List<Achievment>();
    private AchievementsSaving _achievementsSaving = new AchievementsSaving();

    public static AchievementSystem Instance
    {
        get;
        private set;
    } = null;

    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new AchievementSystem();

            Instance.AddAchievementsHere();
            Instance.LoadAchievements();

            // Temp test
            //Instance.ResetAchievements();
        }
    }

    private void ResetAchievements()
    {
        foreach (Achievment achievment in _achievments)
        {
            achievment.Reset();
        }
        SaveAchievments();
    }

    private void AddAchievementsHere()
    {
        /// DON'T REMOVE THIS LINE!
        _achievments.Add(new FirstResurrectionAchievement());

        /// Auto-generated achievements go here (up)

        _achievments.Add(new DeathAchievement());
    }

    public void SaveAchievments()
    {
        _achievementsSaving.UpdateInfo(_achievments);
        PlayerPrefs.SetString("AchievementsData", JsonUtility.ToJson(_achievementsSaving));
        //PlayerPrefs.Save();
        return;
    }

    private void LoadAchievements()
    {
        if (PlayerPrefs.HasKey("AchievementsData") == false)
        {
#if UNITY_EDITOR
            Debug.Log("PlayerPrefs for AchievementsData not exist!");
#endif
            SaveAchievments();
            return;
        }
        _achievementsSaving = JsonUtility.FromJson<AchievementsSaving>(PlayerPrefs.GetString("AchievementsData"));

        int i = 0;

        // Existed ones
        for (; i < _achievementsSaving.Achievements.Count; ++i)
        {
            _achievments[i].Init(_achievementsSaving.Achievements[i]);
        }

#if UNITY_EDITOR
        if (i < _achievments.Count - 1)
            Debug.Log("Has new achievements in system");
#endif
        // Not existed ones
        for (; i < _achievments.Count; ++i)
        {
            _achievments[i].Init(false);
        }
        return;
    }
}
