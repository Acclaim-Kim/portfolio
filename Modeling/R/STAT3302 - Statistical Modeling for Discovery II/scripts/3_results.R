summary(quasi_model)

# Residual Plot
plot(
  fitted(quasi_model), 
  residuals(quasi_model, type = "deviance"),
  xlab = "Fitted values", 
  ylab = "Deviance residuals",
  main = "Residual Plot",
  pch = 20,
  col = "blue"
)
abline(h = 0, lty = 2, col = "red")

## Q-Q Plot
qqnorm(residuals(quasi_model, type = "deviance"),
       main = "Q-Q Plot of Deviance Residuals",
       pch = 20,
       col = "blue")
qqline(residuals(quasi_model, type = "deviance"), col = "red", lty = 2)