def pythagorean_triplet_by_sum(sum: int) -> None:
    if sum < 0:  # validity check
        return None
    for c in range(5, sum):  # the minimal c of a pythagorean triplet is 5 for (3,4,5)
        for b in range(1, c):
            a = sum - c - b
            if a < 0 or a > b or (a ** 2 + b ** 2 != c ** 2):
                continue
            else:
                print(f'{a}<{b}<{c}')


if __name__ == '__main__':
    sum = int(input('enter sum: '))
    while sum != -1:
        pythagorean_triplet_by_sum(sum)
        sum = int(input('enter sum: '))
