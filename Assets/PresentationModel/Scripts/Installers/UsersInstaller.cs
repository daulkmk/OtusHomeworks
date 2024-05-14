using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM
{
    public class UsersInstaller : MonoInstaller
    {
        [SerializeField] private Character[] _characters;

        [ShowInInspector, Header("Use it in runtime to test popup reaction on data changes")]
        private (UserInfo, CharacterInfo, PlayerLevel)[] _users;

        [System.Serializable]
        private class Character
        {
            public string name;
            public string description;
            public Sprite icon;
            public int exp;
            public Stat[] stats;
        }

        [System.Serializable]
        private class Stat
        {
            public string name;
            public int value;
        }

        public override void InstallBindings()
        {
            Container.Bind<(UserInfo, CharacterInfo, PlayerLevel)[]>()
                .FromInstance(_users = CreateUsers())
                .AsSingle();
        }

        private (UserInfo, CharacterInfo, PlayerLevel)[] CreateUsers()
        {
            return System.Array.ConvertAll(_characters, Convert);
        }

        private (UserInfo, CharacterInfo, PlayerLevel) Convert(Character character)
        {
            var userInfo = new UserInfo();
            userInfo.ChangeName(character.name);
            userInfo.ChangeDescription(character.description);
            userInfo.ChangeIcon(character.icon);

            var characterInfo = new CharacterInfo();
            foreach (var statInfo in character.stats)
            {
                var stat = new CharacterStat(statInfo.name);
                stat.ChangeValue(statInfo.value);
                characterInfo.AddStat(stat);
            }

            var playerLevel = new PlayerLevel();

            var exp = character.exp;
            while (exp > 0)
            {
                var expToAdd = Mathf.Min(exp, playerLevel.RequiredExperience);
                exp -= expToAdd;

                playerLevel.AddExperience(expToAdd);
                playerLevel.LevelUp();
            }

            return (userInfo, characterInfo, playerLevel);
        }
    }
}