using System;
using System.Collections.Generic;
using System.Text;

namespace Yan.Domain.Abstractions
{
    /// <summary>
    /// 抽象实体类
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract object[] GetKeys();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Keys={string.Join(",", GetKeys())}";
        }

        #region 领域事件处理
        /// <summary>
        /// 存储 存储领域对象事件
        /// 在一个实体的操作中可能会发生多个领域事件 所以用List存储
        /// </summary>
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// 领域事件应该是可以被领域模型之外的代码读到，所以这里定义的是IReadOnlyCollection
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="domainEvent"></param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        //移除领域事件
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        //清空领域事件
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual TKey Id { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        private int? _requestedHashCode;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <summary>
        /// 判断两个实体是否是同一个实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            //Object.ReferenceEquals(left, right)静态方法：从名称中便可知它用来比较两者是否是相同的引用，我们也永远不应该去重写该方法。它对于值类型对象的比较永远返回false；对于两个null的比较永远返回true。
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            Entity<TKey> item = (Entity<TKey>)obj;
            if (item.IsTransient() || this.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(this.Id);
            }

        }

        /// <summary>
        /// 重写 GetHashCode 方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (_requestedHashCode.HasValue)
                {
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                }
                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// 判断对象是否是新创建的，没有持久化的
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            //这里的判断逻辑很简单，没有Id就表示没有持久化的
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Id={Id}";
        }

        /// <summary>
        /// 重载 == 运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Object.Equals(left, null))
            {
                return Object.Equals(right, null) ? true : false;
            }
            else
            {
               return left.Equals(right);
            }
        }

        /// <summary>
        ///  重载 != 运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
