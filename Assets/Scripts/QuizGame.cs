using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizGame : MonoBehaviour
{
    public Image m_background;
    public TextMeshProUGUI m_text;
    public GameObject m_rightPrefab;
    public GameObject m_wrongPrefab;
    public AudioSource m_rightSound;
    public AudioSource m_wrongSound;
    public float m_tiltAng = 45.0f;
    public float m_resetAng = 30.0f;

    /// <summary>
    /// This is a single message to display
    /// </summary>
    public class QuizElement
    {
        // we only have a single string in here, but made it a class in case you ever want to expand
        public string text;
    }

    /// <summary>
    /// This is a whole list of QuizElements
    /// </summary>
    public class QuizList
    {
        // this is nothing but a list, but like QuizElement, the class allows us to expand easily
        public List<QuizElement> elements;
    }

    // The Quiz
    QuizList m_list;
    int m_listElement = -1;

    /// <summary>
    /// Bundle up all the potential input values
    /// These are "abstracted" away from the input mechanism
    /// This "decouples" the input commands from the input mechanism
    /// This makes it easy to create alternative input mechanisms (keyboard, joystick, AI, etc)
    /// </summary>
    class QuizInput
    {
        public bool up = false;
        public bool down = false;
        public bool center = true;
    }

    // the elements
    Color m_origBackgroundColor;
    Color m_origTextColor;
    Color m_rightBackgroundColor;
    string m_rightText;
    Color m_rightTextColor;
    Color m_wrongBackgroundColor;
    string m_wrongText;
    Color m_wrongTextColor;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;  // this keeps the phone app from going into sleep mode

        // remember the original color of the text and background
        m_origBackgroundColor = m_background.color;
        m_origTextColor = m_text.color;

        {   // remember the colors for the "correct" response
            Image image = m_rightPrefab.GetComponentInChildren<Image>();
            if (image)
            {
                m_rightBackgroundColor = image.color;
            }
            TextMeshProUGUI text = m_rightPrefab.GetComponentInChildren<TextMeshProUGUI>();
            if (text)
            {
                m_rightText = text.text;
                m_rightTextColor = text.color;
            }
        }
        {   // remember the colors for the "wrong" response
            Image image = m_wrongPrefab.GetComponentInChildren<Image>();
            if (image)
            {
                m_wrongBackgroundColor = image.color;
            }
            TextMeshProUGUI text = m_wrongPrefab.GetComponentInChildren<TextMeshProUGUI>();
            if (text)
            {
                m_wrongText = text.text;
                m_wrongTextColor = text.color;
            }
        }

        {   // TODO load in the TextAsset for the specified "file"
        }

        // begin the game
        StartCoroutine(RunGame());

#if false   //create example quizlist
            //it can be hard to get the xml format correct, so it's useful to have Unity make one for you the first time.
        {
            string[] s_gameNames =
            {
                "Tony Hawk",
                "Zelda",
                "GTA",
                "Mario",
                "Uncharted",
                "GoldenEye",
                "BioShock",
                "Half-Life",
                "Halo",
                "Pac-Man"
            };
            m_list = new QuizList();
            m_list.elements = new List<QuizElement>();
            foreach (string title in s_gameNames)
            {
                QuizElement element = new QuizElement();
                element.text = title;
                m_list.elements.Add(element);
            }
            XmlSerializer xmls = new XmlSerializer(typeof(QuizList));
            StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/Games.xml");
            xmls.Serialize(sw, m_list);
            sw.Close();
        }
#endif
    }

    void Update()
    {
        // TODO your code here
    }

    float ReadAngle()
    {
        float ang = 0.0f;
        {   // TODO read the accelerometer, and turn that into an angle
        }
        return ang;
    }

    QuizInput ReadInput()
    {
        QuizInput quizInput = new QuizInput();
        {   // TODO use ReadAngle and input.GetKey() to fill in quizInput
        }
        return quizInput;
    }

    IEnumerator RunGame()
    {
        {   // TODO first wait for the phone to be upright
        }

        // Reset to the beginning of the list
        m_listElement = -1;
        bool isDone = NextQuestion();

        while (false == isDone)
        {
            {   // TODO wait for a choice (up or down)
                // then wait for 2 seconds
                // then wait for the phone to be re-centered
                // finally advance to the next question by calling NextQuestion()
            }
            yield return null;
        }

        // We are done... move on to the wrap-up screen
        SceneManager.LoadScene("End");
    }

    void Correct()
    {
        m_text.text = m_rightText;
        m_text.color = m_rightTextColor;
        m_background.color = m_rightBackgroundColor;
        if (null != m_rightSound)
        {
            m_rightSound.Play();
        }
    }

    void Wrong()
    {
        m_text.text = m_wrongText;
        m_text.color = m_wrongTextColor;
        m_background.color = m_wrongBackgroundColor;
        if (null != m_wrongSound)
        {
            m_wrongSound.Play();
        }
    }

    /// <summary>
    /// Advance to the next question.
    /// Return true if we are out of questions and false if there are still more questions
    /// </summary>
    /// <returns>true if we are out of questions</returns>
    bool NextQuestion()
    {
        bool ret = true;

        {
            // TODO advance to the next element in the list
            // set m_text.text to the text of the new question
            // If the list is NOT over, set ret = false
        }

        // reset the colors to the original colors for the question
        m_text.color = m_origTextColor;
        m_background.color = m_origBackgroundColor;
        return ret;
    }
}
