from matplotlib import pyplot as plt


def get_avg(series: list) -> float:
    return sum(series) / len(series)


def postivies_count(series: list) -> int:
    return sum(1 for num in series if num > 0)


def ascending_sort(series: list) -> list:
    return sorted(series)


def create_graph(series: list) -> None:
    x = [index for index in range(0, len(series))]
    plt.plot(x, series, marker='o', color='blue', linestyle='')
    for i in range(len(series)):
        plt.text(x[i], series[i], f'{series[i]}')
    plt.show()


def get_pcc(series: list) -> float:
    pass


if __name__ == '__main__':
    num_series = []
    num = int(input("Enter an integer for your series, finish with -1: "))
    while num != -1:
        num_series.append(num)
        num = int(input("Enter an integer for your series, finish with -1: "))
    print(f'Average: {get_avg(num_series)}\nPositive numbers count: {postivies_count(num_series)}\n'
          f'Sorted series: {ascending_sort(num_series)}\nScatter opened in a new window')
    create_graph(num_series)
