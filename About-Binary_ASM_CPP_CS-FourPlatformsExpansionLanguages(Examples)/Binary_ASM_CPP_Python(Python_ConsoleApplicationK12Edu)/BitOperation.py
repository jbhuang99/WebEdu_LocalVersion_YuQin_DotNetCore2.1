import string


def BitOperation():
    # 按位与操作
    a = 60            # 二进制表示为 0011 1100
    b = 13            # 二进制表示为 0000 1101
    result = a & b    # 结果为 0000 1100 二进制表示为 12
    print(result)
    # 按位或操作
    result = a | b    # 结果为 0011 1101 二进制表示为 61
    print(result)
    # 按位异或操作
    result = a ^ b    # 结果为 0011 0001 二进制表示为 49
    print(result)
    # 按位取反操作
    result = ~a       # 结果为 1100 0011 二进制表示为 -60（64位机器上的补码表示）
    print(result)
    # 左移操作
    result = a << 2   # 结果为 0111 1000 二进制表示为 240
    print(result)
    # 右移操作
    result = a >> 2   # 结果为 0000 0111 二进制表示为 15 
    print(result)

