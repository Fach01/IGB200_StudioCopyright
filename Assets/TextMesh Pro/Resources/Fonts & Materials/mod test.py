import math


a = [1, 2, 3, 4, 5, 6, 7]
for num in a:
    b = num - 1
    #print(1/2 * b)

d = 2
for i in range(len(a)):
    b = (len(a) - 1) * d/2
    c = -b + i * d
    print(c)