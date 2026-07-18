using UnityEngine;

public class CreditsNpc : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject creditsPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInteraction interaction))
        {
            interaction.SetInteractible(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInteraction interaction))
        {
            interaction.SetInteractible(null);
        }
    }

    public void Interact(GameObject player)
    {
        Time.timeScale = 0f;
        creditsPanel.SetActive(true);
    }
}