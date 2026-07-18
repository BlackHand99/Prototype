using TMPro;
using UnityEngine;

public class EncounterTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Update()
    {
        RoomDirector room = RoomDirector.ActiveRoom;

        if (room == null)
        {
            timerText.text = "";
            return;
        }

        float time = room.TimeRemaining;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = $"{minutes}:{seconds:00}";
    }
}