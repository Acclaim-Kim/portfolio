## This script is for model selection.
## Last modified: 2025-04-19
############################
## libraries we are using 
## TODO: Add libraries
############################

volcano <- read_csv("./data/volcano_cleaned.csv")

# Fit the null poisson model
# null_poisson <- glm(num_eruptions ~ 1, family = poisson, data = volcano)

# Fit the full Poisson model
full_poisson <- glm(
  num_eruptions ~ primary_volcano_type + last_eruption_year + region +
    latitude + longitude + elevation + tectonic_settings + major_rock_1,
  family = poisson(link = "log"),
  data = volcano
)

# Use stepwise AIC (both directions by default)
step_poisson <- step(full_poisson, direction = "both")
summary(step_poisson)

# View step AIC formula
step_form <- formula(step_poisson)

# Formula of step AIC model with no latitude or longitude
no_lat_long_form <- update(step_form, . ~ . - latitude - longitude)
model_no_lat_long <- glm(no_lat_long_form,
                         family = poisson(link = "log"),
                         data = volcano)


# Formula of step AIC model with no region
no_region_form <- update(step_form, . ~ . - region)
model_no_region <- glm(no_region_form,
                       family = poisson(link = "log"),
                       data = volcano)

# Formula of step AIC model with no region, latitude, or longitude.
no_reg_lat_long_form <- update(step_form, . ~ . - region - latitude - longitude)
model_no_reg_lat_long <- glm(no_reg_lat_long_form,
                       family = poisson(link = "log"),
                       data = volcano)

# View the summary of the models
summary(model_no_lat_long)
summary(model_no_region)
summary(model_no_reg_lat_long)

# Analysis of deviance tables
# Adding region only significantly improves the model.
anova(model_no_reg_lat_long, model_no_lat_long, test = "Chisq")
# adding latitude and longitude but not region significantly improves the model.
anova(model_no_reg_lat_long, model_no_region, test = "Chisq")
# Adding latitude and longitude on top of region DOES significantly improve the model.
anova(model_no_lat_long, step_poisson, test = "Chisq")
# adding region in top of latitude and longitude DOES significantly improve the model.
anova(model_no_region, step_poisson, test = "Chisq")

# The model with region has the lower (better) AIC and BIC compared to the model with latitude and longitude.
AIC(model_no_lat_long, model_no_region, model_no_reg_lat_long)
BIC(model_no_lat_long, model_no_region, model_no_reg_lat_long)

# We select the model with region and not latitude and longitude.
final_formula <- no_lat_long_form

# Now, refit using quasi-Poisson
quasi_model <- glm(
  formula = final_formula,
  family = quasipoisson(link = "log"),
  data = volcano
)

summary(quasi_model)

# We re-facotrize major_rock_1 attribute into 3 levels
volcano_merged <- volcano %>%
  mutate(
    major_rock_1 = case_when(
      major_rock_1 == "Phono-tephrite /  Tephri-phonolite" ~ "Phono-tephrite / Tephri-phonolite",
      major_rock_1 == "Phonolite" ~ "Phonolite",
      TRUE ~ "Other"
    ),
    major_rock_1 = factor(
      major_rock_1,
      levels = c("Other", "Phono-tephrite / Tephri-phonolite", "Phonolite")  # "Other" as baseline
    )
  )

# Now, refit using quasi-Poisson
merged_quasi_model <- glm(
  formula = final_formula,
  family = quasipoisson(link = "log"),
  data = volcano_merged
)

summary(merged_quasi_model)

## Confidence Interval if everything remains the same but eruption year increases by 100
beta <- 0.0005756
se <- 8.219e-05
delta_years <- 100
z <- 1.96 
linear_change <- beta * delta_years
linear_se <- se * delta_years
lower_linear <- linear_change - z * linear_se
upper_linear <- linear_change + z * linear_se
multiplicative_change <- exp(linear_change)
lower_bound <- exp(lower_linear)
upper_bound <- exp(upper_linear)
lower_bound
upper_bound

## Everything is constant but elevation is increased by 1000 feet
beta_elevation <- 2.243e-04
se_elevation <- 5.298e-05
delta_elevation <- 1000
z <- 1.96 
linear_change_elevation <- beta_elevation * delta_elevation
linear_se_elevation <- se_elevation * delta_elevation

lower_linear_elevation <- linear_change_elevation - z * linear_se_elevation
upper_linear_elevation <- linear_change_elevation + z * linear_se_elevation
multiplicative_change_elevation <- exp(linear_change_elevation)
lower_bound_elevation <- exp(lower_linear_elevation)
upper_bound_elevation <- exp(upper_linear_elevation)
lower_bound_elevation
upper_bound_elevation

## If everything remains constant but region is changed to Alaska
beta_alaska <- 0.1982
se_alaska <- 0.6376
z <- 1.96 
lower_linear <- beta_alaska - z * se_alaska
upper_linear <- beta_alaska + z * se_alaska
multiplicative_change <- exp(beta_alaska)
lower_bound_Alaska <- exp(lower_linear)
upper_bound_Alaska <- exp(upper_linear)
lower_bound_Alaska
upper_bound_Alaska