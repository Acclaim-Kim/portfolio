import sys
import itertools

# Question2: read_databsae function
def read_database(filename):
    # Opens the file in read mode and assign it to variable f
    with open(filename, 'r') as f:
        # By using readlines, read all the lines of the file and stores them in a list called lines.
        lines = f.readlines()

    # The first line of the file contains the number of transactions and the number of distinct items.
    # In the first line, remove \n space, and split strings, and then save them into variables as integer.
    num_transactions, num_items = map(int, lines[0].strip().split())
    database = []
    for line in lines[1:]:
        # For each line, turns line into a list of integers
        transaction = list(map(int, line.strip().split()))
        # Wrap it into a set to make item lookup fast, and append it into database list.
        database.append(set(transaction))
    return database, num_transactions, num_items

# Question4-1: generate_F1 function
# generaet_F1 function will return a list of 1-itemsets that meet the minimum support, each as a frozenset with its count.
def generate_F1(database, minsupp_count):
    # Generate a dictionary to keep track of how many times each item appears across all transactions.
    item_counts = {}

    # For each transaction, we loop through each item in it.
    for transaction in database:
        for item in transaction:
            # We use dict.get(key, default) to get the current count of the item if it exists, and default to 0 if the item isn't in the dictionary yet.
            item_counts[item] = item_counts.get(item, 0) + 1
    
    # Generate a list for frequent F=1 item
    F1 = []

    # For each item in the count dictinoary:
    for item, count in item_counts.items():
        # If its count is greater than or equal to minsupp_count, it's consdiered a frequent 1-itemset.
        # Then, we wrap ecah item in frozenset([item]), so it can later be treated the same way as multi-itemsets and used as a dictionary key.
        if count >= minsupp_count:
            # frozenset is used to represent itemsets, so they can be immutable, and hashable
            F1.append((frozenset([item]), count))
    return F1

# Question4-2: generate_candidate function
def generate_candidate(Fk):
    # Create empty set for candidates
    candidates = set()

    # This removes the support count, and save it as a list.
    Fk_list = [itemset for itemset, _ in Fk]

    for i in range(len(Fk_list)):
        for j in range(i+1, len(Fk_list)):
            # Compare every pair of (k-1)-itemsets and combine them using set union(|) to try forming a k-itemset.
            union = Fk_list[i] | Fk_list[j]

            # Only add the union if len(union) = k
            if len(union) == len(Fk_list[0]) + 1:
                candidates.add(union)
    return list(candidates)

# Question4-3: prune_candidate function
# This function filters out any candidates that have infrequent (k-1)-subsets.
def prune_candidate(candidates, prev_freq_sets):
    # Extract just frozensets, ignoring counts.
    # Convert to a set for fast membership checks
    prev_freq_set = set([itemset for itemset, _ in prev_freq_sets])

    # Create empty list for pruned_candidates
    pruned_candidates = []

    for candidate in candidates:
        # Generate all (k-1) subsets
        subsets = itertools.combinations(candidate, len(candidate) - 1)

        # Check if all subsets are frequent
        # If all (k-1)subsets of the candidate exist in prev_freq_set, keep it, otherwise, discard it.
        if all(frozenset(subset) in prev_freq_set for subset in subsets):
            pruned_candidates.append(candidate)
    return pruned_candidates

# Question4-4: count_support
# This function counts how many transactions each canddiate itemset appears in.
def count_support(candidates, database):
    # Create an empty dictionary for counts representation.
    counts = {}

    for transaction in database:
        for candidate in candidates:
            # For each transaction, check each candidate to see if it's a subset of the transaction.
            if candidate.issubset(transaction):
                # If so, that means this candidate is present in the transaction
                counts[candidate] = counts.get(candidate, 0) + 1
    return counts

# This function filters out infrequent itemsets by checking their support counts.
def eliminate_candidate(counts, minsupp_count):
    return [(itemset, count) for itemset, count in counts.items() if count >= minsupp_count]

# Question4-5: output_frequent_itemsets function
# Takes all the frequent itemsets from all levels, and write them to a text file.
def output_freq_itemsets(freq_itemsets, output_file):
    # Open file in write mode
    with open(output_file, 'w') as f:

        # Create empty set
        all_items = set()

        # Collect all unique items across all frequent itemsets.
        for level in freq_itemsets:
            for itemset, _ in level:
                all_items.update(itemset)

        # Write the header line (Total number of frequent itemsets, Total number of items)
        f.write(f"{sum(len(level) for level in freq_itemsets)} {max(all_items)+1}\n")

        # Write each frequent itemset
        for level in freq_itemsets:
            for itemset, _ in level:
                # Convert each item to a string, sorts them in increasing order, joins with spaces and writes to the file.
                f.write(" ".join(map(str, sorted(itemset))) + "\n")

# Queastion3: apriori function
def apriori(database, minsupp, output_file):
    # Based on the alpha value, convert percentage to an absolute count.
    minsupp_count = int(minsupp * len(database))

    # Create a list for frquent itemsets.
    freq_itemsets = []

    # generaet_F1 function will scan the database file and find frequent single items.
    F1 = generate_F1(database, minsupp_count)
    # Add this single frequent itemset into the list of frequent itesemts.
    freq_itemsets.append(F1)

    # k tracks the current itemset size (starting at 2).
    k = 2
    # Fk holds the frequent itemsets of size k-1 (starting at F1).
    Fk = F1

    # Loop continues as long as frequent itemsets are found in the previous round.
    while Fk:
        # Combine (k-1)-itemsets to from candidate k-itemsets.
        candidates = generate_candidate(Fk)
        
        # Remove any candidate whose (k-1)-sized subsets are not all frequent.
        # This is key concept of apriori: If any subset is infrequent, the full set cannot be frequent.
        candidates = prune_candidate(candidates, Fk)

        # Count how often each candidate occurs in the database.
        counts = count_support(candidates, database)

        # Keep only those itemsets that meet the minsupp_count threshold..
        Fk = eliminate_candidate(counts, minsupp_count)

        # If there are valid frequent itemsets at this level, store them.
        if Fk:
            freq_itemsets.append(Fk)

        # Move to the next level (k+1 itemests).
        k += 1
    # After the loop ends, all frequent itemsets are written to output_file.
    output_freq_itemsets(freq_itemsets, output_file)

if __name__ == "__main__":
    # sys.argv is a list of all arguments passed on the command line.
    args = sys.argv
    db_file = None
    minsupp = None
    out_file = None

    # Combine flags + values into a dictionary for safer parsing
    for i in range(len(args)):
        if args[i] == "-database" and i + 1 < len(args) and args[i+1].startswith("file="):
            db_file = args[i+1].split("=")[1]
        elif args[i].startswith("-minsupp="):
            minsupp = float(args[i].split("=")[1])
        elif args[i] == "-output" and i + 1 < len(args) and args[i+1].startswith("file="):
            out_file = args[i+1].split("=")[1]

    # If any required argument is missing, it prints usage instructions and exit.
    if not all([db_file, minsupp, out_file]):
        print("Usage: python Apriori.py -database file=database.txt -minsupp=alpha -output file=output.txt")
        sys.exit(1)

    # Catch Errors
    try:
        # Reads the database from the input file.
        database, _, _ = read_database(db_file)
        # Runs the apriori algorithm with the parsed parameters
        apriori(database, minsupp, out_file)
        print(f"Apriori completed. Results written to '{out_file}'.")
    except Exception as e:
        # Print a error message instead of crashing.
        print(f"Error occurred: {e}")

