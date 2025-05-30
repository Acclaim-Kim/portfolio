## This script looks at exploratory data analysis 
## Last modified: 2025-04-20
############################
## libraries we are using 
library(ggplot2)
library(GGally)
library(dplyr)
library(readr)
library(corrplot)
library(patchwork)
############################

# Your working directory must be the STAT3302 Project folder and not the scripts folder for this to work.
volcano <- read_csv("./data/volcano_cleaned.csv")

# Show the relationship between response variable and numerical variables
# Select only numeric variables
volcano_numeric <- volcano %>%
  select(last_eruption_year, latitude, longitude, elevation, num_eruptions)
# Draw scatter plot matrix
ggpairs(volcano_numeric) + theme_minimal()

# plot specifically last_eruption_year which appears to have a strong exponential relationship with num_eruptions.
ggplot(volcano, aes(x = last_eruption_year, y = num_eruptions)) +
  geom_point() + theme_minimal()

## There is no strong relation between num_eruptions and other numerical variables.

# convert categorical columns to factors
volcano$primary_volcano_type <- as.factor(volcano$primary_volcano_type)
volcano$region <- as.factor(volcano$region)
volcano$tectonic_settings <- as.factor(volcano$tectonic_settings)
volcano$last_eruption_year <- as.numeric(volcano$last_eruption_year)

# Compare region to latitude
# ggplot(data = volcano, aes(x = region, y = latitude)) + 
#   geom_boxplot() +
#   theme_minimal() + 
#   theme(axis.text.x = element_text(angle = 90, size = 6))

# Reorder region by median latitude
volcano <- volcano %>%
  mutate(region = fct_reorder(region, latitude, .fun = median, .na_rm = TRUE))

# Compare region to latitude with ordered regions
ggplot(data = volcano, aes(x = region, y = latitude)) + 
  geom_boxplot() +
  theme_minimal() + 
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  labs(title = "Latitude Distribution by Region (Ordered by Median Latitude)",
       x = "Region (ordered by median latitude)",
       y = "Latitude")

# Compare region to longitude
# ggplot(data = volcano, aes(x = region, y = longitude)) + 
#   geom_boxplot() +
#   theme_minimal() + 
#   theme(axis.text.x = element_text(angle = 90, size = 6))

# Reorder region by median longitude
volcano <- volcano %>%
  mutate(region = fct_reorder(region, longitude, .fun = median, .na_rm = TRUE))

# Compare region to longitude with ordered regions.
ggplot(data = volcano, aes(x = region, y = longitude)) + 
  geom_boxplot() +
  theme_minimal() + 
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  labs(title = "Longitude Distribution by Region (Ordered by Median Longitude)",
       x = "Region (ordered by median longitude)",
       y = "Longitude")

# It seems from these graphs that a given region has a relatively 
# small distribution of latitudes and longitudes.  
# region and latitude and longitude are likely correlated.
# Furthermore, the longitude graph exposes a potential issue of using 
# longitude as a predictor - 
# The Fiji/New Zealand region has an apparently wide spread of longitude because
# it is right where the longitude transitions from -180 and +180.
# Plainly, if you go east far enough, you'll end up west.

# compare categorical variables to the number of eruptions without outlier to compare factors clearly.

# PLOTS WITH OUTLIERS
# Volcano Type
v_type_plot <- ggplot(volcano, aes(x = primary_volcano_type, y = num_eruptions)) + 
  geom_boxplot() + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6))
# Region
reg_plot <- ggplot(volcano, aes(x = region, y = num_eruptions)) + 
  geom_boxplot() +
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6))
# Tectonic Settings
tect_set_plot <- ggplot(volcano, aes(x = tectonic_settings, y = num_eruptions)) + 
  geom_boxplot() + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6))
# Major Rock
maj_rock_plot <- ggplot(volcano, aes(x = major_rock_1, y = num_eruptions)) + 
  geom_boxplot() + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6))

(v_type_plot | reg_plot)

(tect_set_plot | maj_rock_plot)


# REMOVE OUTLIERS
# Volcano Type
v_type_plot <- ggplot(volcano, aes(x = primary_volcano_type, y = num_eruptions)) + 
  geom_boxplot(outlier.shape = NA) + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  coord_cartesian(ylim = c(0, quantile(volcano$num_eruptions, 0.95)))  # Optional: zoom in
# Region
reg_plot <- ggplot(volcano, aes(x = region, y = num_eruptions)) + 
  geom_boxplot(outlier.shape = NA) + # Hide outliers
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  coord_cartesian(ylim = c(0, quantile(volcano$num_eruptions, 0.95)))
# Tectonic Settings
tect_set_plot <- ggplot(volcano, aes(x = tectonic_settings, y = num_eruptions)) + 
  geom_boxplot(outlier.shape = NA) + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  coord_cartesian(ylim = c(0, quantile(volcano$num_eruptions, 0.99)))
# Major Rock
maj_rock_plot <- ggplot(volcano, aes(x = major_rock_1, y = num_eruptions)) + 
  geom_boxplot(outlier.shape = NA) + 
  theme_minimal() +
  theme(axis.text.x = element_text(angle = 90, size = 6)) +
  coord_cartesian(ylim = c(0, quantile(volcano$num_eruptions, 0.95)))

(v_type_plot | reg_plot)

(tect_set_plot | maj_rock_plot)


