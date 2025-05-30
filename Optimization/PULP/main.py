from modules import input_csv as i
from modules import algo as a
from modules import output_csv as o
import pandas as pd
import os

#gets data from xlsx doc format (folder name, file name)
data = i.import_file(os.path.join('input_data', 'test.xlsx'))


'''
this takes the raw input from the excel file (row by row)
and converts the data from excel rows into a nest hashmap 

hashmap structure:

plant
    workcenter
        material (hashmap that contains excel data)

'''
factories = i.parseRecords(data)


'''
the following items are the two separate algorithms that have been developed
one will be commented out, the other will be the one that is selected to run
'''
a.scheduleItems(factories)
#a.optimizeScheduleItems(factories)

'''
this takes the raw input from the excel file (row by row)
and converts the data from excel rows into a nest hashmap 

hashmap structure:

plant
    workcenter
        material (hashmap that contains excel data)

'''

#writes executive summary text file
o.writeTXT(factories)
#writes excel schedule
o.write_to_excel(factories)

print('Scheduling Complete\n')
