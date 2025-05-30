import pandas as pd

def import_file(filePath):
    try:
        df = pd.read_excel(filePath)
        return df.to_dict(orient='records')
    except Exception as e:
        print(f"Error reading Excel file: {e}")
        return []

def parseRecords(data):
    factories = {}


    for row in data:
        factory = row['plant']
        work_center = row['wc']
        material = row['material']
        with_ctb = row['with ctb']

        # Ensure factory exists
        if factory not in factories:
            factories[factory] = {}

        # Ensure work center exists within factory, adds work center and workschedule if not
        if work_center not in factories[factory] and with_ctb == 'Yes':
            factories[factory][work_center] = {}
            work_center_schedule = {}
            work_center_schedule['plant'] = factory
            work_center_schedule['wc'] = work_center
            for i in range(1, 169):
                name = "hour" + str(i)
                work_center_schedule[name] = 0
            factories[factory][work_center]['schedule'] = work_center_schedule


        # Store the material data inside the correct hierarchy, ignores with_ctb is no
        if with_ctb == 'Yes':
            factories[factory][work_center][material] = row

    return factories

#optional debug function to display records on the command line
def displayRecords(data_dict):
    if data_dict:
        print(data_dict)
    else:
        print("No data was read from the Excel file.")

