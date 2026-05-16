using System.Collections.Generic;
using UnityEngine;

public class pickupscript : MonoBehaviour
{
    public int wealth = 0;

    public List<fihscript> fishBag = new List<fihscript>();

    public int bagSize = 7;

    public bool AddFish(fihscript fish)
    {
        if (fishBag.Count >= bagSize)
        {
            Debug.Log("Bag is full!");
            return false;
        }

        fishBag.Add(fish);

        Debug.Log("Picked up fish worth $" + fish.cost);

        return true;
    }

    public void SellAllFish()
    {
        int total = 0;

        foreach (fihscript fish in fishBag)
        {
            total += fish.cost;
        }

        wealth += total;

        Debug.Log("Sold all fish for $" + total);

        fishBag.Clear();
    }
}