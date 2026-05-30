using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public player PS;
    public TextMeshProUGUI AmmoText;
    public TextMeshProUGUI ObjectiveText;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponentInParent<player>();
        AmmoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        ObjectiveText = GameObject.Find("ObjectiveText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Paints == null)
        {

        } else if (player.Paints.Selected.IsReloading)
        {
            AmmoText.text = "Reloading";
        } else
        {
            AmmoText.text = $"{player.Paints.Selected.Ammo}/{player.Paints.Selected.MagizineSize}";
        }
        ObjectiveText.text = 
            $"{PS.fishHeld}/{PS.difficulty + 5} fish";
    }
}
