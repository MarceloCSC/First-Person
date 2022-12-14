using System.Collections.Generic;

namespace An01malia.FirstPerson.DialogueModule
{
    public class DialogueTagsHandler
    {
        #region Fields

        private const string SPEAKER_TAG = "speaker";

        #endregion

        #region Properties

        public string Speaker { get; } = string.Empty;

        #endregion

        #region Constructor

        public DialogueTagsHandler(ICollection<string> tags)
        {
            if (tags.Count == 0) return;

            foreach (string tag in tags)
            {
                string[] keyValue = tag.Split(':');

                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                switch (key)
                {
                    case SPEAKER_TAG:
                        Speaker = value;
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion
    }
}