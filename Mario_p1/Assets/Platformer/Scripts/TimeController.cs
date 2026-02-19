using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public TextMeshProUGUI timeText;
   float timeRemaining = 500;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timeText.text = $"TIME\n{((int)timeRemaining).ToString()}";
        
    }
}
