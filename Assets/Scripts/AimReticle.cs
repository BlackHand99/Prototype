using UnityEngine;
using UnityEngine.InputSystem;

public class AimReticle : MonoBehaviour
{
    private static AimReticle instance;

    [SerializeField] private RectTransform reticle;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        reticle.position = Mouse.current.position.ReadValue();
    }
}