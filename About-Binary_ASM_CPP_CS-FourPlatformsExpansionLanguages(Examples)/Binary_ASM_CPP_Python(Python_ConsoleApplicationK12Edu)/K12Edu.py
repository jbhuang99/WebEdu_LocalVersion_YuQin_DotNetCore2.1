# ******C#.Net无需下述部分代码。因为VS会自动去查找，AOT编译。C++.CRT需要此部分代码，因为VS不会自动去查找，JIT编译。
#模块不仅限于.py文件 ，还可以是扩展名为.pyd、.so等的二进制模块 ，或是.zip压缩包内的Python代码。它们都是Python解释器可以识别并载入的代码单元，旨在提高代码的组织性和可重用性。
#from Zero_or_One_Unify_Char_or_NotChar import Zero_or_One_Unify_Char_or_NotChar #装载模块中指定名称的函数进入内存。
import Zero_or_One_Unify_Char_or_NotChar #装载模块中所有名称的函数进入内存。
#from BitOperation import BitOperation
import BitOperation
# ******C#.Net无需上述部分代码。
    
while True:
   try:
        user_input = int(input("请输入一个整数 (输入 'q' 退出): "))
        
        print(f"您输入的整数是: {user_input}")
       
        if user_input==1:
            #print(f"字符 'A' 的二进制表示是: {Zero_or_One_Unify_Char_or_NotChar(bin('A'))[2:]}") # 切片去掉前缀 '0b'
            print("字符 A的二进制表示是:"+bin(ord('A'))[2:]) # 切片去掉前缀 '0b'
        if user_input==2:
            print(f"字符 B的二进制表示是: {Zero_or_One_Unify_Char_or_NotChar(bin(ord('B')))[2:]}") # 切片去掉前缀 '0b'
        if user_input==3:
            BitOperation()
        else:
            print("请输入1或2")
   except ValueError:
        user_input = input("输入无效，请输入一个有效的整数或 'q' 退出: ")
        if user_input.lower() == 'q':
            print("程序已退出。")
            break
       
      #  binary_representation = bin(ord(char))[2:]  # 切片去掉前缀 '0b' print(f"字符 '{char}' 的二进制表示是: {binary_representation}")
