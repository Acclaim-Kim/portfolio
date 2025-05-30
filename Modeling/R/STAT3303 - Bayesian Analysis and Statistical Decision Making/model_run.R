# Load necessary libraries
library(tidyverse)
library(rjags)
library(coda)

# Set random seed for reproducibility
set.seed(3303)

# Prepare data for JAGS
# Reindex stocks uniquely (sector 1-5 and stock 1-10 inside)
stock$stockID <- (stock$sector - 1) * 10 + stock$stock

# Number of sectors and stocks
n_sectors <- length(unique(stock$sector))
n_stocks <- length(unique(stock$stockID))

# Bundle data for JAGS
jags_data <- list(
  flip = stock$flip,
  stockID = stock$stockID,
  sectorID = stock$sector,
  N = nrow(stock),
  n_sectors = n_sectors,
  n_stocks = n_stocks
)

# Write the model
model_string <- "
model {
  for (n in 1:N) {
    flip[n] ~ dbern(theta[stockID[n]])
  }
  
  for (i in 1:n_stocks) {
    theta[i] ~ dbeta(alpha[sectorID[i]], beta[sectorID[i]])
  }
  
  for (j in 1:n_sectors) {
    alpha[j] ~ dgamma(1, 0.1)
    beta[j] ~ dgamma(1, 0.1)
  }
}
"

# Write model to temporary file
writeLines(model_string, con = "model.bug")

# Set starting values
inits <- function() {
  list(
    alpha = rgamma(n_sectors, 1, 0.1),
    beta = rgamma(n_sectors, 1, 0.1),
    theta = rbeta(n_stocks, 1, 1)
  )
}

# Parameters to monitor
params <- c("theta", "alpha", "beta")

# MCMC settings
n.chains <- 3      # Number of chains
n.iter <- 10000    # Total iterations per chain
n.burnin <- 5000   # Burn-in (first 5000 iterations discarded)
n.thin <- 5        # Thinning (keep every 5th sample)

# Run JAGS
model <- jags.model(file = "model.bug",
                    data = jags_data,
                    inits = inits,
                    n.chains = n.chains,
                    n.adapt = 1000)

update(model, n.iter = n.burnin)  # Burn-in

# Draw samples
samples <- coda.samples(model,
                        variable.names = params,
                        n.iter = n.iter,
                        thin = n.thin)

# Combine chains
samples_combined <- do.call(rbind, samples)