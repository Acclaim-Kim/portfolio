import argparse #This library allows users to provide custom parameters when running the script
import numpy as np #Python library that supports for large, multi-dimensional arrays and matrices and their operations

#Question3-1: Create a Node class to store tree information
class Node:
    #Question3-1: Stores all necessary properties of a decision tree node
    def __init__(self, data_idx, impurity_method, level, labels):
        self.data_idx = data_idx #It stores the indices of data points at this node.
        self.impurity_method = impurity_method #It stores the impurity method (gini or entropy)
        self.impurity = None #Impurity value for the node
        self.feature = None #Feature index used for splitting
        self.level = level #Depth level of the node in the tree.
        self.majority_class = self.find_majority_class(labels) #Majority class label at this node
        #find_majority_class(): determines the most common class among the training examples at this node.
        self.left_child = None #left child node
        self.right_child = None #right child node
    
    #Question3-3: Determine the majority class at each node
    #This finds the most frequent class in the subset of training data at this node
    def find_majority_class(self, labels):
        if len(self.data_idx) == 0:
            return None
        unique, counts = np.unique(labels[self.data_idx], return_counts=True)
        return unique[np.argmax(counts)]
    
    #Question3-5: Computes the impurity for a node using either calculate_Gini or Entropy methods
    def calculate_ip(self, data, labels):
        if self.impurity_method == 'gini':
            return self.calculate_gini(labels)
        elif self.impurity_method == 'entropy':
            return self.calculate_entropy(labels)
    
    #Question3-6: Compute impurity values used for tree splitting (gini)
    #Gini index measures how impure a node is
    def calculate_gini(self, labels):
        _, counts = np.unique(labels, return_counts=True)
        probs = counts / counts.sum()
        return 1 - np.sum(probs ** 2) #Gini index formula
    
    #Question3-6: Compute impurity values used for tree splitting (entropy)
    #Entropy measures the disorder in the labels
    def calculate_entropy(self, labels):
        _, counts = np.unique(labels, return_counts=True)
        probs = counts / counts.sum()
        return -np.sum(probs * np.log2(probs + 1e-9)) #Entropy formula (logarithm prevents division by zero)
    
    #Question3-4: Find the  best feature to split the data and recursively divides the dataset into left and right children.
    def split_node(self, data, labels, nlevels, pthrd):
        if self.level >= nlevels or self.impurity <= pthrd:
            return #stop splitting if max depth is reached or impurity is low.
        
        best_feature = -1 #-1 means no valid feature found yet
        best_gain = -1 #Track best impurity reduction
        
        for feature in range(data.shape[1]): #loop through features
            left_idx = self.data_idx[data[self.data_idx, feature] == 0] #Left Child
            right_idx = self.data_idx[data[self.data_idx, feature] == 1] #Right Child
            
            if len(left_idx) == 0 or len(right_idx) == 0:
                continue #skip feature if no split occurs
            
            left_impurity = self.calculate_ip(data[left_idx], labels[left_idx])
            right_impurity = self.calculate_ip(data[right_idx], labels[right_idx])
            weighted_impurity = (len(left_idx) * left_impurity + len(right_idx) * right_impurity) / len(self.data_idx) #impurity after split
            gain = self.impurity - weighted_impurity #Compute impurity reduction

            #Updates the best feature if this split gives a higher gain
            if gain > best_gain:
                best_gain = gain
                best_feature = feature
                best_left_idx = left_idx
                best_right_idx = right_idx
        
        if best_gain > 0:
            #Creates left and right child node and stores the best splitting feature
            self.feature = best_feature
            self.left_child = Node(best_left_idx, self.impurity_method, self.level + 1, labels)
            self.right_child = Node(best_right_idx, self.impurity_method, self.level + 1, labels)
            self.left_child.impurity = self.calculate_ip(data[best_left_idx], labels[best_left_idx])##
            self.right_child.impurity = self.calculate_ip(data[best_right_idx], labels[best_right_idx])##
            self.left_child.split_node(data, labels, nlevels, pthrd)
            self.right_child.split_node(data, labels, nlevels, pthrd)

#Question3-2: Implement node.buildDT to build a decision tree, starting from the root.
def buildDT(data, labels, impurity_method, nlevels, pthrd):
    root = Node(np.arange(len(labels)), impurity_method, 0, labels) #Create a root node with all training data
    root.impurity = root.calculate_ip(data, labels) #Compute impurity for the root node
    root.split_node(data, labels, nlevels, pthrd) #split_node method is to recursively grow the tree
    return root

#Question4: Takes a single test sample and determines its class by traversing the decision tree
def predict(node, x):
    if node.left_child is None or node.right_child is None:
        return node.majority_class #At a leaf node, we return the majority class stored at that node
    if x[node.feature] == 0:
        return predict(node.left_child, x) #Traverse left if feature is 0
    else:
        return predict(node.right_child, x) #Traverse right if feature is 1

#Question4: Classify every row in the test dataset and saves all predicvtions into an output file
def classify(tree, test_data, output_file):
    predictions = [predict(tree, x) for x in test_data] #Loop through each row in test_data and predict each test sample with predict method
    #save predictions to output.txt
    with open(output_file, 'w') as f:
        for label in predictions:
            f.write(f"{label}\n") #Save predictions to file

#Question2: This function reads a dataset file and convert it into a numpy array
def load_data(file_path):
    return np.genfromtxt(file_path, delimiter=' ')
    #genfromtxt(): It reads the file and return the data as a structured NumPy array using the space as delimiter

#Bonus Question: Calculate the performance of the classifier and print it
from sklearn.metrics import precision_score, recall_score, accuracy_score #use those three methods to calculate them from sklearn library
def evaluate_performance(pred_file, true_label_file):
    # Load predicted labels from output.txt
    predicted_labels = np.loadtxt(pred_file, dtype=int)

    # Load true labels from test_label.txt
    true_labels = np.loadtxt(true_label_file, dtype=int)

    # Compute metrics
    precision = precision_score(true_labels, predicted_labels, average='macro', zero_division=0)
    recall = recall_score(true_labels, predicted_labels, average='macro', zero_division=0)
    accuracy = accuracy_score(true_labels, predicted_labels)

    # Print results
    print(f"Precision: {precision:.4f}")
    print(f"Recall: {recall:.4f}")
    print(f"Accuracy: {accuracy:.4f}")

#Question5: Implement a main() function
def main():

    #Question1: read the parameter values from the command line store them
    parser = argparse.ArgumentParser()
    #argparse.ArgumentParser: creates an object to handle command-line arguments.

    parser.add_argument('-train_data', required=True)
    parser.add_argument('-train_label', required=True)
    parser.add_argument('-test_data', required=True)
    parser.add_argument('-nlevels', type=int, required=True)
    parser.add_argument('-pthrd', type=float, required=True)
    parser.add_argument('-impurity', choices=['gini', 'entropy'], required=True)
    parser.add_argument('-pred_file', required=True)
    #Define options users must provide.
    
    args = parser.parse_args() #This collects user input and stores it in the args object
    
    #Question1: Convert arguments as string and save them as variable.
    train_file = str(args.train_data)
    train_label_file = str(args.train_label)
    test_file = str(args.test_data)
    pred_file = str(args.pred_file)
    #str: convert the argument value to a string

    #Question2: load the training and testing data
    train_data = load_data(train_file)
    train_labels = load_data(train_label_file).astype(int)
    test_data = load_data(test_file)

    
    #Question4: After training the decision tree
    tree = buildDT(train_data, train_labels, args.impurity, args.nlevels, args.pthrd) #creates the decision tree
    classify(tree, test_data, args.pred_file) #applies the trained tree to test data and save predictions
    print(f"Predictions saved to {args.pred_file}") #output the file

    #Bonus question: Print out the performance of classifier
    evaluate_performance(args.pred_file, 'test_label.txt')

if __name__ == '__main__':
    main()
