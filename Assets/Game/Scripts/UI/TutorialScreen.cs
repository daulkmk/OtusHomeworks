using TMPro;
using UnityEngine;

namespace Lessons.Lesson_Components.UI
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Show(string message)
        {
            _text.text = message;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}