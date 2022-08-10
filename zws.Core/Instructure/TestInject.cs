using zws.Core.Abstract;

namespace zws.Core.Instructure
{
    public class TestSingletonInject : ITestSingletonInject, ISingleton
    {
        public TestSingletonInject() { }
        public void CallTest()
        {
            Console.WriteLine("测试ISingleton注入接口");
        }
    }
    public class TestScopeInject : ITestScopeInject, IScope
    {
        public TestScopeInject() { }
        public void CallTest()
        {
            Console.WriteLine("测试IScope注入接口");
        }
    }
    public class TestTransientInject : ITestTransientInject, ITransient
    {
        public TestTransientInject() { }
        public void CallTest()
        {
            Console.WriteLine("测试ITransient注入接口");
        }
    }
    public interface ITestSingletonInject
    {
        void CallTest();
    }
    public interface ITestScopeInject
    {
        void CallTest();
    }
    public interface ITestTransientInject
    {
        void CallTest();
    }
}
