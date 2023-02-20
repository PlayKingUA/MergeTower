using UnityEngine;
using TMPro;

namespace BlueStellar.Cor
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textName;
        [SerializeField] private string playerName;
        [SerializeField] private string[] notCorrectNames;

        public string Name()
        {
            LoadSave();
            return playerName;
        }

        private void Start()
        {
            LoadSave();
            SetName();
        }

        public void NewName(string name)
        {
            foreach(var i in notCorrectNames)
            {
                if (i == name)
                    return;
            }

            playerName = name;
            SetName();
        }

        private void SetName()
        {
            textName.text = playerName;
            Save();
        }

        #region Save&Load

        private void LoadSave()
        {
            playerName = ES3.LoadString("playerName", playerName);
        }

        private void Save()
        {
            ES3.Save("playerName", playerName);
        }

        #endregion
    }
}
