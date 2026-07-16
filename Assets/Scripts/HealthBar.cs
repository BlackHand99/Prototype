using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        //currenthealthBar.fillAmmount = playerHealth.currentHealth / needs health ui;   
    }

    private void Update()
    {
        //currenthealthBar.fillAmmount = playerHealth.currentHealth / needs health ui;
    }
}


