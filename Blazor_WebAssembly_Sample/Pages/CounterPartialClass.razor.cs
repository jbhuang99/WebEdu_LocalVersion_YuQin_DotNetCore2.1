namespace Blazor_WebAssembly_Sample.Pages;

public  partial class CounterPartialClass
{
   static private int currentCount = 0;

  static  private void IncrementCount()
    {
        currentCount++;
    }

}