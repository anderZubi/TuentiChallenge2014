###Challenge 4 - Shape shifters
Humans have evolved and have mastered the art of changing their DNA, this process allowing them to change their appearance.

The shape shifting process is painful and dangerous.

Shifting from one form to another requires many small incremental DNA mutations (changing one nucleotide at a time).

Also there are clearly defined states in which the nucleotides can be set, any other configuration would lead to the irrevocable disintegration of the shape shifter's DNA.

Given these constraints, you must find a way to shape shift from one state to another through a set of possible intermediary safe DNA states.

#####Input and output definition

These DNA states are represented as strings.

The length of the strings is equal to the number of nucleotides, and the value of each character represents the value of a nucleotide at that index. Nucleotide values are A (adenine), C (cytosine), G (guanine), and T (thymine).

The input you are given is a source state (first line), a target state (second line) and all the permitted safe states for the shape shifter in the following format (example for three-nucleotide DNA):
```
AGC
CAA
AGC
TTT
CGC
CGA
CAA
TGT
```
The output should consist of a minimum number of transitions required to get from the source state to the target state, and in each step only one nucleotide may change value.

Remember that you can only go through permitted safe states and you can only change one nucleotide at a time.

For the example, the correct output would be:
```
AGC->CGC->CGA->CAA
```
