using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar() 
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    [SerializeField] private int minimum;
    [SerializeField] private int maximum;
    [SerializeField] private int current;
    [SerializeField] private Image fill;
    [SerializeField] private Color color;

    public ProgressBar(int min, int max, int current) 
    {
        this.minimum = min;
        this.maximum = max;
        this.current = current;
    }
    public int Minimum 
    {
        get 
        {
            return minimum;
        }
        set 
        {
            minimum = value;
        }
    }
    public int Maximum
    {
        get
        {
            return maximum;
        }
        set
        {
            maximum = value;
        }
    }
    public int Current
    {
        get
        {
            return current;
        }
        set
        {
            current = value;
        }
    }
    public Image Fill
    {
        get
        {
            return fill;
        }
        set
        {
            fill = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill() 
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;
        float fillAmount = currentOffset/maximumOffset;
        Fill.fillAmount = fillAmount;
    }
}
