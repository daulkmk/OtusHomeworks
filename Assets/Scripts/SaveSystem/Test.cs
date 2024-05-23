using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SaveLoad
{
    public class Test : MonoBehaviour
    {
        [ContextMenu("TEST")]
        public async UniTask Testtt()
        {
            var repo = new GameRepository(new AesCryptographyService());

            try
            {
                await repo.Save<int>("1", 100);

                var loaded = await repo.Load<int>("1");

                Debug.Log(loaded);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}