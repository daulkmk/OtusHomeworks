using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Security.Cryptography;

namespace SaveLoad
{
    public class Test : MonoBehaviour
    {
        [ContextMenu("TEST")]
        public async UniTask Test_()
        {
            var repo = new GameRepository(new LocalFilesContainer(new AesCryptographyService()));

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

        [ContextMenu("Generate Key/IV")]
        public void GenerateKey()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                var key = aes.Key;

                string s = "";
                foreach (var x in key)
                {
                    s += x;
                    s += ",";
                }

                Debug.Log(s);

                aes.GenerateIV();

                var ev = aes.IV;

                s = "";
                foreach (var x in ev)
                {
                    s += x;
                    s += ",";
                }

                Debug.Log(s);
            }
        }
    }
}