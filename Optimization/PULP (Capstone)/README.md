# Capstone
This is our project repository for CSE5915 and the TE AI Cup. The goal of this project is to take a given excel file of materials, and output a production schedule in excel for each work center.

# REQUIRED LIBRARIES
- Pulp
- Pandas
- Math

# Main.py

The structure of main is as follows:<br />
- Calls the input parser (i) by giving a filename (Currently using test.xlsx)<br />
- Schedules the work (a)<br />
- Outputs the schedule into an excel document, along with an executive sumamry (o)<br />


# Input_CSV.py

The input file will be placed within the input data folder in this project. This will retrive it from the folder and parse the data into hashmaps.

# Output_CSV.py

This script is responsible for taking the hashmaps and putting them into a excel document and calculating and outputting an executive summary of each plant.

# Algo.py

## Round Robin / Priority Scheduling Hybrid Algorithm

This algorithm functions based on two constraints:<br />
- Priority<br />
    - This is based on the given priority from the excel data, the timeslots to be produced will be awarded based on this data<br />
- Window<br />
    - This is the maximum amount of time that a material may be produced<br />
    - The window size is postively correlated with the amount of late materials<br />
    - The window size is negatively correlated with the amount of late revenue<br />

## Optimization Algorithm

This algorithm uses linear programming (via the Pulp library) to generate an optimized production schedule for each work center. 
The goal is to allocate limited production time (168 hours per week) to fulfill material demands in priority order, while reducing changeover downtime.

The optimization algorithm follows these principles:<br />
- **Objective Function**:<br />
    - Minimize total production penalties:  
      `work_center_penalties = SUM((required production - total produced) * priority weight)`  
    - This ensures materials with higher priority (lower priority number) are scheduled first and that total shortages are minimized.<br />

- **Decision Variables**:<br />
    - Binary decision variable:  
      `x[m,h] = 1` if material `m` is produced at hour `h`, otherwise 0  
    - Each work center has a matrix of `x[m][h]` representing hour-by-hour decisions for each material.

- **Constraints**:<br />
    - **Constraint 1 – Only one material can be produced at any given hour**  
      `Σ x[m,h] ≤ 1 for all h`  
      No overlap is allowed in scheduling at each hour.
    - **Constraint 2 – Total production hours per work center cannot exceed 168**  
      `Σ x[m,h] over all m and h ≤ 168`  
      Respects weekly capacity limits.
    - **Constraint 3 – Don’t over-produce materials**  
      `Total produced ≤ required production`  
      Production for each material is limited to only what’s needed (based on `reqbal_wk1`).

- **Additional Features**:<br />
    - A post-processing step (`make_schedule_continuous`) rearranges each material’s assigned hours into a contiguous block.
    - A 2-hour changeover gap (`"0"`) is inserted when switching between different materials.
    - Updated `reqbal_wk1` values reflect actual production assigned, ensuring accurate unmet demand tracking.

The final schedule is saved back to the data dictionary, exported to Excel per work center, and summarized in a report showing remaining unmet demand and production efficiency.


