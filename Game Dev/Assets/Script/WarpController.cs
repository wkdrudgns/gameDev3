using TMPro;
using UnityEngine;

public class WarpController : MonoBehaviour
{
    public Transform player;
    public float warpPointDetectionRadius = 3f;
    private Vector3 lastWarpPoint;
    public bool isNearWarpPoint = false;
    private int lastWarpPointLayer;

    public TextMeshProUGUI Text;
    public float sayCool;
    public bool canTp;
    public AudioSource audioSource1;

    private void Start()
    {
        audioSource1 = GetComponent<AudioSource>();
        canTp = false;
        if (Text == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned.");
        }
    }

    void Update()
    {
        if (canTp == false)
        {
            sayCool += Time.deltaTime;
        }

        if (Text != null && sayCool >= 1f)
        {
            ClearText();
        }

        GameObject[] warpPoints = GameObject.FindGameObjectsWithTag("WarpPoint");
        float closestDistance = Mathf.Infinity;
        GameObject closestWarpPoint = null;

        foreach (GameObject warpPoint in warpPoints)
        {
            float distance = Vector3.Distance(player.position, warpPoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestWarpPoint = warpPoint;
            }
        }

        if (closestWarpPoint != null && closestDistance <= warpPointDetectionRadius)
        {
            isNearWarpPoint = true;
        }
        else
        {
            isNearWarpPoint = false;
        }

        if (isNearWarpPoint == true)
        {
            if (isNearWarpPoint && Input.GetKeyDown(KeyCode.F))
            {
                lastWarpPoint = closestWarpPoint.transform.position;
                lastWarpPointLayer = closestWarpPoint.layer;
                Debug.Log("Warp point saved at: " + lastWarpPoint);
            }
        }

        if (player.gameObject.layer == lastWarpPointLayer)
        {
            canTp = true;
        }

        if (player.gameObject.layer != lastWarpPointLayer)
        {
            canTp = false;
        }
        CheckTp();
    }

    private void CheckTp()
    {
        if (Input.GetKeyDown(KeyCode.P) && player.gameObject.layer == lastWarpPointLayer)
        {
            warp();
        }

        if (Input.GetKeyDown(KeyCode.P) && player.gameObject.layer != lastWarpPointLayer)
        {
            Debug.Log("can't warp");
            Say();
        }
    }

    private void Say()
    {
        if (Text != null)
        {
            Text.text = "You can't teleport here";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
        sayCool = 0;
    }

    private void ClearText()
    {
        if (Text != null)
        {
            Text.text = "";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    private void warp()
    {
        player.position = lastWarpPoint;
        if (audioSource1 != null)
        {
            audioSource1.Play();
        }
        Debug.Log("Teleported to warp point at: " + lastWarpPoint);
    }
}
