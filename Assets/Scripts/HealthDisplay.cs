using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private Player player;

    private void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (player != null)
        {
            healthText.text = player.GetHealth().ToString();
        }
        else
        {
            healthText.text = "0";
        }
    }
}
