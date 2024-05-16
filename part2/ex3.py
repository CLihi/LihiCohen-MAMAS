def is_sorted_polyndrom(str: str) -> bool:
    length = len(str)
    part1 = str[0:length // 2]
    part2 = str[(length // 2):length] if length % 2 == 0 else str[(length // 2) + 1:length]
    part1_extended = part1 + str[length // 2] if length % 2 != 0 else part1
    if part1 != part2[::-1] or sorted(part1_extended) != [char for char in part1_extended]:
        return False
    return True
