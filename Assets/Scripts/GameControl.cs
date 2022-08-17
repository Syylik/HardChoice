using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Control.Game
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyChangeText, repChangeText, healthChangeText;

        [Header("Lose Panel Values"), Space(5)]
        [SerializeField] private GameObject losePanel;
        [SerializeField] private TMP_Text losePanelMoneyText;
        [SerializeField] private Image losePanelRepBar, losePanelHealthBar;

        [Header("Win Panel Values"), Space(5)]
        [SerializeField] private GameObject winPanel;
        [SerializeField] private TMP_Text winPanelMoneyText;
        [SerializeField] private Image winPanelRepBar, winPanelHealthBar;

        [Space(10)]
        [SerializeField] private TMP_Text questionText;
        [SerializeField] private TMP_Text answer1Text, answer2Text;

        [Space(10)]
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private Image repBar, healthBar;

        [Space(10)]
        [SerializeField] private float money;
        [SerializeField] private float health;
        [SerializeField] private float rep;

        [Space(10)]
        [SerializeField] private float repLoseValue; //при каком значении репутации мы проиграем
        [SerializeField] private float moneyWinValue; //при каком кол-ве деняг мы выиграем

        private bool clickable = true;

        [System.Serializable]
        public struct Answer
        {
            [TextArea] public string answerRus;
            [TextArea] public string answerEng;
            public float moneyGet, healthGet, repGet;
        }

        [System.Serializable]
        public struct Question
        {
            public string questionRus;
            public string questionEng;

            public Answer answer1;
            public Answer answer2;
        }
        [Space(10)]
        public Question[] questions;
        private Question curQuestion;

        private void Start()
        {
            UpdateQuestion();
            SetStats(moneyText, repBar, healthBar);
        }
        public void OnClickAnswer1() => AddStatsByAnswer(curQuestion.answer1);
        public void OnClickAnswer2() => AddStatsByAnswer(curQuestion.answer2);
        private void AddStatsByAnswer(Answer answer)
        {
            if (clickable)
            {
                clickable = false;

                money += answer.moneyGet;
                rep += answer.repGet;
                health += answer.healthGet;

                rep = Mathf.Clamp(rep, -250f, 100f);
                health = Mathf.Clamp(health, 0f, 100f);

                ShowChangeInStats(moneyChangeText, answer.moneyGet, "$");
                ShowChangeInStats(repChangeText, answer.repGet, "rep");
                ShowChangeInStats(healthChangeText, answer.healthGet, "Health");

                SetStats(moneyText, repBar, healthBar);
                CheckStats();
                StartCoroutine(Reload());
            }
        }
        private void ShowChangeInStats(TMP_Text changeText, float value)
        {
            if (value != 0)
            {
                var changeTextAnim = changeText?.GetComponent<Animator>();
                if (value > 0)
                {
                    changeText.text = $"+{value}";
                }
                else if (value < 0) changeText.text = $"-{value}";
                changeTextAnim.SetTrigger("show");
            }
        }

        /// <summary>
        /// Показывает изменение в параметрах
        /// </summary>
        /// <param name="changeText">Текст который будет показывать изменение</param>
        /// <param name="value">На сколько изменилось</param>
        /// <param name="endChar">Символ в конце, Например: +500$</param>
        private void ShowChangeInStats(TMP_Text changeText, float value, string endChar)
        {
            if (value != 0)
            {
                var changeTextAnim = changeText?.GetComponent<Animator>();
                if (value > 0)
                {
                    changeText.color = Color.green;
                    changeText.text = $"+{value}{endChar}";
                }
                else if (value < 0)
                {
                    changeText.color = Color.red;
                    changeText.text = $"-{value}{endChar}";
                }
                changeTextAnim.SetTrigger("show");
            }
        }
        private void SetStats(TMP_Text moneyText, Image repBar, Image healthBar)
        {
            moneyText.text = $"{money}$";
            repBar.fillAmount = rep / 100;
            healthBar.fillAmount = health / 100;
        }
        private void CheckStats()
        {
            if (health <= 0 || rep <= repLoseValue || money <= 0) Lose();
            if (money >= moneyWinValue) Win();
        }
        private void UpdateQuestion()
        {
            curQuestion = questions[Random.Range(0, questions.Length)];
            UpdateQuestionText();
        }
        public void UpdateQuestionText()
        {
            if (LangsList.currLang == 0)
            {
                questionText.text = curQuestion.questionRus;
                answer1Text.text = curQuestion.answer1.answerRus;
                answer2Text.text = curQuestion.answer2.answerRus;
            }
            else if (LangsList.currLang == 1)
            {
                questionText.text = curQuestion.questionEng;
                answer1Text.text = curQuestion.answer1.answerEng;
                answer2Text.text = curQuestion.answer2.answerEng;
            }
        }
        private System.Collections.IEnumerator Reload()
        {
            YandexSDK.instance.ShowInterstitial();

            yield return new WaitForSeconds(0.5f);
            SceneFade.PlayAnim();

            clickable = true;

            yield return new WaitForSeconds(0.5f);
            UpdateQuestion();
        }
        private void Lose()
        {
            losePanel.SetActive(true);
            SetStats(losePanelMoneyText, losePanelRepBar, losePanelHealthBar);
        }
        private void Win()
        {
            SetStats(winPanelMoneyText, winPanelRepBar, winPanelHealthBar);
            winPanel.SetActive(true);
        }
        public void BackToMenu() => SceneFade.ChangeScene(0);
    }
}