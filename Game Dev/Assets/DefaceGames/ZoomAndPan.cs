using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cameraToZoom;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    MapOpen mapOpen;

    private Vector3 dragOrigin;

    private void Start()
    {
        mapOpen = GetComponent<MapOpen>();
    }

    void Update()
    {
        if (mapOpen.OnMap == true)
        {
            HandleZoom();
        }
        else
        {
            return;
        }
    }

    void HandleZoom()
    {
        // ���콺 �� �Է� �� ���
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            // ���� ī�޶��� orthographicSize �Ǵ� fieldOfView ��
            float currentZoom;
            if (cameraToZoom.orthographic)
            {
                currentZoom = cameraToZoom.orthographicSize;
            }
            else
            {
                currentZoom = cameraToZoom.fieldOfView;
            }

            // ���ο� �� �� ���
            float newZoom = Mathf.Clamp(currentZoom - scrollData * zoomSpeed, minZoom, maxZoom);

            // ī�޶� ���� �ٶ󺸰� �ִ� ����Ʈ�κ��� ���콺 ��ġ������ ����
            Vector3 mousePosition = cameraToZoom.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - cameraToZoom.transform.position;

            // �� ����
            float zoomFactor = newZoom / currentZoom;

            // ���ο� ī�޶� ��ġ
            Vector3 newCameraPosition = cameraToZoom.transform.position + direction * (1 - zoomFactor);

            // ī�޶��� ������ �� �� ����
            cameraToZoom.transform.position = newCameraPosition;
            if (cameraToZoom.orthographic)
            {
                cameraToZoom.orthographicSize = newZoom;
            }
            else
            {
                cameraToZoom.fieldOfView = newZoom;
            }
        }
    }
}
