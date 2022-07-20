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
    [SerializeField] private TextMeshProUGUI characterMenuBarTextMesh;

    private ExperienceSystem experienceSystem;
    // Start is called before the first frame update
    void Start()
    {
        experienceSystem = GetComponent<ExperienceSystem>();
    }

    public void SetExperienceToUI(int minExp, int currentExp, int maxExp) 
    {
        mainBar.Minimum = minExp;
        mainBar.Maximum = maxExp;
        mainBar.Current = currentExp;


        characterMenuBar.Minimum = minExp;
        characterMenuBar.Maximum = maxExp;
        characterMenuBar.Current = currentExp;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
