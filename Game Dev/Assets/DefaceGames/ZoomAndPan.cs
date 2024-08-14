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
        // 마우스 휠 입력 값 얻기
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            // 현재 카메라의 orthographicSize 또는 fieldOfView 값
            float currentZoom;
            if (cameraToZoom.orthographic)
            {
                currentZoom = cameraToZoom.orthographicSize;
            }
            else
            {
                currentZoom = cameraToZoom.fieldOfView;
            }

            // 새로운 줌 값 계산
            float newZoom = Mathf.Clamp(currentZoom - scrollData * zoomSpeed, minZoom, maxZoom);

            // 카메라가 현재 바라보고 있는 포인트로부터 마우스 위치까지의 벡터
            Vector3 mousePosition = cameraToZoom.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - cameraToZoom.transform.position;

            // 줌 비율
            float zoomFactor = newZoom / currentZoom;

            // 새로운 카메라 위치
            Vector3 newCameraPosition = cameraToZoom.transform.position + direction * (1 - zoomFactor);

            // 카메라의 포지션 및 줌 적용
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
