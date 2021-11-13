using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataCharacter;
using Vuforia;

public class Game_Manager : DefaultTrackableEventHandler
{

    [Header("Variables")]
    [SerializeField] private AnimalDatas[] animalDatas;
    private string detectedTrackerName;
    private float initialDistance;
    private Vector3 initialScale;

    [Header("Game Object")]
    [SerializeField] private GameObject[] animal3DObject;

    [Header("UI")]
    public GameObject desPanel;
    public Text animalName;
    //public Text animalDes;

    void Awake()
    {

        animalDatas = Resources.LoadAll("ScriptableData", typeof(AnimalDatas)).Cast<AnimalDatas>().ToArray();

    }

    void Update()
    {

        detectedTrackerName = PlayerPrefs.GetString("TrackerName");
        Debug.Log("<color=blue>Tracker Name : " + detectedTrackerName + "</color>");

        Detection(detectedTrackerName);
    
    }


    #region MainCode

    private void Detection(string detectedTrackerName)
    {
        
        for (int i = 0; i < animal3DObject.Length; i++)
        {

            for (int j = 0; j < animalDatas.Length; j++)
            {

                if (detectedTrackerName.Contains(animalDatas[j].answerAnimalName) && detectedTrackerName.Contains(animal3DObject[i].name))
                {

                    animal3DObject[i].gameObject.SetActive(true);
                    Debug.Log("<color=lime>3D Activated : " + animal3DObject[i] + "</color>");
                    PinchToZoom(i);

                    //Activated UI and Show Description of the animal(s)
                    desPanel.SetActive(true);
                    animalName.text = animalDatas[j].answerAnimalName.ToString();
                    //animalDes.text = animalDatas[i].description.ToString();

                }
                else if (detectedTrackerName == "")
                {

                    animal3DObject[i].gameObject.SetActive(false);
                    Debug.Log("<color=lime> All 3D Deactivated </color>");

                    //Deactivated UI
                    desPanel.SetActive(false);
                    animalName.text = "";
                    //animalDes.text = "";

                }

            }

        }
          

    }

    #endregion

    #region ButtonAction

    public void SceneChange(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }


    #endregion

    #region TouchControl

    public void PinchToZoom(int i)
    {

        /**
         * scale using pinch involves two touches
         * we need to count both of the touches, store it somewhere, and measure the distance between pinch
         * and scale game object depending on the pinch
         * we need to ignore it if the pinch distance small (incase of touching accidentally)
         **/

        if (Input.touchCount == 2)
        {

            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            //if anyone of touchzero or touchone is cancelled or maybe, ended then do nothing
            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {

                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = animal3DObject[i].transform.localScale;
                //Debug.Log("Initial Distance = " + initialDistance + " GameObject Name = " + animal3DObject[i].name);

            }
            else // if touch move
            {

                var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                //if accidentally touched or pinch movement with very small movement
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; //do nothing
                }

                var factor = currentDistance / initialDistance;
                animal3DObject[i].transform.localScale = initialScale * factor;

            }

        }

    }

    #endregion

}
