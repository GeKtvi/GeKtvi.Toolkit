namespace GeKtvi.Toolkit.WpfKit.Tests;
public class WpfTestMethodAttribute : TestMethodAttribute
{
    public override TestResult[] Execute(ITestMethod testMethod)
    {
        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            return Invoke(testMethod);

        TestResult[]? result = null;
        Thread thread = new Thread(() => result = Invoke(testMethod));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
        return result ?? Array.Empty<TestResult>();
    }

    private TestResult[] Invoke(ITestMethod testMethod) => [testMethod.Invoke(null)];
}
