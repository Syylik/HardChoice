using UnityEngine;
using UnityEngine.UI;

namespace Control.Menu
{
    public class MenuControl : MonoBehaviour
    {
        [SerializeField] private Toggle translationToggle;
        private void Start() => translationToggle.isOn = LangsList.currLang == 0 ? true : false;
        public void Play() => SceneFade.ChangeScene(1);
        public void Translation(bool rus)
        {
            if (rus) LangsList.SetLanguage(0, true);
            else LangsList.SetLanguage(1, true);
        }
    }
}
