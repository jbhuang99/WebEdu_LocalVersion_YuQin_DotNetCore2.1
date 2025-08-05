'''
https://zhuanlan.zhihu.com/p/501824360
命名空间

命名空间，英文名字：namespaces。

什么是命名空间？

由于Python一切皆对象，而对象很多时候直接使用并不是很方便，所以要给它取一个名字。

打个比方，我们常常使用的手机，我们买手机的时候经常会给销售说: ''我看一下华为 夏日胡杨色 Mate40Pro 手机"，这就是手机的对象。但是我们平时在提及的时候，总是说：“华为 夏日胡杨色 Mate40Pro 手机"，是不是太麻烦了？于是我们人类就为这个世界上的各种对象命名，比如将"华为 夏日胡杨色 Mate40Pro 手机”称之为"手机"。

在编程中也是如此，为了更好的表示变量与引用对象的关系，我们常常通过变量赋值完成它们之间的映射。

比如:

上面这个代码，5就是一个计算机内存中存在的对象，使用id()内置函数可以查看对象在内存中的地址。a 就是为这个对象赋予的别名，如前面所讲，它是与内存中一个编号为4483750368的对象关联，这个对象就是5。

所以命名空间是从所定义的命名到对象的映射。因此大部分的命名空间都是通过 Python 字典来实现的，字典内保存了变量名称与对象之间的映射关系不同的命名空间，可以同时存在，但彼此相互独立互不干扰。

一般有三种命名空间:

内置名称（built-in names）， Python 语言内置的名称，比如函数名 max、id 和异常名称 BaseException、Exception 等等。
全局名称（global names），模块中定义的名称，记录了模块的变量，包括函数、类、其它导入的模块、模块级的变量和常量。
局部名称（local names），函数中定义的名称，记录了函数的变量，包括函数的参数和局部定义的变量。（类中定义的也是）
它们的包含关系如下:






如果我们要搜索一个变量a，查找顺序为：局部的命名空间 -> 全局命名空间 -> 内置命名空间。从内圈向外圈查找。

如何来查看局部和全局的变量有哪些呢？我们通常使用 Python 内置的函数：globals() 和 locals()

比如：

number = 10

# 声明 func 函数
def func():
 n = 10
 m = 5
 total = 0
 for i in range(n):
  total = i+m
 print(locals())

# 定义类
class Person:
 def __init__(self,name):
  self.name=name
 
 def __str__(self):
  return self.name

print(globals())
# 调用函数
func()
运行结果:






LEGB

那 LEGB 规则与命名空间有什么关联呢?

前面讲到，Python的命名空间是一个字典，字典内保存了变量名称与对象之间的映射关系，因此，查找变量名就是在命名空间字典中查找键-值对。Python有多个命名空间，因此，需要有规则来保证命名空间的查找顺序，而LEGB就是用来规定命名空间查找顺序的规则。

LEGB 是指：






什么是内建作用域呢？

内建作用域是通过一个名为 builtin 的标准模块来实现的，但是这个变量名自身并没有放入内置作用域内，所以必须导入这个文件才能够使用它。在Python3.0及以上版本中，可以使用以下的代码来查看到底预定义了哪些变量：

import builtins
dir(builtins)
我们通过一个案例来看一下查找规则:

number = 10

# 声明 func 函数
def func():
 number = 100
 def inner_func():
  number = 1000
  print(number)
 # 调用 inner_func()函数
  inner_func()

# 调用外部函数
func()





查找顺序:

以inner_func()内部的print(number)为例：

1.如果inner_func()函数内部有number这个变量，那么number这个变量就是局部的；

2.如果inner_func()函数内，没有这个变量，那么就会去找该函数外层的函数func()中有没有number这个变量，如果有，那么这个number就是闭包的；

3.如果外层函数func()中没有变量number，那么就会去最外层的全局变量找；

4.如果全局变量没有number，就去内置中找。

但是需要注意的是:

Python 中只有模块（module），类（class）以及函数（def、lambda）才会引入新的作用域，其它的代码块（如 if/elif/else/、try/except、for/while等）是不会引入新的作用域的，也就是说这些语句内定义的变量，外部也可以访问。

a = 10
b = 8
if a>b:
 c = 5
 print(a+b+c)
else:
 print(a+b)
print(a,b,c)
结果:

23

10 8 5

可以发现没有报错，在 if 中声明了新的变量 c，但是在 if..else 的外层也可以使用此变量c。

在函数的内部不可以直接修改全局变量的值,需要使用关键字 global 声明

num = 1
def fun1():
    global num  # 需要使用 global 关键字声明
    print(num)
    num+=5   # 此处对全局变量进行修改了
    print(num)
fun1()
print(num)
同样在内层函数中也不能直接修改外层函数的变量需要加 nonlocal 声明。

num = 1
def fun():
    print(num)
    a =10
    def inner_func():
     nonlocal a
     a+=5
     print(a)
    # 调用 inner_func 函数
    inner_func()
    print(a+num)
 
#调用 func
func()
结果:

1

15

16

为什么是这个结果呢？上面如果分别去掉global 或者 nonlocal 会有什么报错呢？在第一个案例中如果 num 不是整型变量 1，而换成一个列表还用使用 global 关键字声明吗？

闭包

闭包是跟上面的案例有关的，为什么？因为上面案例中我们在一个函数中定义了另一个函数，外部的称作外层函数，里面的函数称作内层函数。

那怎样就构成闭包呢？需要在上面的基础上再加上一些条件

闭包条件

在一个外函数中定义了一个内函数。
内函数里运用了外函数的临时变量。
并且外函数的返回值是内函数的引用
满足了这三个条件我们就把函数称作闭包函数了。为什么要这样写呢？

一般情况下，如果一个函数结束，函数的内部所有东西都会释放掉，还给内存，局部变量都会消失。但是闭包是一种特殊情况，如果外函数在结束的时候发现有自己的临时变量将来会在内部函数中用到，就把这个临时变量绑定给了内部函数，然后自己再结束。

看一个案例：

# 闭包函数的实例
# outer是外部函数 a和b都是外函数的临时变量
def outer(a):
    b = 10
    # inner是内函数
    def inner():
        #在内函数中 用到了外函数的临时变量
        print(a+b)
    # 外函数的返回值是内函数的引用
    return inner

if __name__ == '__main__':
    # 在这里我们调用外函数传入参数5
    # 此时外函数两个临时变量 a是5 b是10 ，并创建了内函数，然后把内函数的引用返回给了demo变量
    # 外函数结束的时候发现内部函数将会用到自己的临时变量，这两个临时变量就不会释放，会绑定给这个内部函数
    demo = outer(5)
    # 我们调用内部函数，看一看内部函数是不是能使用外部函数的临时变量
    # demo存了外函数的返回值，也就是inner函数的引用，这里执行demo()就相当于执行inner()函数
    demo() # 15
那我们就说 outer 就是一个闭包函数。

在闭包内函数中，可以随意使用外函数绑定来的临时变量，但是如果想修改外函数临时变量数值的时候就需要添加 nonlocal(前提这个临时变量是不可变的，如果是可变类型的就无需添加 nonlocal 了)

还有一点需要注意：使用闭包的过程中，一旦外函数被调用一次返回了内函数的引用，虽然每次调用内函数，是开启一个函数执行过后消亡，但是闭包变量实际上只有一份，每次开启内函数都在使用同一份闭包变量

即：

def outer(a):
    b = 10
    # inner是内函数
    def inner(c):
        #在内函数中 用到了外函数的临时变量
        print(a+b+c)
    # 外函数的返回值是内函数的引用
    return inner

if __name__ == '__main__':
 
    demo = outer(5)
  # 下面是相同的 demo 函数,同一份闭包变量
    demo(5) 
    demo(6)
闭包用途

装饰器！装饰器是做什么的？其中一个应用就是，我们工作中写了一个登录功能，我们想统计这个功能执行花了多长时间，我们可以用装饰器装饰这个登录模块，装饰器帮我们完成登录函数执行之前和之后取时间。

面向对象！经历了上面的分析，我们发现外函数的临时变量送给了内函数。对象有好多类似的属性和方法，所以我们创建类，用类创建出来的对象都具有相同的属性方法。
'''
'''
‌Python支持多重继承‌。多重继承是指一个子类可以同时继承多个父类的属性和方法。在Python中，多重继承是通过在定义类时在类名后的括号中列出多个父类来实现的。‌
1

多重继承的基本用法
在Python中，多重继承的基本语法如下：

python
Copy Code
class SubClass(Base1, Base2):
    pass
这个语法表示SubClass类同时继承了Base1和Base2的特性。

多重继承的优缺点
‌优点‌：

‌代码重用‌：多重继承允许子类重用多个父类的代码，有助于减少重复代码，使代码更加模块化。
‌灵活性‌：多重继承提供了更大的灵活性，允许子类从多个不同的父类中获取所需的功能。
‌缺点‌：

‌复杂性‌：多重继承可能导致复杂的类层次结构，使代码难以理解、维护和调试。
‌菱形继承问题‌（钻石问题）：当两个父类都从同一个类继承时，可能会导致方法解析顺序不明确，从而产生不可预见的结果。虽然Python使用C3线性化算法来解决这个问题，但在某些情况下，它仍然可能导致困惑。
‌紧耦合‌：多重继承可能导致类之间的紧耦合，一个类的更改可能会影响多个子类，降低代码的可维护性和可扩展性。
替代方案
在许多情况下，可以通过其他方法（如组合、接口、混入等）来实现多重继承的功能，而无需使用多重继承。这些替代方法通常更简单、更易于理解，并且更符合单一职责原则。
'''
'''
Python没有专门的接口关键词‌。Python是一种动态类型语言，它没有接口关键字，因此不需要定义接口关键字。不过，可以通过抽象基类来实现接口的效果。抽象基类是一种特殊的类，它不能被实例化，只能被其子类继承。抽象基类可以定义抽象方法，这些方法没有实现，子类必须实现这些方法以确保子类实现了特定的功能‌

在Python3中，定义接口的方法主要包括使用抽象基类（ABCs）和使用第三方库如Protocol。这两种方法各有特点和使用场景，能够帮助Python开发者在编写清晰且具有良好架构的代码时，确保接口的一致性和强制性。

使用抽象基类是一种更传统的方法，它通过import ABC和abstractmethod来强制子类实现特定的方法和属性。这种方式特别适用于你已经清楚知道未来的类需要哪些方法时。此外，它还能够在一定程度上提供类型检查的功能，从而确保子类正确实现了接口。

使用抽象基类（ABC）
定义抽象基类
要在Python中定义一个抽象基类，你首先需要从abc模块中导入ABC类和abstractmethod装饰器。随后，你可以创建一个继承自ABC的类，在这个类中利用@abstractmethod装饰器来标记那些必须由子类实现的方法。

from abc import ABC, abstractmethod
class MyInterface(ABC):


    @abstractmethod


    def my_method(self):


        pass



实现抽象基类
任何尝试实例化抽象基类的行为都会抛出错误。只有当所有的抽象方法都被子类实现时，这个子类才能被实例化。这强制开发者必须按照抽象基类的规定来实现必需的方法。

class MyClass(MyInterface):
    def my_method(self):


        print("实现了必需的方法")
'''
import types #types 模块导入了 BuiltinFunctionType，这是表示 CPython 中内建函数或方法的类型。
class A(object):
    pass
 
class B(A):
    pass
 
class C(A):
    pass
 
class D(B):
    pass

#print(dir(A)) #A对象类型的名称空间作用域
print(A.__dict__)  # 查看类的名称空间作用域
#obj = A("Instance Value")
#print(obj.__dict__)      # 查看实例的名称空间作用域

'''
Python名称空间详解
1. Python中名称空间（namespace）的概念
名称空间是从名称到对象的映射，是Python中用于存放变量名及其对应值的地方。它类似于一个字典，其中键是变量名，值是变量对应的数据对象。名称空间使得Python能够区分不同作用域中的同名变量，从而避免命名冲突。

2. Python中名称空间的类型
Python中存在三种主要的名称空间：

‌内置名称空间（Built-in namespace）‌：这是解释器级别的名称空间，存储了所有内置的函数、异常和关键字等。例如，print、len和def等内置名称都存储在这个空间中。
‌全局名称空间（Global namespace）‌：这是模块级别的名称空间，当Python文件被执行时，会产生一个全局名称空间，用于存储文件中定义的变量、函数、类等。这个空间在模块执行期间一直存在，直到模块执行结束。
‌局部名称空间（Local namespace）‌：这是函数或方法级别的名称空间，当函数被调用时，会产生一个局部名称空间，用于存储函数中定义的变量、函数参数等。这个空间在函数调用期间存在，函数返回或抛出异常时会被删除。
3. Python名称空间的作用域规则
Python中的作用域是指变量名在代码中的有效范围。Python是静态作用域语言，变量的作用域由其定义的位置决定。Python中的作用域规则如下：

‌局部作用域（Local）‌：变量在函数或方法内部定义，只能在函数或方法内部访问。
‌全局作用域（Global）‌：变量在模块级别定义，可以在整个模块中访问。
‌内置作用域（Built-in）‌：内置函数和异常等在整个Python程序中都可以访问。
当Python代码需要访问一个变量时，会按照以下顺序查找名称空间：

局部名称空间（如果代码在函数或方法内部）。
全局名称空间（如果代码在模块级别）。
内置名称空间（如果变量不是局部或全局的）。
如果Python在这些名称空间中找不到变量，将引发NameError异常。

4. Python中名称空间的一个简单示例
下面是一个简单的Python代码示例，展示了全局名称空间和局部名称空间的使用：

python
Copy Code
# 全局变量
global_var = "I am a global variable"

def my_function():
    # 局部变量
    local_var = "I am a local variable"
    
    # 打印局部变量
    print(local_var)
    
    # 尝试打印全局变量（需要在全局作用域中定义）
    print(global_var)

# 调用函数
my_function()

# 尝试在全局作用域中打印局部变量（将引发NameError）
# print(local_var)  # Uncommenting this line will cause a NameError
在这个示例中，global_var是一个全局变量，可以在整个模块中访问。local_var是一个局部变量，只能在my_function函数内部访问。尝试在全局作用域中打印local_var将引发NameError。

5. 如何在Python中管理不同的名称空间，避免命名冲突
在Python中，管理不同的名称空间以避免命名冲突的方法主要有以下几种：

‌使用不同的模块‌：将相关的函数和变量组织在不同的模块中，利用模块的全局名称空间来避免命名冲突。
‌使用局部变量‌：在函数或方法内部使用局部变量，避免与全局变量冲突。
‌使用命名空间包‌：将多个模块组织在命名空间包中，以便更好地组织和管理代码。
‌使用前缀或后缀‌：对于全局变量或函数名，可以使用特定的前缀或后缀来避免命名冲突。例如，可以使用公司名或项目名作为前缀。
‌使用__all__变量‌：在模块中定义__all__变量，指定模块对外公开的接口名称，从而避免不必要的命名冲突。
通过合理使用这些技术，可以有效地管理Python中的名称空间，避免命名冲突。
'''

print( issubclass(A,object))
class Person:
    def __init__(self, name, age):
        self.name = name
        self.age = age
 
    def greet(self):
        print(f"Hello, my name is {self.name} and I am {self.age} years old.")
 
# 创建Person类型的实例
person1 = Person("Alice", 30)
person2 = Person("Bob", 25)
 
# 调用方法
person1.greet()  # 输出: Hello, my name is Alice and I am 30 years old.
person2.greet()  # 输出: Hello, my name is Bob and I am 25 years old.


'''在上述案例中，Person 类定义了一个对象类型，其中包含name和age两个属性，以及一个greet方法。通过__init__方法在创建对象时初始化属性。person1和person2是Person类型的实例。
'''
'''
怎么查看python对象类型的继承关系
在Python中，您可以使用内置的issubclass()函数来检查一个类是否是另一个类的子类，或者使用__bases__属性来查看一个类的所有基类。但是，如果您想要查看一个类及其所有子类的继承关系，可以使用第三方库如pydot和graphviz来生成类继承图，或者手动编写代码遍历类的继承树。

以下是一个简单的函数，用于打印一个类及其所有子类的继承关系：
'''
def print_class_hierarchy(klass, level=0):
    # 缩进输出以可视化层级关系
    indent = '    ' * level
    print(f'{indent}{klass.__name__}')
    # 递归打印所有子类
    for sub_class in klass.__subclasses__():
        print_class_hierarchy(sub_class, level + 1)

 
print_class_hierarchy(A)

print_class_hierarchy(object)


print(type(print))
print(type(isinstance))

def MyFunction():
    return 1    
print(type(MyFunction))

print(isinstance(print,types.BuiltinFunctionType))

print(isinstance(print,object))
print(isinstance(isinstance,object))

print(isinstance(MyFunction,object))

print(isinstance(MyFunction,types.FunctionType))