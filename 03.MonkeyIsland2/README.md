## Challenge 3 - The Gambler’s Club - Monkey Island 2 « Prev Next »

Guybrush Threepwood has found an illegal gambling wheel in an alley.

Gambler's Club - Spinning wheel
Guybrush is not able to win, but there's a guy in his fancy green pants who wins every time he plays. But there's something suspicious about him. Every time he wins, he goes away and comes back a few minutes later.

You decide to follow him and watch him receive the next winning number from a man behind a door. You just have to give him the right password in order to receive the number. Later you will learn that you have discovered "The Gambler's Club".

Gambler's Club - Secret society
You decide to help Guybrush, so you knock on the door and try to obtain the next winning number.

Since you are the new guy in town, the man behind the door doesn't trust you very much, so he decides to change the password algorithm he's been using to give you the next winning number for the wheel.

The new algorithm gets two natural numbers x, y and returns a float rounded up to two decimals.

In order for you to practice and discover what the algorithm does, the man behind the door gives you access to:

http://gamblers.contest.tuenti.net

where you can ask him as many times as you want and he will give you the correct answer, but here, he will only accept x, y <= 30.

You need to solve the algorithm, so that when faced with the final test (he can provide inputs > 30), you tell him the correct solution or he won't give you the next winning number for the wheel!

### Input

The first line is N, N cases will follow.

For each case, in a single line, whitespace-separated, X and Y, the inputs of the problem (X,Y < 1337)

### Output

N lines with the solution (apply the Gambler's Club algorithm to the input).

### Sample input

5
25 25
12 8
1 19
123 37
0 5

### Sample output

35.36
14.42
19.03
128.44
5
