using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Yan.Utility
{
    /// <summary>
    /// 动态类
    /// </summary>
    public class ExtendsObject : DynamicObject
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        private readonly Dictionary<string, object> _properties;

        /// <summary>
        /// 方法集合
        /// </summary>
        private readonly Dictionary<string, DelegateObj> _methodes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="methodes"></param>
        public ExtendsObject(Dictionary<string, object> properties, Dictionary<string, DelegateObj> methodes = null)
        {
            _properties = properties;
            if (methodes == null)
            {
                _methodes = new Dictionary<string, DelegateObj>();
            }
            else
            {
                _methodes = methodes;
            }
        }

        /// <summary>
        /// 获取所有属性名称
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private object GetPropertyValye(string propertyName)
        {
            if (_properties.ContainsKey(propertyName))
            {
                return _properties[propertyName];
            }
            return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        private void SetPropertyValue(string propertyName, object value)
        {
            if (_properties.ContainsKey(propertyName))
            {
                _properties[propertyName] = value;
            }
            else
            {
                _properties.Add(propertyName, value);
            }
        }

        /// <summary>
        /// 设置方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="delegateObj"></param>
        private void SetMethod(string methodName, DelegateObj delegateObj)
        {
            if (_methodes.ContainsKey(methodName))
            {
                _methodes[methodName] = delegateObj;
            }
            else
            {
                _methodes.Add(methodName, delegateObj);
            }
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private DelegateObj GetMethod(string methodName)
        {
            if (_methodes.ContainsKey(methodName))
            {
                return _methodes[methodName];
            }
            return null;
        }

        /// <summary>
        /// 实现动态对象属性成员访问的方法，得到返回指定属性的值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetPropertyValye(binder.Name);
            return result == null ? false : true;
        }

        /// <summary>
        /// 实现动态对象属性值设置的方法
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (value.GetType() == typeof(DelegateObj))
            {
                SetMethod(binder.Name, (DelegateObj)value);
            }
            else
            {
                SetPropertyValue(binder.Name, value);
            }

            return true;
        }

        /// <summary>
        /// 动态对象动态方法调用时执行的实际代码
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var theDelegateObj = GetMethod(binder.Name);
            if (theDelegateObj == null || theDelegateObj.CallMeth == null)
            {
                result = null;
                return false;
            }
            result = theDelegateObj.CallMeth(this, args);
            return true;
        }
    }

    /// <summary>
    /// 定义一个委托,参数个数可变,参数都是object类型:这里的委托多有个dynamic参数,代表调用这个委托的动态对象本身.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="pms"></param>
    /// <returns></returns>
    public delegate object MyDelegate(dynamic sender, params object[] pms);

    /// <summary>
    /// 定义一个委托转载对象,因为dynamic对象不能直接用匿名方法,这里用对象去承载
    /// </summary>
    public class DelegateObj
    {
        private readonly MyDelegate _delegate;

        public MyDelegate CallMeth
        {
            get { return _delegate; }
        }

        private DelegateObj(MyDelegate d)
        {
            _delegate = d;
        }


        public static DelegateObj Function(MyDelegate d)
        {
            return new DelegateObj(d);
        }
    }


    #region 使用方式

    //Dictionary<string, object> dic = new Dictionary<string, object>()
    //{
    //    {"Name","Yan"},
    //    {"Age",32}
    //};

    //dynamic myObj = new ZhhtExtendsObject(dic);

    //myObj.Sex = 1;
    //myObj.TestMethod = DelegateObj.Function((s, pms) =>
    //{
    //    //做你想做的

    //    return "方法执行完毕";
    //});

    //Console.WriteLine("对象属性：Age=" + myObj.Age);
    //string strObj = JsonConvert.SerializeObject(myObj);
    //Console.WriteLine("对象序列化："+strObj);
    //Console.WriteLine("对象方法执行结果："+myObj.TestMethod(3));

    #endregion
}
