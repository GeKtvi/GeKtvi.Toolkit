using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GeKtvi.Toolkit
{
    [Experimental]
    public class ObjectPool<T> where T : notnull
    {
        public int DesiredFreeObjectLimit { get; }
        public Func<T, bool> IsObjectFreeSelector { get; }
        public Action<T>? ObjectIsNotFreeSetter { get; }
        public Func<T> ObjectFactory { get; }
        public Func<T, IObservable<T>> ObjectIsFreeSubscriber { get; }
        public int ObjectsCount => _pool.Count;
        public int FreeObjectsCount => _pool.Where(IsObjectFreeSelector).Count();

        private readonly List<T> _pool;
        private readonly Subject<T> _requestRemoveFreeObjects = new();

        public ObjectPool(TimeSpan throttleBeforeClear,
                          Func<T> objectFactory,
                          Func<T, IObservable<T>> objectIsFreeSubscriber,
                          int desiredFreeObjectLimit = 25,
                          Func<T, bool>? isObjectFreeSelector = null,
                          Action<T>? objectIsNotFreeSetter = null)
        {
            _pool = new(desiredFreeObjectLimit);

            _requestRemoveFreeObjects.Throttle(throttleBeforeClear)
                .Subscribe(_ => RemoveFreeObjectsAboveLimit());

            DesiredFreeObjectLimit = desiredFreeObjectLimit;
            IsObjectFreeSelector = isObjectFreeSelector ?? (_ => true);
            ObjectIsNotFreeSetter = objectIsNotFreeSetter;
            ObjectFactory = objectFactory;
            ObjectIsFreeSubscriber = objectIsFreeSubscriber;
        }

        public T GetObject()
        {
            var freeObj = _pool.FirstOrDefault(IsObjectFreeSelector);


            if (freeObj is null)
            {
                return CreateNewObject();
            }
            else
            {
                ObjectIsNotFreeSetter?.Invoke(freeObj); 
                return freeObj;
            }
        }

        private void Add(T obj) =>
            _pool.Add(obj);

        private T CreateNewObject()
        {
            T newObj = ObjectFactory.Invoke();
            ObjectIsFreeSubscriber.Invoke(newObj)
                .Subscribe(_requestRemoveFreeObjects.OnNext);

            _pool.Add(newObj);
            return newObj;
        }

        private void RemoveFreeObjectsAboveLimit()
        {
            var freeObjects = _pool.Where(IsObjectFreeSelector).ToArray();
            if (freeObjects.Length > DesiredFreeObjectLimit)
                for (int i = DesiredFreeObjectLimit; i < freeObjects.Length; i++)
                    _pool.Remove(freeObjects[i]);
        }
    }
}
