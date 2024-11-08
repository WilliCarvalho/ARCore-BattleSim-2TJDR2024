using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceBattlers : MonoBehaviour
{
    [SerializeField] private GameObject battler1Prefab;
    [SerializeField] private GameObject battler2Prefab;

    private GameObject spawnedBattler1;
    private GameObject spawnedBattler2;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private int touchCounter = 0;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount <= 0 && touchCounter < 2) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (touchCounter <= 0)
                {
                    spawnedBattler1 = Instantiate(battler1Prefab, hitPose.position, hitPose.rotation);
                    touchCounter++;
                }
                else if (touchCounter <= 1)
                {
                    spawnedBattler2 = Instantiate(battler2Prefab, hitPose.position, hitPose.rotation);
                    touchCounter++;
                }

                if(touchCounter >= 2)
                {
                    GameManager.Instance.StartBattle();
                }
            }
        }
    }
}
