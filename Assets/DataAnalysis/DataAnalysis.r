# Load data
library(rtdists)
data(speed_acc)

# See which priors you can set for this model
get_prior(rt ~ condition + (1|id), speed_acc, family = shifted_lognormal())

# Define them
prior = c(
  set_prior('normal(-1, 0.5)', class = 'Intercept'),  # around exp(-1) = 0.36 secs
  set_prior('normal(0.4, 0.3)', class = 'sigma'),  # SD of individual rts in log-units
  set_prior('normal(0, 0.3)', class = 'b'),  # around exp(-1) - exp(-1 + 0.3) = 150 ms effect in either direction
  set_prior('normal(0.3, 0.1)', class = 'sd')  # some variability between participants
)

data_plot = filter(data, id %in% c(1, 8, 15))

ggplot(data_plot, aes(x=rt)) + 
  geom_histogram(aes(fill=condition), alpha=0.5, bins=60) + 
  facet_grid(~id) +  # One panel per id
  coord_cartesian(xlim=c(0, 1.6))  # Zoom in

# Remove bad trials
library(tidyverse)
data = speed_acc %>%
  filter(rt > 0.18, rt < 3) %>%  # Between 180 and 3000 ms
  mutate_at(c('stim_cat', 'response'), as.character) %>%  # from factor to char
  filter(response != 'error', stim_cat == response) # Only correct responses

library(brms)
fit = brm(formula = bf(rt ~ congruent + trial_no + bad_lag1 + (congruent|id) + (1|counterbalancing)),
          data = data, 
          family = shifted_lognormal(),
          file = 'fit_slog')  # Save all that hard work.

pp_check(fit) 
summary(fit)  # See parameters