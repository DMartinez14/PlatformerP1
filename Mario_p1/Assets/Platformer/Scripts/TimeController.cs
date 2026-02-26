using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   public TextMeshProUGUI timeText;
   float timeRemaining = 100;
   public TextMeshProUGUI CompleteText;
   private bool stopTimer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (CompleteText != null)
            CompleteText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTimer)
        {
            timeRemaining -= Time.deltaTime;
            timeText.text = $"TIME\n{((int)timeRemaining).ToString()}";
        }
        if(timeRemaining <= 0)
        {
            Debug.Log("Game Over!");
            if (CompleteText != null)
            {
                CompleteText.text = "Game Over!";
                CompleteText.gameObject.SetActive(true);
                timeRemaining = 0; 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If Timer is attached to Finish or another object, check for Player
        if (other.CompareTag("Player"))
        {
            stopTimer = true;
            if (CompleteText != null)
            {
                CompleteText.text = "You Won!";
                CompleteText.gameObject.SetActive(true);
            }
        }
    
    }
}
