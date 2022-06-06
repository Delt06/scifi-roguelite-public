using System;
using JetBrains.Annotations;
using Leveling.UI;
using UnityEngine;

namespace Leveling.Raw
{
    public class PrefsRawPlayerProgress : IRawPlayerExperience, IRawPlayerStats
    {
        private const string Key = nameof(PrefsRawPlayerProgress);
        [CanBeNull]
        private PlayerProgressData _cache;

        public int Energy { get; set; }

        public int LevelIndex
        {
            get
            {
                EnsureCached(out var data);
                return data.LevelIndex;
            }
            set
            {
                EnsureCached(out var data);
                data.LevelIndex = value;
                Save();
            }
        }

        public int this[Stat stat]
        {
            get
            {
                EnsureCached(out var playerProgress);
                return stat switch
                {
                    Stat.Damage => playerProgress.DamageLevelIndex,
                    Stat.Health => playerProgress.HealthLevelIndex,
                    _ => throw new ArgumentOutOfRangeException(nameof(stat), stat, null),
                };
            }
            set
            {
                EnsureCached(out var data);

                switch (stat)
                {
                    case Stat.Damage:
                        data.DamageLevelIndex = value;
                        break;
                    case Stat.Health:
                        data.HealthLevelIndex = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
                }

                Save();
            }
        }

        public int FreeStatPoints
        {
            get
            {
                EnsureCached(out var data);
                return data.FreeStatPoints;
            }
            set
            {
                EnsureCached(out var data);
                data.FreeStatPoints = value;
                Save();
            }
        }

        private void EnsureCached(out PlayerProgressData data)
        {
            if (_cache != null)
            {
                data = _cache;
            }
            else
            {
                var prefsString = PlayerPrefs.GetString(Key);
                var parsedObject = JsonUtility.FromJson<PlayerProgressData>(prefsString);
                _cache = data = parsedObject ?? new PlayerProgressData();
            }
        }

        private void Save()
        {
            EnsureCached(out var playerProgress);
            var json = JsonUtility.ToJson(playerProgress);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}