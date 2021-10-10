using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 
public class HealthBarHandler : MonoBehaviour
{
    private Image HealthBarImage;

    [SerializeField] Color red;
    [SerializeField] Color orange;
    [SerializeField] Color green;

    /// Initialize the variable
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    /// Sets the health bar value
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        if (HealthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(red);
        }
        else if (HealthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(orange);
        }
        else
        {
            SetHealthBarColor(green);
        }
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// Sets the health bar color
    /// <param name="healthColor">Color </param>
    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }


}