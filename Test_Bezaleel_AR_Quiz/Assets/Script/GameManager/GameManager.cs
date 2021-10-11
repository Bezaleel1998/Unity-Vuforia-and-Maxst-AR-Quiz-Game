using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataCharacter;
using Vuforia;

public class GameManager : DefaultTrackableEventHandler
{

    [Header("UI")]
    public GameObject questionUI;
    public Text questionText;
    public Text[] buttonTextChoices;
    public Button[] buttons;
    public Text scoreText;
    public Text lastScoreText;

    [Header("Variables")]
    [SerializeField] private AnimalDatas[] animalDatas;
    private string detectedTrackerName;
    private int dataIndex;
    [SerializeField]
    private int scores;

    [Header("Game Object")]
    [SerializeField] private GameObject[] animal3DObject;
    public GameObject gameOverMenu;

    [Header("TimerVariable")]
    [Tooltip("15 minutes = 900 second")]
    public float timeValue = 900;
    /// <summary>
    /// 15 minutes = 900 second
    /// </summary>
    public bool timerIsRunning = false;
    public Text timeText;


    void Awake()
    {

        animalDatas = Resources.LoadAll("ScriptableData", typeof(AnimalDatas)).Cast<AnimalDatas>().ToArray();

    }

    void Start()
    {

        for (int i = 0; i < animalDatas.Length; i++)
        {
            animalDatas[i].hasChoosen = false;

            animalDatas[i].status = "";

            animalDatas[i].indexAnswer = "";

            for (int j = 0; j < animalDatas[i].multipleChoices.Length; j++)
            {

                if (animalDatas[i].multipleChoices[j] == animalDatas[i].answerAnimalName)
                {
                    animalDatas[i].trueIndex = j;
                }

            }
        }

        timerIsRunning = true;

    }


    void Update()
    {

        Detection();
        
        scoreText.text = "Score = " + scores.ToString();

        Timer();

    }

    #region GameMechanic

    private void Detection()
    {

        detectedTrackerName = PlayerPrefs.GetString("TrackerName");
        Debug.Log("<color=blue>Tracker Name : " + detectedTrackerName + "</color>");

        for (int i = 0; i < animal3DObject.Length; i++)
        {

            for (int j = 0; j < animalDatas.Length; j++)
            {

                if (detectedTrackerName.Contains(animal3DObject[i].name.ToString()) && detectedTrackerName.Contains(animalDatas[j].answerAnimalName.ToString()))
                {

                    animal3DObject[i].gameObject.SetActive(true);
                    Debug.Log("<color=lime>3D Activated : " + animal3DObject[i] + "</color>");

                    questionUI.gameObject.SetActive(true);
                    questionText.text = animalDatas[j].question.ToString();

                    dataIndex = j;

                    InputChoicesButtonText(dataIndex);

                    CheckingAnswersOnTracker(dataIndex);

                    


                }
                else if (detectedTrackerName.Equals(""))
                {
                    animal3DObject[i].gameObject.SetActive(false);

                    dataIndex = 0;

                    questionUI.gameObject.SetActive(false);
                    questionText.text = "";
                }

            }

        }

    }

    void InputChoicesButtonText(int indexI)
    {

        for (int k = 0; k < buttonTextChoices.Length; k++)
        {

            buttonTextChoices[k].text = animalDatas[indexI].multipleChoices[k].ToString();

        }

    }

    void CheckingAnswersOnTracker(int indexI)
    {

        for (int k = 0; k < buttons.Length; k++)
        {

            if (animalDatas[indexI].hasChoosen == true && animalDatas[indexI].status == "True" && animalDatas[indexI].indexAnswer != null)
            {

                buttons[k].interactable = false;

                int indexA = int.Parse(animalDatas[indexI].indexAnswer);

                Color TrueColor = Color.green;
                ColorBlock cb = buttons[indexA].colors;
                cb.disabledColor = TrueColor;
                cb.normalColor = TrueColor;
                cb.highlightedColor = TrueColor;
                cb.pressedColor = TrueColor;
                buttons[indexA].colors = cb;

                for (int l = 0; l < buttons.Length; l++)
                {

                    if (l != indexA)
                    {

                        Color Other = Color.grey;
                        ColorBlock cobl = buttons[l].colors;
                        cobl.disabledColor = Other;
                        cobl.normalColor = Other;
                        cobl.highlightedColor = Other;
                        cobl.pressedColor = Other;
                        buttons[l].colors = cobl;

                    }

                }

            }
            else if(animalDatas[indexI].hasChoosen == true && animalDatas[indexI].status == "False" && animalDatas[indexI].indexAnswer != null)
            {

                buttons[k].interactable = false;

                int indexA = int.Parse(animalDatas[indexI].indexAnswer);

                Color TrueColor = Color.red;
                ColorBlock cb = buttons[indexA].colors;
                cb.disabledColor = TrueColor;
                cb.normalColor = TrueColor;
                cb.highlightedColor = TrueColor;
                cb.pressedColor = TrueColor;
                buttons[indexA].colors = cb;

                for (int l = 0; l < buttons.Length; l++)
                {

                    if (l != indexA && l != animalDatas[indexI].trueIndex)
                    {

                        Color Other = Color.grey;
                        ColorBlock cobl = buttons[l].colors;
                        cobl.disabledColor = Other;
                        cobl.normalColor = Other;
                        cobl.highlightedColor = Other;
                        cobl.pressedColor = Other;
                        buttons[l].colors = cobl;

                    }
                    if(l == animalDatas[indexI].trueIndex)
                    {
                        
                        Color Other = Color.green;
                        ColorBlock cobl = buttons[l].colors;
                        cobl.disabledColor = Other;
                        cobl.normalColor = Other;
                        cobl.highlightedColor = Other;
                        cobl.pressedColor = Other;
                        buttons[l].colors = cobl;

                    }

                }

            }
            else
            {

                buttons[k].interactable = true;

                Color TrueColor = Color.white;
                ColorBlock cb = buttons[k].colors;
                cb.disabledColor = TrueColor;
                cb.normalColor = TrueColor;
                cb.highlightedColor = TrueColor;
                cb.pressedColor = TrueColor;
                buttons[k].colors = cb;

            }

        }

    }

    void OutOfTime()
    {

        for (int z = 0; z < buttons.Length; z++)
        {

            buttons[z].interactable = false;

            Color TrueColor = Color.grey;
            ColorBlock cb = buttons[z].colors;
            cb.disabledColor = TrueColor;
            cb.normalColor = TrueColor;
            cb.highlightedColor = TrueColor;
            cb.pressedColor = TrueColor;
            buttons[z].colors = cb;

        }

        gameOverMenu.gameObject.SetActive(true);
        lastScoreText.text = "Score = " + scores.ToString();


    }

    #endregion


    #region ButtonActionQuizGame

    public void ChoiseOnClick(int index)
    {

        if (animalDatas[dataIndex].answerAnimalName == animalDatas[dataIndex].multipleChoices[index] && animalDatas[dataIndex].hasChoosen == false && animalDatas[dataIndex].status == "")
        {

            animalDatas[dataIndex].hasChoosen = true;
            animalDatas[dataIndex].status = "True";
            animalDatas[dataIndex].indexAnswer = index.ToString();


            scores += 1;
            Debug.Log("<color=yellow> Scores = " + scores +"</color>");

        }
        else if(animalDatas[dataIndex].hasChoosen == false && animalDatas[dataIndex].status == "")
        {

            animalDatas[dataIndex].hasChoosen = true;
            animalDatas[dataIndex].status = "False";
            animalDatas[dataIndex].indexAnswer = index.ToString();

        }
        else
        {

            animalDatas[dataIndex].hasChoosen = false;
            animalDatas[dataIndex].status = "";

        }

    }

    #endregion

    #region UIManager

    public void SceneChange(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    void Timer()
    {

        if (timerIsRunning)
        {

            if (timeValue > 0)
            {

                timeValue -= Time.deltaTime;
                DisplayTime(timeValue);
                gameOverMenu.gameObject.SetActive(false);

            }
            else
            {

                Debug.Log("<color=white>Sorry, Your Time Has Been Running Out !</color>");
                timeValue = 0;
                timerIsRunning = false;

                //Show Last Score and Game Over UI
                OutOfTime();

            }

        }

    }

    void DisplayTime(float timeToDisplay)
    {

        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miliseconds = timeToDisplay % 1 * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);

    }

    #endregion


}
