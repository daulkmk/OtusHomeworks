using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI
{
    public class CountdownScreen : MonoBehaviour
    {
        [SerializeField] private Text _counterTxt;

        private void OnEnable()
        {
            _counterTxt.text = "";
        }

        public void Show(int time, Action onCountComplete)
        {
            gameObject.SetActive(true);
            StartCoroutine(Countdown(time, onCountComplete));
        }

        public void Hide() => gameObject.SetActive(false);

        private IEnumerator Countdown(int time, Action onCountComplete)
        {
            var yield = new WaitForSecondsRealtime(1);
            for (; time > 0; time--)
            {
                SetCount(time);
                yield return yield;
            }

            Debug.Log("Countdown complete");
            onCountComplete?.Invoke();
        }

        private void SetCount(int count) => _counterTxt.text = count.ToString();
    }
}
