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


            for (int i = 0; i < 24; i++)
            {
                var multiObj = _testingPool.GetObject();
                allObjects.Add(multiObj);
            }

            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 0);
            foreach (var item in allObjects)
                item.IsFree = true;

            Thread.Sleep(ClearThrottleTimeInMilliseconds);

            Assert.AreEqual(_testingPool.FreeObjectsCount, 25);
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