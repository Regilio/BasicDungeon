using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{

    public int HPMax;
    public Text HPText;


    public void UpdateHPBar(int HPCurrent)
    {
        float ratio = (float)HPCurrent / (float)HPMax;
        
        GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
        HPText.text = HPCurrent.ToString() + " / " + HPMax.ToString();
    }
}