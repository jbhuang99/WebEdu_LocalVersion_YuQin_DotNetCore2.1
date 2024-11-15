// Final liver.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//该项目使用的结构为 .h头文件放计算函数， .cpp文件放调用函数，因此对调用行为做出以下规范:
//例：你要使用例2.15的函数，因为在这个例题里面只有一个函数，所以只需要输入 215
//例：你要使用例2.16的函数，但它不止一个例题里面不止一个程序，如果你要调用第一个，则输入 2161 ,如果你要调用第二个，则输入 2162 。
//1.0版本 例题版

#include <iostream>
#include"huizong.h"


using namespace std;



int main()
{
    cout << "欢迎使用" << endl;
    int xuanti,i=0;
    cout << "请输入您想要使用的函数编号：";
    cout << "\n";

loop://循环的头嘞
    
        if (i != 0)
        {
            cout << "请继续输入您想要使用的函数编号：";
        }
        
        cin >> xuanti;
        int a = 1;

        switch (xuanti)
        {
        case 24:func2_4(); break;
        case 25:func2_5(); break;
        case 26:func2_6(); break;
        case 27:func2_7(); break;
        case 28:func2_8(); break;
        case 29:func2_9(); break;
        case 2101:func2_101(); break;
        case 2102:func2_102(); break;
        case 212:func2_12(); break;
        case 213:func2_13(); break;
        case 214:func2_14(); break;
        case 215:func2_15(); break;
        case 2161:func2_161(); break;
        case 2162:func2_162(); break;
        case 2163:func2_163(); break;
        case 217:func2_17(); break;
        case 218:func2_18(); break;
        case 219:func2_19(); break;
        case 220:func2_20(); break;
        case 221:func2_21(); break;
        case 222:func2_22(); break;
        case 223:func2_23(); break;
        case 224:func2_24(); break;
        case 225:func2_25(); break;
        case 226:func2_26(); break;
        case 227:func2_27(); break;
        case 228:func2_28(); break;
        case 229:func2_29(); break;
        case 230:func2_30(); break;
        case 231:func2_31(); break;
        case 232:func2_32(); break;
        case 31:func3_1(); break;
        case 32:func3_2(); break;
        case 33:func3_3(); break;
        case 341:func3_41(); break;
        case 342:func3_42(); break;
        case 36:func3_6(); break;
        case 37:func3_7(); break;
        case 38:func3_8(); break;
        case 310:func3_10(); break;
        case 3111:func3_111(); break;
        case 3112:func3_112(); break;
        case 312:func3_12(); break;
        case 314:func3_14(); break;
        case 315:func3_15(); break;
        case 3161:func3_161(); break;
        case 3162:func3_162(); break;
        case 317:func3_17(); break;
        case 3181:func3_181(); break;
        case 3182:func3_182(); break;
        case 3191:func3_191(); break;
        case 3192:func3_192(); break;
        case 320:func3_20(); break;
        case 3211:func3_211(); break;
        case 3212:func3_212(); break;
        case 3231:func3_231(); break;
        case 3232:func3_232(); break;
        case 324:func3_24(); break;
        case 325:func3_25(); break;
        case 3261:func3_261(); break;
        case 3262:func3_262(); break;
        case 3271:func3_271(); break;
        case 3272:func3_272(); break;
        case 3281:func3_281(); break;
        case 3282:func3_282(); break;
        case 329:func3_29(); break;
        case 41:func4_1(); break;
        case 42:func4_2(); break;
        case 43:func4_3(); break;
        case 44:func4_4(); break;
        case 451:func4_51(); break;
        case 452:func4_52(); break;
        case 461:func4_61(); break;
        case 462:func4_62(); break;
        case 47:func4_7(); break;
        case 48:func4_8(); break;
        case 49:func4_9(); break;
        case 410:func4_10(); break;
        case 411:func4_11(); break;
        case 412:func4_12(); break;
        case 413:func4_13(); break;
        case 414:func4_14(); break;
        case 4151:func4_151(); break;
        case 4152:func4_152(); break;
        case 416:func4_16(); break;
        case 4171:func4_171(); break;
        case 4172:func4_172(); break;
        case 4181:func4_181(); break;
        case 4182:func4_182(); break;
        case 419:func4_19(); break;
        case 420:func4_20(); break;
        case 421:func4_21(); break;
        case 422:func4_22(); break;
        case 423:func4_23(); break;
        case 424:func4_24(); break;
      //case 425:func4_25(); break;
        case 426:func4_26(); break;
        case 4271:func4_271(); break;
        case 4272:func4_272(); break;
        case 4281:func4_281(); break;
        case 4282:func4_282(); break;
        case 429:func4_29(); break;
        case 4301:func4_301(); break;
        case 4302:func4_302(); break;
        case 432:func4_32(); break;
        case 433:func4_33(); break;
        case 434:func4_34(); break;
        case 435:func4_35(); break;
        case 436:func4_36(); break;
        case 437:func4_37(); break;
        case 438:func4_38(); break;
        case 439:func4_39(); break;
        case 51:func5_1(); break;
        case 52:func5_2(); break;
        case 53:func5_3(); break;
        case 54:func5_4(); break;
        case 551:func5_51(); break;
        case 552:func5_52(); break;
        case 561:func5_61(); break;
        case 562:func5_62(); break;
        case 57:func5_7(); break;
        case 58:func5_8(); break;
        case 59:func5_9(); break;
        case 510:func5_10(); break;
        case 511:func5_11(); break;
        case 512:func5_12(); break;
        case 5131:func5_131(); break;
        case 5132:func5_132(); break;
        case 5133:func5_133(); break;
        case 5141:func5_141(); break;
        case 5142:func5_142(); break;
        case 5143:func5_143(); break;
        case 515:func5_15(); break;
        case 517:func5_17(); break;

            
        }
        cout << "\n";
        cout << "\n";
        cout << "\n";
        cout << "该函数已经运行完毕\n";
        cout << "请问是否想继续使用，若想继续，请输入除了0之外的任意整数，若不想，请输入0：";
        i++;
        cin >> a;

        cout << "\n";
        cout << "\n";
        cout << "\n";
    
    if (a != 0)
        goto loop;//搞循环的嘞
    
    return 0;
}


