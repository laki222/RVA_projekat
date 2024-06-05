using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Class
{
    class ActionHistory
    {
        public int HistoryPosition { get; private set; }
        private List<RevertableCommand> actions;



        public ActionHistory()
        {
            HistoryPosition = 0;
            actions = new List<RevertableCommand>();
        }

        public void AddAndExecute(RevertableCommand action)
        {
            if (HistoryPosition < actions.Count)
                actions.RemoveRange(HistoryPosition, actions.Count - HistoryPosition);

            action.Execute(null);
            actions.Add(action);
            HistoryPosition++;
        }

        public void Undo()
        {
            if (!CanUndo())
                return;

            actions[HistoryPosition - 1].Revert();
            HistoryPosition--;
        }

        public bool CanUndo()
        {
            if (HistoryPosition > 0)
                return true;

            return false;
        }

        public void Redo()
        {
            if (!CanRedo())
                return;

            actions[HistoryPosition].Execute(null);
            HistoryPosition++;
        }

        public bool CanRedo()
        {
            if (HistoryPosition < actions.Count)
                return true;

            return false;
        }

        public RevertableCommand this[int i]
        {
            get { return actions[i]; }
        }
    }
}
