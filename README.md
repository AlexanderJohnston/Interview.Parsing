# Bigram Frequency Analyzer.

This program was built by Alexander Johnston and submitted 2020-28-10.

PURPOSE:
---
This program takes a phrase and tokenizes it into bigrams and then creates a simple histogram to show token frequency.

It supports symbols in the middle of bigrams, such as `Price $100.00`.

Punctuation and grammar will be trimmed automatically by the program when it creates tokens.

HOW TO USE:
---
This program runs in three different modes, "Single", "Multi", and "File".

[Single Mode]:
`Interview.Parsing.exe "Input Phrase Input Phrase"`

This mode will parse a single phrase and display the output in the console or stdout.

[Multi Mode]:
`Interview.Parsing.exe -multi "One Bigram" "Two Bigram Two Bigram"`

Multi-mode will parse as many outputs as you can fit into separate arguments and output to the console.

The -multi switch is required.

[File Mode]:
`Interview.Parsing.exe "input.txt"`

`Interview.Parsing.exe "input.txt" "output.txt"`

The first argument should contain a valid path to your input phrases as a plaintext file. Any number of phrases

can be added to the file. Phrases separated by newlines will be procesed separately.

The second argument specifies an output file, otherwise the program will output to the console.
