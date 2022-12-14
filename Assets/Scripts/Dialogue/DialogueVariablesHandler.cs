using Ink.Runtime;
using System.Collections.Generic;

namespace An01malia.FirstPerson.DialogueModule
{
    public class DialogueVariablesHandler
    {
        #region Fields

        private readonly Dictionary<string, Object> _variables;

        #endregion

        #region Constructor

        public DialogueVariablesHandler(string text)
        {
            var globalVariables = new Story(text);

            _variables = new Dictionary<string, Object>();

            foreach (string name in globalVariables.variablesState)
            {
                var value = globalVariables.variablesState.GetVariableWithName(name);

                _variables.Add(name, value);
            }
        }

        #endregion

        #region Public Methods

        public void StartListening(Story story)
        {
            IncludeGlobalVariables(story);

            story.variablesState.variableChangedEvent += UpsertVariable;
        }

        public void StopListening(Story story)
        {
            story.variablesState.variableChangedEvent -= UpsertVariable;
        }

        public Object GetVariableState(string name)
        {
            _variables.TryGetValue(name, out Object value);

            return value;
        }

        #endregion

        #region Private Methods

        private void UpsertVariable(string name, Object value)
        {
            if (_variables.TryAdd(name, value)) return;

            _variables[name] = value;
        }

        private void IncludeGlobalVariables(Story story)
        {
            foreach (var variable in _variables)
            {
                story.variablesState.SetGlobal(variable.Key, variable.Value);
            }
        }

        #endregion
    }
}