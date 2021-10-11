using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataCharacter
//A namespace is simply a collection of classes that are referred to 
//using a chosen prefix on the class name.
{

    [CreateAssetMenu(fileName = "Animals", menuName = "Animals/AddNewAnimals")]
    public class AnimalDatas : ScriptableObject
    {

        #region Animal_Info

        [Header("Kind Of Animals")]
        public string animalTrackerName;
        [TextArea(20, 40)]
        public string description;
        [TextArea(20, 30)]
        public string question;
        public string answerAnimalName;

        public string[] multipleChoices;

        public bool hasChoosen = false;

        public string status = "";

        public string indexAnswer = "";

        public int trueIndex;

        #endregion

    }

}
