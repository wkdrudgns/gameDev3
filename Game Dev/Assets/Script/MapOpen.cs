using UnityEngine;

public class MapOpen : MonoBehaviour
{
    public GameObject mapUI;
    public bool OnMap = true;
    public float OnMapTime;
    public float OffMapCool = 30f;
    public float mapCloseDuration = 0.5f;

    private void Start()
    {
        OnMapTime = 0f;
        OffMapCool = 0f;
        OnMap = true;
        if (mapUI != null)
        {
            mapUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (OffMapCool <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CloseMap();
            }
        }

        if (OffMapCool > 0f)
        {
            OffMapCool -= Time.deltaTime;
        }

        if (!OnMap)
        {
            OnMapTime += Time.deltaTime;
            if (OnMapTime >= mapCloseDuration)
            {
                OpenMap();
            }
        }
    }

    private void CloseMap()
    {
        if (mapUI != null && OnMap)
        {
            mapUI.SetActive(false);
            OnMap = false;
            OnMapTime = 0f;
        }
    }

    private void OpenMap()
    {
        if (mapUI != null)
        {
            mapUI.SetActive(true);
            OnMap = true;
            OffMapCool = 30f;
        }
    }
}
