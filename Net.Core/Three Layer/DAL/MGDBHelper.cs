using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    //public class MongodbHelper<T> : IMongodb
    //{
    //    /// <summary>
    //    /// 数据库连接
    //    /// </summary>
    //    private const string MongodbConnection = "mongodb://127.0.0.1:27017";
    //    /// <summary>
    //    /// 指定的数据库
    //    /// </summary>
    //    private const string MongodbDataBaseName = "test";
    //    private MongoCollection col;
    //    private MongoDatabase db;
    //    public MongodbHelper()
    //    {
    //        //创建数据连接
    //        MongoServer server = MongoServer.Create(MongodbConnection);
    //        //获取指定数据库
    //        db = server.GetDatabase(MongodbDataBaseName);
    //    }
    //    /// <summary>
    //    /// 新增
    //    /// </summary>
    //    /// <param name="t">新增对象值</param>
    //    /// <param name="tableName">操作对象所属表名</param>
    //    /// <returns></returns>
    //    public bool Insert(object t)
    //    {
    //        bool result = false;
    //        try
    //        {
    //            col = db.GetCollection(typeof(T).Name);
    //            col.Insert(t);
    //            result = true;
    //        }
    //        catch (Exception)
    //        {
    //            result = false;
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 删除对象
    //    /// </summary>
    //    /// <param name="objId">要删除的对象ID</param>
    //    /// <param name="tableName">操作对象所属表名</param>
    //    /// <returns></returns>
    //    public bool Delete(string objId)
    //    {
    //        bool result = false;
    //        try
    //        {
    //            MongoCollection<T> col = db.GetCollection<T>(typeof(T).Name);
    //            IMongoQuery query = Query.EQ("_id", new ObjectId(objId));
    //            col.Remove(query);
    //            result = true;
    //        }
    //        catch (Exception)
    //        {
    //            result = false;
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 修改
    //    /// </summary>
    //    /// <param name="t">修改新对象</param>
    //    /// <param name="objId">要修改的的对象ID</param>
    //    /// <param name="tableName">操作对象所属表名</param>
    //    /// <returns></returns>
    //    public bool Update(object t, string objId)
    //    {
    //        bool result = false;
    //        try
    //        {
    //            BsonDocument bd = BsonExtensionMethods.ToBsonDocument(t);
    //            MongoCollection<T> col = db.GetCollection<T>(typeof(T).Name);
    //            IMongoQuery query = Query.EQ("_id", new ObjectId(objId));
    //            col.Update(query, new UpdateDocument(bd));
    //            result = true;
    //        }
    //        catch (Exception)
    //        {
    //            result = false;
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 单个对象更新指定对象到指定新值
    //    /// </summary>
    //    /// <param name="Obj">要更新的条件字段</param>
    //    /// <param name="ObjValue">更新条件字段指定的值</param>
    //    /// <param name="UObj">更新的字段</param>
    //    /// <param name="UObjValue">更新的新值</param> 
    //    public bool Update(string Obj, string ObjValue, string UObj, string UObjValue)
    //    {
    //        bool result = false;
    //        try
    //        {
    //            if (Obj.ToLower() == "_id")
    //                db.GetCollection(typeof(T).Name).Update(Query.EQ(Obj, new ObjectId(ObjValue)), new UpdateDocument { { "$set", new QueryDocument { { UObj, UObjValue } } } });
    //            else
    //                db.GetCollection(typeof(T).Name).Update(Query.EQ(Obj, ObjValue), new UpdateDocument { { "$set", new QueryDocument { { UObj, UObjValue } } } });
    //            result = true;
    //        }
    //        catch (Exception)
    //        {
    //            result = false;
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// 更新操作
    //    /// </summary>
    //    /// <typeparam name="T">类型</typeparam>
    //    /// <param name="collectionName">表名</param>
    //    /// <param name="query">条件</param>
    //    /// <param name="entry">新实体</param>
    //    public void Update<T>(string filterValue, T query)
    //    {
    //        BsonDocument bd = BsonExtensionMethods.ToBsonDocument(query);
    //        db.GetCollection(typeof(T).Name).Update(Query.EQ("_id", new ObjectId(filterValue)), bd, true);
    //    }
    //    /// <summary>
    //    /// 根据ObjectID 查询
    //    /// </summary>
    //    public T SelectOne<T>(string objId) where T : class
    //    {
    //        MongoCollection<T> col = db.GetCollection<T>(typeof(T).Name);
    //        return col.FindOne(Query.EQ("_id", new ObjectId(objId)));
    //    }
    //    /// <summary>
    //    /// 查询所有
    //    /// </summary> 
    //    public List<T> SelectAll<T>() where T : class
    //    {
    //        MongoCollection<T> col = db.GetCollection<T>(typeof(T).Name);
    //        MongoCursor<T> p = col.FindAllAs<T>();
    //        List<T> list = new List<T>();
    //        list.AddRange(p);
    //        return list;
    //    }
    //}
}
