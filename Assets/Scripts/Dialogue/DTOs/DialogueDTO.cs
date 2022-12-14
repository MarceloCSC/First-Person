namespace An01malia.FirstPerson.DialogueModule.DTOs
{
    public class DialogueDTO
    {
        #region Properties

        public string Dialogue { get; }
        public string Speaker { get; }
        public bool CanContinue { get; }

        #endregion

        #region Constructor

        public DialogueDTO(string dialogue, string speaker, bool canContinue = false)
        {
            Dialogue = dialogue;
            Speaker = speaker;
            CanContinue = canContinue;
        }

        #endregion
    }
}