###Challenge 2 - F1 - Bird's-eye Circuit 


Tuenti Móvil F1 Racing Team is an ambitious project that has already been born. You are an engineer in our brand new F1 team and as a welcome (after a big round of applause) we have a task for you.
We have just received next season’s tracks in plain text format (yes, FIAL is really slapdash with these kinds of things). We want to beautify the graphic representations in order to have a visual approximation of what each track will look like in real life from an overhead view. This is your task and we are certain that you will do your best!

Oh, I almost forgot! This morning we found a post-it note on our desk written by an engineer from another team containing some basic information that is helpful for understanding the data received:

```
Each track is represented as a plain text line.
There are only four valid characters:
‘#’: Defines the starting/finishing line
‘-’: Straight line, this describes a stretch of track that is straight. This means that direction doesn’t change.
‘/’: It will depend on the track’s current orientation:
Horizontal: Curve to the left
Vertical: Curve to the right
‘\’: It will depend on the track’s current orientation:
Horizontal: Curve to the right
Vertical: Curve to the left
The start/finish line will always appear on a straight, and that straight is referred to as “start/finish straight”.
All given tracks will be well-formed.
Track path will never cross itself.
Track will always be a fully connected path.
```

We have only a few requirements for the representation (output):

We want an overhead of the circuit in which the start/finish straight must be represented horizontally and from left to right.

All the lines of the resulting graphic representation must have the same length and empty spaces may be added if needed to match the longest line length.

Characters used for the graphic representation:

* Dash ‘-’: Horizontal stretch of track.
* Pipe ‘|’: Vertical stretch of track.
* Slash ‘/’: Curve.
* Back-slash ‘\’: Curve.
* Space ‘ ‘: Empty space.
* Line feed (as EOL) ‘\n’: This is used to change to start next line of the drawing.

In order to clarify what we want and how we want it, below are some examples of last year’s tracks:

#####Input
```
#----\-----/-----\-----/
```

#####Expected output
```
/#----\
|     |
|     |
|     |
|     |
|     |
\-----/
```

#####Input
```
------\-/-/-\-----#-------\--/----------------\--\----\---/---
```

#####Expected output
```
/---------\
|         |
|       /-/
|       |
\----\  \-----#-------\
     |                |
     |                |
     \----------------/
```
