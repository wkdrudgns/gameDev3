using UnityEngine;

public class FKeyActive : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public string warpPointTag = "WarpPoint";
    public string activateTag = "F";

    private GameObject[] warpPoints;
    private GameObject[] fObjects;

    private void Start()
    {
        warpPoints = GameObject.FindGameObjectsWithTag(warpPointTag);

        fObjects = GameObject.FindGameObjectsWithTag(activateTag);

        foreach (GameObject fObject in fObjects)
        {
            fObject.SetActive(false);
        }
    }

    private void Update()
    {
        bool isNearWarpPoint = false;

        foreach (GameObject warpPoint in warpPoints)
        {
            float distance = Vector3.Distance(player.position, warpPoint.transform.position);
            if (distance <= detectionRadius)
            {
                isNearWarpPoint = true;
                break;
            }
        }

        foreach (GameObject fObject in fObjects)
        {
            fObject.SetActive(isNearWarpPoint);
        }
    }
}
