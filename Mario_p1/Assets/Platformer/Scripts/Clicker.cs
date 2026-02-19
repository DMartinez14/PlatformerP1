using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    private int coinCount = 0;

 void Update()
{
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Brick"))
            {
                Destroy(hitObj);
            }
            else if (hitObj.CompareTag("Question"))
            {
                AddCoin();
            }
        }
    }
}

    void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = $"X{coinCount}";
    }
}
