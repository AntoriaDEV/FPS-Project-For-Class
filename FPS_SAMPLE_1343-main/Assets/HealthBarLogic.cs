using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarLogic : MonoBehaviour
{

    Image healthBarImage;
    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(int newHealth)
    {
        healthBarImage.fillAmount = (float)newHealth / 100;
    }
}
