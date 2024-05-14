using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.PresentationModel
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private string _prefix = "EXP: ";
        [SerializeField] private Image _progress;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _filledSprite;
        [SerializeField] private Text _text;
        private int _maxValue;

        public void UpdateProgress(int progress, int maxValue)
        {
            _maxValue = maxValue;
            UpdateProgress(progress);
        }

        public void UpdateProgress(int progress)
        {
            _progress.fillAmount = progress / (float)_maxValue;

            UpdateSprite(progress);
            SetText(progress, _maxValue);
        }

        private void SetText(int progress, int maxValue)
        {
            _text.text = $"{_prefix}{progress}/{maxValue}";
        }

        private void UpdateSprite(int progress)
        {
            _progress.sprite = progress >= _maxValue ? _filledSprite : _normalSprite;
        }
    }
}