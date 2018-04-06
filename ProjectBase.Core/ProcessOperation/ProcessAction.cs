using ProjectBase.Core.Interface;

namespace ProjectBase.Core
{
    public abstract class ProcessAction<T> : IsAction<T>, IAction<T>, IsValidationRequired<T>, IValidateBusiness<T>, IOnBeforeTransaction<T>, IActionStart<T>, IOnAfterTransaction<T>, IActionFail<T>, IActionScuccess<T>
    {
        public T Action(T t, out MessageList msg)
        {
            Messages.Clear();
            if (IsAction(t) && IsValidationRequired(t) && ValidateBusiness(t) && OnBeforeTransaction(t) && ActionStart(t) && OnAfterTransaction(t))
                ActionScuccess(t);
            else
                ActionFail(t);
            msg = this.Messages;
            return t;
        }
        public abstract bool IsAction(T t);
        public abstract bool IsValidationRequired(T t);
        public abstract bool ValidateBusiness(T t);
        public abstract bool OnBeforeTransaction(T t);
        public abstract bool ActionStart(T t);
        public abstract bool OnAfterTransaction(T t);
        public abstract T ActionFail(T t);
        public abstract T ActionScuccess(T t);
        MessageList _Messages = new MessageList();
        public virtual MessageList Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
            }
        }
    }
}
