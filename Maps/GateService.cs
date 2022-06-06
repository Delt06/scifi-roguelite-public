using System;
using System.Collections.Generic;
using _Shared;
using JetBrains.Annotations;
using UnityEngine;

namespace Maps
{
    public class GateService : IGateService
    {
        private const string Key = nameof(SavedGateData);
        private readonly SceneData _sceneData;
        [CanBeNull]
        private SavedGateData _savedGateData;

        public GateService(SceneData sceneData) => _sceneData = sceneData;

        public void Open(Gate gate)
        {
            var guid = FindGuid(gate);
            Open(gate, guid);
        }

        public void Refresh(Gate gate, string guid)
        {
            if (IsOpened(guid))
                Open(gate, guid);
        }

        private void Open(Gate gate, string guid)
        {
            SetOpened(guid);
            OpenGateObjects(gate);
        }

        private string FindGuid(Gate gate)
        {
            var gates = _sceneData.MapData.Gates;

            foreach (var gateData in gates)
            {
                if (gateData.Gate == gate)
                    return gateData.Guid;
            }

            throw new ArgumentException($"Gate {gate} is not registered in the map.");
        }

        private static void OpenGateObjects(Gate gate)
        {
            gate.Door.SetActive(false);
            gate.Trigger.SetActive(false);
        }

        private bool IsOpened(string guid)
        {
            EnsureCached(out var savedGateData);
            return savedGateData.OpenGates.Contains(guid);
        }

        private void SetOpened(string guid)
        {
            if (IsOpened(guid)) return;
            EnsureCached(out var savedGateData);
            savedGateData.OpenGates.Add(guid);
            Save();
        }

        private void Save()
        {
            EnsureCached(out var savedGateData);
            var json = JsonUtility.ToJson(savedGateData);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }

        private void EnsureCached(out SavedGateData savedGateData)
        {
            if (_savedGateData == null)
            {
                var json = PlayerPrefs.GetString(Key);
                _savedGateData = JsonUtility.FromJson<SavedGateData>(json)
                                 ?? new SavedGateData();
                _savedGateData.OpenGates ??= new List<string>();
            }

            savedGateData = _savedGateData;
        }
    }
}