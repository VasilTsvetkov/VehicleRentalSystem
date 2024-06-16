# Vehicle Rental System

## Approach

### Initial Steps
- **Requirement Analysis** I carefully reviewed the requirements several times to ensure I understood the problem.
- **Code Structure Planning** Then I started to plan how would the overall structure of the code look like and how to organize the classes and their relationships.

### Class Implementation
- **Abstract Vehicle Class**: Implemented an abstract `Vehicle` class serving as the base for all vehicle types.
- **Derived Classes**: Developed `Car`, `Motorcycle`, and `CargoVan` classes extending the `Vehicle` abstract class, each with unique properties and behaviors according to the requirements.

### Invoice Logic Development
- **Step-by-Step Implementation**: Gradually built the invoice generation logic, starting from basic vehicle rental cost calculation and progressively adding complexity such as insurance rates, discounts, and additional charges.

### Discovery of Additional Requirements
- **Insurance Rate Class**: Identified the need for an `InsuranceRate` class to calculate the total rate of the rent, taking into account various factors like discounts or additions as per the business rules.

### Iterative Refinement
- **Continuous Testing and Adjustment**: Continuously tested and refined the solution, adjusting the implementation to accurately reflect the business rules and requirements.
