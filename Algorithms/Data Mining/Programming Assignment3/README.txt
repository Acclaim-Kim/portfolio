CSE 5243 - Programming Assignment 3
K-means Clustering

Student Name: [Hwanho Kim]
Student ID: [kim.8757]

-----------------------------------
Command to Run the Program:

python3 kmeans.py -database_file=data.txt -k=3 -max_iters=100 -eps=0.0001 -output_file=output.txt

-----------------------------------
Data Structure Used:

- Data Points:
  Represented as a list of lists.
  Each inner list corresponds to a data point with float feature values.

- Clusters:
  Represented as a list of lists.
  Each inner list stores the indices (IDs) of data points assigned to that cluster.

- Centroids:
  Represented as a list of lists.
  Each inner list contains float numbers corresponding to the centroid coordinates.
