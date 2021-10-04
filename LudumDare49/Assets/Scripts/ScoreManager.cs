using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Canvas canv;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform child = canv.transform.Find("Text");
        Text t = child.GetComponent<Text>();
        t.text = score.ToString();
    }
}
