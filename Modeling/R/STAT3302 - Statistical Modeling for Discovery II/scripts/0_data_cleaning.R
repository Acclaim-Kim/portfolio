############################
## This script performs data cleaning
## Last modified: 2025-04-18
## WK: I add num_eruptions into the volcano variable, delete unnecessary columns and count the empty spaces for each variable..
## GD: Added cleaning for primary volcano types
############################
## libraries we are using 
library(tidyverse)
library(dplyr)
############################

# Load volcano and eruption data set
volcano <- readr::read_csv('https://raw.githubusercontent.com/rfordatascience/tidytuesday/main/data/2020/2020-05-12/volcano.csv')
eruptions <- readr::read_csv('https://raw.githubusercontent.com/rfordatascience/tidytuesday/main/data/2020/2020-05-12/eruptions.csv')

# From the eruptions data set, extract the number of eruptions for each volcano
eruption_counts <- eruptions %>%
  count(volcano_number, sort = TRUE)
names(eruption_counts)[names(eruption_counts) == 'n'] <- 'num_eruptions'

# View the top volcanoes with the most eruptions
#head(eruption_counts)

# Merge volcano data set to the eruption_count data set by the volcano_number as a key
volcano <- merge(volcano, eruption_counts, by="volcano_number")

# Delete unnecessary variables
volcano <- volcano[ , !(names(volcano) %in% c("evidence_category", "major_rock_2", "major_rock_3", "major_rock_4", "major_rock_5"))]
volcano <- volcano[ , !(names(volcano) %in% c("minor_rock_1", "minor_rock_2", "minor_rock_3", "minor_rock_4", "minor_rock_5"))]
volcano <- volcano[ , !(names(volcano) %in% c("population_within_5_km", "population_within_10_km", "population_within_30_km", "population_within_100_km"))]
volcano <- volcano[ , !(names(volcano) %in% c("country", "subregion"))]

# Make a function to summarize the data frame for the null and empty space confirmation
summarize_df <- function(df) {
  tibble(
    variable = names(df),
    type = sapply(df, class),
    num_na = sapply(df, function(x) sum(is.na(x))),
    num_empty = sapply(df, function(x) sum(x == "", na.rm = TRUE)),
    num_unknown = sapply(df, function(x) sum(x == "Unknown", na.rm = TRUE)),
    total_missing = sapply(df, function(x) sum(is.na(x) | x == "" | x == "Unknown", na.rm = TRUE))
  )
}
summary_table <- summarize_df(volcano)
print(summary_table)

## last_eruption_year has 32 "Unknown" value as a character type variable, which means that about 4.6 percent data has Unknown values.

# Remove the rows that has Unknown in last_eruption_year
volcano <- volcano %>%
  filter(last_eruption_year != "Unknown")

# convert last_eruption_year to numeric
volcano$last_eruption_year <- as.numeric(volcano$last_eruption_year)

# the primary volcano types have types like "Stratovolcano", "Stratovolcano?", "Stratovolcano(es)" as different types, 
# we merge these into one single type.
volcano$primary_volcano_type <- gsub("\\(s\\)|\\(es\\)|\\?", "", volcano$primary_volcano_type)

# Confirm that there is no empty space or null or Unknown value in the data frame
summary_table <- summarize_df(volcano)
print(summary_table)

# Look how many factor variables we have if we convert character type variable to factor variable.
cat("primary_volcano_type: ", length(unique(volcano$primary_volcano_type)), "\n")
# cat("country: ", length(unique(volcano$country)), "\n")
cat("region: ", length(unique(volcano$region)), "\n")
# cat("subregion: ", length(unique(volcano$subregion)), "\n")
cat("tectonic_settings: ", length(unique(volcano$tectonic_settings)), "\n")
cat("major_rock_1: ", length(unique(volcano$major_rock_1)), "\n")
## Since country, region, subregion, latitude, longitude variables represent the location feature.
## We select only region for location feature having fewer factor variable and prevent over fitting.

# Ranges of our continuous variables
range(volcano$last_eruption_year)
range(volcano$latitude)
range(volcano$longitude)
range(volcano$elevation)

# How many n and p in our data frame
cat("We have n observations and p features in cleaned data, (n, p) is: ", dim(volcano), "\n")

# Write the volcano data frame as csv file.
# Your working directory must be the STAT3302 Project folder and not the scripts folder for this to work.
write.table(volcano, "./data/volcano_cleaned.csv", sep = ",", row.names = FALSE)

