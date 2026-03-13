using UnityEngine;

public class AmmoRefill : MonoBehaviour
{
    [SerializeField] float interactRange = 3f;
    [SerializeField] int refillAmount = 30;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    FPSController player;

    void Start()
    {
        player = FindObjectOfType<FPSController>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactRange && Input.GetKeyDown(interactKey))
        {
            RefillAmmo();
        }
    }

    void RefillAmmo()
    {
        player.IncreaseAmmo(refillAmount);
    }
}