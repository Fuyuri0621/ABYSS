using UnityEngine;
using Cinemachine;


public class CamController : MonoBehaviour
{
    [Header("Players")]
    public Transform player1;
    public Transform player2;

    [Header("Camera Settings")]
    public float zoomMin = 6f;
    public float zoomMax = 12f;
    public float zoomDistanceThreshold = 10f;
    public float followSmooth = 5f;
    [Range(0.01f, 0.4f)]
    public float edgeEnterThreshold = 0.1f;   
    [Range(0.01f, 0.4f)]
    public float edgeExitThreshold = 0.2f;  

    private CinemachineVirtualCamera vcam;
    private Transform midPoint;
    private Camera mainCam;
    private bool isFollowingMidpoint = false;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        mainCam = Camera.main;


        if (midPoint == null)
        {
            GameObject midObj = GameObject.Find("CameraMidPoint");
            if (midObj == null)
                midObj = new GameObject("CameraMidPoint");
            midPoint = midObj.transform;
        }

        vcam.Follow = player1;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null||!GameManager.Instance.datacontainer.coopMode) return;

    
        if (player1.position.x > player2.position.x)
        {
            if (vcam.Follow != player1)
            {
                vcam.Follow = player1;
                isFollowingMidpoint = false;
            }
        }
        else
        {

            Vector3 player2ViewportPos = mainCam.WorldToViewportPoint(player2.position);

            bool nearEdge =
                player2ViewportPos.x < edgeEnterThreshold || player2ViewportPos.x > 1f - edgeEnterThreshold ||
                player2ViewportPos.y < edgeEnterThreshold || player2ViewportPos.y > 1f - edgeEnterThreshold;

            bool backToCenter =
                player2ViewportPos.x > edgeExitThreshold && player2ViewportPos.x < 1f - edgeExitThreshold &&
                player2ViewportPos.y > edgeExitThreshold && player2ViewportPos.y < 1f - edgeExitThreshold;


            Vector3 midpoint = (player1.position + player2.position) / 2f+new Vector3(-3,0,0);
            midPoint.position = Vector3.Lerp(midPoint.position, midpoint, Time.deltaTime * followSmooth);


            if (!isFollowingMidpoint && nearEdge)
            {
                vcam.Follow = midPoint;
                isFollowingMidpoint = true;
            }
            else if (isFollowingMidpoint && backToCenter)
            {
                vcam.Follow = player1;
                isFollowingMidpoint = false;
            }
        }

        float distance = Vector2.Distance(player1.position, player2.position);
        float targetSize = Mathf.Lerp(zoomMin, zoomMax, distance / zoomDistanceThreshold);
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, targetSize, Time.deltaTime * 2f);
    }


}
