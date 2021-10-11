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

    [Header("Game Object")]
    [SerializeField] private GameObject[] animal3DObject;

    [Header("UI")]
    public GameObject desPanel;
    public Text animalName;
    public Text animalDes;

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
        
        for (int j = 0; j < animal3DObject.Length; j++)
        {

            for (int i = 0; i < animalDatas.Length; i++)
            {

                if (detectedTrackerName.Contains(animalDatas[i].answerAnimalName) && detectedTrackerName.Contains(animal3DObject[j].name))
                {

                    animal3DObject[i].gameObject.SetActive(true);
                    Debug.Log("<color=lime>3D Activated : " + animal3DObject[i] + "</color>");

                    //Activated UI and Show Description of the animal(s)
                    desPanel.SetActive(true);
                    animalName.text = animalDatas[i].answerAnimalName.ToString();
                    animalDes.text = animalDatas[i].description.ToString();

                }
                else if (detectedTrackerName == "")
                {

                    animal3DObject[i].gameObject.SetActive(false);
                    Debug.Log("<color=lime> All 3D Deactivated </color>");

                    //Deactivated UI
                    desPanel.SetActive(false);
                    animalName.text = "";
                    animalDes.text = "";

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

}
