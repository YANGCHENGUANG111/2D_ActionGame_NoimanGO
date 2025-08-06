using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay2 : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro的引用

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = ScoreManager.score.ToString();
    }
}
