import string


def BitOperation():
    # ��λ�����
    a = 60            # �����Ʊ�ʾΪ 0011 1100
    b = 13            # �����Ʊ�ʾΪ 0000 1101
    result = a & b    # ���Ϊ 0000 1100 �����Ʊ�ʾΪ 12
    print(result)
    # ��λ�����
    result = a | b    # ���Ϊ 0011 1101 �����Ʊ�ʾΪ 61
    print(result)
    # ��λ������
    result = a ^ b    # ���Ϊ 0011 0001 �����Ʊ�ʾΪ 49
    print(result)
    # ��λȡ������
    result = ~a       # ���Ϊ 1100 0011 �����Ʊ�ʾΪ -60��64λ�����ϵĲ����ʾ��
    print(result)
    # ���Ʋ���
    result = a << 2   # ���Ϊ 0111 1000 �����Ʊ�ʾΪ 240
    print(result)
    # ���Ʋ���
    result = a >> 2   # ���Ϊ 0000 0111 �����Ʊ�ʾΪ 15 
    print(result)

