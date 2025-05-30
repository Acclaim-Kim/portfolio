import os
import pandas as pd

def writeTXT(data):

    folder_path = "output_data" # Replace with your desired folder path
    file_name = "exec_summary.txt"
    file_path = os.path.join(folder_path, file_name)

    # Create the folder if it doesn't exist
    os.makedirs(folder_path, exist_ok=True)

    text_content = "This is the text content to be written to the file."

    factorySummary = []

    with open(file_path, "w") as file:
        if data:
            for factory, work_centers in data.items():
                FnumLate = 0
                FvalueLate = 0
                for work_center, materials in work_centers.items():
                    #file.write(f"\t Work Center: {work_center}\n")
                    WCnumLate = 0
                    WCvalueLate = 0
                    # Iterate through the materials to count late ones
                    for material, row in materials.items():
                        # Check if the material is late (negative reqbal_wk1)
                        if row.get('reqbal_wk1', 0) < 0:
                            WCnumLate += 1  # Increment the count of late materials

                        '''
                        THIS IS THE AREA WHERE YOU WOULD SUBSITUTE THE ACUTAL COST FORMULA
                        We are currently using pd-value / pd-qty to estimate this cost but subsituting for the actual cost will provide more accurate results
                        '''
                        # Extract relevant values safely
                        qty = float(row.get('pd_qty_tradedist', 0))
                        value = float(row.get('pd_value_tradedist', 0))
                        reqbal_wk1 = float(row.get('reqbal_wk1', 0))

                         # Avoid division by zero and NaN propagation
                        if qty > 0 and reqbal_wk1 < 0 and not any(map(lambda x: x != x, [value, reqbal_wk1])):  # Check if values are NaN
                            #calculates late value
                            #assumes pd_value/pd_qty = individual item value
                            WCvalueLate += (value/ qty) * reqbal_wk1
                        #-----------------------------------------------------------------------------------------------------------

                    # Write the number of late materials for each work center - can be put into the exec summary if you'd like
                    #file.write(f"\t\t Number of Late Materials: {WCnumLate}\n")
                    #file.write(f"\t\t Amount of Late Revenue: ${round(abs(WCvalueLate),2)}\n")

                    FvalueLate += WCvalueLate
                    FnumLate += WCnumLate

                factorySummary.append([factory, FnumLate, FvalueLate])

            file.write("\n\nExecutive Summary:\n\n")
            for summary in factorySummary:
                file.write(f"Plant Number: {summary[0]}\n")
                file.write(f"\tPlant Number of Late Materials: {summary[1]}\n")
                file.write(f"\tPlant Amount of Late Revenue: ${round(abs(summary[2]),2)}\n")
        else:
            print("No data was read from the Excel file.")

    print(f"Text file created at: {file_path}")




def write_to_excel(factories):

    folder_path = "output_data" # Replace with your desired folder path
    file_name = "work_schedule.xlsx"
    output_file = os.path.join(folder_path, file_name)
    data_list= []
    for factory, work_centers in factories.items():
        for work_center in work_centers.values():
            data_list.append(work_center.get('schedule'))


# Convert list of dicts to DataFrame
    df = pd.DataFrame(data_list)

# Save to Excel
    df.to_excel(output_file, index=False)
