#scheudling algroithm psuedocode
import math
from pulp import LpProblem, LpMinimize, LpVariable, lpSum, value, LpBinary


def scheduleItems(data):
    if data:
        for factory, work_centers in data.items():
           for work_center, materials in work_centers.items():
                items = []
                
                
                for material, row in materials.items():
                   index = row.get('priority', len(items))
                   #this checks priority ranking, if no priority exist places it at the back of the priority
                   if math.isnan(index):
                        index = len(items)
                   index = int(index)
                   items.insert(index, row)
                
                #this is the hour variable for the formula, the function will increment until this meets 168
                i = 1
                #this is the array element for the product to be produced
                p = 1
                #this iniitialized the beginning of the time window for production
                window = 0
                #this is the limit on the amount of time one product can be produced --------------------------- this can be changed to your liking
                maxTimeSlot = 12
                #initialized
                qty_needed = 0
                #this is the penalty for switching materials --------------------------------------------------- this can be changed to your liking
                changeover_penalty = 2
                
                while i < 169:
                    if window == 0:
                        material = items[p]
                    
                    qty_needed = material.get('reqbal_wk1', 0)
                    rate = material.get('ratephr', 0)
                    qty_needed += rate
                    material["reqbal_wk1"] =  qty_needed
                    materials.get('schedule')["hour" + str(i)] = material.get('material')
                    i += 1
                    window += 1
                    if window == maxTimeSlot or qty_needed > 0:
                        if qty_needed > 0:
                            items.remove(items[p])
                        else:
                            p += 1
                        
                        window = 0
                        
                        i += changeover_penalty
                        if p >= len(items):
                            p = 1
                    if len(items) == 1:
                        break
                    
def make_schedule_continuous(schedule, materials):
    """
    Rearrange the schedule to ensure each material is scheduled in a continuous block,
    starting with highest priority material from hour 1.
    Also adds 2-hour changeover periods between different materials.
    Preserves plant and wc information.
    """
    # Store plant and wc information
    plant = schedule.get("plant", "")
    wc = schedule.get("wc", "")
    
    # Count total hours needed for each material from initial schedule
    material_hours = {}
    for h in range(1, 169):
        material = schedule.get(f"hour{h}")
        if material != "x" and material != "infeasible":
            material_hours[material] = material_hours.get(material, 0) + 1  
    
    # Create new continuous schedule
    new_schedule = {}
    
    # Add plant and wc information first
    new_schedule["plant"] = plant
    new_schedule["wc"] = wc
    
    current_hour = 1
    
    # Get materials with their priorities
    materials_with_priority = []
    for m in materials:
        if m != "schedule" and material_hours.get(m, 0) > 0:
            materials_with_priority.append((m, materials[m].get('priority', float('inf'))))
    
    # Sort by priority (lower number = higher priority)
    materials_with_priority.sort(key=lambda x: x[1])
    
    # Schedule materials in priority order, each in a continuous block
    for i, (m, _) in enumerate(materials_with_priority):
        hours_needed = material_hours[m]
        if hours_needed > 0:
            # Add changeover time (2 hours) if this is not the first material
            if i > 0 and current_hour <= 166:  # Ensure we have room for changeover
                new_schedule[f"hour{current_hour}"] = "0"
                new_schedule[f"hour{current_hour + 1}"] = "0"
                current_hour += 2
            
            # Schedule this material in a continuous block
            for _ in range(hours_needed):
                if current_hour <= 168:
                    new_schedule[f"hour{current_hour}"] = m
                    current_hour += 1
                else:
                    break
    
    # Fill remaining hours with "x"
    while current_hour <= 168:
        new_schedule[f"hour{current_hour}"] = "x"
        current_hour += 1
    
    return new_schedule

def optimizeScheduleItems(data):
    """Optimize production schedule using linear programming with PuLP"""
    print("Running simplified optimization-based scheduler...")
    
    # Iterate through each factory and its work centers
    for factory, work_centers in data.items():
        for wc, materials in work_centers.items():
            # Skip the 'schedule' key as it's not a work center
            if wc == 'schedule':
                continue

            print(f"\nProcessing {factory} - {wc}")
            
            # Get all material IDs
            material_ids = [m for m in materials if m != 'schedule']
            
            # Print essential material information
            print("Materials:")
            for m in material_ids:
                reqbal = materials[m].get('reqbal_wk1', 0)
                rate = materials[m].get('ratephr', 0)
                print(f"  {m}: priority={materials[m].get('priority')}, reqbal={reqbal}, rate={rate}")
            
            # Create a new linear programming problem for this work center
            prob = LpProblem(f"Factory_{factory}_WC_{wc}", LpMinimize)
            
            # Define time periods (hours) for a week (168 hours)
            hours = range(1, 169)
            
            # Create binary decision variables for this work center
            # x[m,h] = 1 if material m is produced at hour h, 0 otherwise
            x = LpVariable.dicts("x", 
                               ((m, h) for m in material_ids for h in hours),
                               cat=LpBinary)
            
            # Initialize list to store recovery penalties for this work center
            work_center_penalties = []
            
            # Process each material in this work center
            for m in material_ids:
                row = materials[m]
                reqbal_wk1 = row.get('reqbal_wk1', 0)  # Current required balance
                rate = row.get('ratephr', 0)  # Production rate per hour
                priority = row.get('priority', 1)  # Get priority, default to 1 if not specified
                
                # Calculate priority weight
                priority_weight = 1000000 * (1 / priority) if priority and priority > 0 else 100
                
                # Validate and handle production rate
                try:
                    rate = float(rate)
                    if not (isinstance(rate, (int, float)) and rate > 0 and rate != float('inf')):
                        print(f"Warning: Invalid rate for material {m}: {rate}. Skipping this material.")
                        continue  # Skip this material entirely
                except (ValueError, TypeError):
                    print(f"Warning: Could not convert rate for material {m} to float. Skipping this material.")
                    continue  # Skip this material entirely
                
                # Calculate total production for this material across all hours
                total_produced = lpSum([x[m, h] * rate for h in hours])
                
                # Handle reqbal_wk1 based on its sign
                if reqbal_wk1 < 0:
                    # Negative reqbal_wk1 means we need more products
                    # For example, reqbal_wk1 = -100 means we need 100 more products
                    required_production = abs(reqbal_wk1)
                    
                    # Add constraint: once total_produced >= required_production, no more production
                    # This ensures we move on to next priority material once balance is satisfied
                    prob += total_produced <= required_production, f"max_production_{m}"
                    
                    # Add penalty based on priority weight
                    work_center_penalties.append((required_production - total_produced) * priority_weight)
                elif reqbal_wk1 > 0:
                    # Positive reqbal_wk1 means we have extra products
                    # For example, reqbal_wk1 = 1250 means we have 1250 extra products
                    # No need to produce more for this material
                    pass
                else:
                    # reqbal_wk1 = 0 means we have exactly the required amount
                    # No need to produce more for this material
                    pass
            
            # Set the objective function for this work center
            prob += lpSum(work_center_penalties)
            
            # Add constraint: only one material can be produced at any given hour
            for h in hours:
                prob += lpSum([x[m, h] for m in material_ids]) <= 1, f"one_material_at_time_{h}"
            
            # Add constraint: total production hours cannot exceed 168
            prob += lpSum([x[m, h] for m in material_ids for h in hours]) <= 168, "total_hours_constraint"
            
            # Solve the optimization problem for this work center
            print(f"Solving optimization for {factory} - {wc}...")
            prob.solve(PULP_CBC_CMD(timeLimit=30, msg=True, options=['mipGap 0.05', 'maxNodes 10000']))
            
            # Process the solution for this work center
            status = prob.status
            print(f"Solver status: {status}")
            
            # Handle non-optimal solutions
            if status != 1:  # 1 means optimal
                print(f"Warning: Solver did not find optimal solution. Status: {status}")
                if status == -1:
                    print("Problem is infeasible")
                    schedule = materials.get("schedule", {})
                    for h in hours:
                        schedule[f"hour{h}"] = "infeasible"
                    continue
                elif status == 0:  # Not solved
                    print("Solver did not complete within time limit")
                    if any(value(x[m,h]) is not None for m in material_ids for h in hours):
                        print("Using partial solution found so far")
                        schedule = {}
                        for h in hours:
                            for m in material_ids:
                                if value(x[m,h]) is not None and value(x[m,h]) > 0.5:
                                    schedule[f"hour{h}"] = m
                                    break
                            else:
                                schedule[f"hour{h}"] = "x"
                        
                        # Make schedule continuous
                        schedule = make_schedule_continuous(schedule, materials)
                        
                        # Store the schedule
                        materials["schedule"] = schedule
                    else:
                        print("No solution found, marking as infeasible")
                        schedule = materials.get("schedule", {})
                        for h in hours:
                            schedule[f"hour{h}"] = "infeasible"
                        continue
            
            # Store the optimized schedule
            schedule = materials.get("schedule", {})
            
            # Build temporary schedule from LP solution
            temp_schedule = {}
            
            # Add factory info first
            temp_schedule["plant"] = factory
            temp_schedule["wc"] = wc
            
            for h in hours:
                material_scheduled = None
                for m in material_ids:
                    if value(x[m, h]) == 1:
                        material_scheduled = m
                        break

                if material_scheduled is not None:
                    temp_schedule[f"hour{h}"] = material_scheduled
                else:
                    temp_schedule[f"hour{h}"] = "x"

            # Make the schedule continuous
            final_schedule = make_schedule_continuous(temp_schedule, materials)

            # Update reqbal_wk1 based on actual production
            for m in material_ids:
                # Count how many hours this material is scheduled
                hours_scheduled = sum(1 for h in hours if final_schedule.get(f"hour{h}") == m)
                rate = materials[m].get('ratephr', 0)
                if rate > 0:
                    # Calculate production amount
                    production = hours_scheduled * rate
                    
                    # Get current reqbal_wk1
                    current_reqbal = materials[m]['reqbal_wk1']
                    
                    if current_reqbal < 0:
                        # If we need more products (negative reqbal_wk1)
                        # Adding production reduces the deficit
                        materials[m]['reqbal_wk1'] = min(0, current_reqbal + production)
                    elif current_reqbal > 0:
                        # If we have extra products (positive reqbal_wk1)
                        # Adding production increases the surplus
                        materials[m]['reqbal_wk1'] = current_reqbal + production
                    else:
                        # If we have exactly the required amount
                        # Adding production creates a surplus
                        materials[m]['reqbal_wk1'] = production

            # Save back
            materials["schedule"] = final_schedule


    print("Optimization scheduling complete.")

# psuedocode for this algorithm

#for each plant/facotry
    #for each work center
        #while hours less than 168
            #ALL THE ITEMS NEED TO BE ADJUSTED AS THEY ARE TOUCHED IN THE DICITIONARY

            #get the highest priority item (priority) that has a negative reqbal_wk1
            #produce one hour of material (ratephr)
            #adjust reqbal_wk1 in the dictionary accordingly by the amount produced
            
            #write the work of that hour into the work schedule dictionary
            
            #if reqbal_wk1 is now positive
                #skip the next two hours for changeover time to the next material
            #move onto the next hour
