###Challenge 7 - Yes we scan

You work for the NSA and your mission is to monitor the two most dangerous terrorists in the world: terrorist A and terrorist B. In order to do this, you must develop a system that monitors phone calls throughout the world. When your system detects that a person calls another person, you mark them as contacts. If your system detects at any given time that terrorist A can reach terrorist B through a list of contacts, it must raise an alarm immediately!

####Problem

Given a log of phone calls represented as a pair of integers (where each integer is the ID of a person), and given the ID of terrorist A and terrorist B, write a program that prints “Connected at <phone_call_index>” where <phone_call_index> is the index of the phone call in the log (starting with 0) at which terrorist A and terrorist B become connected through a list of contacts, or prints “Not connected” if terrorist A and terrorist B are not connected after processing all phone calls.

The phone call log consists of a file with 106 lines. Each line is a pair of integers X and Y separated by one space, representing a phone call between X and Y, with 0 <= X < 109 and 0 <= Y < 109. The phone call log is always the same and can be downloaded here:

[phone_call.log.gz]

The terrorist IDs are provided as two integers, one per line, as the problem’s input.

####Example 1:

Phone call log (assuming it contains only four phone calls):
```
2 0
1 2
1 4
3 4
```
#####Input (the IDs of the terrorists):
```
0
4
```
#####Output:
```
Connected at 2
```
The ID of terrorist A is 0 and the ID of terrorist B is 4, so we have to find phe phone call through which person 0 and person 4 become connected by a list of contacts.

The first phone call (index 0) is between person 2 and person 0, so 2 and 0 are marked as contacts. 0 and 4 are not yet connected at this point.
The second phone call (index 1) is between person 1 and person 2, so now person 0 is connected to person 2 directly and to person 1 through person 2, but it is still not connected to person 4.
The third phone call (index 2) is between 1 and 4, so now there is a connection between 0 and 4 because 0 can reach 4 through 2 and 1 (0 -> 2 -> 1 -> 4). Since the phone call at index 2 is the one that connected terrorist A to terrorist B, we print “Connected at 2”.

####Example 2:

Phone call log (assuming it contains only four phone calls):
```
0 3
1 0
2 999999999
1 0
```
#####Input (the IDs of the terrorists):
```
0
999999999
```
#####Output:
```
Not connected
```
Since 0 and 999999999 are never connected after processing the four phone calls. Notice that phone calls may be repeated.

[phone_call.log.gz]:https://contest.tuenti.net/resources/phone_call.log.gz
