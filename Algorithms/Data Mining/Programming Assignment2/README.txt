# README for Programming Assignment 2 (CSE5243)

## Command to Run
To execute the program on stdlinux, use the following command:

./Apriori -database file=database.txt -minsupp=0.2 -output file=output.txt

Make sure the script has executable permissions:
chmod +x Apriori

## Data Structures Used

- **Database**: A list of sets. Each transaction is a set of items.
- **Itemsets**: Represented using Python's `frozenset`, which allows them to be used as dictionary keys.
- **Frequent Itemsets**: A list of tuples. Each tuple contains a `frozenset` and its support count.
- **Candidates**: A list of `frozenset`s.
- **Counts**: A dictionary with `frozenset` as keys and support counts as values.

These structures help efficiently check subsets and store itemsets in the Apriori algorithm.

## Author
Hwanho Kim
