using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthImage;

    [SerializeField] private Sprite hp3;
    [SerializeField] private Sprite hp2;
    [SerializeField] private Sprite hp1;

    private void Update()
    {
        switch (Mathf.CeilToInt(playerHealth.CurrentHealth))
        {
            case 3:
                healthImage.sprite = hp3;
                break;

            case 2:
                healthImage.sprite = hp2;
                break;

            case 1:
                healthImage.sprite = hp1;
                break;
        }
    }
}