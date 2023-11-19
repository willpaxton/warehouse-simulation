# warehouse-simulation
# Notes
- It is assumed that this warehouse is open 24/7 and will not have any downtime
- Our truck arrival distribution is based on a scale with the following times
  - 10pm-6am - "Low" Period - Rolls a dice with a [INSERT]% chance of making a truck show up in a time period 
  - 6am-11am, 5pm-10pm - "Moderate" Period - Rolls two dice, each with a [INSERT]% chance of making a truck show up in a time period (max of 1 truck)
  - 11am-5pm - "High" Period - Rolls four dice, each with a [INSERT]% chance of making a truck show up in a time period (max of 1 truck)
