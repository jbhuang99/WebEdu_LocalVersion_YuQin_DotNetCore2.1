if (args.Length > 0)
{
    string input = args[0];
    char[] charArray = input.ToCharArray();
    Array.Reverse(charArray);
    string result = new string(charArray).ToUpper();
    Console.WriteLine(result); // 将结果输出到标准输出(stdout)，供 AI 读取
}
else
{
    Console.WriteLine("Error: 请在调用时提供字符串参数。");
}