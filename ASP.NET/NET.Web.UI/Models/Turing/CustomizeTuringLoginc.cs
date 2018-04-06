using DotNet.Utilities;
using NET.Architect.Model;
using NET.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;

public class CustomizeTuringLoginc
{
    /// <summary>
    /// 根据会画获取答案
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public string GetText(string sender)
    {
        string result = string.Empty;
        try
        {
            result = Sennder(sender.ToLower().Trim());
        }
        catch{ }
        return result;
    }
    /// <summary>
    /// 刷新缓存
    /// </summary>
    public  void CaseRefresh()
    {
        KnowledgeBaseBLL bll = new KnowledgeBaseBLL();
        CacheHelper.SetCache(ConstParameter.cacheKey, bll.Find());
    }
    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <returns></returns>
    public  List<KnowledgeBase> GetCaseh()
    {
        List<KnowledgeBase> list = new List<KnowledgeBase>();
        var obj = CacheHelper.GetCache(ConstParameter.cacheKey);
        if (obj == null)
        {
            CaseRefresh();
        }
        obj = obj == null ? CacheHelper.GetCache(ConstParameter.cacheKey) : obj;
        if (obj != null)
        {
            list = obj as List<KnowledgeBase>;
        }
        return list;
    }
    KnowledgeBaseBLL bll = new KnowledgeBaseBLL();
    private  string Sennder(string sender)
    {
        string result = string.Empty;
        List<KnowledgeBase> list = bll.Find(sender);// GetCaseh();
        if (list != null&& list.Count>0)
        {
            var model = list.First(); //list.Where(n => n.Info == sender).ToList();
            if (model != null)
            {
               // var model = modelList.ToList()[0];
                if (model != null)
                {
                    switch (model.Code)
                    {
                        case "555002":
                            result = string.Format("<dl><dt><strong>{0}</strong></dt><dd><pre>{1}</pre></dd></dl>", sender, model.Text);
                            break;
                        default:
                            result = model.Text;
                            break;
                    }
                }
            }
        }
        return result;
    }
}