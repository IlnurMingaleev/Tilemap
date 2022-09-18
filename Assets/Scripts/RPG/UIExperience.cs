using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIExperience : MonoBehaviour
{
    [SerializeField] private ProgressBar mainBar;
    [SerializeField] private TextMeshProUGUI mainBarTextMesh;

    [SerializeField] private ProgressBar characterMenuBar;
    [SerializeField] private TextMeshProUGUI expTextMesh;
    [SerializeField] private TextMeshProUGUI expToNextLevelTextMesh;
    [SerializeField] private TextMeshProUGUI levelTextMesh;

    private ExperienceSystem experienceSystem;
    // Start is called before the first frame update
    void Start()
    {
        experienceSystem = GetComponent<ExperienceSystem>();
    }

    public void SetExperienceToUI(ExperienceSystem experienceSystem) 
    {
        mainBar.Minimum = experienceSystem.GetMinExp();
        mainBar.Maximum = experienceSystem.GetMaxExp();
        mainBar.Current = experienceSystem.GetCurrentExp();
        mainBarTextMesh.text = string.Format("{0}/{1}", experienceSystem.GetCurrentExp(), experienceSystem.GetMaxExp());


        characterMenuBar.Minimum = experienceSystem.GetMinExp();
        characterMenuBar.Maximum = experienceSystem.GetMaxExp();
        characterMenuBar.Current = experienceSystem.GetCurrentExp();
        expTextMesh.text = string.Format("XP:{0}", experienceSystem.GetCurrentExp());
        expToNextLevelTextMesh.text = string.Format("Next level in: {0}", experienceSystem.GetExpToNextLevel());
        levelTextMesh.text = string.Format("LEVEL: {0}", experienceSystem.GetLevel());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
