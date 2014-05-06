###Challenge 12 - Taxi Driver

You are a taxi driver and you want to offer customers the fastest service possible. You drive in a city that has special traffic rules:

* You are only allowed to make turns to the right.
* You cannot turn and continue in the same position.
* You cannot go in reverse or make 180º turns.

Calculate the length of the shortest path from the starting point to the destination.

####Example
```
......#.
..#.....
.....#..
.S...#.X
.....#..
........
```
* S = start
* X = destination
* # = walls

####Possible solution
```
......#.
..#┌───┐
...│.#.│
.S─┼┐#.X
...└┘#..
........
```
Length: 14

#####Input format

The input starts with a number (T) indicating the number of cases. Each case is comprised by two numbers (M, N), followed by N lines of the map. M and N are the width and height of the map.

#####Output format

For each case, print:
```
Case #Ti: Ri
```
Where Ti is the number of the test case and Ri the result for that case.

If the destination cannot be reached, the result should be ERROR.

#####Limits
```
T <= 20
M, N <= 100
```
