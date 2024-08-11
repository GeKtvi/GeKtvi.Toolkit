using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GeKtvi.Toolkit.Tests
{
    [TestClass]
    public class ObjectPoolTests
    {
        private readonly ObjectPool<TestObject> _testingPool;

        private const int ClearThrottleTimeInMilliseconds = 300; 

        public ObjectPoolTests()
        {
            _testingPool = new(TimeSpan.FromMilliseconds(ClearThrottleTimeInMilliseconds),
                               () => new(),
                               x => x.SubscribeToFree(),
                               isObjectFreeSelector: x => x.IsFree,
                               objectIsNotFreeSetter: x => x.IsFree = false);
        }

        [TestMethod]
        public void GetObject_NewObject_CorrectCreatedObject()
        {
            HashSet<TestObject> allObjects = new();

            var obj = _testingPool.GetObject();
            allObjects.Add(obj);

            obj.IsFree = true;

            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 1);

            obj.IsFree = false;

            AddItems(allObjects, 24);

            Assert.AreEqual(_testingPool.ObjectsCount, 25);

            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 0);
            foreach (var item in allObjects)
                item.IsFree = true;

            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 25);

            AddItems(allObjects, 50, true);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 50);
            Assert.AreEqual(_testingPool.ObjectsCount, 50);

            Thread.Sleep(ClearThrottleTimeInMilliseconds);
            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.ObjectsCount, 25);
        }

        private void AddItems(HashSet<TestObject> allObjects, int count, bool isFreeObject = false)
        {
            HashSet<TestObject> addedObjects = new();

            for (int i = 0; i < count; i++)
            {
                var multiObj = _testingPool.GetObject();
                addedObjects.Add(multiObj);
                allObjects.Add(multiObj);
            }

            if (isFreeObject)
                foreach (var item in addedObjects)
                    item.IsFree = true;
        }

        private class TestObject
        {
            public bool IsFree
            {
                get => _isFree;
                set
                {
                    _isFree = value;
                    if (value == true)
                        _onFree.OnNext(this);
                }
            }

            private bool _isFree;
            private Subject<TestObject> _onFree = new();

            public IObservable<TestObject> SubscribeToFree() => _onFree.AsObservable();
        }
    }
}