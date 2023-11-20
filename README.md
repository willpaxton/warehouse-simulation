# warehouse-simulation
# Notes
- The program must be ran by using the ```dotnet run``` command inside of the working folder so that the program can properly access files
- The crateData.csv file will export to the data folder
- It is assumed that this warehouse is open 24/7 and will not have any downtime
- Our truck arrival distribution is based on a scale with the following times
  - 10pm-6am - "Low" Period - Rolls a dice with a 33% chance of making a truck show up in a time period 
  - 6am-11am, 5pm-10pm - "Moderate" Period - Rolls a dice with a 50% chance of making a truck show up in a time period 
  - 11am-5pm - "High" Period - Rolls a dice with a 80% chance of making a truck show up in a time period, with a 33% chance on top for 2 trucks to show up in a given time period
- The simulation starts at 2am, in the middle of the low period.
- Both of the recommendations were based off the Warehouse being open for 7 days, with each truck carrying a maximum of 25 crates.

---

### Nick Trahan ###
I believe the proper amount of docks to be open is 8. This is because of the fact that it led to the highest profit. Its profit was about $426,917, while 7 and 9 were around 325k - 375k. 8 docks, while not the highest average per truck, was the highest average price per Crate.

---

### Will Paxton ###
Using the 7 days of simulation with each truck having a maximum capacity of 25, it looks that having 7 docks would be the best for the warehouse.  With 7 docks, every dock would have an 80% or higher usage time, meaning that operation costs are not being wasted.  7 docks also consistently gets the highest profit over other options (such as 6 or 8 docks).  Using 7 docks nets around $375k in profit, while using 6 or 8 docks nets $300k-$350k in profit.
