using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayLinker : MonoBehaviour
{
    public List<GameObject> exits = new();
    public int i = 0;

    public void Snap(RoomGen generator)
    {
        List<GameObject> possibleDoorwaysCopy = new List<GameObject>(generator.possibleDoorways); // we are modifying the list so we use a copy
        foreach(GameObject exit in exits)
        {
            possibleDoorwaysCopy.Remove(exit);
        }

        if (possibleDoorwaysCopy.Count == 0)
        {
            Debug.LogWarning($"{gameObject.name}: No possible doorways to snap to!");
            foreach (GameObject exit in exits)
            {
                generator.possibleDoorways.Remove(exit);
            }
            Destroy(gameObject);
            return;
        }

        GameObject chosenDoorway = possibleDoorwaysCopy[0];
        Vector3 targetPosition = chosenDoorway.transform.position;
        targetPosition.y = generator.startingRoom.transform.position.y;
        transform.position = targetPosition;

        generator.possibleDoorways.Remove(chosenDoorway);

        StaticBatchingUtility.Combine(gameObject);
    }

/*    // Update is called once per frame
    void Update()
    {
        if (rancode == false)
        {
            if (starter == false)
            {
                if (!target[i].transform.IsChildOf(gameObject.transform) && target[i])
                {
                    GetComponent<Transform>().position = target[i].GetComponent<Transform>().position;
                    Destroy(target[i]);
                    rancode = true;
                    StaticBatchingUtility.Combine(gameObject);

                }
                else
                {
                    i++;
                }
                if (i >= target.Length)
                {
                    Destroy(gameObject);
                }

            }
        }
    }*/
}
