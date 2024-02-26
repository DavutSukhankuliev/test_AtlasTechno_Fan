using System.Collections.Generic;
using UnityEngine;

namespace FanComposition
{
    [CreateAssetMenu(fileName = "FanConfig", menuName = "Configs/FanConfig", order = 0)]
    public class FanConfig : ScriptableObject
    {
        [SerializeField] private FanModel[] _models;

        private bool _isInit = false;
        private Dictionary<string, FanModel> _dictionary = new Dictionary<string, FanModel>();

        private void OnValidate()
        {
            _isInit = false;
        }
        
        private void Init()
        {
            foreach (var model in _models)
            {
                _dictionary.Add(model.ID, model);
            }

            _isInit = true;
        }

        public FanModel Get(string id)
        {
            if (!_isInit)
            {
                Init();
            }

            if (_dictionary.TryGetValue(id, out var value))
            {
                return value;
            }
            Debug.LogError($"Couldn't find FanModel with name {id}");
            return new FanModel();
        }
    }
}