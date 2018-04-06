namespace ProjectBase.Core.Interface
{
    #region 验证进入Action前提条件
    public interface IsAction<T>
    {
        /// <summary>
        ///验证进入Action前提条件
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool IsAction(T t);
    }
    #endregion
    #region 数据验证
    public interface IsValidationRequired<T>
    {
        /// <summary>
        ///数据验证
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool IsValidationRequired(T t);
    }
    #endregion
    #region 逻辑流程验证
    public interface IValidateBusiness<T>
    {
        /// <summary>
        ///逻辑流程验证
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool ValidateBusiness(T t);
    }
    #endregion
    #region 执行前操作
    public interface IOnBeforeTransaction<T>
    {
        /// <summary>
        ///执行前操作
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool OnBeforeTransaction(T t);
    }
    #endregion
    #region 开始执行任务
    public interface IActionStart<T>
    {
        /// <summary>
        ///开始执行任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool ActionStart(T t);
    }
    #endregion
    #region 执行完成后操作
    public interface IOnAfterTransaction<T>
    {
        /// <summary>
        ///执行完成后
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool OnAfterTransaction(T t);
    }
    #endregion
    #region 执行发生错误
    public interface IActionFail<T>
    {
        /// <summary>
        ///执行发生错误
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T ActionFail(T t);
    }
    #endregion
    #region 执行任务
    public interface IAction<T>
    {
        /// <summary>
        ///执行任务
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T Action(T t, out MessageList msg);
    }
    #endregion
    #region 执行成功
    public interface IActionScuccess<T>
    {
        /// <summary>
        ///执行成功
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        T ActionScuccess(T t);
    }
    #endregion
}
