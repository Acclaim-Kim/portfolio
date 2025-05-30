# Import required libraries
import sys        # handling command-line arguments
import random     # random selection
import math       # mathematical operations


# Question2: Function to read data from the input file
# Input: file path
# Output: data list where each item is a list of features
def read_data(file_path):
    data = []  # Empty list to store data points
    with open(file_path, 'r') as file:  # Open file
        for line in file:  # Read each line in the file
            # Split the line into values, convert to float, and store as a list
            data.append([float(x) for x in line.strip().split()])
            # Output will be the matrix form
    return data


# Question2: Function to calculate Euclidean distance between two points
def euclidean_distance(point1, point2):
    # Calculate sum of squared differences for each feature and take square root
    return math.sqrt(sum((a - b) ** 2 for a, b in zip(point1, point2)))


# Question3: Function to compute the centroid of a given cluster
# Input: cluster - list of point indices
# Output: centroid coordinates as a list
def compute_centroid(cluster, data):
    if not cluster:  # If cluster is empty, return zero vector
        return [0] * len(data[0])

    num_points = len(cluster)  # Number of points in the cluster
    dimension = len(data[0])   # Number of features per point
    centroid = [0] * dimension  # Initialize centroid coordinates to 0

    # Sum all the points in the cluster for each dimension
    for idx in cluster:
        for j in range(dimension):
            # Loop thorugh each point index in the cluster, and add its value to the centroid.
            centroid[j] += data[idx][j]

    # Divide by number of points to get the mean (centroid)
    return [x / num_points for x in centroid]


# Question3: Main function that implements K-means clustering algorithm
def genKmeans(database, k, max_iters, eps, output_file):
    n_points = len(database)   # Total number of data points
    dimension = len(database[0])  # Number of features per point

    # Step 1: Randomly select k points as initial centroids
    centroids = random.sample(database, k)

    # Step 2: Repeat until convergence or maximum iterations
    for iteration in range(max_iters):
        # Initialize empty list for each cluster
        clusters = [[] for _ in range(k)]

        # Step 3: Assignment Step
        # For each point, calculate distance to all centroids
        for idx, point in enumerate(database):
            distances = [euclidean_distance(point, centroid) for centroid in centroids]
            cluster_id = distances.index(min(distances))  # Assign to closest centroid
            clusters[cluster_id].append(idx)  # Add point index to corresponding cluster

        # Step 4: Update Step
        # Calculate new centroids based on current cluster assignments
        new_centroids = [compute_centroid(cluster, database) for cluster in clusters]

        # Step 5: Check for convergence
        # Convergence happens if maximum movement of any centroid is less than epsilon
        max_shift = max(
            euclidean_distance(old, new)
            for old, new in zip(centroids, new_centroids)
        )
        # compute how much the centroids have changed.
        if max_shift < eps:
            # if it's smaller than eps, the algorithm hsa converged and stops early
            break  # Stop if converged
        centroids = new_centroids  # Update centroids for next iteration
        # otherwise, update the centroid and repeat

    # Step 6: Output Clustering Result to Output File
    with open(output_file, 'w') as f:
        for cluster_id, cluster in enumerate(clusters):
            cluster.sort()  # Sort points within the cluster in increasing order
            # Write in required output format: cluster_id: point1 point2 ...
            f.write(f"{cluster_id}: {' '.join(map(str, cluster))}\n")


# Main function to handle input arguments and run the program
def main():
    args = sys.argv
    db_file = None
    k = None
    max_iters = None
    eps = None
    out_file = None

    # Parse command-line arguments
    for arg in args:
        if arg.startswith("-database_file="):
            db_file = arg.split("=")[1]
        elif arg.startswith("-k="):
            k = int(arg.split("=")[1])
        elif arg.startswith("-max_iters="):
            max_iters = int(arg.split("=")[1])
        elif arg.startswith("-eps="):
            eps = float(arg.split("=")[1])
        elif arg.startswith("-output_file="):
            out_file = arg.split("=")[1]

    # Check if all required arguments are provided
    if not all([db_file, k, max_iters, eps, out_file]):
        # Debug print for missing arguments
        print(db_file, k, max_iters, eps, out_file)
        # Print correct usage format
        print("Usage: ./kmeans -database_file=database.txt -k=k -max_iters=n -eps=e -output_file=output.txt")
        sys.exit(1)  # Exit the program with error

    # Read input data
    data = read_data(db_file)

    # Execute K-means algorithm
    genKmeans(data, k, max_iters, eps, out_file)

    # Notify successful execution
    print("All algorithm process works well")


# Program entry point
if __name__ == "__main__":
    main()
